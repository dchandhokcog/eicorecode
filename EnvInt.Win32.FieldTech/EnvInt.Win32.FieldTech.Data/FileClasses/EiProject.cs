using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ionic.Zip;
using Microsoft.VisualBasic.FileIO;

namespace EnvInt.Win32.FieldTech.Data
{
    public static class EiProject
    {

        public static string DataFileName = "projectdata.s3db";
        
        public static string WorkingDirectory
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\EiProjectTemp";
            }
        }

        public static string MarkedDrawingsDirectory
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\EiProjectTemp\\MarkedDrawings";
            }
        }

        public static string DrawingsDirectory
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\EiProjectTemp\\ProjectDrawings";
            }
        }

        public static string CurrentEIP { get; set; }

        public static bool ResetWorkingDirectory()
        {
            try
            {
                if (Directory.Exists(WorkingDirectory))
                {
                    ClearWorkingDirectory();
                }

                Directory.CreateDirectory(WorkingDirectory);
                Directory.CreateDirectory(MarkedDrawingsDirectory);
                Directory.CreateDirectory(DrawingsDirectory);
            }
            catch {
                return false;
            }
            return true;
        }

        public static bool ExtractEIP(string fileName)
        {
            try
            {
                ZipFile zip = new ZipFile(fileName);
                zip.ExtractAll(WorkingDirectory, ExtractExistingFileAction.OverwriteSilently);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static bool SaveEIP(string fileName)
        {
            try
            {
                ZipFile zip = new ZipFile(fileName);
                zip.AddDirectory(WorkingDirectory);
                zip.Save();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static bool ClearWorkingDirectory()
        {
            //delete it to make sure we are clean...
            try
            {
                if (System.IO.Directory.Exists(WorkingDirectory))
                {

                    //Move to recycle bin
                    FileSystem.DeleteDirectory(WorkingDirectory, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                }
            }
            catch
            {
                return false;            
            }

            return true;
        }

    }
}
