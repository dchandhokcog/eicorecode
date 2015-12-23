using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EnvInt.Win32.FieldTech.Data;

namespace EnvInt.Win32.FieldTech.Migrate.Forms
{
    public partial class CreateLocalDatabase : Form
    {

        public string RecipeFile = string.Empty;
        public string TargetFile = string.Empty;
        public string Connection = string.Empty;
        
        public CreateLocalDatabase()
        {
            InitializeComponent();

            DBConnectionManager dbc = new DBConnectionManager();
            dbc.loadConnections();

            comboBoxConnection.Items.Clear();

            foreach (DBConnection conect in dbc.ConnectionList)
            {
                comboBoxConnection.Items.Add(conect.ConnectionName);
            }

            if (comboBoxConnection.Items.Count > 0) comboBoxConnection.SelectedIndex = 0;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBoxRecipe.Text))
            {
                MessageBox.Show("Recipe File is empty!");
                return;
            }

            if (string.IsNullOrEmpty(textBoxDestinationDB.Text))
            {
                MessageBox.Show("Destination File is empty!");
                return;
            }
            
            
            RecipeFile = textBoxRecipe.Text;
            TargetFile = textBoxDestinationDB.Text;
            Connection = comboBoxConnection.Text;
            
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
         
        private void buttonSelectRecipe_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Multiselect = false;
            of.Filter = "XML Recipe File (*.xml)|*.xml";

            DialogResult dr = of.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                textBoxRecipe.Text = of.FileName;
            }
        }

        private void buttonSelectDestination_Click(object sender, EventArgs e)
        {
            SaveFileDialog of = new SaveFileDialog();
            of.Filter = "SQLite Files (*.s3db)|*.s3db";

            DialogResult dr = of.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                textBoxDestinationDB.Text = of.FileName;
            }

        }
    }
}
