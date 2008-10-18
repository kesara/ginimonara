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

namespace GiniMonara.MetaData
{
    class gTag
    {
        public string name { get; set; }
        public string category { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int p { get; set; }
        public int q { get; set; }
        public int st { get; set; }
        public int et { get; set; }
        public string data { get; set; }

        public gTag(XElement xmlElement)
        {
            name = xmlElement.Attribute("name").Value;
            category = xmlElement.Attribute("category").Value;
            if (xmlElement.Attribute("x") != null)
            {
                x = Convert.ToInt32(xmlElement.Attribute("x").Value);
            }
            else
            {
                x = -1;
            }

            if (xmlElement.Attribute("y") != null)
            {
                y = Convert.ToInt32(xmlElement.Attribute("y").Value);
            }

            if (xmlElement.Attribute("p") != null)
            {
                p = Convert.ToInt32(xmlElement.Attribute("p").Value);
            }

            if (xmlElement.Attribute("q") != null)
            {
                q = Convert.ToInt32(xmlElement.Attribute("q").Value);
            }

            if (xmlElement.Attribute("st") != null)
            {
                st = Convert.ToInt32(xmlElement.Attribute("st").Value);
            }
            else
            {
                st = -1;
            }

            if (xmlElement.Attribute("et") != null)
            {
                et = Convert.ToInt32(xmlElement.Attribute("et").Value);
            }

            data = xmlElement.Value;
        }

        public gTag(string name, string category, string data)
        {
            this.name = name;
            this.category = category;
            this.data = data;
            this.x = -1;
            this.st = -1;
        }

        public gTag(string name, string category, string data, int x, int y, int p, int q)
        {
            this.name = name;
            this.category = category;
            this.data = data;
            this.x = x;
            this.y = y;
            this.p = p;
            this.q = q;
            this.st = -1;
        }

        public gTag(string name, string category, string data, int st, int et)
        {
            this.name = name;
            this.category = category;
            this.data = data;
            this.st = st;
            this.et = et;
            this.x = -1;
        }

        public gTag(string name, string category, string data, int x, int y, int p, int q, int st, int et)
        {
            this.name = name;
            this.category = category;
            this.data = data;
            this.x = x;
            this.y = y;
            this.p = p;
            this.q = q;
            this.st = st;
            this.et = et;
        }

        public XElement XElement
        {
            get
            {
                if (x != -1 && st != -1)
                {
                    return new XElement("tag",
                        new XAttribute("name", name),
                        new XAttribute("category", category),
                        new XAttribute("x", x),
                        new XAttribute("y", y),
                        new XAttribute("p", p),
                        new XAttribute("q", q),
                        new XAttribute("st", st),
                        new XAttribute("et", et),
                        data);
                }
                else if (x != -1 && st == -1)
                {
                    return new XElement("tag",
                        new XAttribute("name", name),
                        new XAttribute("category", category),
                        new XAttribute("x", x),
                        new XAttribute("y", y),
                        new XAttribute("p", p),
                        new XAttribute("q", q),
                        data);
                }
                else if (x == -1 && st != -1)
                {
                    return new XElement("tag",
                        new XAttribute("name", name),
                        new XAttribute("category", category),
                        new XAttribute("st", st),
                        new XAttribute("et", et),
                        data);
                }
                else
                {
                    return new XElement("tag",
                        new XAttribute("name", name),
                        new XAttribute("category", category),
                        data);
                }
            }
        }
    }
}
