using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * MetaDatum - Gini Monara Metadata Item
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
    class MetaDatum
    {
        private int a;
        private int b;
        private int c;
        private int d;
        private string metaData;

        public int x {
            get {
                return a;
            }
            set {
                a = value;
            }
        }

        public int y
        {
            get
            {
                return b;
            }
            set
            {
                b = value;
            }
        }

        public int p
        {
            get
            {
                return c;
            }
            set
            {
                c = value;
            }
        }

        public int q
        {
            get
            {
                return d;
            }
            set
            {
                d = value;
            }
        }

        public string data
        {
            get
            {
                return metaData;
            }
            set
            {
                metaData = value;
            }
        }

        public MetaDatum(string data, string x, string y, string p, string q)
        {
            this.data = data;
            this.x = Convert.ToInt32(x);
            this.y = Convert.ToInt32(y);
            this.p = Convert.ToInt32(p);
            this.q = Convert.ToInt32(q);
        }
    }
}
