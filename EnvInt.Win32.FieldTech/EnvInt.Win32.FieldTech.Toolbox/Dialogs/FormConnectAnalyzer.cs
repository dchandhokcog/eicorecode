using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using phx21;

using EnvInt.Win32.FieldTech;

namespace EnvInt.Win32.FieldTech.Dialogs
{
    public partial class FormConnectAnalyzer : Form
    {
        MainForm _mainForm;

        public FormConnectAnalyzer(MainForm mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
        }

        private void buttonSelectDevice_Click(object sender, EventArgs e)
        {
            _mainForm.resetPHX();
            MainForm.phxConnect.ShowConnectForm(new Point(20, 20));
        }

        private void buttonControl_Click(object sender, EventArgs e)
        {
            MainForm.phxConnect.ShowConnectForm(new Point(20, 20));
        }

    }
}
