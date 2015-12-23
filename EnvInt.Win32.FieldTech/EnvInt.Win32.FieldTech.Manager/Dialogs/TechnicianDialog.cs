using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EnvInt.Win32.FieldTech.Manager.Dialogs
{
    public partial class TechnicianDialog : Form
    {
        public TechnicianDialog()
        {
            InitializeComponent();
        }

        public string TechnicianName
        {
            get
            {
                return textBoxTechnicianName.Text;
            }
            set
            {
                textBoxTechnicianName.Text = value;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }
    }
}
