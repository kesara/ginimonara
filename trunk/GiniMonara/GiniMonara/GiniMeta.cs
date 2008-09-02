using System;
using System.Collections;
using System.Text;
using System.Xml;

/*
 * GiniMeta - Metadata Access Class
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
    class GiniMeta
    {
        #region Variables
        public string error;
        private string applicationDataDirectory;
        private string metaDataDirectory;
        #endregion

        public GiniMeta()
        {
            applicationDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\GiniMonara";
            metaDataDirectory = applicationDataDirectory + @"\metadata";
        }

        public bool insertTag(string signature, string tag, string data)
        {
            string metaDataFileName = metaDataDirectory + @"\" + signature + ".xml";
            if (MetaDataUtility.checkMetaDataExsists(metaDataFileName))
            {
                try
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(metaDataFileName);

                    XmlElement xmlElement = xmlDocument.CreateElement("tag");
                    XmlAttribute xmlAtrribute = xmlDocument.CreateAttribute("name");
                    xmlAtrribute.Value = tag;
                    xmlElement.Attributes.Append(xmlAtrribute);
                    xmlElement.InnerText = data;
                    xmlDocument.DocumentElement.AppendChild(xmlElement);

                    xmlDocument.PreserveWhitespace = true;
                    XmlTextWriter xmlWriter = new XmlTextWriter(metaDataFileName, Encoding.UTF8);
                    xmlWriter.Formatting = Formatting.Indented;
                    xmlDocument.WriteTo(xmlWriter);
                    xmlWriter.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    return false;
                }
            }
            else
            {
                try
                {
                    XmlTextWriter xmlWriter = new XmlTextWriter(metaDataFileName, Encoding.UTF8);
                    xmlWriter.Formatting = Formatting.Indented;
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("metaData");

                    xmlWriter.WriteStartElement("tag");
                    xmlWriter.WriteAttributeString("name", tag);
                    xmlWriter.WriteString(data);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndElement();
                    xmlWriter.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    return false;
                }
            }
        }

        public bool insertTag(string signature, string tag, string data, int x, int y, int p, int q)
        {
            string metaDataFileName = metaDataDirectory + @"\" + signature + ".xml";
            if (MetaDataUtility.checkMetaDataExsists(metaDataFileName))
            {
                try
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(metaDataFileName);

                    XmlElement xmlElement = xmlDocument.CreateElement("tag");
                    XmlAttribute xmlAtrribute = xmlDocument.CreateAttribute("name");
                    xmlAtrribute.Value = tag;
                    xmlElement.Attributes.Append(xmlAtrribute);
                    xmlAtrribute = xmlDocument.CreateAttribute("x");
                    xmlAtrribute.Value = x.ToString();
                    xmlElement.Attributes.Append(xmlAtrribute);
                    xmlAtrribute = xmlDocument.CreateAttribute("y");
                    xmlAtrribute.Value = y.ToString();
                    xmlElement.Attributes.Append(xmlAtrribute);
                    xmlAtrribute = xmlDocument.CreateAttribute("p");
                    xmlAtrribute.Value = p.ToString();
                    xmlElement.Attributes.Append(xmlAtrribute);
                    xmlAtrribute = xmlDocument.CreateAttribute("q");
                    xmlAtrribute.Value = q.ToString();
                    xmlElement.Attributes.Append(xmlAtrribute);
                    xmlElement.InnerText = data;
                    xmlDocument.DocumentElement.AppendChild(xmlElement);

                    xmlDocument.PreserveWhitespace = true;
                    XmlTextWriter xmlWriter = new XmlTextWriter(metaDataFileName, Encoding.UTF8);
                    xmlWriter.Formatting = Formatting.Indented;
                    xmlDocument.WriteTo(xmlWriter);
                    xmlWriter.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    return false;
                }
            }
            else
            {
                try
                {
                    XmlTextWriter xmlWriter = new XmlTextWriter(metaDataFileName, Encoding.UTF8);
                    xmlWriter.Formatting = Formatting.Indented;
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("metaData");

                    xmlWriter.WriteStartElement("tag");
                    xmlWriter.WriteAttributeString("name", tag);
                    xmlWriter.WriteAttributeString("x", x.ToString());
                    xmlWriter.WriteAttributeString("y", y.ToString());
                    xmlWriter.WriteAttributeString("p", p.ToString());
                    xmlWriter.WriteAttributeString("q", q.ToString());
                    xmlWriter.WriteString(data);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndElement();
                    xmlWriter.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    return false;
                }
            }
        }

        public bool editTag(string signature, string tag, string data)
        {
            string metaDataFileName = metaDataDirectory + @"\" + signature + ".xml";
            if (MetaDataUtility.checkMetaDataExsists(metaDataFileName))
            {
                try
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(metaDataFileName);

                    XmlNode node = xmlDocument.SelectSingleNode("//tag[@name='" + tag + "']");
                    if (node != null)
                    {
                        node.InnerText = data;
                    }
                    else
                    {
                        error = "Metadata file not found.";
                        return false;
                    }

                    xmlDocument.PreserveWhitespace = true;
                    XmlTextWriter xmlWriter = new XmlTextWriter(metaDataFileName, Encoding.UTF8);
                    xmlWriter.Formatting = Formatting.Indented;
                    xmlDocument.WriteTo(xmlWriter);
                    xmlWriter.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    return false;
                }
            }
            else
            {
                error = "Metadata file not found.";
                return false;
            }
        }

        public bool editTag(string signature, string tag, string data, int x, int y, int p, int q)
        {
            string metaDataFileName = metaDataDirectory + @"\" + signature + ".xml";
            if (MetaDataUtility.checkMetaDataExsists(metaDataFileName))
            {
                try
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(metaDataFileName);

                    XmlNode node = xmlDocument.SelectSingleNode("//tag[@name='" + tag + "']");
                    if (node != null)
                    {
                        /*
                         * TODO: Chanage x, y, p, q attributes.
                         */
                        node.InnerText = data;
                    }
                    else
                    {
                        error = tag + " not found on the metadata file.";
                        return false;
                    }

                    xmlDocument.PreserveWhitespace = true;
                    XmlTextWriter xmlWriter = new XmlTextWriter(metaDataFileName, Encoding.UTF8);
                    xmlWriter.Formatting = Formatting.Indented;
                    xmlDocument.WriteTo(xmlWriter);
                    xmlWriter.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    return false;
                }
            }
            else
            {
                error = "Metadata file not found.";
                return false;
            }
        }

        public string getTagValue(string signature, string tag)
        {
            string metaDataFileName = metaDataDirectory + @"\" + signature + ".xml";
            if (MetaDataUtility.checkMetaDataExsists(metaDataFileName))
            {
                try
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(metaDataFileName);

                    XmlNode node = xmlDocument.SelectSingleNode("//tag[@name='" + tag + "']");
                    if (node != null)
                    {
                        return node.InnerText;
                    }
                    else
                    {
                        error = tag + " not found on the metadata file.";
                        return "";
                    }
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    return "";
                }
            }
            else
            {
                error = "Metadata file not found.";
                return "";
            }
        }


        public ArrayList getTagSelectionValues(string signature, string tag)
        {
            ArrayList selectionData = new ArrayList();
            string metaDataFileName = metaDataDirectory + @"\" + signature + ".xml";
            if (MetaDataUtility.checkMetaDataExsists(metaDataFileName))
            {
                try
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(metaDataFileName);

                    XmlNodeList nodes = xmlDocument.SelectNodes("//tag[@name='" + tag + "']");
                    if (nodes.Count > 0)
                    {
                        foreach (XmlNode node in nodes)
                        {
                            selectionData.Add(new MetaDatum(node.InnerText, node.Attributes["x"].Value, node.Attributes["y"].Value,node.Attributes["p"].Value,node.Attributes["q"].Value));
                        }
                        return selectionData;
                    }
                    else
                    {
                        error = tag + " not found on the metadata file.";
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    return null;
                }
            }
            else
            {
                error = "Metadata file not found.";
                return null;
            }
        }

        public MetaDatum getMetaDatum(string signature, string tag)
        {
            string metaDataFileName = metaDataDirectory + @"\" + signature + ".xml";
            if (MetaDataUtility.checkMetaDataExsists(metaDataFileName))
            {
                try
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(metaDataFileName);

                    XmlNode node = xmlDocument.SelectSingleNode("//tag[@name='" + tag + "']");
                    if (node != null)
                    {
                        MetaDatum datum = new MetaDatum(node.InnerText, node.Attributes["x"].InnerText, node.Attributes["y"].InnerText, node.Attributes["p"].InnerText, node.Attributes["q"].InnerText);
                        return datum;
                    }
                    else
                    {
                        error = tag + " not found on the metadata file.";
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    return null;
                }
            }
            else
            {
                error = "Metadata file not found.";
                return null;
            }
        }
    }
}
