using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

using EnvInt.Win32.FieldTech.Containers;
using Ionic.Zip;

namespace EnvInt.Win32.FieldTech.HelperClasses
{
    public static class PDFParser
    {
        public static CADPackage Load(string fileName, bool useRelativePath = false, string localFileName = "")
        {
            CADPackage details = new CADPackage();
            details.PackageType = CADPackageType.PDF;
            details.FileCreateDate = File.GetCreationTime(fileName);

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

            details.PackageId = Path.GetFileNameWithoutExtension(fileName);

            using (StreamReader sr = new StreamReader(File.OpenRead(fileName)))
            {
                string text = sr.ReadToEnd();
                Regex regexPage = new Regex(@"/Type\s*/Page[^s]");
                MatchCollection matchesPage = regexPage.Matches(text);
                details.PageCount = matchesPage.Count;

                Regex regexSource = new Regex(@"<</Producer([.+])");
                MatchCollection matchesSource = regexSource.Matches(text);
                if (matchesSource.Count > 0)
                {
                    details.SourceProductName = matchesSource[0].Value;
                }
            }

            details.PackageValid = true;
            return details;
        }
    }
}
