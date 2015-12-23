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

namespace EnvInt.Win32.EiMOC
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
                return comboBoxComponentType.SelectedItem.ToString();
            }
        }

        public string LocationDescription
        {
            get
            {
                return textBoxLocation.Text;
            }
        }

        public string PreviousTag
        {
            get
            {
                return textBoxPreviousTag.Text;
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


        public FormChildObject(string location)
        {
            InitializeComponent();

            comboBoxComponentType.Items.Clear();
            comboBoxComponentType.Items.Add("FLANGE");
            comboBoxComponentType.Items.Add("PLUG");
            comboBoxComponentType.Items.Add("CAP");
            comboBoxComponentType.Items.Add("CONNECTOR");
            comboBoxComponentType.SelectedIndex = 0;

            textBoxLocation.Text = location;
        }

        public void setTypeList(List<LDARComponentClassType> typeList)
        {
            comboBoxComponentType.Items.Clear();
            foreach (LDARComponentClassType t in typeList)
            {
                if (t.ComponentClass.Contains("CON")) comboBoxComponentType.Items.Add(t.ComponentClass + " - " + t.ComponentType);
            }
            comboBoxComponentType.Sorted = true;
            comboBoxComponentType.SelectedIndex = 0;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void buttonInspect_Click(object sender, EventArgs e)
        {
            FormInspect ins = new FormInspect();
            ins.StartPosition = FormStartPosition.CenterScreen;
            ins.Background = _currentBackground;
            ins.Inspector = _currentInspector;
            DialogResult dr = ins.ShowDialog();
            ins.TopMost = true;
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                _currentInspectionDate = DateTime.Now;
                _currentInstrument = ins.Instrument;
                _currentReading = ins.Reading;
                _currentInspector = ins.Inspector;
                _currentBackground = ins.Background;
                _inspected = true;
                labelInspection.Text = ins.Reading + " PPM by " + ins.Inspector + " on " + _currentInspectionDate.ToString();
            }


        }
    }
}
