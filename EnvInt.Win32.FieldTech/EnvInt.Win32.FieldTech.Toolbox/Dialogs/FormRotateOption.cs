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
    public partial class FormRotateOption : Form
    {
        public FormRotateOption()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (radioButtonLeft.Checked)
                this.Tag = "Left";
            else
                this.Tag = "Right";

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

    }
}
