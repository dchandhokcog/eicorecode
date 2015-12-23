using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EnvInt.Win32.FieldTech.Containers;
using EnvInt.Win32.FieldTech.Manager.HelperClasses;
using EnvInt.Win32.FieldTech.Manager.Dialogs;

namespace EnvInt.Win32.FieldTech.Manager.Controls
{
    public partial class CompletedTagsControl : UserControl
    {
        private MainForm _mainForm = null;

        public CompletedTagsControl()
        {
            InitializeComponent();
        }

        public bool LoadProjectTags(MainForm mainform, List<ProjectTags> projectTags)
        {
            _mainForm = mainform;

            objectListViewExportedTagData.SetObjects(projectTags.Where(t => t.Exported == true));

            return true;
        }

        private void objectListViewPendingTagData_HyperlinkClicked(object sender, BrightIdeasSoftware.HyperlinkClickedEventArgs e)
        {
            MessageBox.Show("Not Yet!");
            //TODO: Extract the clicked tag data to a temp file
            //TODO: Open the temp file in EXCEL
            //TODO: Monitor the file in this EXE to see if modified date is newer
            //TODO: prompt user to reload the file
        }

        private void objectListViewExportedTagData_ItemActivate(object sender, EventArgs e)
        {
            ComponentViewer cv = new ComponentViewer(_mainForm);
            List<TaggedComponent> taggedComps = new List<TaggedComponent>();

            taggedComps = ((ProjectTags)objectListViewExportedTagData.SelectedObject).getAllAsTaggedComponent();

            cv.loadProjectData(taggedComps);

            cv.ShowDialog();
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                MainForm.CurrentProjectTags.Remove(((ProjectTags)objectListViewExportedTagData.SelectedObject));
            }
            catch { }

            objectListViewExportedTagData.SetObjects(MainForm.CurrentProjectTags.Where(t => t.Exported == true));
        }

        private void returnToNotExportedBinToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (objectListViewExportedTagData.SelectedObjects.Count > 0)
            {
                DialogResult dr = MessageBox.Show("This will reset the currently selected sets to \"unexported,\" continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No) return;
            }
            else
            {
                MessageBox.Show("Please select set(s) to return");
                return;
            }
            
            foreach (ProjectTags pt in objectListViewExportedTagData.SelectedObjects)
            {
                pt.Exported = false;
            }

            objectListViewExportedTagData.SetObjects(MainForm.CurrentProjectTags.Where(t => t.Exported == true));
        }

        private void CompletedTagsControl_VisibleChanged(object sender, EventArgs e)
        {
            objectListViewExportedTagData.SetObjects(MainForm.CurrentProjectTags.Where(t => t.Exported == true));
        }


    }
}
