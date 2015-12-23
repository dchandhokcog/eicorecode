using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using EnvInt.Win32.FieldTech.Containers;
using EnvInt.Win32.FieldTech.Common;
using EnvInt.Win32.FieldTech.Manager.HelperClasses;
using EnvInt.Win32.FieldTech.Manager.Dialogs;

namespace EnvInt.Win32.FieldTech.Manager.Controls
{
    public partial class ProjectTagsControl : UserControl
    {
        private MainForm _mainForm = null;
        private List<FileSystemWatcher> _watchers = new List<FileSystemWatcher>();

        public ProjectTagsControl()
        {
            InitializeComponent();
        }

        public bool LoadProjectTags(MainForm mainform, List<ProjectTags> projectTags)
        {
            _mainForm = mainform;

            objectListViewPendingTagData.SetObjects(projectTags.Where(t=>t.Exported == false));
            radGridViewCollectedTags.DataSource = MainForm.CollectedTags;
            radGridViewCollectedTags.BestFitColumns(Telerik.WinControls.UI.BestFitColumnMode.DisplayedCells);

            return true;
        }

        public bool ReloadUpdatedProjectTags()
        {
            var projectTagsToReload = MainForm.CurrentProjectTags.Where(p => p.NeedsReloadedFromWorkingFile.HasValue);
            foreach (ProjectTags pt in projectTagsToReload)
            {
                if (File.Exists(pt.WorkingFileName))
                {
                    _watchers.RemoveAll(w => w.Filter == pt.WorkingFileName);
                    string csvData = File.ReadAllText(pt.WorkingFileName);
                    pt.Tags = FileUtilities.GetTaggedComponentsFromCSV(csvData, true, true, false);
                    pt.NeedsReloadedFromWorkingFile = null;
                    pt.WorkingFileName = String.Empty;
                    
                }
            }
            objectListViewPendingTagData.SetObjects(MainForm.CurrentProjectTags.Where(t => t.Exported == false));
            return true;
        }

        public void refreshStaging()
        {
            radGridViewCollectedTags.DataSource = null;
            radGridViewCollectedTags.DataSource = MainForm.CollectedTags;
            radGridViewCollectedTags.MasterTemplate.Refresh(null);
            radGridViewCollectedTags.Refresh();
            radGridViewCollectedTags.BestFitColumns(Telerik.WinControls.UI.BestFitColumnMode.DisplayedCells);
        }

        private void objectListViewPendingTagData_ItemActivate(object sender, EventArgs e)
        {

        }

         private void ExportExcel()
        {

            if (radGridViewCollectedTags.SelectedRows.Count() == 0)
            {
                MessageBox.Show("No components selected!");
                return;
            }

            List<TaggedComponent> exportList = new List<TaggedComponent>();

            foreach (Telerik.WinControls.UI.GridViewRowInfo tc in radGridViewCollectedTags.SelectedRows)
            {
                exportList.Add((TaggedComponent)tc.DataBoundItem);
            }

            string csvFileName = "Saved_Components";

            SaveFileDialog sd = new SaveFileDialog();
            sd.AddExtension = true;
            sd.Filter = "Excel Files (*.xlsx|*.xlsx|Comma Separated Value Files (*.csv|*.csv|All files (*.*)|*.*";
            sd.FileName = csvFileName;
            DialogResult dr = sd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                if (Path.GetExtension(sd.FileName) == ".csv")
                {
                    string csvData = FileUtilities.GetTaggedComponentsAsCSV(exportList);
                    try
                    {
                        File.WriteAllText(sd.FileName, csvData, Encoding.UTF8);
                        System.Diagnostics.Process.Start(sd.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("File is already in use or open in another application. Close the file and then try again.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    string errorMessage = FileUtilities.SendTaggedComponentsToExcel(exportList, sd.FileName);
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        MessageBox.Show(errorMessage);
                    }
                    else
                    {
                        try
                        {
                            System.Diagnostics.Process.Start(sd.FileName);
                        }
                        catch { }
                    }
                }
            }
        }

        private void toolStripButtonEditTags_Click(object sender, EventArgs e)
        {

            if (radPageViewTags.SelectedPage.Text == "Incoming Sets")
            {
                if (objectListViewPendingTagData.SelectedItems.Count == 1)
                {
                    ProjectTags pts = objectListViewPendingTagData.SelectedItem.RowObject as ProjectTags;
                    if (pts != null)
                    {
                        List<TaggedComponent> exportList = pts.Tags;

                        string csvFileName = Path.GetFileNameWithoutExtension(pts.Device.Replace(":", "-").Replace("//", "-"));

                        SaveFileDialog sd = new SaveFileDialog();
                        sd.AddExtension = true;
                        sd.Filter = "Excel Files (*.xlsx)|*.xlsx|Comma Separated Value Files (*.csv)|*.csv|All files (*.*)|*.*";
                        sd.FileName = csvFileName;
                        DialogResult dr = sd.ShowDialog();

                        if (dr == DialogResult.OK)
                        {
                            if (Path.GetExtension(sd.FileName) == ".csv")
                            {
                                string csvData = FileUtilities.GetTaggedComponentsAsCSV(exportList.Where(c => !c.isChild).ToList());
                                try
                                {
                                    File.WriteAllText(sd.FileName, csvData, Encoding.UTF8);
                                    System.Diagnostics.Process.Start(sd.FileName);
                                }
                                catch
                                {
                                    MessageBox.Show("File is already in use or open in another application. Close the file and then try again.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                string errorMessage = FileUtilities.SendTaggedComponentsToExcel(exportList.Where(c => !c.isChild).ToList(), sd.FileName);
                                if (!string.IsNullOrEmpty(errorMessage))
                                {
                                    MessageBox.Show(errorMessage);
                                }
                                else
                                {
                                    try
                                    {
                                        System.Diagnostics.Process.Start(sd.FileName);
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select an export set");
                }
            }
            else
            {
                ExportExcel();
            }
        }

        private static void OnFileChanged(object source, FileSystemEventArgs e)
        {
            ProjectTags pts = MainForm.CurrentProjectTags.Where(p => p.WorkingFileName == e.FullPath).FirstOrDefault();
            if (pts != null)
            {
                pts.NeedsReloadedFromWorkingFile = DateTime.Now;
            }
        }

        private void toolStripButtonViewData_Click(object sender, EventArgs e)
        {
            List<TaggedComponent> allTags = new List<TaggedComponent>();

            foreach (ProjectTags pt in MainForm.CurrentProjectTags)
            {
                if (!pt.Exported) allTags.AddRange(pt.getAllAsTaggedComponent());
            }

            ComponentViewer cv = new ComponentViewer(_mainForm);
            cv.Text = "Component Viewer - All Tags";
            cv.loadProjectData(allTags);
            cv.ShowDialog();
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {

            if (radPageViewTags.SelectedPage.Text.Contains("Staged"))
            {
                DialogResult dr = MessageBox.Show("Are you sure you want to delete " + this.radGridViewCollectedTags.SelectedRows.Count().ToString() + " rows?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == System.Windows.Forms.DialogResult.No) return;

                foreach (Telerik.WinControls.UI.GridViewRowInfo row in radGridViewCollectedTags.SelectedRows)
                {
                    MainForm.CollectedTags.Remove((TaggedComponent)row.DataBoundItem);
                }
                radGridViewCollectedTags.DataSource = null;
                radGridViewCollectedTags.DataSource = MainForm.CollectedTags;
                radGridViewCollectedTags.BestFitColumns(Telerik.WinControls.UI.BestFitColumnMode.AllCells);
                Cursor.Current = Cursors.Arrow;
            }
            else
            {
                if (objectListViewPendingTagData.SelectedItems.Count > 0)
                {
                    //List<string> idsToDelete = new List<string>();
                    //foreach(ProjectTags pts in objectListViewPendingTagData.SelectedObjects.Cast<ProjectTags>())
                    //{
                    //    idsToDelete.Add(pts.Id);
                    //}
                    //DialogResult dr = MessageBox.Show("The following tag data sets will be permanantly deleted from the project. You will need to re-import them from the data file to bring them back. Are you sure you want to continue?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //if (dr == DialogResult.Yes)
                    //{
                    //    MainForm.CurrentProjectTags.RemoveAll(t => idsToDelete.Contains(t.Id));
                    //    objectListViewPendingTagData.SetObjects(MainForm.CurrentProjectTags.Where(t => t.Exported == false));
                    //}

                    DialogResult dr = MessageBox.Show("The following tag data sets will be permanantly deleted from the project. You will need to re-import them from the data file to bring them back. Are you sure you want to continue?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        foreach (object item in objectListViewPendingTagData.SelectedObjects)
                        {
                            MainForm.CurrentProjectTags.Remove(item as ProjectTags);
                        }
                        objectListViewPendingTagData.SetObjects(MainForm.CurrentProjectTags.Where(t => t.Exported == false));
                    }
                }
                else
                {
                    MessageBox.Show("Please select a set to delete");
                }
            }
        }

        private void objectListViewPendingTagData_DoubleClick(object sender, EventArgs e)
        {
            //List<TaggedComponent> allTags = new List<TaggedComponent>();

            //foreach (ProjectTags pt in MainForm.CurrentProjectTags)
            //{
            //    allTags.AddRange(((ProjectTags)pt).getAllAsTaggedComponent());
            //}

            //ComponentViewer cv = new ComponentViewer();
            //cv.loadProjectData(allTags);
            //cv.ShowDialog();
            ComponentViewer cv = new ComponentViewer(_mainForm);
            ProjectTags pts = objectListViewPendingTagData.SelectedItem.RowObject as ProjectTags;
            cv.loadProjectData(pts.getAllAsTaggedComponent());
            cv.Text = "Component Viewer - " + pts.Device;
            cv.ShowDialog();

            objectListViewPendingTagData.SetObjects(MainForm.CurrentProjectTags.Where(t => t.Exported == false));
        }

        private void toolStripButtonImportExcel_Click(object sender, EventArgs e)
        {
            importFromFile();
            objectListViewPendingTagData.SetObjects(MainForm.CurrentProjectTags.Where(t => t.Exported == false));
        }

        private void importFromFile(bool specifyDelimiter = false)
        {

            ProjectTags pts;
            string delimiter = "";

            if (radPageViewTags.SelectedPage.Text.Contains("Staged")) objectListViewPendingTagData.DeselectAll();

            if (objectListViewPendingTagData.SelectedItems.Count == 0)
            {
                pts = new ProjectTags();
                pts.setDefaults();
                MainForm.CurrentProjectTags.Add(pts);
                MessageBox.Show("A new import set will be created");
            }
            else
            {
                pts = objectListViewPendingTagData.SelectedItem.RowObject as ProjectTags; 
            }

            Cursor.Current = Cursors.WaitCursor;

           //pts = MainForm.CurrentProjectTags.Where(p => p.Id == pts.Id).FirstOrDefault();
            if (pts != null)
            {
                OpenFileDialog od = new OpenFileDialog();
                od.Filter = "Excel Files (*.xlsx)|*.xlsx|Delimited Files (*.csv;*.txt)|*.csv;*.txt|All files (*.*)|*.*";
                od.Multiselect = false;

                DialogResult dr = od.ShowDialog();

                if (specifyDelimiter)
                {
                    Globals.InputBox("Enter delimiter value", "Enter delimiter value", ref delimiter);
                }


                if (dr == DialogResult.OK)
                {

                    try
                    {
                        string csvData = File.ReadAllText(od.FileName);
                        List<TaggedComponent> tags;
                        switch (Path.GetExtension(od.FileName))
                        {
                            case ".txt":
                                tags = FileUtilities.GetTaggedComponentsFromCSV(csvData, true, true, true, delimiter);
                                break;
                            case ".csv":
                                tags = FileUtilities.GetTaggedComponentsFromCSV(csvData, true, true, false, delimiter);
                                break;
                            case ".xlsx":
                                tags = FileUtilities.GetTaggedComponentFromExcel(od.FileName);
                                pts.FileName = od.FileName;
                                pts.Device = Path.GetFileNameWithoutExtension(od.FileName);
                                break;
                            default:
                                tags = FileUtilities.GetTaggedComponentsFromCSV(csvData, true, true, false, delimiter);
                                break;
                        }

                        if (tags != null)
                        {
                            pts.Tags.Clear();
                            pts.Tags.AddRange(tags);
                            objectListViewPendingTagData.SetObjects(null);
                            objectListViewPendingTagData.SetObjects(MainForm.CurrentProjectTags.Where(t => t.Exported == false));
                            MessageBox.Show("Data re-imported sucessfully.", "Tag Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _mainForm.RefreshProjectTagData();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            if (pts.Tags.Count == 0) MainForm.CurrentProjectTags.Remove(pts);

            Cursor.Current = Cursors.Arrow;
        }

        private void toolStripButtonExportAll_Click(object sender, EventArgs e)
        {

            //DialogResult drWarning = MessageBox.Show("This function will allow you to export all tagged component sets to one xlsx.  This \"Merged\" set can only be re-imported after all remaining sets are merged", "Warning", MessageBoxButtons.OKCancel);

            //if (drWarning == DialogResult.Cancel) return;

            List<TaggedComponent> exportList = new List<TaggedComponent>();
            string csvFileName = string.Empty;

            if (radPageViewTags.SelectedPage.Text != "Incoming Sets")
            {
                exportList.AddRange(MainForm.CollectedTags);
                csvFileName = Path.GetFileNameWithoutExtension(MainForm.CurrentProjectData.ProjectName + "_StagedTags");
            }
            else
            {
                foreach (ProjectTags tagList in MainForm.CurrentProjectTags)
                {
                    exportList.AddRange(tagList.Tags);
                    csvFileName = Path.GetFileNameWithoutExtension(MainForm.CurrentProjectData.ProjectName + "_ImportSetTags");
                }
            }

            SaveFileDialog sd = new SaveFileDialog();
            sd.AddExtension = true;
            sd.Filter = "Excel Files (*.xlsx)|*.xlsx|Comma Separated Value Files (*.csv)|*.csv|All files (*.*)|*.*";
            sd.FileName = csvFileName;
            DialogResult dr = sd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                if (Path.GetExtension(sd.FileName) == ".csv")
                {
                    string csvData = FileUtilities.GetTaggedComponentsAsCSV(exportList.ToList());
                    try
                    {
                        File.WriteAllText(sd.FileName, csvData, Encoding.UTF8);
                        System.Diagnostics.Process.Start(sd.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("File is already in use or open in another application. Close the file and then try again.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    string errorMessage = FileUtilities.SendTaggedComponentsToExcel(exportList.ToList(), sd.FileName);
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        MessageBox.Show(errorMessage);
                    }
                    else
                    {
                        try
                        {
                            System.Diagnostics.Process.Start(sd.FileName);
                        }
                        catch { }
                    }
                }
            }
        }

        private void toolStripButtonImportAll_Click(object sender, EventArgs e)
        {

            if (objectListViewPendingTagData.SelectedItems.Count < 2)
            {
                MessageBox.Show("Please select at least two sets to merge");
                return;
            }
            
            DialogResult dr = DialogResult.No;
            dr = MessageBox.Show("This will merge the selected data sets into one new dataset.  This action cannot be undone! Continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.No) return;

            ProjectTags newPt = new ProjectTags();

            newPt.CreateDate = DateTime.Now;
            newPt.Device = Environment.MachineName + "_" + string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
            newPt.Id = Guid.NewGuid().ToString();
            newPt.WorkingFileName = Environment.MachineName + "_" + string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}.csv", DateTime.Now);
            newPt.FileName = Environment.MachineName + "_" + string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}.csv", DateTime.Now);
            newPt.Tags = new List<TaggedComponent>();

            foreach (ProjectTags pt in objectListViewPendingTagData.SelectedObjects)
            {
                newPt.Tags.AddRange(pt.Tags);
            }

            //MainForm.CurrentProjectTags = new List<ProjectTags>();

            MainForm.CurrentProjectTags.Add(newPt);

            foreach (ProjectTags pt in objectListViewPendingTagData.SelectedObjects)
            {
                MainForm.CurrentProjectTags.Remove(pt);
            }

            MainForm.CurrentProjectDirty = true;

            objectListViewPendingTagData.SetObjects(MainForm.CurrentProjectTags.Where(t => t.Exported == false));
            
        }

        private void toolStripButtonRemoveDups_Click(object sender, EventArgs e)
        {
            DialogResult dr = DialogResult.No;
            dr = MessageBox.Show("This action will remove exact duplicates from the selected sets.  In each case, the first found duplicate will be removed.  Continue?", "Process Duplicates?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dr == DialogResult.No) return;

            List<TaggedComponent> allTags = new List<TaggedComponent>();
            List<int> hashCodes = new List<int>();
            List<int> hashDupes = new List<int>();

            Guid deleteGuid = Guid.NewGuid();
            
            foreach (ProjectTags pt in MainForm.CurrentProjectTags.OrderByDescending(c => c.CreateDate))
            {
                allTags.AddRange(pt.Tags);
            }

            foreach (TaggedComponent tc in allTags)
            {
                if (!hashCodes.Contains(tc.GetHashCode()))
                {
                    hashCodes.Add(tc.GetHashCode());
                }
                else
                {
                    if (!hashDupes.Contains(tc.GetHashCode())) hashDupes.Add(tc.GetHashCode());
                }
            }

            foreach (int delHash in hashDupes)
            {
                List<TaggedComponent> delTags = allTags.Where(c => c.GetHashCode() == delHash).OrderByDescending(d => d.ModifiedDate).ToList();
                delTags.RemoveRange(0, 1);
                foreach (TaggedComponent delTag in delTags)
                {
                    delTag.WarningMessage = deleteGuid.ToString();
                }
            }

            foreach (ProjectTags pt in MainForm.CurrentProjectTags.Where(t => t.Exported == false))
            {
                pt.Tags.RemoveAll(c => c.WarningMessage == deleteGuid.ToString());
            }

            MainForm.CurrentProjectDirty = true;
            objectListViewPendingTagData.SetObjects(MainForm.CurrentProjectTags.Where(t => t.Exported == false));

        }

        private void toolStripButtonReloadDelimited_Click(object sender, EventArgs e)
        {
            importFromFile(true);
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (objectListViewPendingTagData.SelectedItems.Count == 1)
            {
                string newName = string.Empty;
                Globals.InputBox("Rename", "Please enter a new name", ref newName);
                ((ProjectTags)(objectListViewPendingTagData.SelectedItem.RowObject)).Device = newName;
                objectListViewPendingTagData.SetObjects(MainForm.CurrentProjectTags.Where(t => t.Exported == false));
            }
        }

        private void ProjectTagsControl_Enter(object sender, EventArgs e)
        {
            objectListViewPendingTagData.SetObjects(MainForm.CurrentProjectTags.Where(t => t.Exported == false));
        }

        private void ProjectTagsControl_VisibleChanged(object sender, EventArgs e)
        {
            objectListViewPendingTagData.SetObjects(MainForm.CurrentProjectTags.Where(t => t.Exported == false));
        }

        private void objectListViewPendingTagData_ItemsChanged(object sender, BrightIdeasSoftware.ItemsChangedEventArgs e)
        {
            try
            {
                _mainForm.setImportEnabled();
            }
            catch { }
        }

        private void toolStripButtonAddToCollected_Click(object sender, EventArgs e)
        {

            List<TaggedComponent> newTCList = new List<TaggedComponent>();
            bool GhostTagFound = false;
            
            if (objectListViewPendingTagData.SelectedItems.Count > 0)
            {
                DialogResult dr = MessageBox.Show("This will move data to the \"Staged Tags\" collection.  This will overwrite previously existing records!", "Confirm Staging", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    foreach (object item in objectListViewPendingTagData.SelectedObjects)
                    {
                        newTCList = ((ProjectTags)item).Tags;
                        GhostTagFound = newTCList.Where(c => c.GhostTag == true).Count() > 0;
                        ((ProjectTags)item).SentToCollected = DateTime.Now;
                        foreach (TaggedComponent tc in newTCList.Where(c => c.GhostTag == false))
                        {
                            List<TaggedComponent> tcListwKids = tc.CopyComponentAndChildren();
                            foreach (TaggedComponent tcInside in tcListwKids)
                            {
                                MainForm.CollectedTags.RemoveAll(c => c.Id == tcInside.Id);
                            }

                            MainForm.CollectedTags.AddRange(tc.CopyComponentAndChildren());

                        }
                    }

                    if (GhostTagFound) MessageBox.Show("Ghost tags were found in the set(s) you staged.  Ghost tags cannot be added to staged tags.  Please review the incoming sets for a list of affected components.");

                    objectListViewPendingTagData.SetObjects(MainForm.CurrentProjectTags.Where(t => t.Exported == false));
                    radGridViewCollectedTags.DataSource = null;
                    radGridViewCollectedTags.DataSource = MainForm.CollectedTags;
                    radGridViewCollectedTags.MasterTemplate.Refresh(null);
                    radGridViewCollectedTags.Refresh();
                    radGridViewCollectedTags.BestFitColumns(Telerik.WinControls.UI.BestFitColumnMode.DisplayedCells);
                }
            }
            else
            {
                MessageBox.Show("Please select a set to add to staging");
            }
        }

        private void radPageViewTags_SelectedPageChanged(object sender, EventArgs e)
        {
            if (radPageViewTags.SelectedPage.Text == "Incoming Sets")
            {
                toolStripButtonAddToCollected.Enabled = true;
                toolStripButtonRemoveDups.Enabled = true;
                toolStripButtonImportAll.Enabled = true;
                toolStripButtonFindReplace.Enabled = false;
                toolStripButtonMassEdit.Enabled = false;
                toolStripButtonExportEid.Enabled = false;
                toolStripButtonQuickView.Enabled = true;
            }
            else
            {
                toolStripButtonAddToCollected.Enabled = false;
                toolStripButtonRemoveDups.Enabled = false;
                toolStripButtonImportAll.Enabled = false;
                toolStripButtonFindReplace.Enabled = true;
                toolStripButtonMassEdit.Enabled = true;
                toolStripButtonExportEid.Enabled = true;
                toolStripButtonQuickView.Enabled = false;
                radGridViewCollectedTags.DataSource = null;
                radGridViewCollectedTags.DataSource = MainForm.CollectedTags;
                radGridViewCollectedTags.MasterTemplate.Refresh(null);
                radGridViewCollectedTags.Refresh();
                radGridViewCollectedTags.BestFitColumns(Telerik.WinControls.UI.BestFitColumnMode.DisplayedCells);
            }

        }

        private void ProjectTagsControl_SizeChanged(object sender, EventArgs e)
        {
            radGridViewCollectedTags.BestFitColumns(Telerik.WinControls.UI.BestFitColumnMode.DisplayedCells);
        }

        private void toolStripButtonFindReplace_Click(object sender, EventArgs e)
        {
            string errorMessage = string.Empty;
            TaggedComponent tempTc = new TaggedComponent();
            string fieldToChange = string.Empty;
            string findValue = string.Empty;
            string changeValue = string.Empty;

            if (this.radGridViewCollectedTags.SelectedRows.Count() == 0)
            {
                MessageBox.Show("No components selected!");
                return;
            }

            Globals.InputBox("Field Selection", "Please enter the field you wish to change", ref fieldToChange);

            if (!tempTc.getHeaderAsList().Contains(fieldToChange))
            {
                MessageBox.Show("A valid field is required");
                return;
            }

            Globals.InputBox("Find Selection", "Please enter the value you wish to find", ref findValue);

            Globals.InputBox("Value Selection", "Please enter the value you wish to replace found items with", ref changeValue);

            DialogResult confirmBoxResult = MessageBox.Show("This will find/replace in the specified field for all selected records, are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmBoxResult == System.Windows.Forms.DialogResult.No) return;

            if (string.IsNullOrEmpty(changeValue)) return;

            List<TaggedComponent> filteredTcList = new List<TaggedComponent>();

            foreach (Telerik.WinControls.UI.GridViewRowInfo tc in radGridViewCollectedTags.SelectedRows)
            {
                filteredTcList.Add((TaggedComponent)tc.DataBoundItem);
            }

            if (MainForm.CurrentProjectData == null)
            {
                MessageBox.Show("There is no project data available.", "Project Data Export", MessageBoxButtons.OK);
            }
            else
            {
                foreach (TaggedComponent component in filteredTcList)
                {
                    PropertyInfo prop = component.GetType().GetProperty(fieldToChange, BindingFlags.Public | BindingFlags.Instance);
                    if (null != prop && prop.CanWrite)
                    {
                        errorMessage = component.setValueByName(fieldToChange, component.getValueByName(fieldToChange).Replace(findValue, changeValue));
                        if (!string.IsNullOrEmpty(errorMessage))
                        {
                            MessageBox.Show(errorMessage);
                            return;
                        }
                    }
                }
            }

            radGridViewCollectedTags.MasterTemplate.Refresh(null);
            MainForm.CurrentProjectDirty = true;
        }

        private void toolStripButtonMassEdit_Click(object sender, EventArgs e)
        {
            string errorMessage = string.Empty;
            TaggedComponent tempTc = new TaggedComponent();
            string fieldToChange = string.Empty;
            string changeValue = string.Empty;

            if (radGridViewCollectedTags.SelectedRows.Count() == 0)
            {
                MessageBox.Show("No components selected!");
                return;
            }

            Globals.InputBox("Field Selection", "Please enter the field you wish to change", ref fieldToChange);

            if (!tempTc.getHeaderAsList().Contains(fieldToChange))
            {
                MessageBox.Show("A valid field is required");
                return;
            }

            Globals.InputBox("Value Selection", "Please enter the value you wish to change all items to", ref changeValue);

            DialogResult confirmBoxResult = MessageBox.Show("This will change the values for your selected records, are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmBoxResult == System.Windows.Forms.DialogResult.No) return;

            if (string.IsNullOrEmpty(changeValue)) return;

            List<TaggedComponent> filteredTcList = new List<TaggedComponent>();

            foreach (Telerik.WinControls.UI.GridViewRowInfo tc in radGridViewCollectedTags.SelectedRows)
            {
                filteredTcList.Add((TaggedComponent)tc.DataBoundItem);
            }

            if (MainForm.CurrentProjectData == null)
            {
                MessageBox.Show("There is no project data available.", "Project Data Export", MessageBoxButtons.OK);
            }
            else
            {
                foreach (TaggedComponent component in filteredTcList)
                {
                    PropertyInfo prop = component.GetType().GetProperty(fieldToChange, BindingFlags.Public | BindingFlags.Instance);
                    if (null != prop && prop.CanWrite)
                    {
                        errorMessage = component.setValueByName(fieldToChange, changeValue);
                        if (errorMessage != string.Empty)
                        {
                            MessageBox.Show(errorMessage);
                            return;
                        }
                    }
                }
                radGridViewCollectedTags.MasterTemplate.Refresh(null);
                MainForm.CurrentProjectDirty = true;
            }
        }

        private void toolStripButtonExportEid_Click(object sender, EventArgs e)
        {
            if (radGridViewCollectedTags.SelectedRows.Count() == 0)
            {
                MessageBox.Show("No components selected!");
                return;
            }

            List<TaggedComponent> filteredTcList = new List<TaggedComponent>();

            foreach (Telerik.WinControls.UI.GridViewRowInfo tc in radGridViewCollectedTags.SelectedRows)
            {
                filteredTcList.Add((TaggedComponent)tc.DataBoundItem);
            }

            List<TaggedComponent> rejoinedList = TaggedComponent.rejoinTagFamilies(filteredTcList, MainForm.CurrentProjectData.LDARDatabaseType.Contains("Guideware"));

            if (MainForm.CurrentProjectData == null)
            {
                MessageBox.Show("There is no project data available.", "Project Data Export", MessageBoxButtons.OK);
            }
            else
            {
                ExportFieldConfigurationDialog ecd = new ExportFieldConfigurationDialog(MainForm.CurrentProjectData);
                DialogResult dr = ecd.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {

                    MainForm.CurrentProjectData.DefaultDrawing = ecd.DefaultDrawingPackage;

                    string exportFile = ecd.ExportFileName;
                    List<string> selectedDrawingPackages = new List<string>();
                    selectedDrawingPackages.Add(ecd.DefaultDrawingPackage);
                    if (ecd.DrawingPackagesAdditional.Count > 0) selectedDrawingPackages.AddRange(ecd.DrawingPackagesAdditional);
                    List<string> selectedLDARProcessUnits = ecd.SelectedLDARProcessUnits;

                    _mainForm.SaveProjectFileAs(exportFile, selectedDrawingPackages, selectedLDARProcessUnits, true, rejoinedList);

                }
            }
        }

        private void radGridViewCollectedTags_RowsChanged(object sender, Telerik.WinControls.UI.GridViewCollectionChangedEventArgs e)
        {
            if (_mainForm != null) _mainForm.setImportEnabled();
        }

        private void toolStripButtonQuickView_Click(object sender, EventArgs e)
        {
            List<TaggedComponent> allTags = new List<TaggedComponent>();

            foreach (ProjectTags pt in MainForm.CurrentProjectTags.Where(t => t.Exported == false))
            {
                allTags.AddRange(pt.Tags);
            }

            ComponentViewer cv = new ComponentViewer(_mainForm);
            cv.Text = "Component Viewer - All Tags";
            cv.loadProjectData(allTags);
            cv.ShowDialog();
        }          
    }
}
