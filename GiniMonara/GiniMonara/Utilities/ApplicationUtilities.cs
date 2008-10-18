using GiniMonara.Categories;
using System;
using System.IO;

/*
 * ApplicationUtility - GiniMonara Application Utilities
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

namespace GiniMonara.Utilities
{
    class ApplicationUtility
    {
        #region Variables
        public static string applicationDataDirectory { get; set; }
        public static string metaDataDirectory { get; set; }
        public static string catogoriesFile { get; set; }
        public static CategoryList categories { get; set; }
        #endregion

        public static void applicationStart()
        {
            /* Set application data directory */
            applicationDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\GiniMonara";
            /* Set meta data directory */
            metaDataDirectory = applicationDataDirectory + @"\metadata";
            /* Set categories file path */
            catogoriesFile = applicationDataDirectory + @"\Category.xml";

            /* Check exsistance of application directory */
            if (!Directory.Exists(applicationDataDirectory))
            {
                /* Create application data directory */
                Directory.CreateDirectory(applicationDataDirectory);
            }

            /* Check esistance of meta data directory */
            if (!Directory.Exists(metaDataDirectory))
            {
                /* Create meta data directory */
                Directory.CreateDirectory(metaDataDirectory);
            }

            /* Check esistance of categories file */
            if (!File.Exists(catogoriesFile))
            {
                /* Create categories file */
                copyOriginalCategoryFile();
            }

            /* Set categories */
            loadCategories();
        }

        private static void copyOriginalCategoryFile() {
            if (File.Exists(catogoriesFile))
            {
                File.Delete(catogoriesFile);
            }
            File.Copy(AppDomain.CurrentDomain.BaseDirectory + @"\xml\Category.xml", catogoriesFile);
        }

        private static void loadCategories() {
            categories = new CategoryList();
            categories.load(catogoriesFile);
        }

        public static void addCategory(Category category)
        {
            categories.Add(category);
            categories.save(catogoriesFile);
            loadCategories();
        }

        public static void resetCategories()
        {
            copyOriginalCategoryFile();
            loadCategories();
        }
    }
}
