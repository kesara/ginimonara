using System;
using System.IO;

/*
 * ApplicationUtilities - Gini Monara Application Utilities
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
    class ApplicationUtilities
    {
        public static void applicationStart()
        {
            string applicationDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\GiniMonara";
            string metaDataDirectory = applicationDataDirectory + @"\metadata";
            if (!Directory.Exists(applicationDataDirectory))
            {
                Directory.CreateDirectory(applicationDataDirectory);
            }
            if (!Directory.Exists(metaDataDirectory))
            {
                Directory.CreateDirectory(metaDataDirectory);
            }
        }
    }
}
