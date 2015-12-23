using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using EnvInt.Win32.FieldTech.Containers;
using EnvInt.Win32.FieldTech.Data;

namespace EnvInt.Win32.FieldTech
{
    public partial class FormChildObject : Form
    {
        double _currentBackground = 0;
        double _currentReading = 0;
        string _currentInspector = "";
        string _currentInstrument = "";
        DateTime _currentInspectionDate = DateTime.Today;
        bool _inspected = false;

        public string ComponentType
        {
            get
            {
                return comboBoxComponentType.Text;
            }
            set
            {
                comboBoxComponentType.Text = value;
            }
        }

        public string LocationDescription
        {
            get
            {
                return textBoxLocation.Text;
            }
            set
            {
                textBoxLocation.Text = value;
            }
        }

        public string PreviousTag
        {
            get
            {
                return textBoxPreviousTag.Text;
            }
            set
            {
                textBoxPreviousTag.Text = value;
            }
        }

        public double InspectionBackground
        {
            get
            {
                return _currentBackground;
            }
            set
            {
                _currentBackground = value;
            }
        }

        public string InspectionInspector
        {
            get
            {
                return _currentInspector;
            }
            set
            {
                _currentInspector = value;
            }
        }

        public string InspectionInstrument
        {
            get
            {
                return _currentInstrument;
            }
            set
            {
                _currentInstrument = value;
            }
        }

        public bool Inspected
        {
            get
            {
                return _inspected;
            }
        }

        public double InspectionReading
        {
            get
            {
                return _currentReading;
            }
        }

        public DateTime InspectionDate
        {
            get
            {
                return _currentInspectionDate;
            }
        }

        public double Size
        {
            get
            {
                return Globals.getSizeFromString(textBoxSize.Text);
            }
            set
            {
                textBoxSize.Text = value.ToString();
            }
        }

        public bool AllowInspections { get; set; }

        public bool EditMode
        {
            get
            {
                return pictureBoxEditMode.Visible;
            }
            set
            {
                pictureBoxEditMode.Visible = value;
            }
        }

        public string LDARTag
        {
            get
            {
                return textBoxLDARTag.Text;
            }
            set
            {
                textBoxLDARTag.Text = value;
            }
        }

        public bool AllowEditTag
        {
            get
            {
                TableLayoutRowStyleCollection rowStyles = tableLayoutPanel1.RowStyles;
                return rowStyles[2].Height == 0;
            }
            set
            {
                TableLayoutRowStyleCollection rowStyles = tableLayoutPanel1.RowStyles;

                if (value)
                {
                    rowStyles = tableLayoutPanel1.RowStyles;
                    rowStyles[2].Height = 32;
                }
                else
                {
                    rowStyles = tableLayoutPanel1.RowStyles;
                    rowStyles[2].Height = 0;
                }
            }
        }

        public FormChildObject(string location)
        {
            InitializeComponent();

            if (Globals.CurrentProjectData != null)
            {
                setTypeList(Globals.CurrentProjectData.LDARData.ComponentClassTypes);
            }

            foreach (Control c in this.Controls)
            {
                if (c.GetType().ToString() == "System.Windows.Forms.TextBox" || c.GetType().ToString() == "System.Windows.Forms.ComboBox")
                {
                    c.KeyPress += FormEditChild_KeyPress;
                }
            }

        }

        public void setTypeList(List<LDARComponentClassType> typeList)
        {
            
            comboBoxComponentType.Items.Clear();
            foreach (LDARComponentClassType t in typeList)
            {
                if (t.childType) comboBoxComponentType.Items.Add(t.ComponentClass + " - " + t.ComponentType);
            }
            comboBoxComponentType.Sorted = true;

            //we have to have some sort of list if one isn't provided.
            if (comboBoxComponentType.Items.Count == 0)
            {
                comboBoxComponentType.Items.Clear();
                comboBoxComponentType.Items.Add("FLANGE");
                comboBoxComponentType.Items.Add("PLUG");
                comboBoxComponentType.Items.Add("CAP");
                comboBoxComponentType.Items.Add("CONNECTOR");
            }

        }

        public void setComponent(ChildComponent child)
        {
            comboBoxComponentType.Text = child.ComponentType;
            textBoxLocation.Text = child.Location;
            textBoxPreviousTag.Text = child.PreviousTag;
            textBoxSize.Text = child.Size.ToString();
            textBoxLDARTag.Text = child.LDARTag;
            if (child.Inspected)
            {
                _currentBackground = child.InspectionBackground;
                _currentReading = child.InspectionReading;
                _currentInspector = child.InspectionInspector;
                _currentInstrument = child.InspectionInstrument;
                DateTime _currentInspectionDate = child.InspectionDate;
                _inspected = true;
                labelInspection.Text = _currentReading + " PPM by " + _currentInspector + " on " + _currentInspectionDate.ToString();
            }

        }

        private bool validateForm()
        {

            List<string> errorList = new List<string>();
            string errString = "";
            
            if (string.IsNullOrEmpty(comboBoxComponentType.Text))
            {
                errorList.Add("Type is empty");
            }

            if (AllowEditTag)
            {
                if (string.IsNullOrEmpty(textBoxLDARTag.Text)) errorList.Add("Tag is empty"); 
            }

            if (string.IsNullOrEmpty(textBoxSize.Text))
            {
                errorList.Add("Size is empty");
            }

            //this is an ugly hack for sinclair
            if (!EditMode && Globals.CurrentProjectData.LDARDatabaseType == "LeakDAS")
            {
                if (!Properties.Settings.Default.AllowDuplicateLocation) if (LocalData.doesLocationDescriptionExist(textBoxLocation.Text, "")) errorList.Add("The location description has already been used");
            }

            if (errorList.Count() > 0)
            {
                foreach (string err in errorList)
                {
                    errString = errString + err + Environment.NewLine;
                }
                MessageBox.Show(errString);
                return false;
            }
            else
            {
                return true;
            }

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (validateForm())
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
        }

        private void buttonInspect_Click(object sender, EventArgs e)
        {
            
            FormInspect ins = new FormInspect();

            if (!_inspected)
            {
                ins.Inspector = MainForm._lastInspector;
                ins.Instrument = MainForm._lastInstrument;
                ins.Background = MainForm._lastBackground;
            }
            else
            {
                ins.EditExisting = true;
                ins.Reading = _currentReading;
                ins.Background = _currentBackground;
                ins.Inspector = _currentInspector;
                ins.Instrument = _currentInstrument;
            }

            ins.StartPosition = FormStartPosition.CenterScreen;
            DialogResult dr = ins.ShowDialog();
            ins.TopMost = true;
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                if (ins.RemoveInspection)
                {
                    _inspected = false;
                    labelInspection.Text = "None";
                    _currentBackground = 0;
                    _currentInspectionDate = DateTime.MinValue;
                    _currentInspector = string.Empty;
                    _currentReading = 0;
                }
                else
                {
                    _currentInspectionDate = DateTime.Now;
                    _currentInstrument = ins.Instrument;
                    _currentReading = ins.Reading;
                    _currentInspector = ins.Inspector;
                    _currentBackground = ins.Background;
                    MainForm._lastBackground = ins.Background;
                    MainForm._lastInstrument = ins.Instrument;
                    MainForm._lastInspector = ins.Inspector;
                    _inspected = true;
                    labelInspection.Text = ins.Reading + " PPM by " + ins.Inspector + " on " + _currentInspectionDate.ToString();
                }

            }


        }

        private void FormChildObject_Activated(object sender, EventArgs e)
        {
            buttonInspect.Visible = AllowInspections;
            labelInspection.Visible = AllowInspections;
            labelInspectionLabel.Visible = AllowInspections;

            //set a type if one isn't provided
            if (string.IsNullOrEmpty(comboBoxComponentType.Text))
            {
                foreach (string typ in comboBoxComponentType.Items)
                {
                    if (typ.Contains("FLANGE"))
                    {
                        comboBoxComponentType.SelectedIndex = comboBoxComponentType.Items.IndexOf(typ);
                    }
                }

                //if we still don't have a valid entry we'll default to flange
                if (string.IsNullOrEmpty(comboBoxComponentType.Text)) comboBoxComponentType.Text = "FLANGE";
            }
        }

        private void textBoxSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == '/')
            {
                if (e.KeyChar == '.' & this.textBoxSize.Text.Contains(".")) e.KeyChar = char.MinValue;
                if (e.KeyChar == '/' & this.textBoxSize.Text.Contains("/")) e.KeyChar = char.MinValue;
                if (e.KeyChar == '/' & this.textBoxSize.Text.Contains(".")) e.KeyChar = char.MinValue;
                if (e.KeyChar == '.' & this.textBoxSize.Text.Contains("/")) e.KeyChar = char.MinValue;
            }
            else e.KeyChar = char.MinValue;
        }

        private void FormEditChild_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Properties.Settings.Default.ForceUpperCase)
            {
                if (char.IsLetter(e.KeyChar))
                {
                    e.KeyChar = char.ToUpper(e.KeyChar);
                }
            }
        }
    }
}
