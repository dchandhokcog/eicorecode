using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnvInt.Win32.EiMOC
{
    public partial class DialogSettings : Form
    {
        public DialogSettings()
        {
            InitializeComponent();

            checkBoxOpenLastProject.Checked = Properties.Settings.Default.OpenLastProject;
            checkBoxShowSplash.Checked = Properties.Settings.Default.ShowSplash;
            textBoxDeviceIdentifier.Text = Properties.Settings.Default.DeviceIdentifier;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ShowSplash = checkBoxShowSplash.Checked;
            Properties.Settings.Default.OpenLastProject = checkBoxOpenLastProject.Checked;
            Properties.Settings.Default.DeviceIdentifier = textBoxDeviceIdentifier.Text;
            Properties.Settings.Default.Save();
            Close();
        }
    }
}
