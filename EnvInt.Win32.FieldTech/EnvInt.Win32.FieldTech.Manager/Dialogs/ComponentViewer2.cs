using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

using EnvInt.Win32.FieldTech.Manager.HelperClasses;
using EnvInt.Win32.FieldTech.Containers;
using EnvInt.Win32.FieldTech.Common;
using EnvInt.Win32.FieldTech.Manager.Dialogs;

using Telerik.WinControls.Data;

namespace EnvInt.Win32.FieldTech.Manager.Dialogs
{
    public partial class ComponentViewer2 : Form
    {

        List<TaggedComponent> _projectTags = new List<TaggedComponent>();
        
        public ComponentViewer2()
        {
            InitializeComponent();
        }

        public void loadProjectData(List<TaggedComponent> projectTags)
        {
            
           if (projectTags != null)
            {
                //_projectTags = projectTags;
                Application.DoEvents();

                _projectTags = projectTags;
                dataGridView1.DataSource = _projectTags;
                //int x = 0;
                //TaggedComponent tmpTg = new TaggedComponent();
                //foreach (string header in tmpTg.getHeaderAsList())
                //{
                //    objectListViewTags.Columns.Add(new BrightIdeasSoftware.OLVColumn(header, header));
                //    x++;
                //}
                Application.DoEvents();
            }
        }
    }
}
