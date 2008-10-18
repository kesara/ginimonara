using FlickrNet;
using GiniMonara.MetaData;
using GiniMonara.Utilities;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Photos;
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

/*
 * MainForm - GiniMonara Main User Interface
 * Developer: Kesara Nanayakkara Rathnayake < kesara@bcs.org >
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
        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        private void loadMetaData()
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
                tagList.save(metaDataFileName);
            }
            panelImage.Refresh();
        }

        private void comboBoxCategoryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxTagName.Items.Clear();
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
            dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif|Video Files|*.mpg;*.mpeg";
            dialog.Title = "Open File";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                fileName = dialog.FileName;
                signature = Signature.getSignature(fileName);
                metaDataFileName = ApplicationUtility.metaDataDirectory + @"\" + signature + @".xml";
                panelImage.Visible = false;
                panelImage.Visible = true;
                zoom = "actual";
                zoomFactor = 10;
                zoomStep = 25;
                loadMetaData();
            }

            panelSelection.Visible = false;

        }

        private void ribbonButtonImageCloseFile_Click(object sender, EventArgs e)
        {
            panelImage.Visible = false;
            fileName = null;
            tagList = null;
            textBoxTitle.Clear();
            textBoxDescription.Clear();
            textBoxData.Clear();
        }

        private void ribbonButtonImageSave_Click(object sender, EventArgs e)
        {
            if (fileName != null)
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
            try
            {
                #region Google API Credidentials
                PicasaService pService = new PicasaService("GiniMonara");
                pService.setUserCredentials(textBoxUserName.Text, textBoxPassword.Text);
                #endregion

                #region Upload Photo
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);
                System.IO.FileStream fileStream = fileInfo.OpenRead();
                PicasaEntry pEntry = (PicasaEntry)pService.Insert(new Uri("http://picasaweb.google.com/data/feed/api/user/default/albumid/default"), fileStream, "image/jpeg", fileName);
                #endregion

                #region Update Photo Info
                /*GiniMeta gMeta = new GiniMeta();
                pEntry.Title.Text = gMeta.getTagValue(signature, "Title");
                pEntry.Summary.Text = gMeta.getTagValue(signature, "Description");*/
                PicasaEntry updatedEntry = (PicasaEntry)pEntry.Update();
                #endregion

                MessageBox.Show("Image uploaded succesfully", "GiniMonara", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch
            {
                MessageBox.Show("Image upload failed.", "GiniMonara", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ribbonButtonImageActual_Click(object sender, EventArgs e)
        {
            if (fileName != null)
            {
                zoom = "actual";
                showHotSpots();
                panelImage.Refresh();
            }
        }

        private void ribbonButtonImageBestFit_Click(object sender, EventArgs e)
        {
            if (fileName != null)
            {
                zoom = "bestfit";
                hideHotSpots();
                panelImage.Refresh();
            }
        }

        private void ribbonButtonImageZoomIn_Click(object sender, EventArgs e)
        {
            if (fileName != null)
            {
                zoom = "zoom";
                zoomFactor += zoomStep;
                hideHotSpots();
                panelImage.Refresh();
            }
        }

        private void ribbonButtonImageZoomOut_Click(object sender, EventArgs e)
        {
            if (fileName != null)
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
            try
            {
                Flickr flickr = new Flickr("984be4fc9f6888a86bd7360dcd3e6f30", "c6d74f5993f765e1");
                string frob = flickr.AuthGetFrob();
                string flickrUrl = flickr.AuthCalcUrl(frob, AuthLevel.Write);
                Auth auth = flickr.AuthGetToken(frob);
                System.Diagnostics.Process.Start(flickrUrl);
                flickr.AuthToken = auth.Token;
                flickr.UploadPicture(fileName, textBoxTitle.Text, textBoxDescription.Text, "testtag,testtag2");
                MessageBox.Show("Image uploaded succesfully", "GiniMonara", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch
            {
                MessageBox.Show("Image upload failed.", "GiniMonara", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panelImage_Paint(object sender, PaintEventArgs e)
        {
            if (fileName != null)
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
            if (fileName != null && zoom == "actual")
            {
                hideHotSpots();
                mouseCaptured = true;
                pointStart.X = e.X;
                pointStart.Y = e.Y;
                pointEnd.X = -1;
                pointEnd.Y = -1;
            }
        }

        private void panelImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (fileName != null && zoom == "actual")
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
            if (fileName != null && zoom == "actual")
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
            if (fileName != null && zoom == "actual")
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
            loadMetaData();
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
            if (hotSpots.Count > 0)
            {
                foreach (Panel panel in hotSpots)
                {
                    panel.Visible = true;
                }
            }
        }

        private void hideHotSpots()
        {
            if (hotSpots.Count > 0)
            {
                foreach (Panel panel in hotSpots)
                {
                    panel.Visible = false;
                }
            }
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            panelImage.Refresh();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            loadCategories(comboBoxCategoryName);
            loadCategories(comboBoxSelectionCategory);
        }

        private void loadCategories(ComboBox comboBox)
        {
            comboBox.Items.Clear();
            var categories = ApplicationUtility.categories.Select(c => c.category).Distinct();
            foreach (string category in categories)
            {
                comboBox.Items.Add(category);
            }
            comboBox.SelectedIndex = 0;
        }

        private void comboBoxTagName_SelectedIndexChanged(object sender, EventArgs e)
        {
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
    }
}
