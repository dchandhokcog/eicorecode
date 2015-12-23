using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing;
using System.Data;

using EnvInt.Win32.FieldTech.Common;
using EnvInt.Win32.FieldTech.Containers;
using EnvInt.Win32.FieldTech.HelperClasses;
using EnvInt.Win32.FieldTech.Manager.Dialogs;
using EnvInt.Win32.FieldTech.Manager.HelperClasses;
using Ionic.Zip;

namespace EnvInt.Win32.FieldTech.Manager
{
    public static class Globals
    {
        public static string WorkingFolder
        {
            get
            {
                string wFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return wFolder + "\\FieldTechManager\\Working";
            }
        }

        public static string ReportTempFolder
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
            }
        }

        public static string ExecutableFolder
        {
            get
            {
                return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).ToString().Replace("file:\\", "");
            }
        }

        public static void ResetWorkingFolder()
        {
            try
            {
                System.IO.Directory.Delete(WorkingFolder, true);
                System.IO.Directory.CreateDirectory(WorkingFolder);
                System.IO.Directory.CreateDirectory(WorkingFolder + "\\MarkedDrawings");
            }
            catch { }
        }

        public static string UserFolder
        {
            get
            {
                string bFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return bFolder + "\\FieldTechManager";
            }
        }

        public static string ErrorLog_FTM
        {
            get
            {
                string bFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return bFolder + "\\FieldTechManager\\errorLog.txt";
            }
        }

        public static string ErrorLog_FTT
        {
            get
            {
                string bFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return bFolder + "\\FieldTechManager\\Working\\errorLog.txt";
            }
        }

        public static bool WriteProjectToWorkingFolder()
        {
            string projectDataSer = FileUtilities.SerializeObject(MainForm.CurrentProjectData);
            string projectTagSer = FileUtilities.SerializeObject(MainForm.CurrentProjectTags);

            try
            {
                File.WriteAllText(WorkingFolder + "\\project.json", projectDataSer);
                File.WriteAllText(WorkingFolder + "\\tags.json", projectTagSer);
            }
            catch
            {
                return false;
            }

            return true;
        }

        //public static bool SaveProjectFile(string projectFile, List<string> drawingPackageFilter, List<string> ldarComponentsProcessUnitFilter, bool ignoreMarkedDrawings = false, List<TaggedComponent> selectedTags = null)
        //{

        //    //this function is also still in the main form until i can validate that the "globalized" version works properly.  it is being referenced in the ComponentViewer.
            
        //    Cursor.Current = Cursors.WaitCursor;
            
        //    try
        //    {
        //        //package up our project file here...
        //        using (ZipFile zip = new ZipFile())
        //        {
        //            string projectDataSer = FileUtilities.SerializeObject(MainForm.CurrentProjectData);
        //            string projectTagSer = FileUtilities.SerializeObject(MainForm.CurrentProjectTags);

        //            //--------------------------------------------------------
        //            //TODO: This is currently a hack because we are still using JSON. Once we switch over to CSV files for each LDAR table
        //            //then this will become a bit cleaner since the LDAR data will be saved in seperate files, it will be one step easier
        //            //I need to deserialize this and then reserialize it because that is the only way to make a deep copy where I can delete filtered
        //            //components, otherwise, I would delete them in the main ProjectData which would be very bad.

        //            ProjectData localProjectData = FileUtilities.DeserializeObject<ProjectData>(projectDataSer);

        //            //--------------------------------------------------------
        //            if (ldarComponentsProcessUnitFilter != null && selectedTags == null)
        //            {
        //                List<int> validProcessUnits = localProjectData.LDARData.ProcessUnits.Where(u => ldarComponentsProcessUnitFilter.Contains(u.UnitDescription)).Select(p => p.ProcessUnitId).ToList();

        //                //if we index and go backwards, we can delete by index and not ruin things, plus this is super fast
        //                for (int i = localProjectData.LDARData.ExistingComponents.Count - 1; i >= 0; i--)
        //                {
        //                    if (!validProcessUnits.Contains(localProjectData.LDARData.ExistingComponents[i].ProcessUnitId))
        //                    {
        //                        localProjectData.LDARData.ExistingComponents.RemoveAt(i);
        //                    }
        //                }
        //            }

        //            if (drawingPackageFilter != null)
        //            {
        //                for (int i = localProjectData.CADPackages.Count - 1; i >= 0; i--)
        //                {
        //                    if (!drawingPackageFilter.Contains(Path.GetFileName(localProjectData.CADPackages[i].FileName)))
        //                    {
        //                        localProjectData.CADPackages.RemoveAt(i);
        //                    }
        //                }
        //            }
        //            //now add to the file, the one we filtered
        //            projectDataSer = FileUtilities.SerializeObject(localProjectData);

        //            //--------------------------------------------------------

        //            ZipEntry e1 = zip.AddEntry("project.json", projectDataSer, Encoding.UTF8);
        //            ZipEntry e2 = zip.AddEntry("tags.json", projectTagSer, Encoding.UTF8);
        //            ZipEntry e3 = zip.AddEntry("version.txt", "Version=" + Application.ProductVersion.ToString(), Encoding.UTF8);
        //            ZipEntry e4 = zip.AddEntry("project.id", MainForm.CurrentProjectData.ProjectId.ToString(), Encoding.UTF8);
        //            if (selectedTags != null)
        //            {
        //                ZipEntry e5 = zip.AddEntry("tags.csv", FileUtilities.GetTaggedComponentsAsCSV(selectedTags), Encoding.UTF8);
        //            }
        //            if (Directory.Exists(Globals.WorkingFolder + "\\MarkedDrawings") && !ignoreMarkedDrawings)
        //            {
        //                foreach (CADPackage markedDrawing in MainForm.CurrentProjectData.MarkedDrawings)
        //                {
        //                    if (File.Exists(Globals.WorkingFolder + "\\MarkedDrawings\\" + System.IO.Path.GetFileName(markedDrawing.FileName)))
        //                    {
        //                        //grab file from working directory, not original
        //                        zip.AddFile(Globals.WorkingFolder + "\\MarkedDrawings\\" + System.IO.Path.GetFileName(markedDrawing.FileName), "MarkedDrawings");
        //                    }
        //                }
        //            }

        //            foreach (CADPackage package in MainForm.CurrentProjectData.CADPackages)
        //            {
        //                if (File.Exists(Globals.WorkingFolder + "\\" + System.IO.Path.GetFileName(package.FileName)))
        //                {
        //                    if (drawingPackageFilter == null ||
        //                        (drawingPackageFilter != null && drawingPackageFilter.Contains(Path.GetFileName(package.FileName))))
        //                    {
        //                        //grab file from working directory, not original
        //                        zip.AddFile(Globals.WorkingFolder + "\\" + System.IO.Path.GetFileName(package.FileName), "");
        //                    }
        //                }
        //            }

        //            zip.Save(projectFile);
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("There was an error saving project. ProjectFile: " + projectFile + " - " + ex.Message);
        //    }

        //    Cursor.Current = Cursors.Arrow;
        //    return false;
        //}

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;
            form.TopMost = true;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        public static string getTagWithoutPoint(string tag)
        {
            if (tag.Contains("."))
            {
                return tag.Split('.')[0];
            }
            else
            {
                return tag;
            }
        }

        public static string getTagPoint(string tag, int paddedZeros = 0)
        {

            string tagPoint = string.Empty;
            
            if (tag.Contains("."))
            {
                tagPoint = tag.Split('.')[1];
            }

            while (tagPoint.Length < paddedZeros)
            {
                tagPoint = "0" + tagPoint;
            }

            return tagPoint;
        }

        public static DataTable GetObjectListAsTable<T>(List<T> stuffList)
        {
            DataTable dt = new DataTable();

            List<PropertyInfo> propList = stuffList[0].GetType().GetProperties().ToList();

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


            foreach (T o in stuffList)
            {
                DataRow dr = dt.NewRow();
                foreach (PropertyInfo pi in o.GetType().GetProperties().ToList())
                {
                    if (o.GetType().GetProperty(pi.Name).GetValue(o, null) == null)
                    {
                        dr[pi.Name] = DBNull.Value;
                    }
                    else
                    {
                        dr[pi.Name] = o.GetType().GetProperty(pi.Name).GetValue(o, null);
                    }
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }


    }
}
