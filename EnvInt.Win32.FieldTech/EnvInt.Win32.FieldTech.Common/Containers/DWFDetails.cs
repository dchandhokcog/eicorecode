using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvInt.Win32.FieldTech.Containers
{
    public class CADPackage
    {
        public CADPackageType PackageType { get; set; }
        public string FileName { get; set; }
        //LocalName indicates a file that differs in name from the original drawing.  This will be the case if "duplicates" of the original are loaded for the project.
        public string LocalName { get; set; }
        public bool PackageValid { get; set; }
        public string PackageId { get; set; }
        public DateTime? LastRefreshed { get; set; }
        public DateTime? FileCreateDate { get; set; }
        public string ProductVendor { get; set; }
        public string ProductVersion { get; set; }
        public string ToolkitVersion { get; set; }
        public string SourceProductName { get; set; }
        public string SourceProductVendor { get; set; }
        public string SourceProductVersion { get; set; }
        public int PageCount { get; set; }
        public string ValidationError { get; set; }

        public CADPackage()
        { }

        public CADPackage(CADPackage refPackage)
        {
            FileCreateDate = refPackage.FileCreateDate;
            FileName = refPackage.FileName;
            LastRefreshed = refPackage.LastRefreshed;
            LocalName = refPackage.LocalName;
            PackageId = refPackage.PackageId;
            PackageType = refPackage.PackageType;
            PackageValid = refPackage.PackageValid;
            PageCount = refPackage.PageCount;
            ProductVendor = refPackage.ProductVendor;
            ProductVersion = refPackage.ProductVersion;
            SourceProductName = refPackage.SourceProductName;
            SourceProductVersion = refPackage.SourceProductVersion;
            ToolkitVersion = refPackage.ToolkitVersion;
            ValidationError = refPackage.ValidationError;
        }

    }

    public enum CADPackageType
    {
        Unknown = 0,
        DWF,
        PDF
    }
}
