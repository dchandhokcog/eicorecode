using System;
using System.IO;
using System.Xml;
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
    public partial class FormOldTags : Form
    {
        BindingSource BindingSourceOldTags = new BindingSource();

        public FormOldTags()
        {
            InitializeComponent();
        }

        private void FormOldTags_Load(object sender, EventArgs e)
        {
            if (Globals.CurrentProjectData != null)
            {
                BindingSourceOldTags.DataSource = Globals.CurrentProjectData.LDARData.ExistingComponents;
                this.dataGridView1.DataSource = BindingSourceOldTags;
                this.lblNoTagsLoaded.Visible = false;
            }

        }

        /*private void btnImportOldTags_Click(object sender, EventArgs e)
        {

            bool reloadResult = true;

            if (dataGridView1.ColumnCount > 0)
            {
                DialogResult dgResult = MessageBox.Show("Old tags have already been loaded, do you want to clear them and load a different file?", "Confirm", MessageBoxButtons.YesNo);
                if (dgResult == DialogResult.Yes)
                {
                    //Globals.dsOldTags.Tables.Remove("OldTags");
                }
                else
                {
                    reloadResult = false;
                }
            }

            if (reloadResult)
            {
                string fName = string.Empty;

                OpenFileDialog fd = new OpenFileDialog();
                fd.Filter = "XML Files|*.xml";
                fd.Multiselect = false;

                DialogResult dr = fd.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.lblNoTagsLoaded.Text = "Please Wait...";
                    this.Refresh();
                    BindingSourceOldTags.DataSource = Globals.CurrentProjectData.LDARData.ExistingComponents;
                    this.dataGridView1.DataSource = BindingSourceOldTags;
                    this.lblNoTagsLoaded.Visible = false;
                    this.Cursor = Cursors.Default;
                }
            }

        }/*
        /* Preserving this "import excel xml" stuff for posterity
        private DataTable getLDSheet1(string fName)
        {
            DataTable dt = new DataTable();
            dt.TableName = "OldTags";
            DataSet tempDS = new DataSet();

            try
            {
                tempDS.ReadXml(fName);
            }
            catch (Exception e)
            {
                MessageBox.Show("Unable to read file:" + e.Message);
                return dt;
            }

            if (tempDS.Tables.Count == 0)
            {
                MessageBox.Show("Invalid data in selected file");
                return dt;
            }

            int colCount = tempDS.Tables["Column"].Rows.Count;

            //add columns from header row
            try
            {
                for (int i = 1; i <= colCount; i++)
                {
                    dt.Columns.Add(tempDS.Tables["Data"].Rows[i].ItemArray[1].ToString());
                }

                //add data from remaining rows
                List<string> rowItems = new List<string>();
                for (int i = colCount + 1; i < tempDS.Tables["Data"].Rows.Count; i++)
                {
                    if (i % colCount != 0)
                    {
                        rowItems.Add(tempDS.Tables["Data"].Rows[i].ItemArray[1].ToString());
                    }
                    else
                    {
                        dt.Rows.Add(rowItems.ToArray());
                        rowItems.Clear();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occurred during import: " + e.Message);
            }

            
            return dt;
        } */
    }
}