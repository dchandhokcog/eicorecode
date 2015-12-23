using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.HelperClasses
{
    public static class CADPackageFactory
    {

        public static CADPackage LoadFromFile(string filename, bool useRelativePath = false, string localFileName = "")
        {
            CADPackage package = null;
            switch (Path.GetExtension(filename).ToLower())
            {
                case ".dwf":
                    package = DWFParser.Load(filename, useRelativePath, localFileName);
                    break;
                case ".pdf":
                    package = PDFParser.Load(filename, useRelativePath, localFileName);
                    break;
                default:
                    throw new Exception("Unsupported CAD package file: " + Path.GetFileName(filename).ToLower());
            }
            return package;
        }
    }
}
