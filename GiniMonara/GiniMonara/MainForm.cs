using FlickrNet;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Photos;
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

/*
 * MainForm - Gini Monara Main User Interface
 * Developer: Kesara Nanayakkara Rathnayake < kesara@bcs.org >
 * Copyright (C) 2008 Gini Monara Team
 * 
 * This file is part of Gini Monara.
 * 
 * Gini Monara is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License.
 * 
 * Calculator.NET is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Calculator.NET.  If not, see <http://www.gnu.org/licenses/>.
 * 
 */

namespace GiniMonara
{
    public partial class MainForm : Form
    {
        #region Variable
        private string signature;
        private string fileName;
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
            GiniMeta giniMeta = new GiniMeta();
            textBoxTitle.Text = giniMeta.getTagValue(signature, "title");
            textBoxDescription.Text = giniMeta.getTagValue(signature, "description");
            comboBoxTagName.SelectedIndex = 0;
            textBoxData.Text = giniMeta.getTagValue(signature, comboBoxTagName.SelectedItem.ToString());
            ArrayList selectionMetaData = giniMeta.getTagSelectionValues(signature, "selection");
            hotSpots = new ArrayList();
            if (selectionMetaData != null)
            {
                foreach (MetaDatum selectionData in selectionMetaData)
                {
                    Panel panelHotSpot = new Panel();
                    panelHotSpot.Location = new Point(selectionData.x, selectionData.y);
                    panelHotSpot.Width = selectionData.p - selectionData.x;
                    panelHotSpot.Height = selectionData.q - selectionData.y;
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
                    toolTip.SetToolTip(panelHotSpot, selectionData.data);
                    hotSpots.Add(panelHotSpot);
                }
            }
            panelImage.Refresh();
        }

        private void comboBoxTagName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GiniMeta giniMeta = new GiniMeta();
            textBoxData.Text = giniMeta.getTagValue(signature, comboBoxTagName.SelectedItem.ToString());
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
            textBoxTitle.Clear();
            textBoxDescription.Clear();
            textBoxData.Clear();
        }

        private void ribbonButtonImageSave_Click(object sender, EventArgs e)
        {
            if (fileName != null)
            {
                GiniMeta giniMeta = new GiniMeta();
                if (giniMeta.getTagValue(signature, "title") == "")
                {
                    giniMeta.insertTag(signature, "title", textBoxTitle.Text);
                }
                else
                {
                    giniMeta.editTag(signature, "title", textBoxTitle.Text);
                }

                if (giniMeta.getTagValue(signature, "description") == "")
                {
                    giniMeta.insertTag(signature, "description", textBoxDescription.Text);
                }
                else
                {
                    giniMeta.editTag(signature, "description", textBoxDescription.Text);
                }

                if (giniMeta.getTagValue(signature, comboBoxTagName.SelectedItem.ToString()) == "")
                {
                    giniMeta.insertTag(signature, comboBoxTagName.SelectedItem.ToString(), textBoxData.Text);
                }
                else
                {
                    giniMeta.editTag(signature, comboBoxTagName.SelectedItem.ToString(), textBoxData.Text);
                }
            }
        }

        private void ribbonButtonImagePicasa_Click(object sender, EventArgs e)
        {
            try
            {
                #region Google API Credidentials
                PicasaService pService = new PicasaService("Gini Monara");
                pService.setUserCredentials(textBoxUserName.Text, textBoxPassword.Text);
                #endregion

                #region Upload Photo
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);
                System.IO.FileStream fileStream = fileInfo.OpenRead();
                PicasaEntry pEntry = (PicasaEntry)pService.Insert(new Uri("http://picasaweb.google.com/data/feed/api/user/default/albumid/default"), fileStream, "image/jpeg", fileName);
                #endregion

                #region Update Photo Info
                GiniMeta gMeta = new GiniMeta();
                pEntry.Title.Text = gMeta.getTagValue(signature, "title");
                pEntry.Summary.Text = gMeta.getTagValue(signature, "description");
                PicasaEntry updatedEntry = (PicasaEntry)pEntry.Update();
                #endregion

                MessageBox.Show("Image uploaded succesfully", "Gini Monara", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch
            {
                MessageBox.Show("Image upload failed.", "Gini Monara", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ribbonButtonImageActual_Click(object sender, EventArgs e)
        {
            if (fileName != null)
            {
                zoom = "actual";
                panelImage.Refresh();
            }
        }

        private void ribbonButtonImageBestFit_Click(object sender, EventArgs e)
        {
            if (fileName != null)
            {
                zoom = "bestfit";
                panelImage.Refresh();
            }
        }

        private void ribbonButtonImageZoomIn_Click(object sender, EventArgs e)
        {
            if (fileName != null)
            {
                zoom = "zoom";
                zoomFactor += zoomStep;
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
                MessageBox.Show("Image uploaded succesfully", "Gini Monara", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch
            {
                MessageBox.Show("Image upload failed.", "Gini Monara", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            hideHotSpots();
            mouseCaptured = true;
            pointStart.X = e.X;
            pointStart.Y = e.Y;
            pointEnd.X = -1;
            pointEnd.Y = -1;
        }

        private void panelImage_MouseMove(object sender, MouseEventArgs e)
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

        private void panelImage_MouseUp(object sender, MouseEventArgs e)
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

        private void drawSelectionRectangle(Point a, Point b)
        {
            if (fileName != null)
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
            GiniMeta giniMeta = new GiniMeta();
            giniMeta.insertTag(signature, "selection", textBoxSelectionData.Text, pointSelectionStart.X, pointSelectionStart.Y, pointSelectionEnd.X, pointSelectionEnd.Y);
            panelSelection.Visible = false;
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
            foreach (Panel panel in hotSpots)
            {
                panel.Visible = true;
            }
        }

        private void hideHotSpots()
        {
            foreach (Panel panel in hotSpots)
            {
                panel.Visible = false;
            }
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            panelImage.Refresh();
        }
    }
}
