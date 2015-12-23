using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EnvInt.Win32.FieldTech.Common;
using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Dialogs
{
    public partial class FormDuplicateSelection : Form
    {
        public List<LDARComponent> _dups { get; set; }
        public LDARComponent _selectedTag { get; set; }
        
        public FormDuplicateSelection()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {

            if (this.dataGridView1.SelectedRows.Count == 1)
            {
                _selectedTag = _dups.Where(c => c.Id == (int)dataGridView1.SelectedRows[0].Cells["Id"].Value).FirstOrDefault();
                
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Please select a tag to use"); 
            }
        }

        private void FormDuplicateSelection_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Globals.GetObjectListAsTable(_dups);
        }
    }
}
