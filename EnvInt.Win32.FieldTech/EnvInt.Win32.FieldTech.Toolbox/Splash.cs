using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EnvInt.Win32.FieldTech
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
            checkBoxShowSplash.Checked = Properties.Settings.Default.ShowSplash;
            checkBoxShowSample.Checked = Properties.Settings.Default.OpenLastProject;
            if (Globals.isDevelopmentVersion()) labelTesting.Visible = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonSplashOK_Click(object sender, EventArgs e)
        {
            //save checkbox statuses
            Properties.Settings.Default.OpenLastProject = checkBoxShowSample.Checked;
            Properties.Settings.Default.ShowSplash = checkBoxShowSplash.Checked;
            Properties.Settings.Default.Save();

            this.Close();
        }
    }
}
