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
    public partial class ProgramSettings : Form
    {
        public ProgramSettings()
        {
            InitializeComponent();
        }

        private void ProgramSettings_Activated(object sender, EventArgs e)
        {
            checkBoxStrictUnitChecking.Checked = Properties.Settings.Default.StrictUnitChecking;
            checkBoxLimitQAQC.Checked = Properties.Settings.Default.AutoQAQC;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.StrictUnitChecking = checkBoxStrictUnitChecking.Checked;
            Properties.Settings.Default.AutoQAQC = checkBoxLimitQAQC.Checked;
            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}
