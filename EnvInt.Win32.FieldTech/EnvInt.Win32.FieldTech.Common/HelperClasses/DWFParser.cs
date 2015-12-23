using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using EnvInt.Win32.FieldTech.Containers;
using Ionic.Zip;

namespace EnvInt.Win32.FieldTech.HelperClasses
{
    public static class DWFParser
    {
        public static CADPackage Load(string fileName, bool useRelativePath = false, string localFileName = "")
        {
            CADPackage details = new CADPackage();

            if (localFileName == "")
            {
                if (useRelativePath)
                {
                    details.FileName = Path.GetFileName(fileName);
                    details.LocalName = "";
                }
                else
                {
                    details.FileName = fileName;
                    details.LocalName = "";
                }
            }
            else
            {
                if (useRelativePath)
                {
                    details.FileName = Path.GetFileName(fileName);
                    details.LocalName = Path.GetFileName(localFileName);
                }
                else
                {
                    details.FileName = fileName;
                    details.LocalName = localFileName;
                }
            }

            details.PackageType = CADPackageType.DWF;

            //load it up
            using (ZipFile zip = ZipFile.Read(fileName))
            {
                foreach (ZipEntry e in zip)
                {
                    if (e.FileName.ToLower() == "manifest.xml")
                    {
                        using (var ms = new MemoryStream())
                        {
                            e.Extract(ms);
                            ms.Position = 0;
                            StreamReader sr = new StreamReader(ms);

                            XDocument xDoc = XDocument.Parse(sr.ReadToEnd());

                            var manifestNode = xDoc.Descendants().Where(n => n.Name.LocalName == "Manifest").First();
                            details.PackageId = manifestNode.Attribute("objectId").Value;

                            var propertyNodes = xDoc.Descendants().Where(n => n.Name.LocalName == "Property");
                            
                            //TODO: Using try/catch here to keep going in the face of adversity, but this probably needs to handle files that don't have these values somehow.
                            try
                            {
                                details.ToolkitVersion = propertyNodes.Where(n => n.Attribute("name").Value == "DWFToolkitVersion").First().Attribute("value").Value;
                                details.ProductVendor = propertyNodes.Where(n => n.Attribute("name").Value == "DWFProductVendor").First().Attribute("value").Value;
                                details.ProductVersion = propertyNodes.Where(n => n.Attribute("name").Value == "DWFProductVersion").First().Attribute("value").Value;
                                details.SourceProductName = propertyNodes.Where(n => n.Attribute("name").Value == "SourceProductName").First().Attribute("value").Value;
                                details.SourceProductVendor = propertyNodes.Where(n => n.Attribute("name").Value == "SourceProductVendor").First().Attribute("value").Value;
                                details.SourceProductVersion = propertyNodes.Where(n => n.Attribute("name").Value == "SourceProductVersion").First().Attribute("value").Value;
                            }
                            catch { }


                            var sectionNodes = xDoc.Descendants().Where(n => n.Name.LocalName == "Section");
                            details.PageCount = sectionNodes.Count() - 1;

                            details.FileCreateDate = e.LastModified;
                            details.PackageValid = true;
                        }
                        
                    }
                }
            }

            return details;
        }
    }
}
