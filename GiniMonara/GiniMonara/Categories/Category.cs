using System;
using System.Xml.Linq;

/*
 * Category - GiniMonara Category data class
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

namespace GiniMonara.Categories
{
    class Category
    {
        public string category { get; set; }
        public string type { get; set; }
        public string tag { get; set; }

        public Category(XElement xmlElement)
        {
            category = xmlElement.Attribute("name").Value;
            type = xmlElement.Attribute("type").Value;
            tag = xmlElement.Value;
        }

        public Category(string name, string type, string tag)
        {
            this.category = name;
            this.type = type;
            this.tag = tag;
        }

        public XElement XElement
        {
            get
            {
                return new XElement("category",
                    new XAttribute("name", category),
                    new XAttribute("type", type),
                    tag);
            }
        }
    }
}
