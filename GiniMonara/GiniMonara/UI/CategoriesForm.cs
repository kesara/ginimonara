using GiniMonara.Categories;
using GiniMonara.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/*
 * CategoriesForm - GiniMonara Categories User Interface
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
    public partial class CategoriesForm : Form
    {
        private MainForm parentUI;

        public CategoriesForm(MainForm caller)
        {
            parentUI = caller;
            InitializeComponent();
        }

        private void CategoriesForm_Load(object sender, EventArgs e)
        {
            var categories = ApplicationUtility.categories.Select(c => c.category).Distinct();
            foreach (string category in categories)
            {
                comboBoxCategory.Items.Add(category);
            }
            comboBoxCategory.SelectedIndex = 0;

            var types = ApplicationUtility.categories.Select(t => t.type).Distinct();
            foreach (string type in types)
            {
                comboBoxType.Items.Add(type);
            }
            comboBoxType.SelectedIndex = 0;
        }

        private void ribbonButtonTagAdd_Click(object sender, EventArgs e)
        {
            if (textBoxTag.Text != "")
            {
                Category category = new Category(comboBoxCategory.SelectedItem.ToString(), comboBoxType.SelectedItem.ToString(), textBoxTag.Text);
                ApplicationUtility.addCategory(category);
                textBoxTag.Clear();
                MessageBox.Show("Tag added", "GiniMonara", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please provide tag", "GiniMonara Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ribbonButtonReset_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you really want to reset all categories?", "GiniMonara", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dialogResult == DialogResult.OK)
            {
                ApplicationUtility.resetCategories();
            }                
        }

        private void ribbonButtonCategoryAdd_Click(object sender, EventArgs e)
        {
            if (textBoxCategory.Text != "")
            {
                Category category = new Category(textBoxCategory.Text, "hidden", "E0T");
                ApplicationUtility.addCategory(category);
                textBoxTag.Clear();
                MessageBox.Show("Category added", "GiniMonara", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.reloadCategories();
            }
            else
            {
                MessageBox.Show("Please provide tag", "GiniMonara Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void reloadCategories()
        {
            comboBoxCategory.Items.Clear();
            var categories = ApplicationUtility.categories.Select(c => c.category).Distinct();
            foreach (string category in categories)
            {
                comboBoxCategory.Items.Add(category);
            }
            comboBoxCategory.SelectedIndex = 0;
        }

        private void ribbonButtonOk_Click(object sender, EventArgs e)
        {
            parentUI.reloadCategories();
            parentUI.Enabled = true;
            this.Dispose();
        }

        private void CategoriesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            parentUI.reloadCategories();
            parentUI.Enabled = true;
        }
    }
}
