using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EnvInt.Win32.Controls.Controls
{
    public partial class LocationDescriptionUserControl : UserControl
    {
        public event EventHandler LocationDescriptionChanged;
        public delegate void LocationChangedEvent(object sender, object param);
        public bool suspendUpdates = true;

        public string locationDescription { get; set; }

        public string equipmentText
        {
            get 
            { 
                return comboBoxEquipment.Text; 
            }
            set 
            {
                comboBoxEquipment.Text = value;
                Application.DoEvents();
            }
        }

        public LocationDescriptionUserControl()
        {
            InitializeComponent();

            this.LocationDescriptionChanged += new EventHandler(LocationDescriptionUserControl_LocationChanged);
        }

        void LocationDescriptionUserControl_LocationChanged(object sender, EventArgs e)
        {
            setLocation();
        }

        public void setEquipmentList(List<string> eqList)
        {

            suspendUpdates = true;
            comboBoxEquipment.Items.Clear();
            comboBoxEquipment.Items.Add("");
            foreach (var item in eqList)
            {
                comboBoxEquipment.Items.Add(item);
            }
            suspendUpdates = false;
        }

        public void setDirectionList(Dictionary<string,string> dirList)
        {
            suspendUpdates = true;
            comboBoxDirection.DataSource = new BindingSource(dirList, null);
            comboBoxDirection.DisplayMember = "Value";
            comboBoxDirection.ValueMember = "Key";
            suspendUpdates = false;
        }

        private void txtFloor_TextChanged(object sender, EventArgs e)
        {
            if (!suspendUpdates) LocationDescriptionChanged(this, null);
        }
        
        private void txtElevation_TextChanged(object sender, EventArgs e)
        {
            if (!suspendUpdates) LocationDescriptionChanged(this, null);
        }

        private void comboBoxDirection_DataChanged(object sender, EventArgs e)
        {
            if (!suspendUpdates) LocationDescriptionChanged(this, null);
        }

        private void txtDistance_TextChanged(object sender, EventArgs e)
        {
            if (!suspendUpdates) LocationDescriptionChanged(this, null);
        }

        private void floatvalidation_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

            // only allow one "-"
            if ((e.KeyChar == '-') && ((sender as TextBox).Text.IndexOf('-') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtFloor_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != 'g') && (e.KeyChar != 'G'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == 'g') && ((sender as TextBox).Text.IndexOf('g') > -1))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == 'G') && ((sender as TextBox).Text.IndexOf('G') > -1))
            {
                e.Handled = true;
            }

        }

        private void txtDistance_KeyPress(object sender, KeyPressEventArgs e)
        {
             
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void setLocation()
        {

            locationDescription = string.Empty;

            if (!string.IsNullOrEmpty(txtFloor.Text) || !string.IsNullOrEmpty(txtElevation.Text))
                locationDescription += txtFloor.Text + "/" + txtElevation.Text + " ";

            if (!string.IsNullOrEmpty(comboBoxEquipment.Text))
                locationDescription += comboBoxEquipment.Text + " ";

            if (!string.IsNullOrEmpty(comboBoxDirection.Text))
                if (comboBoxDirection.SelectedItem != null) 
                    locationDescription += ((KeyValuePair<string, string>)comboBoxDirection.SelectedItem).Key + " ";
                else
                    locationDescription += comboBoxDirection.Text + " ";
            if (!string.IsNullOrEmpty(txtDistance.Text))
                locationDescription += txtDistance.Text + "M ";
        }

        private void comboBoxEquipment_TextChanged(object sender, EventArgs e)
        {
            if (!suspendUpdates) LocationDescriptionChanged(this, null);
        }
    }
}
