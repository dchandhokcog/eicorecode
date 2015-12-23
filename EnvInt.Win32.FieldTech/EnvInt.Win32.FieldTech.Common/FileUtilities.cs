using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Ionic.Zip;
using System.Runtime.Serialization.Json;
using ClosedXML.Excel;
using System.Data;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Common
{
    public static class FileUtilities
    {
        private static string CSV_SPLIT_PATTERN_EXCEL = @"""?\s*,\s*""?";
        //  "\\"|\\",\\"|\\"
        private static string CSV_SPLIT_PATTERN = @"""\""|\"",\""|\""";

        private static string CSV_SPLIT_TAB = @"""?\t""?";

        private static Random r = new Random();

        public enum GibberishWords
        {
            NSD = 1,
            SSD = 2,
            ESD = 3,
            WSD = 4,
            ABV = 5,
            PUMP = 6,
            DISC = 7,
            CATWALK = 8,
            SUCT = 9,
            PRV = 10,
            BLK = 11,
            BLD = 12,
            P101 = 13,
            P202 = 14
        }

        [Obsolete("GetZipEntryText is deprecated, please use GetZipEntryAsText instead.")]
        public static string GetZipEntryText(ZipFile zip, string filename)
        {
            string fileString = "";
            ZipEntry edj = zip.Entries.Where(entry => entry.FileName.ToLower() == filename).FirstOrDefault();
            if (edj != null)
            {
                using (var ms = new MemoryStream())
                {
                    edj.Extract(ms);
                    ms.Position = 0;
                    using (StreamReader sr = new StreamReader(ms))
                    {
                        fileString = sr.ReadToEnd();
                    }
                }
            }
            return fileString;
        }

        public static string GetZipEntryAsText(ZipFile zip, string filename)
        {
            string fileString = "";
            ZipEntry edj = zip.Entries.Where(entry => entry.FileName.ToLower() == filename).FirstOrDefault();
            if (edj != null)
            {
                using (var ms = new MemoryStream())
                {
                    edj.Extract(ms);
                    ms.Position = 0;
                    using (StreamReader sr = new StreamReader(ms, Encoding.UTF8))
                    {
                        fileString = sr.ReadToEnd();
                       
                    }
                }
            }
            return fileString;
        }

        public static byte[] GetZipEntryAsByteArray(ZipFile zip, string filename)
        {
            byte[] bytes = null;
            ZipEntry edj = zip.Entries.Where(entry => entry.FileName.ToLower() == filename).FirstOrDefault();
            if (edj != null)
            {
                using (var ms = new MemoryStream())
                {
                    edj.Extract(ms);
                    ms.Position = 0;
                    bytes = new byte[ms.Length];
                    ms.Read(bytes, 0, (int)ms.Length);
                }
            }
            return bytes;
        }

        public static void CheckProjectVersion(ZipFile zip, Version applicationVersion)
        {
            bool showWarning = true;
            try
            {
                string vsn = FileUtilities.GetZipEntryAsText(zip, "version.txt");
                if (vsn.Contains("Version="))
                {
                    Version fileVersion = new Version();
                    string[] parts = vsn.Split('=');
                    if (parts.Length == 2)
                    {
                        if (Version.TryParse(parts[1], out fileVersion))
                        {
                            //THIS DOES NOT TEST BUILD, ALL OTHERS MUST MATCH
                            if (fileVersion.Major == applicationVersion.Major
                                || fileVersion.Minor == applicationVersion.Minor
                                || fileVersion.Revision == applicationVersion.Revision)
                            {
                                showWarning = false;
                            }
                        }
                    }
                }
            }
            catch 
            { 
                //TODO: This should log the exception to the local error log
            }

            if (showWarning)
            {
                MessageBox.Show("Warning: This file was created with a version of FieldTech Manager that doesn't match this version of of FTT/EiMOC");
            }
        }

        public static string SerializeObject<T>(T t)
        {
            string text = "";
            using (MemoryStream stream1 = new MemoryStream())
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                ser.WriteObject(stream1, t);
                stream1.Position = 0;
                StreamReader sr = new StreamReader(stream1);
                text = sr.ReadToEnd();
            }
            return text;
        }

        public static void SerializeProjectData(ref ProjectData pData, string fileName)
        {

            FileStream stream1 = new FileStream(fileName, FileMode.Create);
            using (stream1)
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ProjectData));
                ser.WriteObject(stream1, pData);
                stream1.Flush();
                stream1.Close();
                stream1.Dispose();
            }

        }

        public static ProjectData DeserializeProjectData(string fileName)
        {

            ProjectData pd = null;

            if (String.IsNullOrEmpty(fileName)) return default(ProjectData);

            try
            {
                using (FileStream stream1 = new FileStream(fileName, FileMode.OpenOrCreate))
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ProjectData));
                    stream1.Position = 0;
                    pd = (ProjectData)ser.ReadObject(stream1);
                    stream1.Close();
                    stream1.Dispose();
                }

            }
            catch
            {
                Encoding enc = new UnicodeEncoding(false, true, false);

                using (MemoryStream stream1 = new MemoryStream(enc.GetBytes(File.ReadAllText(fileName))))
                {
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ProjectData));
                    stream1.Position = 0;
                    pd = (ProjectData)ser.ReadObject(stream1);
                    stream1.Close();
                    stream1.Dispose();
                }
            }

            return pd;
        }

        public static T DeserializeObject<T>(string json)
        {
            if (String.IsNullOrEmpty(json)) return default(T);

            Encoding enc = new UnicodeEncoding(false, true, false);
            
            using (MemoryStream stream1 = new MemoryStream(enc.GetBytes(json)))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                stream1.Position = 0;
                return (T)ser.ReadObject(stream1);
            }
        }

        public static string GetTaggedComponentsAsCSV(List<TaggedComponent> components)
        {
            StringBuilder csv = new StringBuilder();
            bool headerComplete = false;
            foreach (TaggedComponent component in components)
            {
                if (!headerComplete)
                {
                    csv.AppendLine(component.GetHeader());
                    headerComplete = true;
                }
                csv.AppendLine(component.ToString());
                foreach (string child in component.GetChildrenAsComponents())
                {
                    csv.AppendLine(child);
                }
            }
            return csv.ToString();
        }

        public static List<TaggedComponent> GetTaggedComponentsFromCSV(string csvData, bool autoGenerateIds, bool asExcel, bool asTab, string delimiter = "")
        {
            List<TaggedComponent> components = new List<TaggedComponent>();
            bool forceReturn = false;
            string regExPattern = string.Empty;
            string errorMessage = string.Empty;

            if (asExcel)
            {
                regExPattern = CSV_SPLIT_PATTERN_EXCEL;
            }
            else
            { 
                regExPattern = CSV_SPLIT_PATTERN;
            }

            if (asTab) regExPattern = CSV_SPLIT_TAB;

            if (delimiter != "")
            {
                regExPattern = @"""?" + @delimiter + @"""?";
            }

            string[] rows = csvData.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            if (rows.Length > 0)
            {
                string[] headerValues = System.Text.RegularExpressions.Regex.Split(rows[0], regExPattern);

                for (int i = 1; i < rows.Length; i++)
                {
                    if (!forceReturn)
                    {
                        
                        //System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(CSV_SPLIT_PATTERN);
                        string[] values = System.Text.RegularExpressions.Regex.Split(rows[i], regExPattern, System.Text.RegularExpressions.RegexOptions.ExplicitCapture);
                        TaggedComponent component = new TaggedComponent();
                        string parentComponentId = string.Empty;

                        for (int j = 0; j < headerValues.Length; j++)
                        {
                            if (!forceReturn)
                            {
                                if (j < values.Length)
                                {
                                    if (headerValues[j] == "Relation")
                                    {
                                        parentComponentId = values[j];
                                    }
                                    PropertyInfo prop = component.GetType().GetProperty(headerValues[j], BindingFlags.Public | BindingFlags.Instance);
                                    if (null != prop && prop.CanWrite)
                                    {
                                        component.setValueByName(headerValues[j], values[j]);
                                        errorMessage = component.setValueByName(headerValues[j], values[j]);
                                        if (!string.IsNullOrEmpty(errorMessage))
                                        {
                                            MessageBox.Show(errorMessage);
                                            return new List<TaggedComponent>();
                                        }
                                    }
                                }
                            }
                        }
                        if (!forceReturn)
                        {
                            if (autoGenerateIds && String.IsNullOrEmpty(component.Id))
                            {
                                component.Id = Guid.NewGuid().ToString();
                            }
                            if (parentComponentId != String.Empty)
                            {
                                //this is a child... add it to the parent if found..
                                TaggedComponent parentComponent = components.Where(c => c.Id == parentComponentId).FirstOrDefault();
                                if (parentComponent != null)
                                {
                                    parentComponent.Children.Add(new ChildComponent(component));
                                }
                            }
                            else
                            {
                                //normal parent component
                                components.Add(component);
                            }
                        }
                    }
                }
            }
            return components;
        }

        public static string SendTaggedComponentsToExcel(List<TaggedComponent> tcList, string fileName, bool ignoreChildren = false)
        {
            string errorMessage = string.Empty;
            XLWorkbook wb = new XLWorkbook();
            DataTable dt = new DataTable();
            string parentId = string.Empty;

            try
            {
                foreach (PropertyInfo info in typeof(TaggedComponent).GetProperties())
                {
                    if (info.Name == "Children")
                    {
                        dt.Columns.Add("Relation", typeof(string));
                    }
                    else
                    {
                        dt.Columns.Add(info.Name, typeof(string));
                    }
                }

                dt.AcceptChanges();

                List<TaggedComponent> tcFilteredList = new List<TaggedComponent>();

                if (!ignoreChildren)
                {
                    tcFilteredList = tcList;
                }
                else
                {
                    tcFilteredList.AddRange(tcList.Where(c => !c.isChild));
                }

                foreach (TaggedComponent tc in tcFilteredList)
                {
                    DataRow dr = dt.NewRow();
                    parentId = tc.Id;

                    //add parents
                    foreach (PropertyInfo prop in tc.GetType().GetProperties().Where(c => c.Name != "Children"))
                    {
                        string value = string.Empty;
                        if (prop.GetValue(tc, null) == null)
                        {
                            dr[prop.Name] = string.Empty;
                        }
                        else
                        {
                            dr[prop.Name] = prop.GetValue(tc, null).ToString();
                        }
                    }

                    dt.Rows.Add(dr);

                    if (tc.Children.Count > 0)
                    {
                        //add children
                        foreach (TaggedComponent child in tc.GetChildrenAsTaggedComponent())
                        {

                            dr = dt.NewRow();

                            foreach (PropertyInfo prop in child.GetType().GetProperties().Where(c => c.Name != "Children"))
                            {
                                string value = string.Empty;
                                if (prop.GetValue(child, null) == null)
                                {
                                    dr[prop.Name] = string.Empty;
                                }
                                else
                                {
                                    dr[prop.Name] = prop.GetValue(child, null).ToString();
                                }
                                dr["Relation"] = parentId;
                            }

                            dt.Rows.Add(dr);
                        }
                    }

                }

                wb.Worksheets.Add(dt, Path.GetFileNameWithoutExtension(fileName));
                wb.SaveAs(fileName);
            }
            catch (Exception ex)
            {
                errorMessage = "An error occurred while writing to Excel: " + ex.Message;
            }

            return errorMessage;
        }

        public static List<TaggedComponent> GetTaggedComponentFromExcel(string fileName)
        {

            List<TaggedComponent> tcList = new List<TaggedComponent>();
            int rowCount = 0;
            int columnCount = 0;
            string parentComponentId = string.Empty;
            bool forceReturn = false;
            SortedList<int, string> headerValues = new SortedList<int, string>();
            TaggedComponent tempTc = new TaggedComponent();
            List<string> headerList = tempTc.getHeaderAsList();
            string errorMessage = string.Empty;

            XLWorkbook wb = new XLWorkbook(fileName);
            IXLWorksheet ws = wb.Worksheet(1);


            var xlRange = ws.RangeUsed();

            IXLTable compTable = xlRange.AsTable();

            foreach (IXLRangeRow tr in compTable.Rows())
            {
                rowCount++;
                columnCount = 0;
                if (rowCount == 1)
                {
                    int headerRowNo = 1;
                    foreach (IXLCell cell in tr.Cells())
                    {
                        headerValues.Add(headerRowNo, cell.Value.ToString());
                        headerRowNo++;
                    }
                }
                else
                {
                    TaggedComponent component = new TaggedComponent();
                    foreach (IXLCell cell in tr.Cells())
                    {
                        if (headerValues.ElementAt(columnCount).Value == "Relation")
                        {
                            parentComponentId = cell.Value.ToString();
                        }
                        PropertyInfo prop = component.GetType().GetProperty(headerValues.ElementAt(columnCount).Value, BindingFlags.Public | BindingFlags.Instance);
                        if (null != prop && prop.CanWrite && headerList.Contains(headerValues.ElementAt(columnCount).Value))
                        {
                            errorMessage = component.setValueByName(headerValues.ElementAt(columnCount).Value, cell.Value.ToString());
                            if (!string.IsNullOrEmpty(errorMessage))
                            {
                                MessageBox.Show(errorMessage);
                                return new List<TaggedComponent>();
                            }
                        }
                        columnCount++;
                    }
                    if (!forceReturn)
                    {
                        if (String.IsNullOrEmpty(component.Id))
                        {
                            component.Id = Guid.NewGuid().ToString();
                        }
                        if (parentComponentId != String.Empty)
                        {
                            //this is a child... add it to the parent if found..
                            TaggedComponent parentComponent = tcList.Where(c => c.Id == parentComponentId).FirstOrDefault();
                            if (parentComponent != null)
                            {
                                parentComponent.Children.Add(new ChildComponent(component));
                            }
                        }
                        else
                        {
                            //normal parent component
                            tcList.Add(component);
                        }
                    }
                }
            }

            return tcList;
             
        }

        public static string GetProjectId(string projectFile)
        {
            string projectId = "";
            try
            {
                using (ZipFile zip = new ZipFile(projectFile))
                {
                    ZipEntry projectIdEntry = zip.Entries.Where(e => e.FileName == Path.GetFileName("project.id")).FirstOrDefault();
                    if (projectIdEntry != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            projectIdEntry.Extract(ms);
                            using (StreamReader sr = new StreamReader(ms))
                            {
                                projectId = sr.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error extracting projectId from project file.");
            }
            return projectId;
        }

        public static bool setProjectId(string projectId, string projectFilePath)
        {
            bool success = true;

            using (ZipFile zip = new ZipFile(projectFilePath))
            {
                //set project id file inside zip
                try
                {
                    ZipEntry pid = zip.Entries.Where(c => c.FileName == "project.id").FirstOrDefault();
                    if (pid != null) zip.RemoveEntry("project.id");
                    zip.AddEntry("project.id", Encoding.ASCII.GetBytes(projectId));
                    zip.Save();
                }
                catch (Exception ex)
                {
                    success = false;
                }
            }

            return success;
        }

        public static bool VerifyWorkingFolderStatus(string workingFolder, string projectId)
        {
            //look for existing working project..
            string idFile = workingFolder + "\\project.id";
            if (File.Exists(idFile))
            {
                string workingProjectId = System.IO.File.ReadAllText(idFile);
                if (workingProjectId != projectId)
                {
                    //the user is trying to open a project that is a different ID than what 
                    //is in the working directory currently.
                    DialogResult dr = MessageBox.Show("There are working files remaining from a different project. Loading this project will remove all data from the previous project. \r\n\r\nAre you sure you would like to erase working data?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (dr == System.Windows.Forms.DialogResult.No)
                    {
                        return false;
                    }
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        //delete it to make sure we are clean...
                        if (System.IO.Directory.Exists(workingFolder))
                        {
                            System.IO.Directory.Delete(workingFolder, true);
                        }
                    }
                }
            }
            return true;
        }

        public static bool writeCSVableToFile(object[] csvAbleList,  string fileName)
        {

            try
            {
                System.IO.StreamWriter sr = new StreamWriter(fileName);
                CSVable currentCSVable = new CSVable();

                sr.Write(((CSVable)csvAbleList.FirstOrDefault()).GetHeader() + Environment.NewLine);

                foreach (object item in csvAbleList)
                {
                    sr.Write(((CSVable)item).ToString() + Environment.NewLine);
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting file " + fileName + " - " + ex.Message);
                return false;
            }

            return true;
 
        }


        public static TaggedComponent getRandomComponent()
        {
            
            TaggedComponent tc = new TaggedComponent();

            int AccessInt = r.Next(1, 3);
            switch (AccessInt)
            {
                case 1:
                    tc.Access = "NTM";
                    break;
                case 2:
                    tc.Access = "DTM - SCAFFOLD";
                    break;
                case 3:
                    tc.Access = "UTM";
                    break;
            }

            int AreaInt = r.Next(1, 5);
            tc.Area = Enum.GetName(typeof(GibberishWords), AreaInt);

            tc.AttachDrawing = false;
            tc.Batch = "Batch1";

            int StateInt = r.Next(1, 3);
            switch (StateInt)
            {
                case 1:
                    tc.ChemicalState = "GV";
                    break;
                case 2:
                    tc.ChemicalState = "LL";
                    break;
                case 3:
                    tc.ChemicalState = "HL";
                    break;
            }

            int ClassInt = r.Next(1, 3);
            switch (ClassInt)
            {
                case 1:
                    tc.ComponentType = "VALVE - GATE";
                    break;
                case 2:
                    tc.ComponentType = "PUMP";
                    break;
                case 3:
                    tc.ComponentType = "CONECT - FLANGE";
                    break;
            }

            tc.Drawing = "DRAWING1";
            tc.Id = Guid.NewGuid().ToString();
            tc.Inspected = true;
            tc.Extension = "000";
            tc.RouteSequence = 0;
            tc.InspectionBackground = r.Next(0, 10);
            tc.InspectionDate = DateTime.Now;
            tc.InspectionInspector = "Average Joe";
            tc.InspectionInstrument = "Analyzer123";
            tc.InspectionReading = r.Next(11, 580);
            tc.isDrawingTag = false;
            tc.LDARTag = r.Next(10000, 99999).ToString();

            string location = "G/" + r.Next(0, 14).ToString();
            int locElementCount = r.Next(1, 6);
            for (int x = 1; x <= locElementCount; x++)
            {
                int LocInt = r.Next(1, 14);
                location = location + " " + Enum.GetName(typeof(GibberishWords), LocInt);
            }

            tc.Location = location;

            tc.Manufacturer = "MANUFACTURER1";

            tc.MOCNumber = "MOC1";

            tc.ModifiedBy = "TECH1";
            tc.ModifiedDate = DateTime.Now.ToString();
            tc.PreviousTag = r.Next(10000, 99999).ToString();
            tc.Property = "Property";
            tc.Size = r.Next(0, 10);


            int StreamInt = r.Next(1, 3);
            switch (StreamInt)
            {
                case 1:
                    tc.Stream = "GASOLINE";
                    break;
                case 2:
                    tc.Stream = "BENZENE";
                    break;
                case 3:
                    tc.Stream = "METHANOL";
                    break;
            }

            int UnitInt = r.Next(1, 3);
            switch (UnitInt)
            {
                case 1:
                    tc.Unit = "CATCRACKER";
                    break;
                case 2:
                    tc.Unit = "COKER";
                    break;
                case 3:
                    tc.Unit = "HYDROTREATER";
                    break;
            }

            int EquipInt = r.Next(1, 3);
            switch (EquipInt)
            {
                case 1:
                    tc.Equipment = "CATCRACKER";
                    break;
                case 2:
                    tc.Equipment = "COKER";
                    break;
                case 3:
                    tc.Equipment = "HYDROTREATER";
                    break;
            }

            int childElementCount = r.Next(1, 3);
            for (int x = 1; x < childElementCount; x++)
            {
                ChildComponent cc = new ChildComponent();
                cc.ComponentType = "CONECT - FLANGE";
                cc.LDARTag = tc.LDARTag + "." + x.ToString();
                cc.Location = tc.Location + " FLG";
                cc.PreviousTag = tc.PreviousTag + "." + x.ToString();
                tc.Children.Add(cc);
            }

            return tc;
        }
    }
}
