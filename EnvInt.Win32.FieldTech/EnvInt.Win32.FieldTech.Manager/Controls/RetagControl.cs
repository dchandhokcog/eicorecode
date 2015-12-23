using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EnvInt.Win32.FieldTech.Containers;
using EnvInt.Win32.FieldTech.Manager.Dialogs;
using EnvInt.Win32.FieldTech.Manager.HelperClasses;

namespace EnvInt.Win32.FieldTech.Manager.Controls
{
    public partial class RetagControl : UserControl
    {
        private MainForm _mainForm = null;

        public RetagControl()
        {
            InitializeComponent();
        }

        public bool LoadProjectTags(MainForm mainform, List<ProjectTags> projectTags)
        {
            _mainForm = mainform;

            objectListViewPendingTagData.SetObjects(projectTags);

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

        private void objectListViewPendingTagData_ItemActivate(object sender, EventArgs e)
        {
            ComponentViewer cv = new ComponentViewer(_mainForm);
            List<TaggedComponent> taggedComps = new List<TaggedComponent>();

            taggedComps = (List<TaggedComponent>)objectListViewPendingTagData.SelectedObject;

            cv.loadProjectData(taggedComps);

            cv.ShowDialog();
        }



    }
}
