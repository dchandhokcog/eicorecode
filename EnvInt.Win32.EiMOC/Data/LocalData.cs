using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.EiMOC.Data
{
    public static class LocalData
    {
        public static ProjectData ProjectData;
        public static string DeviceIdentifier;

        private static string componentFileExtension = ".fttd";
        private static string componentFileBackupExtension = ".fttb";

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
                children += component.Children.Count();
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

        public static TaggedComponent GetComponent(string id)
        {
            return _components.Where(c => c.Id.ToLower() == id.ToLower()).FirstOrDefault();
        }

        public static List<TaggedComponent> GetComponents()
        {
            return _components;
        }

        public static bool doesTagExist(string componentTag)
        {
            bool dte = false;

            if (_components.Where(c => c.LDARTag.ToLower() == componentTag.ToLower()).Count() > 0)
            {
                dte = true;
            }

            return dte;
        }

        public static bool doesPreviousTagExist(string componentTag)
        {
            bool dte = false;

            if (_components.Where(c => c.PreviousTag.ToLower() == componentTag.ToLower()).Count() > 0)
            {
                dte = true;
            }

            return dte;
        }

        public static bool SaveComponents()
        {
            using (MemoryStream stream1 = new MemoryStream())
            {
                string componentDataFile = Globals.WorkingFolder + "\\tags.json";
                if (ProjectData == null)
                {
                    //this wont work..
                    Globals.LogError("Project File is unknown or has not been set.", "FieldTechToolbox.LocalData.SaveComponents", "");
                }
                else
                {
                    try
                    {
                        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<TaggedComponent>));
                        ser.WriteObject(stream1, _components);
                        stream1.Position = 0;
                        StreamReader sr = new StreamReader(stream1);
                        string json = sr.ReadToEnd();

                        File.WriteAllText(componentDataFile, json);
                    }
                    catch (Exception ex)
                    {
                        Globals.LogError("There was an error saving component information locally. " + componentDataFile, "FieldTechToolbox.LocalData.SaveComponents", ex.StackTrace);
                    }
                }
            }
            return true;
        }

        public static bool BackupAndClear()
        {
            using (MemoryStream stream1 = new MemoryStream())
            {
                string componentDataFile = Globals.WorkingFolder + "\\tags.json";
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
                        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<TaggedComponent>));
                        ser.WriteObject(stream1, _components);
                        stream1.Position = 0;
                        StreamReader sr = new StreamReader(stream1);
                        string json = sr.ReadToEnd();
                        if (!Directory.Exists(Globals.BackupFolder))
                        {
                            Directory.CreateDirectory(Globals.BackupFolder);
                        }
                        string componentData = Globals.BackupFolder + @"\" + Path.GetFileNameWithoutExtension(Globals.CurrentProjectData.ProjectName) + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + "_" + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + componentFileBackupExtension;
                        File.WriteAllText(componentData, json);
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
                string componentDataFile = Globals.WorkingFolder + "\\tags.json";
                if (File.Exists(componentDataFile))
                {
                    using (FileStream stream1 = new FileStream(componentDataFile, FileMode.Open))
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
    }
}
