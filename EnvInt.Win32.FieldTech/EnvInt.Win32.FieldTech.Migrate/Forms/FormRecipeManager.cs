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
    
    public partial class FormRecipeManager : Form
    {

        DatabaseRecipe currentRecipe;
        string currentFile = string.Empty;
        
        string FormTitle = "Import Recipe Manager: ";

        public FormRecipeManager()
        {
            InitializeComponent();
        }

        private void FormRecipeManager_Load(object sender, EventArgs e)
        {
            textBoxDescription.Enabled = false;
            textBoxRecipeName.Enabled = false;
            tabControl1.Enabled = false;
            textBoxVersion.Enabled = false;
            textBoxInputTable.Enabled = false;
            toolStripButtonSave.Enabled = false;
            this.Text = FormTitle + "(None)";
            comboBoxDatabaseType.DataSource = Enum.GetNames(typeof(DBConnectionType));
        }

        private void toolStripButtonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();

            of.DefaultExt = "*.xml";
            of.Multiselect = false;
            of.Filter = "XML Recipe Files (*.xml)|*.xml";

            DialogResult dr = of.ShowDialog();

            try
            {
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    currentRecipe = (DatabaseRecipe)DataSetSQLExport.LoadObjectFromXMLFile<DatabaseRecipe>(of.FileName);
                    setFormValues();
                    currentFile = of.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            applyFormValues();
            if (currentFile == string.Empty)
            {
                SaveFileDialog sf = new SaveFileDialog();
                sf.Filter = "XML Recipe Files (*.xml)|*.xml";
                DialogResult dr = sf.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    currentFile = sf.FileName;
                }
                else
                {
                    return;
                }
            }

            string errMsg = DataSetSQLExport.SaveObjectAsXMLFile<DatabaseRecipe>(currentRecipe, currentFile);

            if (errMsg != string.Empty)
            {
                MessageBox.Show(errMsg);
            }
        }

        private void toolStripButtonNew_Click(object sender, EventArgs e)
        {
            currentRecipe = new DatabaseRecipe();
            currentRecipe.RecipeName = "New Recipe";
            setFormValues();
            comboBoxDatabaseType.Text = "SQLServer";
            currentFile = string.Empty;
        }

        private void applyFormValues()
        {
            currentRecipe.RecipeName = textBoxRecipeName.Text;
            currentRecipe.RecipeDescription = textBoxDescription.Text;
            currentRecipe.InputTableName = textBoxInputTable.Text;
            currentRecipe.RecipeVersion = textBoxVersion.Text;
            try
            {
                currentRecipe.TargetDatabaseType = (DBConnectionType)Enum.Parse(typeof(DBConnectionType), comboBoxDatabaseType.Text);
            }
            catch { }
 
        }

        private void setFormValues()
        {
            textBoxRecipeName.Text = currentRecipe.RecipeName;
            textBoxDescription.Text = currentRecipe.RecipeDescription;
            textBoxVersion.Text = currentRecipe.RecipeVersion;
            textBoxInputTable.Text = currentRecipe.InputTableName;
            dataGridViewTables.DataSource = currentRecipe.TableList;
            dataGridViewViews.DataSource = currentRecipe.SQLList;
            dataGridViewDestinationTables.DataSource = currentRecipe.DestinationTableList;
            dataGridViewExtraFields.DataSource = currentRecipe.ExtraFieldsList;
            dataGridViewInputFields.DataSource = currentRecipe.InputFieldDefinitions;
            dataGridViewTables.AllowUserToAddRows = true;

            dataGridViewInputFields.AutoResizeColumns();
            dataGridViewTables.AutoResizeColumns();
            dataGridViewViews.AutoResizeColumns();

            textBoxDescription.Enabled = true;
            textBoxRecipeName.Enabled = true;
            tabControl1.Enabled = true;
            toolStripButtonSave.Enabled = true;
            textBoxInputTable.Enabled = true;
            textBoxVersion.Enabled = true;
            this.Text = FormTitle + currentRecipe.RecipeName;
        }

        private void textBoxRecipeName_TextChanged(object sender, EventArgs e)
        {
            this.Text = FormTitle + textBoxRecipeName.Text;
        }

        private void toolStripButtonLoadCSV_Click(object sender, EventArgs e)
        {
            DialogResult confirmResult = MessageBox.Show("Load " + tabControl1.SelectedTab.Text + " From CSV?  This will clear all existing records!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult == System.Windows.Forms.DialogResult.Yes)
            {
                OpenFileDialog of = new OpenFileDialog();
                of.Filter = "CSV " + tabControl1.SelectedTab.Text + " List (*.txt)|*.txt";
                of.Multiselect = false;
                DialogResult fileResult = of.ShowDialog();

                if (fileResult == System.Windows.Forms.DialogResult.OK)
                {
                    switch (tabControl1.SelectedTab.Text)
                    {
                        case "Tables":
                            currentRecipe.getTablesFromCSV(of.FileName);
                            break;
                        case "Views":
                            currentRecipe.getSQLFromCSV(of.FileName);
                            break;
                        case "Relations":
                            currentRecipe.getFieldDefinitionsFromCSV(of.FileName);
                            break;
                    }
                }
            }
        }

        private void toolStripButtonSaveCSV_Click(object sender, EventArgs e)
        {

            DataSetFileExport fileExport = new DataSetFileExport();

            SaveFileDialog of = new SaveFileDialog();
            of.Filter = "CSV " + tabControl1.SelectedTab.Text + " List (*.txt)|*.txt";
            of.FileName = textBoxRecipeName.Text + "_" + tabControl1.SelectedTab.Text + ".txt";
            DialogResult fileResult = of.ShowDialog();

            if (fileResult == System.Windows.Forms.DialogResult.OK)
            {
                switch (tabControl1.SelectedTab.Text)
                {
                    case "Tables":
                        fileExport.DataTable2txt(currentRecipe.TableList, of.FileName, '\t');
                        break;
                    case "Views":
                        fileExport.DataTable2txt(currentRecipe.SQLList, of.FileName, '\t');
                        break;
                    case "Relations":
                        fileExport.DataTable2txt(currentRecipe.InputFieldDefinitions, of.FileName, '\t');
                        break;
                }
            }
        }
    }
}
