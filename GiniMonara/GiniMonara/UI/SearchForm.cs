using GiniMonara.MetaData;
using GiniMonara.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

/*
 * SearchForm - GiniMonara Search User Interface
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
    public partial class SearchForm : Form
    {
        private MainForm parentUI;
        private DataTable searchResults;

        public SearchForm(MainForm caller)
        {
            parentUI = caller;
            InitializeComponent();
        }

        private void ribbonButtonSearch_Click(object sender, EventArgs e)
        {
            searchResults = new DataTable();
            searchResults.Columns.Add("Data");
            searchResults.Columns.Add("File Name");
            searchResults.Columns.Add("Type");

            string[] metaDataFiles = Directory.GetFiles(ApplicationUtility.metaDataDirectory, "*.xml");
            foreach (string metaDataFile in metaDataFiles)
            {
                TagList tagList = new TagList();
                tagList.load(metaDataFile);

                var tags = tagList.Where(d => d.data.ToLower().Contains(textBoxSearch.Text.ToLower())).Select(t => t);

                foreach (gTag tag in tags)
                {
                    DataRow searchRecord = searchResults.NewRow();
                    searchRecord["Data"] = tag.data;
                    searchRecord["File Name"] = tagList.Where(t => t.name == "fileName").Select(v => v.data).FirstOrDefault();
                    searchRecord["Type"] = tagList.Where(t => t.name == "mediaType").Select(v => v.data).FirstOrDefault();
                    searchResults.Rows.Add(searchRecord);
                }
            }

            dataGridViewResults.DataSource = searchResults;
        }

        private void SearchForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            parentUI.reloadCategories();
            parentUI.Enabled = true;
        }

        private void dataGridViewResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (searchResults.Rows[e.RowIndex]["Type"].ToString() == "image")
            {
                parentUI.openImage(searchResults.Rows[e.RowIndex]["File Name"].ToString());
                this.Close();
            }
            else if (searchResults.Rows[e.RowIndex]["Type"].ToString() == "video")
            {
                parentUI.openVideo(searchResults.Rows[e.RowIndex]["File Name"].ToString());
                this.Close();
            }
        }
    }
}
