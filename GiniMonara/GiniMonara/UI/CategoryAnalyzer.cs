using GiniMonara.Categories;
using GiniMonara.Utilities;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

/*
 * CategoriesAnalyzer - GiniMonara Category Analyzer User Interface
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
    public partial class CategoryAnalyzer : Form
    {
        private MainForm parentUI;

        public CategoryAnalyzer(MainForm caller)
        {
            parentUI = caller;
            InitializeComponent();
        }

        private void CategoryAnalyzer_Load(object sender, EventArgs e)
        {
            var categories = ApplicationUtility.categories.Select(c => c.category).Distinct();
            foreach (string category in categories)
            {
                comboBoxCategory.Items.Add(category);
            }
            comboBoxCategory.SelectedIndex = 0;
        }

        private void ribbonButtonOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CategoryAnalyzer_FormClosed(object sender, FormClosedEventArgs e)
        {
            parentUI.Enabled = true;
        }

        private DataTable getTags(string category)
        {
            DataTable tags = new DataTable();
            tags.Columns.Add("Tag");
            tags.Columns.Add("Type");

            var tagList = ApplicationUtility.categories.Where(c => c.category == category).Select(t => t);

            foreach (Category cTag in tagList)
            {
                if (cTag.tag != "E0T")
                {
                    DataRow tag = tags.NewRow();
                    tag["Tag"] = cTag.tag;
                    tag["Type"] = cTag.type;
                    tags.Rows.Add(tag);
                }
            }

            return tags;
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridViewTags.DataSource = getTags(comboBoxCategory.SelectedItem.ToString());
        }
    }
}
