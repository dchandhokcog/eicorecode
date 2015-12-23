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
    public partial class FormOOS : Form
    {

        public bool TagOOS {
            get
            { 
                return checkOOS.Checked; 
            }
            set 
            {
                checkOOS.Checked = value;
            }
        }

        public bool TagPOS
        {
            get
            {
                return checkBoxRemoved.Checked;
            }
            set
            {
                checkBoxRemoved.Checked = value;
            }
        }
        public string TagOOSReason
        {
            get
            {
                return comboBoxOOSReason.Text;
            }
            set
            {
                comboBoxOOSReason.Text = value;
            }
        }

        public string TagPOSReason
        {
            get
            {
                return comboBoxPOSReason.Text;
            }
            set
            {
                comboBoxPOSReason.Text = value;
            }
        }

        
        public FormOOS()
        {
            InitializeComponent();
            setPicklistValues();
            checkOOS_CheckedChanged(this, null);
            checkBoxRemoved_CheckedChanged(this, null);
        }

        private void setPicklistValues()
        {
            comboBoxOOSReason.Items.Clear();
            comboBoxPOSReason.Items.Clear();

            if (Globals.CurrentProjectData.LDARData.OOSDescriptions != null)
            {
                foreach( LDAROOSDescription oos in Globals.CurrentProjectData.LDARData.OOSDescriptions)
                {
                    if (oos.Permanent)
                    {
                        if (oos.OOSDescription != null ) comboBoxPOSReason.Items.Add(oos.OOSDescription);
                    }
                    else
                    {
                        if (oos.OOSDescription != null) comboBoxOOSReason.Items.Add(oos.OOSDescription);
                    }
                }
            }
        }

        private void checkOOS_CheckedChanged(object sender, EventArgs e)
        {
            if (checkOOS.Checked)
            {
                comboBoxOOSReason.Enabled = true;
            }
            else
            {
                comboBoxOOSReason.Enabled = false;
            }
        }

        private void checkBoxRemoved_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRemoved.Checked)
            {
                comboBoxPOSReason.Enabled = true;
            }
            else
            {
                comboBoxPOSReason.Enabled = false;
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
