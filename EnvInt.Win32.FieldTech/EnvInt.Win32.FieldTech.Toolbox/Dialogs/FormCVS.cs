using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech
{
    public partial class FormCVS : Form
    {

        public string TagCVSReason
        {
            get
            {
                return comboBoxCVSReason.Text;
            }
            set
            {
                comboBoxCVSReason.Text = value;
            }
        }


        
        public FormCVS()
        {
            InitializeComponent();
            setPicklistValues();
        }

        private void setPicklistValues()
        {
            comboBoxCVSReason.Items.Clear();

            comboBoxCVSReason.Items.Add("");

            if (Globals.CurrentProjectData.LDARData.LDARCVSReasons != null)
            {
                foreach( LDARCVSReason cvs in Globals.CurrentProjectData.LDARData.LDARCVSReasons)
                {
                    if (cvs != null ) comboBoxCVSReason.Items.Add(cvs.CVSDescription);
                }
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
