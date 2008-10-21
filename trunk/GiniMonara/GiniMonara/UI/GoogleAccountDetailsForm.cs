using GiniMonara.UI;
using GiniMonara.Utilities;
using System;
using System.Windows.Forms;

/*
 * GoogleAccountDetailsForm - GiniMonara Google Account Details User Interface
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
    public partial class GoogleAccountDetailsForm : Form
    {
        MainForm parentUI;

        public GoogleAccountDetailsForm(MainForm parentUI)
        {
            this.parentUI = parentUI;
            InitializeComponent();
        }

        private void ribbonButtonOk_Click(object sender, EventArgs e)
        {
            ApplicationUtility.setGoogleSecrets(textBoxUserName.Text, textBoxPassword.Text);
            parentUI.sendToPicasa();
            this.Close();
        }

        private void ribbonButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GoogleAccountDetailsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            parentUI.Enabled = true;
        }
    }
}
