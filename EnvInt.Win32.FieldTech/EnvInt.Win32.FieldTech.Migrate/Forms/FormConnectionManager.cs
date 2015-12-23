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
    public partial class FormConnectionManager : Form
    {
        public DBConnectionManager connMan = new DBConnectionManager();
        public DBConnection currentConnection = new DBConnection();
        
        public FormConnectionManager()
        {
            InitializeComponent();

            connMan.loadConnections();
            setFormValues(true);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormConnectionManager_Activated(object sender, EventArgs e)
        {
        }

        private void setFormValues(bool refreshList = false)
        {
            if (currentConnection != null)
            {
                textBoxConnectionName.Text = currentConnection.ConnectionName;
                textBoxDatabase.Text = currentConnection.Database;
                textBoxFile.Text = currentConnection.FileLocation;
                textBoxPassword.Text = currentConnection.Password;
                textBoxServer.Text = currentConnection.Server;
                textBoxUser.Text = currentConnection.UserName;
                comboBoxConnectionType.Text = Enum.GetName(typeof(DBConnectionType), currentConnection.ConnectionType);
                checkBoxTrustedConnection.Checked = currentConnection.UseTrustedConnection;

                if (refreshList)
                {
                    listBoxConnections.Items.Clear();

                    foreach (DBConnection dbc in connMan.ConnectionList)
                    {
                        listBoxConnections.Items.Add(dbc.ConnectionName);
                    }
                }
            }
        }

        private void applyFormValues()
        {
            if (currentConnection != null)
            {

                connMan.ConnectionList.Remove(currentConnection);

                currentConnection = new DBConnection();
                
                currentConnection.ConnectionName = textBoxConnectionName.Text;
                currentConnection.Database = textBoxDatabase.Text;
                currentConnection.FileLocation = textBoxFile.Text;
                currentConnection.Password = textBoxPassword.Text;
                currentConnection.Server = textBoxServer.Text;
                currentConnection.UserName = textBoxUser.Text;
                try
                {
                    currentConnection.ConnectionType = (DBConnectionType)Enum.Parse(typeof(DBConnectionType), comboBoxConnectionType.Text);
                }
                catch { }
                currentConnection.UseTrustedConnection = checkBoxTrustedConnection.Checked;

                connMan.ConnectionList.Add(currentConnection);

                listBoxConnections.Items.Clear();
                foreach (DBConnection dbc in connMan.ConnectionList)
                {
                    listBoxConnections.Items.Add(dbc.ConnectionName);
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                connMan.ConnectionList.Remove(connMan.getConnectionByName(listBoxConnections.SelectedItem.ToString()));
            }
            catch { }

            setFormValues(true);

            if (listBoxConnections.Items.Count == 0)
            {
                buttonNew_Click(this, null);
            }
            else
            {
                listBoxConnections.SelectedIndex = 0;
            }


        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            DBConnection newDBC = new DBConnection();

            newDBC.ConnectionName = "New Connection";
            newDBC.ConnectionType = DBConnectionType.SQLite;
            connMan.ConnectionList.Add(newDBC);
            textBoxConnectionName.Text = "New Connection";
            setFormValues(true);
            listBoxConnections.SelectedItem = newDBC.ConnectionName;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            applyFormValues();
            connMan.saveConnections();
            listBoxConnections.SelectedItem = currentConnection.ConnectionName;
        }

        private void listBoxConnections_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                currentConnection = connMan.getConnectionByName(listBoxConnections.SelectedItem.ToString());
                setFormValues();
            }
            catch { }
        }

        private void buttonSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();

            DialogResult dr = of.ShowDialog();

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                textBoxFile.Text = of.FileName;
            }
        }

        private void comboBoxConnectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxConnectionType.Text == "SQLServer")
            {
                textBoxFile.Enabled = false;
                textBoxUser.Enabled = true;
                textBoxDatabase.Enabled = true;
                textBoxServer.Enabled = true;
                checkBoxTrustedConnection.Enabled = true;
            }
            else
            {
                textBoxFile.Enabled = true;
                textBoxUser.Enabled = false;
                textBoxDatabase.Enabled = false;
                textBoxServer.Enabled = false;
                checkBoxTrustedConnection.Enabled = false; 
            }
        }

        private void checkBoxTrustedConnection_CheckedChanged(object sender, EventArgs e)
        {

            if (comboBoxConnectionType.Text != "SQLServer") return;
            
            if (checkBoxTrustedConnection.Checked)
            {
                textBoxUser.Enabled = false;
                textBoxPassword.Enabled = false;
            }
            else
            {
                textBoxUser.Enabled = true;
                textBoxPassword.Enabled = true;
            }
        }

        private void FormConnectionManager_Load(object sender, EventArgs e)
        {
            comboBoxConnectionType.DataSource = Enum.GetNames(typeof(DBConnectionType));

            if (connMan.ConnectionList.Count == 0)
            {
                DBConnection newDBC = new DBConnection();

                newDBC.ConnectionName = "New Connection";
                newDBC.InUse = true;
                newDBC.ConnectionType = DBConnectionType.SQLite;
                connMan.ConnectionList.Add(newDBC);
                textBoxConnectionName.Text = "New Connection";
            }

            currentConnection = connMan.getCurrentConnection();
            setFormValues(true);
            listBoxConnections.SelectedIndex = 0;
        }

    }
}
