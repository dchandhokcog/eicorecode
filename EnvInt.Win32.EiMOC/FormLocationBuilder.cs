using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EnvInt.Win32.EiMOC
{
    public partial class FormLocationBuilder : Form
    {
        public string LocationDescriptor { get; set; }

        public FormLocationBuilder()
        {
            InitializeComponent();

            comboBoxPrimary.Items.AddRange(Globals.ProjectComponents.ToArray());
            comboBoxSecondary.Items.AddRange(Globals.ProjectComponents.ToArray());
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            LocationDescriptor = comboBoxLocationLevel.Text + comboBoxLocationLevel.Text + "-" + comboBoxPrimary.Text + "-" + comboBoxDirectional.Text + "-" + comboBoxSecondary.Text;
            Close();
        }

        private void comboBoxLocationHeight_Leave(object sender, EventArgs e)
        {
            if (!ComboBoxHasValue(comboBoxLocationHeight))
            {
                //MessageBox.Show("Item should be added: " + comboBoxLocationHeight.Text);
            }
            
        }

        private bool ComboBoxHasValue(ComboBox cb)
        {
            string cbText = cb.Text;
            foreach (object item in cb.Items)
            {
                if (item.ToString().ToLower() == cbText.ToLower())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
