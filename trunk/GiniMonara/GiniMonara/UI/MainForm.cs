using FlickrNet;
using GiniMonara.MetaData;
using GiniMonara.UI;
using GiniMonara.Utilities;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Extensions.MediaRss;
using Google.GData.Photos;
using Google.GData.YouTube;
using QuartzTypeLib;
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

/*
 * MainForm - GiniMonara Main User Interface
 * Developer: Kesara Nanayakkara Rathnayake < kesara@bcs.org >
 * Parts of video module is based on code by Prathibha Gamage < bgkprathibha@gmail.com >
 * Copyright (C) 2008 GiniMonara Team
 * 
 * This file is part of GiniMonara.
 * 
 * GiniMonara is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License.
 * 
 * GiniMonara is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with GiniMonara.  If not, see <http://www.gnu.org/licenses/>.
 * 
 */

namespace GiniMonara.UI
{
    public partial class MainForm : Form
    {
        #region Variable
        private string signature;
        private string fileName;
        private string metaDataFileName;
        private TagList tagList;
        private Boolean mouseCaptured;
        private Point pointStart;
        private Point pointEnd;
        private Point pointSelectionStart;
        private Point pointSelectionEnd;
        private ArrayList hotSpots;
        private string zoom;
        private int zoomFactor;
        private int zoomStep;
        private enum MediaStatus { image, video, none }
        private MediaStatus mediaStatus = MediaStatus.none;

        /* Video Module Variables */
        private FilgraphManager filgraphManager;
        private IBasicAudio iBasicAudio;
        private IVideoWindow iVideoWindow;
        private IMediaEvent iMediaEvent;
        private IMediaEventEx iMediaEventEx;
        private IMediaPosition iMediaPosition;
        private IMediaControl iMediaControl;

        private enum VideoStatus { None, Stopped, Paused, Running };
        private VideoStatus videoStatus = VideoStatus.None;
        private int markedFrame;

        private const int WM_APP = 0x8000;
        private const int WM_GRAPHNOTIFY = WM_APP + 1;
        private const int EC_COMPLETE = 0x01;
        private const int WS_CHILD = 0x40000000;
        private const int WS_CLIPCHILDREN = 0x2000000;
        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        private void loadImageMetaData()
        {
            tagList = new TagList();
            if (MetaDataUtility.checkMetaDataExsists(metaDataFileName))
            {
                tagList.load(metaDataFileName);
                textBoxTitle.Text = tagList.Where(t => t.category == "DublinCore").Where(t => t.name == "Title").Select(t => t.data).FirstOrDefault();
                textBoxDescription.Text = tagList.Where(t => t.category == "DublinCore").Where(t => t.name == "Description").Select(t => t.data).FirstOrDefault();
                if (comboBoxTagName.Items.Count > 0)
                {
                    textBoxData.Text = tagList.Where(t => t.category == comboBoxCategoryName.SelectedItem.ToString()).Where(t => t.name == comboBoxTagName.SelectedItem.ToString()).Select(t => t.data).FirstOrDefault();
                }
                var selectionMetaData = from t in tagList
                                        from c in ApplicationUtility.categories
                                        where c.type == "area"
                                        where c.category == t.category
                                        where c.tag == t.name
                                        select t;
                hotSpots = new ArrayList();
                foreach (gTag tag in selectionMetaData)
                {
                    Panel panelHotSpot = new Panel();
                    panelHotSpot.Location = new Point(tag.x, tag.y);
                    panelHotSpot.Width = tag.p - tag.x;
                    panelHotSpot.Height = tag.q - tag.y;
                    panelHotSpot.BackColor = Color.Transparent;
                    panelHotSpot.ForeColor = Color.Black;
                    panelHotSpot.MouseHover += new System.EventHandler(panelSelectionDataMouseHover);
                    panelHotSpot.MouseLeave += new System.EventHandler(panelSelectionDataMouseLeave);
                    panelImage.Controls.Add(panelHotSpot);
                    ToolTip toolTip = new ToolTip();
                    toolTip.AutomaticDelay = 0;
                    toolTip.InitialDelay = 0;
                    toolTip.IsBalloon = true;
                    toolTip.ReshowDelay = 0;
                    toolTip.ShowAlways = true;
                    toolTip.SetToolTip(panelHotSpot, tag.data);
                    hotSpots.Add(panelHotSpot);
                }
            }
            else
            {
                gTag fileNameTag = new gTag("fileName", "hidden", fileName);
                tagList.Add(fileNameTag);
                gTag mediaTypeTag = new gTag("mediaType", "hidden", "image");
                tagList.Add(mediaTypeTag);
                tagList.save(metaDataFileName);
            }
            panelImage.Refresh();
        }

        private void comboBoxCategoryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxTagName.Items.Clear();
            textBoxData.Text = "";
            var tags = ApplicationUtility.categories.Where(c => c.category == comboBoxCategoryName.SelectedItem.ToString()).Where(ty => ty.type == "default").Select(t => t.tag).Distinct();
            foreach (string tag in tags)
            {
                if (tag != "E0T")
                {
                    if (!(comboBoxCategoryName.SelectedItem.ToString() == "DublinCore" && (tag == "Title" || tag == "Description")))
                    {
                        comboBoxTagName.Items.Add(tag);
                    }
                }
            }
            if (comboBoxTagName.Items.Count > 0)
            {
                comboBoxTagName.SelectedIndex = 0;
            }
        }

        private void ribbonButtonImageOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif|All Files|*.*";
            dialog.Title = "Open File";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                closeImage();
                closeVideo();
                fileName = dialog.FileName;
                signature = Signature.getSignature(fileName);
                metaDataFileName = ApplicationUtility.metaDataDirectory + @"\" + signature + @".xml";
                panelImage.Visible = false;
                panelImage.Visible = true;
                zoom = "actual";
                zoomFactor = 10;
                zoomStep = 25;
                mediaStatus = MediaStatus.image;
                loadImageMetaData();
            }

            panelSelection.Visible = false;

        }

        private void ribbonButtonImageCloseFile_Click(object sender, EventArgs e)
        {
            if (fileName != null && mediaStatus == MediaStatus.image)
            {
                closeImage();
            }
        }

        private void ribbonButtonImageSave_Click(object sender, EventArgs e)
        {
            if (fileName != null && mediaStatus == MediaStatus.image)
            {
                gTag newTag;
                gTag oldTag;

                newTag = new gTag("Title", "DublinCore", textBoxTitle.Text);
                oldTag = tagList.Where(t => t.category == "DublinCore").Where(t => t.name == "Title").SingleOrDefault();
                tagList.Remove(oldTag);
                tagList.Add(newTag);

                newTag = new gTag("Description", "DublinCore", textBoxDescription.Text);
                oldTag = tagList.Where(t => t.category == "DublinCore").Where(t => t.name == "Description").SingleOrDefault();
                tagList.Remove(oldTag);
                tagList.Add(newTag);

                newTag = new gTag(comboBoxTagName.SelectedItem.ToString(), comboBoxCategoryName.SelectedItem.ToString(), textBoxData.Text);
                oldTag = tagList.Where(t => t.category == comboBoxCategoryName.SelectedItem.ToString()).Where(t => t.name == comboBoxTagName.SelectedItem.ToString()).SingleOrDefault();
                tagList.Remove(oldTag);
                tagList.Add(newTag);

                tagList.save(metaDataFileName);
            }
        }

        private void ribbonButtonImagePicasa_Click(object sender, EventArgs e)
        {
            if (fileName != null && mediaStatus == MediaStatus.image)
            {
                if (ApplicationUtility.googleSecrets == null)
                {
                    GoogleAccountDetailsForm googleAccountDetailsForm = new GoogleAccountDetailsForm(this);
                    googleAccountDetailsForm.Show();
                    this.Enabled = false;
                }
                else
                {
                    sendToPicasa();
                }
            }
        }

        private void ribbonButtonImageActual_Click(object sender, EventArgs e)
        {
            if (fileName != null && mediaStatus == MediaStatus.image)
            {
                zoom = "actual";
                showHotSpots();
                panelImage.Refresh();
            }
        }

        private void ribbonButtonImageBestFit_Click(object sender, EventArgs e)
        {
            if (fileName != null && mediaStatus == MediaStatus.image)
            {
                zoom = "bestfit";
                hideHotSpots();
                panelImage.Refresh();
            }
        }

        private void ribbonButtonImageZoomIn_Click(object sender, EventArgs e)
        {
            if (fileName != null && mediaStatus == MediaStatus.image)
            {
                zoom = "zoom";
                zoomFactor += zoomStep;
                hideHotSpots();
                panelImage.Refresh();
            }
        }

        private void ribbonButtonImageZoomOut_Click(object sender, EventArgs e)
        {
            if (fileName != null && mediaStatus == MediaStatus.image)
            {
                zoom = "zoom";
                zoomFactor -= zoomStep;
                if (zoomFactor < 10)
                {
                    zoomFactor = 10;
                }
                hideHotSpots();
                panelImage.Refresh();
            }
        }

        private void ribbonButtonImageFlickr_Click(object sender, EventArgs e)
        {
            if (fileName != null && mediaStatus == MediaStatus.image)
            {
                try
                {
                    Flickr flickr = new Flickr("API KEY", "SHARED SECRET");
                    string frob;
                    if (ApplicationUtility.flickrSecrets == null)
                    {
                        frob = flickr.AuthGetFrob();
                        ApplicationUtility.setFlickrSecrets(frob);
                    }
                    else
                    {
                        frob = ApplicationUtility.flickrSecrets.frob;
                    }
                    Auth auth = flickr.AuthGetToken(frob);

                    flickr.AuthToken = auth.Token;
                    flickr.UploadPicture(fileName, tagList.Where(t => t.category == "DublinCore").Where(t => t.name == "Title").Select(t => t.data).FirstOrDefault(), tagList.Where(t => t.category == "DublinCore").Where(t => t.name == "Title").Select(t => t.data).FirstOrDefault(), getCommaSeperatedTags());
                    MessageBox.Show("Image uploaded succesfully", "GiniMonara", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (FlickrApiException fe)
                {
                    if (fe.Code == 108)
                    {
                        MessageBox.Show("Please give authoriztion to GiniMonara to use your Flickr account and resubmit the image.", "GiniMonara", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        Flickr flickr = new Flickr("984be4fc9f6888a86bd7360dcd3e6f30", "c6d74f5993f765e1");
                        string frob;
                        if (ApplicationUtility.flickrSecrets == null)
                        {
                            frob = flickr.AuthGetFrob();
                            ApplicationUtility.setFlickrSecrets(frob);
                        }
                        else
                        {
                            frob = ApplicationUtility.flickrSecrets.frob;
                        }
                        string flickrUrl = flickr.AuthCalcUrl(frob, AuthLevel.Write);
                        System.Diagnostics.Process.Start(flickrUrl);
                    }
                    else
                    {
                        MessageBox.Show("Image upload failed.", "GiniMonara", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void panelImage_Paint(object sender, PaintEventArgs e)
        {
            if (fileName != null && mediaStatus == MediaStatus.image)
            {
                if (zoom == "actual")
                {
                    Bitmap bitmap = new Bitmap(fileName);
                    e.Graphics.DrawImage(bitmap, new Point(0, 0));
                }
                else if (zoom == "bestfit")
                {
                    Bitmap bitmap = new Bitmap(fileName);
                    Bitmap zBitmap = new Bitmap(panelImage.Width, panelImage.Height);
                    Graphics g = Graphics.FromImage((Image)zBitmap);
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(bitmap, 0, 0, panelImage.Width, panelImage.Height);
                    g.Dispose();
                    e.Graphics.DrawImage(zBitmap, new Point(0, 0));
                }
                else if (zoom == "zoom")
                {
                    Bitmap bitmap = new Bitmap(fileName);
                    Bitmap zBitmap = new Bitmap(bitmap.Width + zoomFactor, bitmap.Height + zoomFactor);
                    Graphics g = Graphics.FromImage((Image)zBitmap);
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(bitmap, 0, 0, bitmap.Width + zoomFactor, bitmap.Height + zoomFactor);
                    g.Dispose();
                    e.Graphics.DrawImage(zBitmap, new Point(0, 0));
                }
            }
        }

        private void panelImage_MouseDown(object sender, MouseEventArgs e)
        {
            if (fileName != null && mediaStatus == MediaStatus.image && zoom == "actual")
            {
                disposeHotSpots();
                mouseCaptured = true;
                pointStart.X = e.X;
                pointStart.Y = e.Y;
                pointEnd.X = -1;
                pointEnd.Y = -1;
            }
        }

        private void panelImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (fileName != null && mediaStatus == MediaStatus.image && zoom == "actual")
            {
                Point currentPoint = new Point(e.X, e.Y);

                if (mouseCaptured)
                {
                    if (pointEnd.X != -1)
                    {
                        drawSelectionRectangle(pointStart, pointEnd);
                    }

                    pointEnd = currentPoint;
                    drawSelectionRectangle(pointStart, pointEnd);
                }
            }
        }

        private void panelImage_MouseUp(object sender, MouseEventArgs e)
        {
            if (fileName != null && mediaStatus == MediaStatus.image && zoom == "actual")
            {
                mouseCaptured = false;

                if (pointEnd.X != -1)
                {
                    Point currentPoint = new Point(e.X, e.Y);
                    drawSelectionRectangle(pointStart, pointEnd);

                    pointSelectionStart = pointStart;
                    pointSelectionEnd = pointEnd;
                    panelSelection.Location = pointStart;
                    panelSelection.Visible = true;
                }

                pointEnd.X = -1;
                pointEnd.Y = -1;
                pointStart.X = -1;
                pointStart.Y = -1;
            }
        }

        private void drawSelectionRectangle(Point a, Point b)
        {
            if (fileName != null && mediaStatus == MediaStatus.image && zoom == "actual")
            {
                Rectangle selectionReactangle = new Rectangle();
                a.X = splitContainer1.SplitterDistance + a.X;
                a.Y = splitContainer2.SplitterDistance + a.Y;
                b.X = splitContainer1.SplitterDistance + b.X;
                b.Y = splitContainer2.SplitterDistance + b.Y;
                a = PointToScreen(a);
                b = PointToScreen(b);

                if (a.X < b.X)
                {
                    selectionReactangle.X = a.X;
                    selectionReactangle.Width = b.X - a.X;
                }
                else
                {
                    selectionReactangle.X = b.X;
                    selectionReactangle.Width = a.X - b.X;
                }

                if (a.Y < b.Y)
                {
                    selectionReactangle.Y = a.Y;
                    selectionReactangle.Height = b.Y - a.Y;
                }
                else
                {
                    selectionReactangle.Y = b.Y;
                    selectionReactangle.Height = a.Y - b.Y;
                }

                ControlPaint.DrawReversibleFrame(selectionReactangle, Color.Aqua, FrameStyle.Dashed);
            }
        }

        private void ribbonButtonSelectionTagOk_Click(object sender, EventArgs e)
        {
            gTag tag = new gTag(comboBoxSelectionTag.SelectedItem.ToString(), comboBoxSelectionCategory.SelectedItem.ToString(), textBoxSelectionData.Text, pointSelectionStart.X, pointSelectionStart.Y, pointSelectionEnd.X, pointSelectionEnd.Y);
            tagList.Add(tag);
            tagList.save(metaDataFileName);
            panelSelection.Visible = false;
            loadImageMetaData();
            showHotSpots();
        }

        private void ribbonButtonSelectionCancel_Click(object sender, EventArgs e)
        {
            panelSelection.Visible = false;
            showHotSpots();
        }

        private void panelSelectionDataMouseHover(object sender, EventArgs e)
        {
            Panel panel = (Panel)sender;
            panel.BorderStyle = BorderStyle.FixedSingle;
        }

        private void panelSelectionDataMouseLeave(object sender, EventArgs e)
        {
            Panel panel = (Panel)sender;
            panel.BorderStyle = BorderStyle.None;
        }

        private void showHotSpots()
        {
            if (hotSpots != null)
            {
                foreach (Panel panel in hotSpots)
                {
                    panel.Visible = true;
                }
            }
        }

        private void hideHotSpots()
        {
            if (hotSpots != null)
            {
                foreach (Panel panel in hotSpots)
                {
                    panel.Visible = false;
                }
            }
        }

        private void disposeHotSpots()
        {
            if (hotSpots != null)
            {
                foreach (Panel panel in hotSpots)
                {
                    panel.Dispose();
                }
            }
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            panelImage.Refresh();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            reloadCategories();
        }

        public void reloadCategories()
        {
            loadCategories(comboBoxCategoryName, "default");
            loadCategories(comboBoxSelectionCategory, "area");
            loadCategories(comboBoxTimedCategory, "timeframe");
        }

        private void loadCategories(ComboBox comboBox, string categoryType)
        {
            comboBox.Items.Clear();
            var categories = ApplicationUtility.categories.Where(c => c.type == categoryType).Select(c => c.category).Distinct();
            foreach (string category in categories)
            {
                comboBox.Items.Add(category);
            }
            comboBox.SelectedIndex = 0;
        }

        private void comboBoxTagName_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxData.Text = "";
            if (tagList != null)
            {
                textBoxData.Text = tagList.Where(t => t.category == comboBoxCategoryName.SelectedItem.ToString()).Where(t => t.name == comboBoxTagName.SelectedItem.ToString()).Select(t => t.data).FirstOrDefault();
            }
        }

        private void comboBoxSelectionCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxSelectionTag.Items.Clear();
            var tags = ApplicationUtility.categories.Where(c => c.category == comboBoxSelectionCategory.SelectedItem.ToString()).Where(ty => ty.type == "area").Select(t => t.tag).Distinct();
            foreach (string tag in tags)
            {
                if (tag != "E0T")
                {
                    comboBoxSelectionTag.Items.Add(tag);
                }
            }
            if (comboBoxSelectionTag.Items.Count > 0)
            {
                comboBoxSelectionTag.SelectedIndex = 0;
            }
        }

        private void ribbonButtonCategoryEditor_Click(object sender, EventArgs e)
        {
            CategoriesForm categoriesForm = new CategoriesForm(this);
            categoriesForm.Show();
            this.Enabled = false;
        }

        public void sendToPicasa()
        {
            try
            {
                #region Google API Credidentials
                PicasaService pService = new PicasaService("GiniMonara");
                pService.setUserCredentials(ApplicationUtility.googleSecrets.username, ApplicationUtility.googleSecrets.password);
                #endregion

                #region Upload Photo
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);
                System.IO.FileStream fileStream = fileInfo.OpenRead();
                PicasaEntry pEntry = (PicasaEntry)pService.Insert(new Uri("http://picasaweb.google.com/data/feed/api/user/default/albumid/default"), fileStream, "image/jpeg", fileName);
                #endregion

                #region Update Photo Info
                pEntry.Title.Text = tagList.Where(t => t.category == "DublinCore").Where(t => t.name == "Title").Select(t => t.data).FirstOrDefault();
                pEntry.Summary.Text = tagList.Where(t => t.category == "DublinCore").Where(t => t.name == "Description").Select(t => t.data).FirstOrDefault();

                PicasaEntry updatedEntry = (PicasaEntry)pEntry.Update();
                #endregion

                MessageBox.Show("Image uploaded succesfully", "GiniMonara", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
            catch
            {
                MessageBox.Show("Image upload failed.", "GiniMonara", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ApplicationUtility.googleSecrets = null;
            }
        }

        private void ribbonButtonReloadMetaData_Click(object sender, EventArgs e)
        {
            if (fileName != null && mediaStatus == MediaStatus.image)
            {
                disposeHotSpots();
                loadImageMetaData();
            }
        }

        private string getCommaSeperatedTags()
        {
            string tags = "";
            if (tagList != null)
            {
                foreach (gTag tag in tagList)
                {
                    if (tag.category != "hidden")
                    {
                        if (tags == "")
                        {
                            tags = tag.data;
                        }
                        else
                        {
                            tags = tags + "," + tag.data;
                        }
                    }
                }
            }
            return tags;
        }

        private void closeImage()
        {
            panelImage.Visible = false;
            fileName = null;
            tagList = null;
            textBoxTitle.Clear();
            textBoxDescription.Clear();
            textBoxData.Clear();
            panelTimedTags.Visible = false;
            groupBoxTimedTags.Visible = false;
            mediaStatus = MediaStatus.none;
        }

        /* Video Module */

        private void ribbonButtonVideoOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Video Files|*.mpg;*.avi;*.wmv;*.mov;|All Files|*.*";
            dialog.Title = "Open File";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                closeImage();
                closeVideo();
                fileName = dialog.FileName;
                signature = Signature.getSignature(fileName);
                metaDataFileName = ApplicationUtility.metaDataDirectory + @"\" + signature + @".xml";

                filgraphManager = new FilgraphManager();
                filgraphManager.RenderFile(fileName);
                iBasicAudio = filgraphManager as IBasicAudio;

                mediaStatus = MediaStatus.video;
                markedFrame = -1;

                try
                {
                    iVideoWindow = filgraphManager as IVideoWindow;
                    iVideoWindow.Owner = (int)panelImage.Handle;
                    iVideoWindow.WindowStyle = WS_CHILD | WS_CLIPCHILDREN;
                    iVideoWindow.SetWindowPosition(panelImage.ClientRectangle.Left,
                        panelImage.ClientRectangle.Top,
                        panelImage.ClientRectangle.Width,
                        panelImage.ClientRectangle.Height);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                iMediaEvent = filgraphManager as IMediaEvent;
                iMediaEventEx = filgraphManager as IMediaEventEx;
                iMediaEventEx.SetNotifyWindow((int)this.Handle, WM_GRAPHNOTIFY, 0);
                iMediaPosition = filgraphManager as IMediaPosition;
                iMediaControl = filgraphManager as IMediaControl;

                iMediaControl.Run();
                videoStatus = VideoStatus.Running;
                panelImage.Visible = true;
                loadVideoMetaData();
            }

            panelSelection.Visible = false;
        }

        private void ribbonButtonVideoRewind_Click(object sender, EventArgs e)
        {
            if (fileName != null && mediaStatus == MediaStatus.video)
            {
                iMediaPosition.CurrentPosition = iMediaPosition.CurrentPosition - 10;
            }
        }

        private void ribbonButtonVideoForward_Click(object sender, EventArgs e)
        {
            if (fileName != null && mediaStatus == MediaStatus.video)
            {
                iMediaPosition.CurrentPosition = iMediaPosition.CurrentPosition + 10;
            }
        }

        private void ribbonButtonVideoPlay_Click(object sender, EventArgs e)
        {
            if (fileName != null && mediaStatus == MediaStatus.video)
            {
                if (iMediaPosition.CurrentPosition == iMediaPosition.StopTime)
                {
                    iMediaPosition.CurrentPosition = 0.0;
                }
                iMediaControl.Run();
                videoStatus = VideoStatus.Running;
            }
        }

        private void ribbonButtonVideoPause_Click(object sender, EventArgs e)
        {
            if (fileName != null && mediaStatus == MediaStatus.video)
            {
                iMediaControl.Pause();
                videoStatus = VideoStatus.Paused;
            }
        }

        private void ribbonButtonVideoStop_Click(object sender, EventArgs e)
        {
            if (fileName != null && mediaStatus == MediaStatus.video)
            {
                iMediaControl.Stop();
                videoStatus = VideoStatus.Stopped;
            }
        }

        private void closeVideo()
        {
            panelImage.Visible = false;
            fileName = null;
            tagList = null;
            textBoxTitle.Clear();
            textBoxDescription.Clear();
            textBoxData.Clear();
            panelTimedTags.Visible = false;
            groupBoxTimedTags.Visible = false;
            listBoxTimedTags.Items.Clear();

            if (iMediaControl != null)
            {
                iMediaControl.Stop();
                videoStatus = VideoStatus.Stopped;
            }

            if (iMediaEventEx != null)
            {
                iMediaEventEx.SetNotifyWindow(0, 0, 0);
            }

            if (iVideoWindow != null)
            {
                iVideoWindow.Visible = 0;
                iVideoWindow.Owner = 0;
            }

            filgraphManager = null;
            iBasicAudio = null;
            iVideoWindow = null;
            iMediaEvent = null;
            iMediaEventEx = null;
            iMediaPosition = null;
            iMediaControl = null;
            videoStatus = VideoStatus.None;
            markedFrame = -1;
            mediaStatus = MediaStatus.none;
        }

        private void ribbonButtonVideoYouTube_Click(object sender, EventArgs e)
        {
            if (fileName != null && mediaStatus == MediaStatus.video)
            {
                if (ApplicationUtility.youTubeSecrets == null)
                {
                    YouTubeAccountDetailsForm youTubeAccountDetailsForm = new YouTubeAccountDetailsForm(this);
                    youTubeAccountDetailsForm.Show();
                    this.Enabled = false;
                }
                else
                {
                    sendToYouTube();
                }
            }
        }


        public void sendToYouTube()
        {
            try
            {
                #region Google API Credidentials
                YouTubeService ytService = new YouTubeService("GiniMonara", "CLIENT", "DEVELOPER KEY");
                ytService.setUserCredentials(ApplicationUtility.youTubeSecrets.username, ApplicationUtility.youTubeSecrets.password);
                #endregion

                YouTubeEntry ytEntry = new YouTubeEntry();

                #region Update Photo Info
                ytEntry.Media = new MediaGroup();
                ytEntry.Media.Categories.Add(new MediaCategory("video", YouTubeNameTable.CategorySchema));
                ytEntry.Media.Keywords = new MediaKeywords(getCommaSeperatedTags());
                ytEntry.Media.Description = new MediaDescription(tagList.Where(t => t.category == "DublinCore").Where(t => t.name == "Description").Select(t => t.data).FirstOrDefault());
                ytEntry.Media.Title = new MediaTitle(tagList.Where(t => t.category == "DublinCore").Where(t => t.name == "Title").Select(t => t.data).FirstOrDefault());
                #endregion

                #region Upload Video
                ytEntry.MediaSource = new MediaFileSource(fileName, "video/mpeg");
                YouTubeEntry uploadedEntry = ytService.Upload(ytEntry);
                #endregion

                MessageBox.Show("Video uploaded succesfully", "GiniMonara", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
            catch
            {
                MessageBox.Show("Video upload failed.", "GiniMonara", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ApplicationUtility.youTubeSecrets = null;
            }
        }

        private void loadVideoMetaData()
        {
            tagList = new TagList();
            if (MetaDataUtility.checkMetaDataExsists(metaDataFileName))
            {
                tagList.load(metaDataFileName);
                textBoxTitle.Text = tagList.Where(t => t.category == "DublinCore").Where(t => t.name == "Title").Select(t => t.data).FirstOrDefault();
                textBoxDescription.Text = tagList.Where(t => t.category == "DublinCore").Where(t => t.name == "Description").Select(t => t.data).FirstOrDefault();
                if (comboBoxTagName.Items.Count > 0)
                {
                    textBoxData.Text = tagList.Where(t => t.category == comboBoxCategoryName.SelectedItem.ToString()).Where(t => t.name == comboBoxTagName.SelectedItem.ToString()).Select(t => t.data).FirstOrDefault();
                }

                listBoxTimedTags.Items.Clear();
                var timedMetaData = from t in tagList
                                    from c in ApplicationUtility.categories
                                    where c.type == "timeframe"
                                    where c.category == t.category
                                    where c.tag == t.name
                                    select t;

                foreach (gTag tag in timedMetaData)
                {
                    listBoxTimedTags.Items.Add(tag);
                }
                listBoxTimedTags.ValueMember = "data";

                /*
                var selectionMetaData = from t in tagList
                                        from c in ApplicationUtility.categories
                                        where c.type == "area"
                                        where c.category == t.category
                                        where c.tag == t.name
                                        select t;
                hotSpots = new ArrayList();
                foreach (gTag tag in selectionMetaData)
                {
                    Panel panelHotSpot = new Panel();
                    panelHotSpot.Location = new Point(tag.x, tag.y);
                    panelHotSpot.Width = tag.p - tag.x;
                    panelHotSpot.Height = tag.q - tag.y;
                    panelHotSpot.BackColor = Color.Transparent;
                    panelHotSpot.ForeColor = Color.Black;
                    panelHotSpot.MouseHover += new System.EventHandler(panelSelectionDataMouseHover);
                    panelHotSpot.MouseLeave += new System.EventHandler(panelSelectionDataMouseLeave);
                    panelImage.Controls.Add(panelHotSpot);
                    ToolTip toolTip = new ToolTip();
                    toolTip.AutomaticDelay = 0;
                    toolTip.InitialDelay = 0;
                    toolTip.IsBalloon = true;
                    toolTip.ReshowDelay = 0;
                    toolTip.ShowAlways = true;
                    toolTip.SetToolTip(panelHotSpot, tag.data);
                    hotSpots.Add(panelHotSpot);
                }
                 */
            }
            else
            {
                gTag fileNameTag = new gTag("fileName", "hidden", fileName);
                tagList.Add(fileNameTag);
                gTag mediaTypeTag = new gTag("mediaType", "hidden", "video");
                tagList.Add(mediaTypeTag);
                tagList.save(metaDataFileName);
            }

            groupBoxTimedTags.Visible = true;
        }

        private void ribbonButtonVideoSaveMetaData_Click(object sender, EventArgs e)
        {
            if (fileName != null && mediaStatus == MediaStatus.video)
            {
                gTag newTag;
                gTag oldTag;

                newTag = new gTag("Title", "DublinCore", textBoxTitle.Text);
                oldTag = tagList.Where(t => t.category == "DublinCore").Where(t => t.name == "Title").SingleOrDefault();
                tagList.Remove(oldTag);
                tagList.Add(newTag);

                newTag = new gTag("Description", "DublinCore", textBoxDescription.Text);
                oldTag = tagList.Where(t => t.category == "DublinCore").Where(t => t.name == "Description").SingleOrDefault();
                tagList.Remove(oldTag);
                tagList.Add(newTag);

                newTag = new gTag(comboBoxTagName.SelectedItem.ToString(), comboBoxCategoryName.SelectedItem.ToString(), textBoxData.Text);
                oldTag = tagList.Where(t => t.category == comboBoxCategoryName.SelectedItem.ToString()).Where(t => t.name == comboBoxTagName.SelectedItem.ToString()).SingleOrDefault();
                tagList.Remove(oldTag);
                tagList.Add(newTag);

                tagList.save(metaDataFileName);
            }
        }

        private void ribbonButtonVideoClose_Click(object sender, EventArgs e)
        {
            if (fileName != null && mediaStatus == MediaStatus.video)
            {
                closeVideo();
            }
        }

        private void ribbonButtonVideoReloadMetaData_Click(object sender, EventArgs e)
        {
            if (fileName != null && mediaStatus == MediaStatus.video)
            {
                //disposeHotSpots();
                loadVideoMetaData();
            }
        }

        private void ribbonButtonVideoMark_Click(object sender, EventArgs e)
        {
            if (fileName != null && mediaStatus == MediaStatus.video)
            {
                markedFrame = (int)iMediaPosition.CurrentPosition;
            }
        }

        private void ribbonButtonVideoTag_Click(object sender, EventArgs e)
        {
            if (fileName != null && mediaStatus == MediaStatus.video)
            {
                iMediaControl.Pause();
                videoStatus = VideoStatus.Paused;
                panelTimedTags.Visible = true;
            }
        }

        private void ribbonButtonTimedOk_Click(object sender, EventArgs e)
        {
            if (fileName != null && mediaStatus == MediaStatus.video)
            {
                if (markedFrame != -1)
                {
                    gTag tag = new gTag(comboBoxTimedTag.SelectedItem.ToString(), comboBoxTimedCategory.SelectedItem.ToString(), textBoxTimedData.Text, markedFrame, (int)iMediaPosition.CurrentPosition);
                    tagList.Add(tag);
                    tagList.save(metaDataFileName);
                    listBoxTimedTags.Items.Add(tag);
                }
                else
                {
                    gTag tag = new gTag(comboBoxTimedTag.SelectedItem.ToString(), comboBoxTimedCategory.SelectedItem.ToString(), textBoxTimedData.Text, (int)iMediaPosition.CurrentPosition, (int)iMediaPosition.CurrentPosition + 3);
                    tagList.Add(tag);
                    tagList.save(metaDataFileName);
                    listBoxTimedTags.Items.Add(tag);
                }
                markedFrame = -1;
                panelTimedTags.Visible = false;
                iMediaControl.Run();
                videoStatus = VideoStatus.Running;
                textBoxTimedData.Clear();
            }
        }

        private void ribbonButtonTimedCancel_Click(object sender, EventArgs e)
        {
            if (fileName != null && mediaStatus == MediaStatus.video)
            {
                markedFrame = -1;
                panelTimedTags.Visible = false;
                iMediaControl.Run();
                videoStatus = VideoStatus.Running;
                textBoxTimedData.Clear();
            }
        }

        private void comboBoxTimedCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxTimedTag.Items.Clear();
            var tags = ApplicationUtility.categories.Where(c => c.category == comboBoxTimedCategory.SelectedItem.ToString()).Where(ty => ty.type == "timeframe").Select(t => t.tag).Distinct();
            foreach (string tag in tags)
            {
                if (tag != "E0T")
                {
                    comboBoxTimedTag.Items.Add(tag);
                }
            }
            if (comboBoxTimedTag.Items.Count > 0)
            {
                comboBoxTimedTag.SelectedIndex = 0;
            }
        }

        private void listBoxTimedTags_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fileName != null && mediaStatus == MediaStatus.video)
            {
                gTag tag = (gTag)listBoxTimedTags.SelectedItem;
                iMediaPosition.CurrentPosition = tag.st;
                iMediaControl.Run();
                videoStatus = VideoStatus.Running;
            }
        }

        private void ribbonButtonCategoryAnalyzer_Click(object sender, EventArgs e)
        {
            CategoryAnalyzer categoryAnalyzer = new CategoryAnalyzer(this);
            categoryAnalyzer.Show();
            this.Enabled = false;
        }

        private void ribbonButtonSearch_Click(object sender, EventArgs e)
        {
            SearchForm searchForm = new SearchForm(this);
            searchForm.Show();
            this.Enabled = false;
        }

        public void openImage(string imageFileName)
        {
            closeImage();
            closeVideo();
            fileName = imageFileName;
            signature = Signature.getSignature(fileName);
            metaDataFileName = ApplicationUtility.metaDataDirectory + @"\" + signature + @".xml";
            panelImage.Visible = false;
            panelImage.Visible = true;
            zoom = "actual";
            zoomFactor = 10;
            zoomStep = 25;
            mediaStatus = MediaStatus.image;
            loadImageMetaData();
            panelSelection.Visible = false;
        }

        public void openVideo(string videoFileName)
        {
            closeImage();
            closeVideo();
            fileName = videoFileName;
            signature = Signature.getSignature(fileName);
            metaDataFileName = ApplicationUtility.metaDataDirectory + @"\" + signature + @".xml";

            filgraphManager = new FilgraphManager();
            filgraphManager.RenderFile(fileName);
            iBasicAudio = filgraphManager as IBasicAudio;

            mediaStatus = MediaStatus.video;
            markedFrame = -1;

            try
            {
                iVideoWindow = filgraphManager as IVideoWindow;
                iVideoWindow.Owner = (int)panelImage.Handle;
                iVideoWindow.WindowStyle = WS_CHILD | WS_CLIPCHILDREN;
                iVideoWindow.SetWindowPosition(panelImage.ClientRectangle.Left,
                    panelImage.ClientRectangle.Top,
                    panelImage.ClientRectangle.Width,
                    panelImage.ClientRectangle.Height);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            iMediaEvent = filgraphManager as IMediaEvent;
            iMediaEventEx = filgraphManager as IMediaEventEx;
            iMediaEventEx.SetNotifyWindow((int)this.Handle, WM_GRAPHNOTIFY, 0);
            iMediaPosition = filgraphManager as IMediaPosition;
            iMediaControl = filgraphManager as IMediaControl;

            iMediaControl.Run();
            videoStatus = VideoStatus.Running;
            panelImage.Visible = true;

            loadVideoMetaData();
            panelSelection.Visible = false;
        }

        private void ribbonButtonSaveTags_Click(object sender, EventArgs e)
        {
            if (fileName != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "XML Files|*.xml";
                saveFileDialog.Title = "Save As";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.Copy(metaDataFileName, saveFileDialog.FileName);
                }
            }
        }

        private void ribbonButtonAbout_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm(this);
            aboutForm.Show();
            this.Enabled = false;
        }

        private void ribbonButtonHelp_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://ginimonara.sourceforge.net/help/");
        }
    }
}
