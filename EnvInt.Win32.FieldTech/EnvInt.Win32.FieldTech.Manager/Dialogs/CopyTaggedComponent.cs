using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EnvInt.Win32.FieldTech.Manager.HelperClasses;
using EnvInt.Win32.FieldTech.Common;
using EnvInt.Win32.FieldTech.Containers;
using EnvInt.Win32.FieldTech.Manager.Dialogs;
using EnvInt.Win32.FieldTech.Manager.Controls;


namespace EnvInt.Win32.FieldTech.Manager.Dialogs
{
    public partial class CopyTaggedComponent : Form
    {
        public bool CopyOperation
        {
            get
            {
                return radioButtonCopy.Checked;
            }
        }

        public string selectedDataSet
        {
            get
            {
                return comboBoxSetList.Text;
            }
        }
        
        public CopyTaggedComponent()
        {
            InitializeComponent();
            radioButtonCopy.Checked = true;
        }

        private void CopyTaggedComoponent_Load(object sender, EventArgs e)
        {
            foreach (ProjectTags pt in MainForm.CurrentProjectTags)
            {
                comboBoxSetList.Items.Add(pt.Device);
            }
            comboBoxSetList.Items.Add("[New Set]");
        }

        private void buttonCopyMove_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }


    }
}
