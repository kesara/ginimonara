using GiniMonara.UI;
using System;
using System.Reflection;
using System.Windows.Forms;

/*
 * AboutForm - GiniMonara About User Interface
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
    public partial class AboutForm : Form
    {
        MainForm parentUI;

        public AboutForm(MainForm parentUI)
        {
            this.parentUI = parentUI;
            InitializeComponent();
            labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
        }

        private void ribbonButtonOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AboutForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            parentUI.Enabled = true;
        }

        #region Assembly Attribute Accessors
        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
        #endregion

        private void linkLabelHomePage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://ginimonara.sourceforge.net/");
        }
    }
}
