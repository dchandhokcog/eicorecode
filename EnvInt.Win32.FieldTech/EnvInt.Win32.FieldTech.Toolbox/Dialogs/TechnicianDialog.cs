using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Dialogs
{
    public partial class TechnicianDialog : Form
    {

        public string selectedTech
        {
            get { return listBoxTechnicians.SelectedItem.ToString(); }
            set { listBoxTechnicians.SelectedItem = value; }
        }
        
        public TechnicianDialog()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (listBoxTechnicians.SelectedItems.Count > 0)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Please select a Technician first.");
            }

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void TechnicianDialog_Load(object sender, EventArgs e)
        {
            fillTechnicians();
        }

        private void fillTechnicians()
        {
            listBoxTechnicians.Items.Clear();

            if (Globals.CurrentProjectData != null)
            {
                foreach (LDARTechnician tech in Globals.CurrentProjectData.LDARData.Technicians)
                {
                    if (tech.showInTablet) listBoxTechnicians.Items.Add(tech.Name);
                }
            }
        }

        private void buttonAddTechnician_Click(object sender, EventArgs e)
        {
            string newTechName = string.Empty;
            DialogResult dr = Globals.InputBox("Enter Technician Name", "Enter Technician Name", ref newTechName);
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                LDARTechnician newTech = new LDARTechnician();
                newTech.Id = 0;
                newTech.Name = newTechName;
                newTech.showInTablet = true;
                Globals.CurrentProjectData.LDARData.Technicians.Add(newTech);
            }
            fillTechnicians();

        }

        private void buttonDeleteTechnician_Click(object sender, EventArgs e)
        {
            if (listBoxTechnicians.SelectedItems.Count > 0)
            {
                try
                {
                    Globals.CurrentProjectData.LDARData.Technicians.Remove(Globals.CurrentProjectData.LDARData.Technicians.Where(c => c.Name == listBoxTechnicians.SelectedItem.ToString()).FirstOrDefault());
                }
                catch { }
            }
            else
            {
                MessageBox.Show("Please select a technician to remove.");
            }

            fillTechnicians();
        }
    }
}
