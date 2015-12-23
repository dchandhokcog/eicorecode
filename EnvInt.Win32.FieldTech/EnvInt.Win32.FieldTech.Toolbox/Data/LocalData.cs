using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;
using System.Data;
using System.Reflection;

using EnvInt.Win32.FieldTech.Common;
using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Data
{
    public static class LocalData
    {

        public static string ProjectFile;
        public static string ProjectPath;
        public static string ProjectName;

        public static double LastRouteNo;
        public static double RouteAddNo = .01;

        public static ProjectData ProjectData;
        public static string DeviceIdentifier;

        private static string componentFileExtension = ".eid";
        private static string componentFileBackupExtension = ".eib";

        private static List<TaggedComponent> _components = new List<TaggedComponent>();
        
        public static bool AddComponent(TaggedComponent component)
        {
            TaggedComponent ec = _components.Where(c => c.Id.ToLower() == component.Id.ToLower()).FirstOrDefault();
            if (ec != null)
            {
                _components.Remove(ec);
            }
            _components.Add(component);
            return SaveComponents();
        }

        public static int GetChildComponentCount()
        {
            int children = 0;
            foreach (TaggedComponent component in _components)
            {
                if (component.Children != null)
                {
                    children += component.Children.Count();
                }
            }
            return children;
        }

        public static int GetParentComponentCount()
        {
            return _components.Count;
        }

        public static TaggedComponent GetComponent(string engineeringTag, string componentType)
        {
            return _components.Where(c => c.EngineeringTag.ToLower() == engineeringTag.ToLower() && c.ComponentType.ToLower() == componentType.ToLower()).FirstOrDefault();
        }

        public static DataTable GetComponentsAsTable(string tagFilter, string locationFilter, string equipmentFilter)
        {
            DataTable dt = new DataTable();

            TaggedComponent tempTag = new TaggedComponent();

            List<PropertyInfo> propList = tempTag.GetType().GetProperties().ToList();

            foreach (PropertyInfo propinfo in propList)
            {
                if (propinfo.PropertyType.ToString().Contains("Nullable"))
                {
                    if (propinfo.PropertyType.ToString().Contains("DateTime")) dt.Columns.Add(propinfo.Name, typeof(System.Data.SqlTypes.SqlDateTime));
                    if (propinfo.PropertyType.ToString().Contains("String")) dt.Columns.Add(propinfo.Name, typeof(System.Data.SqlTypes.SqlString));
                    if (propinfo.PropertyType.ToString().Contains("Int")) dt.Columns.Add(propinfo.Name, typeof(System.Data.SqlTypes.SqlInt32));
                    if (propinfo.PropertyType.ToString().Contains("Double")) dt.Columns.Add(propinfo.Name, typeof(System.Data.SqlTypes.SqlDouble));
                    if (propinfo.PropertyType.ToString().Contains("Boolean")) dt.Columns.Add(propinfo.Name, typeof(System.Data.SqlTypes.SqlBoolean));
                }
                else
                {
                    dt.Columns.Add(propinfo.Name, propinfo.PropertyType);
                }
            }

            List<TaggedComponent> filteredList;

            string filterElements = string.Empty;

            if (!string.IsNullOrEmpty(tagFilter)) filterElements += "Tag";
            if (!string.IsNullOrEmpty(locationFilter)) filterElements += "Location";
            if (!string.IsNullOrEmpty(equipmentFilter)) filterElements += "Equipment";

            switch (filterElements)
            {
                case "":
                    filteredList = LocalData.getAllAsTaggedComponent();
                    break;
                case "Tag":
                    filteredList = LocalData.getAllAsTaggedComponent().Where(c => c.LDARTag.Contains(tagFilter) || c.PreviousTag.Contains(tagFilter)).ToList();
                    break;
                case "Location":
                    filteredList = LocalData.getAllAsTaggedComponent().Where(c => c.Location.ToUpper().Contains(locationFilter.ToUpper())).ToList();
                    break;
                case "Equipment":
                    filteredList = LocalData.getAllAsTaggedComponent().Where(c => c.Equipment.ToUpper().Contains(equipmentFilter.ToUpper())).ToList();
                    break;
                case "TagLocation":
                    filteredList = LocalData.getAllAsTaggedComponent().Where(c => c.Location.ToUpper().Contains(locationFilter.ToUpper()) && c.LDARTag.Contains(tagFilter.ToUpper())).ToList();
                    break;
                case "TagEquipment":
                    filteredList = LocalData.getAllAsTaggedComponent().Where(c => c.Equipment.ToUpper().Contains(equipmentFilter.ToUpper()) && c.LDARTag.Contains(tagFilter.ToUpper())).ToList();
                    break;
                case "LocationEquipment":
                    filteredList = LocalData.getAllAsTaggedComponent().Where(c => c.Location.ToUpper().Contains(locationFilter.ToUpper()) && c.Equipment.Contains(equipmentFilter.ToUpper())).ToList();
                    break;
                case "TagLocationEquipment":
                    filteredList = LocalData.getAllAsTaggedComponent().Where(c => c.Location.ToUpper().Contains(locationFilter.ToUpper()) && c.LDARTag.Contains(tagFilter.ToUpper()) && c.Equipment.Contains(equipmentFilter.ToUpper())).ToList();
                    break;
                default:
                    filteredList = LocalData.getAllAsTaggedComponent();
                    break;
            }

            foreach (TaggedComponent tc in filteredList)
            {
                DataRow dr = dt.NewRow();                
                foreach (PropertyInfo pi in tc.GetType().GetProperties())
                {
                    if (tc.getPropertyValueByName(pi.Name) == null)
                    {
                        dr[pi.Name] = DBNull.Value;
                    }
                    else
                    {
                        dr[pi.Name] = tc.getPropertyValueByName(pi.Name);
                    }
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public static TaggedComponent GetComponent(string id)
        {

            //string[] subStr = id.Split(':');
            //string newStr = string.Empty;
            //if (subStr.Count() > 2)
            //{
            //    newStr = (subStr[2].Replace(":", "").Trim() + "::" + subStr[4].Replace(":", "").Trim()).ToLower();
            //}

            try
            {
                if (id.Contains('|'))
                {
                    //this is a child component, identified by | and then LDARTag.  split this out and return it's parent
                    id = id.Split('|')[0];
                }
            }
            catch
            { }

            if (id != null)
            {

                TaggedComponent returnComp = _components.Where(c => c.Id.ToLower() == id.ToLower()).FirstOrDefault();

                return returnComp;
            }
            else
            {
                return null;
            }
        }

        public static List<TaggedComponent> GetComponents()
        {
            return _components;
        }

        public static bool RemoveComponent(TaggedComponent c)
        {
            return _components.Remove(c);
        }

        public static bool doesTagExist(string componentTag, string componentID)
        {
            bool dte = false;

            if (_components.Where(c => c.LDARTag.ToLower() == componentTag.ToLower() && c.Id != componentID).Count() > 0)
            {
                dte = true;
            }

            return dte;
        }

        public static bool doesPreviousTagExist(string componentTag, string componentID)
        {
            bool dte = false;

            if (_components.Where(c => c.PreviousTag.ToLower() == componentTag.ToLower() && c.Id != componentID).Count() > 0)
            {
                dte = true;
            }

            return dte;
        }

        public static double getNextRouteNumber()
        {
            try
            {
                if (LastRouteNo == 0.0)
                {
                    double nextRoute = _components.Max(c => c.RouteSequence) + RouteAddNo;
                    return nextRoute;
                }
                else
                {
                    return LastRouteNo + RouteAddNo;
                }
            }
            catch
            {
                return 0;
            }
        }

        public static double getNextRouteNumber(string RouteAfterTag, string UnitDescription)
        {

            double returnValue = 0.0;

            List<LDARComponent> ldc = new List<LDARComponent>();
            try
            {
                if (ProjectData.LDARData.ExistingComponents != null)
                {
                    LDARProcessUnit puFilter = ProjectData.LDARData.ProcessUnits.Where(c => c.UnitDescription == UnitDescription).FirstOrDefault();
                    if (puFilter == null)
                    {
                        if (ProjectData.LDARDatabaseType.Contains("LeakDAS")) ldc = ProjectData.LDARData.ExistingComponents.Where(c => c.ComponentTag.StartsWith(RouteAfterTag)).ToList();
                        if (ProjectData.LDARDatabaseType.Contains("Guideware")) ldc = ProjectData.LDARData.ExistingComponents.Where(c => c.ComponentTag == RouteAfterTag).ToList();
                        if (ldc.Count > 0)
                        {
                            returnValue = ldc.Max(c => c.RouteSequence) + RouteAddNo;
                        }
                    }
                    else
                    {
                        if (ProjectData.LDARDatabaseType.Contains("LeakDAS")) ldc = ProjectData.LDARData.ExistingComponents.Where(c => c.ComponentTag.StartsWith(RouteAfterTag) && c.ProcessUnitId == puFilter.ProcessUnitId).ToList();
                        if (ProjectData.LDARDatabaseType.Contains("Guideware")) ldc = ProjectData.LDARData.ExistingComponents.Where(c => c.ComponentTag == RouteAfterTag && c.Location1 == puFilter.UnitCode).ToList();
                        if (ldc.Count > 0)
                        {
                            returnValue = ldc.Max(c => c.RouteSequence) + RouteAddNo;
                        }
                    }
                }
            }
            catch
            { }

            return returnValue;
        }

        public static bool doesLocationDescriptionExist(string locationDescription, string componentID)
        {
            bool dte = false;


            foreach (TaggedComponent tc in _components)
            {
                if (tc.Location == locationDescription && componentID != tc.Id) return true;                
                foreach (ChildComponent cc in tc.Children)
                {
                    if (cc.Location == locationDescription && componentID != tc.Id) return true;
                }
            }

            //if (_components.Where(c => c.Location.ToUpper() == locationDescription.ToUpper() && c.Id != componentID).Count() > 0)
            //{
            //    dte = true;
            //}

            //if (_components.Where(c => c.Children.Where(child => child.Location.ToUpper() == locationDescription.ToUpper()).Count() > 0).Count() > 0)
            //{
            //    dte = true;
            //}

            return dte;
        }

        public static bool SaveComponents()
        {
            using (MemoryStream stream1 = new MemoryStream())
            {
                string componentDataFile = Globals.WorkingFolder + "\\tags.csv";
                if (ProjectData == null)
                {
                    //this wont work..
                    Globals.LogError("Project File is unknown or has not been set.", "FieldTechToolbox.LocalData.SaveComponents", "");
                }
                else
                {
                    try
                    {
                        string csvComponents = FileUtilities.GetTaggedComponentsAsCSV(_components);
                        File.WriteAllText(componentDataFile, csvComponents, Encoding.UTF8); 
                        BackupCSV();
                    }
                    catch (Exception ex)
                    {
                        Globals.LogError("There was an error saving component information locally. " + componentDataFile, "FieldTechToolbox.LocalData.SaveComponents", ex.StackTrace);
                    }
                }
            }
            return true;
        }

        public static bool BackupCSV()
        {

            int fileCount = 0;

            try
            {
                fileCount = System.IO.Directory.GetFiles(Globals.BackupFolder).Where(c => c.EndsWith("csv")).Count();

                if (fileCount > 50)
                {
                    DirectoryInfo di = new DirectoryInfo(Globals.BackupFolder);
                    FileInfo[] rgFiles = di.GetFiles("*.csv");
                    FileInfo firstfile = rgFiles[0];
                    System.IO.File.Delete(firstfile.FullName);
                }
            }
            catch { }
            
            try
            {
                if (!System.IO.Directory.Exists(Globals.BackupFolder)) System.IO.Directory.CreateDirectory(Globals.BackupFolder);
                File.Copy(Globals.WorkingFolder + "\\tags.csv", Globals.BackupFolder + "\\tags_" + DateTime.Now.ToString("ddMMyyy_Hmmss") + ".csv");
            }
            catch (Exception ex)
            {
                Globals.LogError("CSV Backup failed.", "FieldTechToolbox.LocalData.BackupCSV", "");
                return false;
            }

            return true;
        }



        public static bool BackupAndClear()
        {
            using (MemoryStream stream1 = new MemoryStream())
            {
                string componentDataFile = Globals.WorkingFolder + "\\tags.csv";
                if (ProjectData == null)
                {
                    //this wont work..
                    Globals.LogError("Project File is unknown or has not been set.", "FieldTechToolbox.LocalData.SaveComponents", "");
                }
                else
                {
                    try
                    {
                        //Save Backup
                        string csvComponents = FileUtilities.GetTaggedComponentsAsCSV(_components);
                        if (!System.IO.Directory.Exists(Globals.BackupFolder)) System.IO.Directory.CreateDirectory(Globals.BackupFolder);
                        string componentData = Globals.BackupFolder + @"\" + Path.GetFileNameWithoutExtension(Globals.CurrentProjectData.ProjectName) + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + "_" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + componentFileBackupExtension;
                        File.WriteAllText(componentData, csvComponents, Encoding.UTF8);
                        //Clear Data
                        _components.Clear();
                        File.WriteAllText(componentDataFile, "");
                        SaveComponents();
                    }
                    catch (Exception ex)
                    {
                        Globals.LogError("There was an error saving component information locally. " + componentDataFile, "FieldTechToolbox.LocalData.SaveComponents", ex.StackTrace);
                    }
                }
            }
            return true;
        }


        public static void LoadFromProject(ProjectData projectData)
        {

            ProjectData = projectData;
            DeviceIdentifier = Properties.Settings.Default.DeviceIdentifier;
            _components.Clear();

            if (ProjectData == null)
            {
                //this wont work..
                Globals.LogError("Project data is unknown or has not been set.", "FieldTechToolbox.LocalData.Initialize", "");
            }
            else
            {
                string componentDataFile = Globals.WorkingFolder + "\\tags.csv";
                if (File.Exists(componentDataFile))
                {
                    string csvData = File.ReadAllText(componentDataFile);
                    _components.AddRange(FileUtilities.GetTaggedComponentsFromCSV(csvData, false, false, false));
                }
            }
        }

        public static void Initialize(string dwfFile)
        {
            ProjectFile = dwfFile;
            ProjectName = System.IO.Path.GetFileNameWithoutExtension(dwfFile);
            ProjectPath = System.IO.Path.GetDirectoryName(dwfFile);

            if (ProjectData == null)
            {
                ProjectData = new ProjectData();
            }

            RouteAddNo = 1 / (Globals.CurrentProjectData.LDARRoutePaddedZeros ^ 10);

            _components.Clear();

            if (ProjectFile == null)
            {
                //this wont work..
                Globals.LogError("Project File is unknown or has not been set.", "FieldTechToolbox.LocalData.Initialize", "");
            }
            else
            {
                string componentData = Path.GetDirectoryName(ProjectFile) + @"\" + Path.GetFileNameWithoutExtension(ProjectFile) + componentFileExtension;
                if (File.Exists(componentData) && System.IO.Path.GetExtension(ProjectFile).ToLower() == ".eid")
                {
                    using (FileStream stream1 = new FileStream(componentData, FileMode.Open))
                    {
                        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<TaggedComponent>));
                        stream1.Position = 0;
                        try
                        {
                            _components = (List<TaggedComponent>)ser.ReadObject(stream1);
                        }
                        catch { }
                    }
                }
            }
        }

        public static List<TaggedComponent> getAllAsTaggedComponent()
        {
            List<TaggedComponent> allComps = new List<TaggedComponent>();

            foreach (TaggedComponent parent in _components)
            {
                allComps.Add(parent);
                if (parent.Children.Count > 0)
                {
                    allComps.AddRange(parent.GetChildrenAsTaggedComponent());
                }
            }

            return allComps;
        }
    }
}
