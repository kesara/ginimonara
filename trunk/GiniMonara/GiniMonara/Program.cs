using System;
using System.Windows.Forms;

/*
 * Program - Gini Monara Main Program
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
    static class Program
    {

        [STAThread]
        static void Main()
        {
            #region Application Initiation
            ApplicationUtilities.applicationStart();
            #endregion

            #region DisplayForm
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            #endregion
        }
    }
}
