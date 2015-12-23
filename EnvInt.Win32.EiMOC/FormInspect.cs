using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EnvInt.Win32.EiMOC.Data;
using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.EiMOC
{
    public partial class FormInspect : Form
    {

        public string Inspector
        {
            get
            {
                return comboInspectionUser.Text;
            }
            set
            {
                comboInspectionUser.Text = value.ToString();
            }
        }

        public double Reading
        {
            get
            {
                return double.Parse(textBoxReading.Text);
            }
        }

        public string Instrument
        {
            get
            {
                return textBoxInstrument.Text;
            }
        }

        public double Background
        {
            get
            {
                return double.Parse(textBoxBackground.Text);
            }
            set
            {
                textBoxBackground.Text = value.ToString();
            }
        }
        
        public FormInspect()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void FormInspect_Load(object sender, EventArgs e)
        {

            this.TopMost = true;

            comboInspectionUser.Items.Clear();

            if (Globals.CurrentProjectData != null)
            {
                foreach (LDARTechnician tech in Globals.CurrentProjectData.LDARData.Technicians)
                {
                    comboInspectionUser.Items.Add(tech.Name.ToString());
                }
               
            }
        }

        private void textBoxBackground_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == '.')
            {
                if (e.KeyChar == '.' & this.textBoxBackground.Text.Contains(".")) e.KeyChar = char.MinValue;
            }
            else e.KeyChar = char.MinValue;
        }

        private void textBoxReading_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == '.')
            {
                if (e.KeyChar == '.' & this.textBoxReading.Text.Contains(".")) e.KeyChar = char.MinValue;
            }
            else e.KeyChar = char.MinValue;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (validateForm())
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }

        }

        private bool validateForm()
        {
            bool formValid = true;
            List<string> errList = new List<string>();
            string errorMsg = string.Empty;

            if (comboInspectionUser.Text == "") errList.Add("Inspector cannot be empty");
            if (textBoxBackground.Text == "") errList.Add("Background cannot be empty");
            if (textBoxReading.Text == "") errList.Add("Reading cannot be empty");
            if (textBoxInstrument.Text == "") errList.Add("Instrument cannot be empty");

            try
            {
                double bg = double.Parse(textBoxBackground.Text);
                if (bg < 0 || bg > 100000) errList.Add("Background must be between 0 and 100000");
            }
            catch
            {
                errList.Add("Background value is invalid");
            }

            try
            {
                double rd = double.Parse(textBoxReading.Text);
                if (rd < 0 || rd > 100000) errList.Add("Reading must be between 0 and 100000");
            }
            catch
            {
                errList.Add("Reading value is invalid");
            }

            if (errList.Count() > 0)
            {
                formValid = false;
                foreach (string str in errList)
                {
                    errorMsg = errorMsg + str + Environment.NewLine; 
                }
                MessageBox.Show(errorMsg);
            }

            return formValid;
        }

    }
}
