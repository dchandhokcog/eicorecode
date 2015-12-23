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
using EnvInt.Win32.FieldTech.Data;

namespace EnvInt.Win32.FieldTech
{
    public partial class FormOldTags : Form
    {
        BindingSource BindingSourceOldTags = new BindingSource();
        MainForm _CurrentMainForm;

        public FormOldTags(MainForm mainForm)
        {
            InitializeComponent();
            _CurrentMainForm = mainForm;
        }

        private void FormOldTags_Load(object sender, EventArgs e)
        {
            if (Globals.CurrentProjectData != null)
            {
                //BindingSourceOldTags.DataSource = Globals.CurrentProjectData.LDARData.ExistingComponents;
                //this.dataGridView1.DataSource = BindingSourceOldTags;
                //this.lblNoTagsLoaded.Visible = false;
                DataTable dt = Globals.GetObjectListAsTable<LDARComponent>(Globals.CurrentProjectData.LDARData.ExistingComponents);

                if (dt.Rows.Count == 0) return;

                BindingSourceOldTags.DataSource = dt;
                this.dataGridView1.DataSource = BindingSourceOldTags;
                this.lblNoTagsLoaded.Visible = false;
            }

            for (int i = 0; i < dataGridView1.Columns.Count; i++ )
            {
                dataGridView1.Columns[i].Visible = false;
                dataGridView1.Columns[i].Width = 100;

            }


            if (Globals.CurrentProjectData.LDARDatabaseType == "Guideware")
            {
                string location1Name = string.Empty;
                string location2Name = string.Empty;
                string location3Name = string.Empty;
                textBoxEquipment.Visible = true;
                labelEquipment.Visible = true;

                try
                {
                    location1Name = Globals.CurrentProjectData.LDARData.LDAROptions.Where(c => c.OptionName == "Location1Name").FirstOrDefault().OptionValue.ToString();
                    location2Name = Globals.CurrentProjectData.LDARData.LDAROptions.Where(c => c.OptionName == "Location2Name").FirstOrDefault().OptionValue.ToString();
                    location3Name = Globals.CurrentProjectData.LDARData.LDAROptions.Where(c => c.OptionName == "Location3Name").FirstOrDefault().OptionValue.ToString();
                }
                catch
                { }
                
                dataGridView1.Columns["ComponentTag"].Visible = true;
                dataGridView1.Columns["TagExtension"].Visible = true;
                dataGridView1.Columns["Location1"].Visible = true;
                if (string.IsNullOrEmpty(location1Name))
                {
                    dataGridView1.Columns["Location1"].HeaderText = location1Name;
                }
                else
                {
                    dataGridView1.Columns["Location1"].HeaderText = "Unit";
                }

                dataGridView1.Columns["Location2"].Visible = true;
                if (string.IsNullOrEmpty(location2Name))
                {
                    dataGridView1.Columns["Location2"].HeaderText = location2Name;
                }
                else
                {
                    dataGridView1.Columns["Location2"].HeaderText = "Area";
                }
                dataGridView1.Columns["Location3"].Visible = true;
                if (string.IsNullOrEmpty(location3Name))
                {
                    dataGridView1.Columns["Location3"].HeaderText = location3Name;
                }
                else
                {
                    dataGridView1.Columns["Location3"].HeaderText = "Equipment";
                }
                dataGridView1.Columns["LocationDescription"].Visible = true;
                dataGridView1.Columns["Size"].Visible = true;
                dataGridView1.Columns["ComponentClassId"].Visible = true;
                dataGridView1.Columns["ComponentClassId"].HeaderText = "Type";
                dataGridView1.Columns["RouteSequence"].Visible = true;

                //Chinese version changes
                if (Globals.isProductChinese)
                {
                    dataGridView1.Columns["ComponentTag"].HeaderText = "成分标签";
                    dataGridView1.Columns["TagExtension"].HeaderText = "标签扩展";
                    dataGridView1.Columns["LocationDescription"].HeaderText = "位置说明";
                    dataGridView1.Columns["Size"].HeaderText = "尺寸";
                    dataGridView1.Columns["Location1"].HeaderText = "单元";
                    dataGridView1.Columns["Location2"].HeaderText = "区域";
                    dataGridView1.Columns["Location3"].HeaderText = "设备";
                    dataGridView1.Columns["ComponentClassId"].HeaderText = "类型";
                    dataGridView1.Columns["RouteSequence"].HeaderText = "路径序列";
                }
            }
            else
            {
                textBoxEquipment.Visible = false;
                labelEquipment.Visible = false;
                dataGridView1.Columns["ComponentTag"].Visible = true;
                dataGridView1.Columns["ProcessUnitId"].Visible = true;
                dataGridView1.Columns["ProcessUnitId"].HeaderText = "Unit";
                dataGridView1.Columns["AreaId"].Visible = true;
                dataGridView1.Columns["AreaId"].HeaderText = "Area";
                dataGridView1.Columns["LocationDescription"].Visible = true;
                dataGridView1.Columns["Size"].Visible = true;
                dataGridView1.Columns["ComponentClassId"].Visible = true;
                dataGridView1.Columns["ComponentClassId"].HeaderText = "Type";
                dataGridView1.Columns["RouteSequence"].Visible = true;
           }

            dataGridView1.Columns["POS"].Visible = true;

            dataGridView1.Columns["LocationDescription"].Width = 170;
            dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(CellFormatting);

        }

        private void CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("ProcessUnitId"))
            {
                // Use helper method to get the string from lookup table
                if (dataGridView1.Rows[e.RowIndex].Cells["ProcessUnitId"].Value.ToString() != "")
                    e.Value = Globals.CurrentProjectData.LDARData.ProcessUnits.Where(c => c.ProcessUnitId == (int)dataGridView1.Rows[e.RowIndex].Cells["ProcessUnitId"].Value).FirstOrDefault().UnitDescription;
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("AreaID"))
            {
                // Use helper method to get the string from lookup table
                System.Data.SqlTypes.SqlInt32 area = (System.Data.SqlTypes.SqlInt32)dataGridView1.Rows[e.RowIndex].Cells["AreaID"].Value;
                if (area.IsNull)
                {
                    e.Value = "";
                }
                else
                {
                     e.Value = Globals.CurrentProjectData.LDARData.Areas.Where(c => c.AreaId == int.Parse(area.ToString())).FirstOrDefault().AreaDescription;
                }
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("ComponentClassId"))
            {
                // Use helper method to get the string from lookup table
                System.Data.SqlTypes.SqlInt32 compClass = (System.Data.SqlTypes.SqlInt32)dataGridView1.Rows[e.RowIndex].Cells["ComponentClassId"].Value;
                if (compClass.IsNull)
                {
                    e.Value = "";
                }
                else
                {
                    LDARComponentClassType ct = Globals.CurrentProjectData.LDARData.ComponentClassTypes.Where(c => c.ComponentClassId == int.Parse(compClass.ToString())).FirstOrDefault();
                    if (ct == null)
                    {
                        e.Value = "";
                    }
                    else
                    {
                        e.Value = ct.ClassDescription;
                    }
                }
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("Location1"))
            {
                // Use helper method to get the string from lookup table
                e.Value = Globals.CurrentProjectData.LDARData.ProcessUnits.Where(c => c.UnitCode == dataGridView1.Rows[e.RowIndex].Cells["Location1"].Value.ToString()).FirstOrDefault().UnitDescription;
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("Location2"))
            {
                // Use helper method to get the string from lookup table
                e.Value = Globals.CurrentProjectData.LDARData.Areas.Where(c => c.AreaCode == dataGridView1.Rows[e.RowIndex].Cells["Location2"].Value.ToString()).FirstOrDefault().AreaCode;
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("Location3"))
            {
                // Use helper method to get the string from lookup table
                e.Value = Globals.CurrentProjectData.LDARData.Equipment.Where(c => c.EquipmentCode == dataGridView1.Rows[e.RowIndex].Cells["Location3"].Value.ToString()).FirstOrDefault().EquipmentCode;
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("POS"))
            {
                // Use helper method to get the string from lookup table
                if (bool.Parse(dataGridView1.Rows[e.RowIndex].Cells["POS"].Value.ToString()))
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }

        }

        private void textBoxLocationFilter_TextChanged(object sender, EventArgs e)
        {
            setFilter();
        }

        private void textBoxTagFilter_TextChanged(object sender, EventArgs e)
        {
            setFilter();
        }

        private void textBoxEquipment_TextChanged(object sender, EventArgs e)
        {
            setFilter();
        }

        private void setFilter()
        {

           string filterElements = string.Empty;

            if (!string.IsNullOrEmpty(textBoxTagFilter.Text)) filterElements += "Tag";
            if (!string.IsNullOrEmpty(textBoxLocationFilter.Text)) filterElements += "Location";
            if (!string.IsNullOrEmpty(textBoxEquipment.Text)) filterElements += "Equipment";

            switch (filterElements)
            {
                case "":
                    BindingSourceOldTags.Filter = "";
                    BindingSourceOldTags.RemoveFilter();
                    break;
                case "Tag":
                    BindingSourceOldTags.Filter = "ComponentTag Like '*" + textBoxTagFilter.Text.ToUpper() + "*'";
                    break;
                case "Location":
                    BindingSourceOldTags.Filter = "LocationDescription Like '*" + textBoxLocationFilter.Text.ToUpper() + "*'";
                    break;
                case "Equipment":
                    BindingSourceOldTags.Filter = "Location3 Like '*" + textBoxEquipment.Text.ToUpper() + "*'";
                    break;
                case "TagLocation":
                    BindingSourceOldTags.Filter = "LocationDescription Like '*" + textBoxLocationFilter.Text.ToUpper() + "*' And ComponentTag Like '*" + textBoxTagFilter.Text.ToUpper() + "*'";
                    break;
                case "TagEquipment":
                    BindingSourceOldTags.Filter = "ComponentTag Like '*" + textBoxTagFilter.Text.ToUpper() + "*' And Location3 Like '*" + textBoxEquipment.Text.ToUpper() + "*'";
                    break;
                case "LocationEquipment":
                    BindingSourceOldTags.Filter = "LocationDescription Like '*" + textBoxLocationFilter.Text.ToUpper() + "*' And Location3 Like '*" + textBoxEquipment.Text.ToUpper() + "*'";
                    break;
                case "TagLocationEquipment":
                    BindingSourceOldTags.Filter = "ComponentTag Like '*" + textBoxTagFilter.Text.ToUpper() + "*' And LocationDescription Like '*" + textBoxLocationFilter.Text.ToUpper() + "*' And Location3 Like '*" + textBoxEquipment.Text.ToUpper() + "*'";
                    break;
            }
            
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (dataGridView1.SelectedRows[0].Cells["POS"].Value.ToString() == "True")
            {
                MessageBox.Show("Cannot edit an old tag that has been permanently removed from service!");
                return;
            }

            string objectProperties = _CurrentMainForm.isObjectSelected();

            if (!string.IsNullOrEmpty(objectProperties))
            {
                launchTagObjectForm(objectProperties);
            }
            else
            {
                launchEditForm();
            }
        }

        private void launchTagObjectForm(string objectProperties)
        {
            //return tag + '\t' + description + '\t' + stream + '\t' + size;

            string[] opArray = objectProperties.Split('\t');
            string tag = string.Empty;
            string description = string.Empty;
            string stream = string.Empty;
            string size = string.Empty;
            try
            {
                tag = opArray[0];
                description = opArray[1];
                stream = opArray[2];
                size = opArray[3];
            }
            catch { }

            if (!string.IsNullOrEmpty(tag))
            {
                DialogResult dr = MessageBox.Show("An object (" + description + ", Tag: " + tag + ") is selected.  Document it instead?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);

                if (dr == DialogResult.Yes)
                {
                    _CurrentMainForm.toolStripButtonTag_Click(_CurrentMainForm, null);
                }
                else
                {
                    launchEditForm();
                }
            }

        }

        private TaggedComponent getTaggedComponentParent()
        {
            TaggedComponent c;

            string inLDARTag = string.Empty;
            string inExtension = string.Empty;

            if (Globals.CurrentProjectData.LDARDatabaseType == "Guideware")
            {
                inLDARTag = dataGridView1.SelectedRows[0].Cells["ComponentTag"].Value.ToString();
                inExtension = dataGridView1.SelectedRows[0].Cells["TagExtension"].Value.ToString();
                c = LocalData.GetComponents().Where(a => a.LDARTag == inLDARTag && a.Extension == inExtension).FirstOrDefault();
            }
            else
            {
                inLDARTag = dataGridView1.SelectedRows[0].Cells["ComponentTag"].Value.ToString();
                c = LocalData.GetComponents().Where(a => a.LDARTag == inLDARTag).FirstOrDefault();

                if (inLDARTag.Contains('.'))
                {
                    inLDARTag = inLDARTag.Split('.')[0];
                }
            }

            return c;
        }

        private LDARComponent getSelectedOldTag()
        {
            List<LDARComponent> tagFamily;
            LDARComponent selectedTag = new LDARComponent();

            string inLDARTag = string.Empty;
            string inExtension = string.Empty;


            if (Globals.CurrentProjectData.LDARDatabaseType == "Guideware")
            {
                inLDARTag = dataGridView1.SelectedRows[0].Cells["ComponentTag"].Value.ToString();
                inExtension = dataGridView1.SelectedRows[0].Cells["TagExtension"].Value.ToString();

                tagFamily = Globals.CurrentProjectData.LDARData.ExistingComponents.Where(b => b.ComponentTag == inLDARTag).ToList();

                if (tagFamily.Count > 0)
                {
                    List<LDARComponent> tempFamily = tagFamily.OrderBy(d => d.TagExtension).ToList();
                    selectedTag = tempFamily[0];
                }
            }
            else
            {
                inLDARTag = dataGridView1.SelectedRows[0].Cells["ComponentTag"].Value.ToString();

                if (inLDARTag.Contains('.'))
                {
                    inLDARTag = inLDARTag.Split('.')[0];
                }

                tagFamily = Globals.CurrentProjectData.LDARData.ExistingComponents.Where(b => b.ComponentTag.StartsWith(inLDARTag)).ToList();

                if (tagFamily.Count > 0)
                {
                    List<LDARComponent> tempFamily = tagFamily.OrderBy(d => d.ComponentTag).ToList();
                    selectedTag = tagFamily[0];
                }
            }

            return selectedTag;
        }

        private void launchEditForm()
        {

            TaggedComponent c = getTaggedComponentParent();
            LDARComponent oldc = getSelectedOldTag();
            string inLDARTag = oldc.ComponentTag;
            string inExtension = oldc.TagExtension;

            if (c == null)
            {
                if (string.IsNullOrEmpty(Properties.Settings.Default.TargetSite) || Properties.Settings.Default.TargetSite.Contains("Default"))
                {
                    //if we're here, use a form that's appropriate to the project
                    if (Globals.CurrentProjectData.LDARDatabaseType == "Guideware")
                    {

                        if (Globals.isProductChinese)
                        {
                            MainForm._editTag_GW_Chinese._allowOldTagRefresh = true;
                            MainForm._editTag_GW_Chinese.SetComponent(inLDARTag, inExtension);
                            MainForm._editTag_GW_Chinese.ShowDialog();
                            MainForm._editTag_GW_Chinese.Focus();
                        }
                        else
                        {
                            //FormEditTag_GW fe = new FormEditTag_GW(new Point(0, 0));
                            MainForm._editTag_GW_Full._allowOldTagRefresh = true;
                            MainForm._editTag_GW_Full.SetComponent(inLDARTag, inExtension);
                            MainForm._editTag_GW_Full.ShowDialog();
                            MainForm._editTag_GW_Full.Focus();
                        }
                    }
                    else
                    {
                        if (Globals.CurrentProjectData.ProjectType == LDARProjectType.EiMOC)
                        {
                            //FormEditTag_Full fe_full = new FormEditTag_Full(new Point(0, 0));
                            MainForm._editTag_Full._allowOldTagRefresh = true;
                            MainForm._editTag_Full.SetComponent(inLDARTag);
                            MainForm._editTag_Full.ShowDialog();
                        }
                        else
                        {
                            //FormEditTag fe = new FormEditTag(new Point(0, 0));
                            MainForm._editTag._allowOldTagRefresh = false;
                            MainForm._editTag.SetComponent(inLDARTag);
                            MainForm._editTag.ShowDialog();
                        }
                    }
                }
                else
                {
                    //if we're here, a form override has been specified - use that form instead
                    switch (Properties.Settings.Default.TargetSite)
                    {
                        case "TAG":
                            FormEditTag fe = new FormEditTag(new Point(0, 0));
                            fe._allowOldTagRefresh = false;
                            fe.SetComponent(inLDARTag);
                            fe.ShowDialog();
                            break;

                        case "MGV":
                            FormEditTag_Garyville fe_garyville = new FormEditTag_Garyville(new Point(0, 0));
                            fe_garyville._allowOldTagRefresh = false;
                            fe_garyville.SetComponent(inLDARTag);
                            fe_garyville.ShowDialog();
                            break;
                        case "MOC":
                            FormEditTag_Full fe_full = new FormEditTag_Full(new Point(0, 0));
                            fe_full.SetComponent(inLDARTag);
                            fe_full.ShowDialog();
                            break;
                        case "GW":
                            MainForm._editTag_GW_Full._allowOldTagRefresh = true;
                            MainForm._editTag_GW_Full.SetComponent(inLDARTag, inExtension);
                            MainForm._editTag_GW_Full.ShowDialog();
                            MainForm._editTag_GW_Full.Focus();
                            break;
                        case "GW_REVAL":
                            MainForm._editTag_GW._allowOldTagRefresh = true;
                            MainForm._editTag_GW.SetComponent(inLDARTag, inExtension);
                            MainForm._editTag_GW.ShowDialog();
                            MainForm._editTag_GW.Focus();
                            break;
                        case "REVAL":
                            FormEditTag_GW_Reval fe_reval = new FormEditTag_GW_Reval(new Point(0, 0));
                            fe_reval._allowOldTagRefresh = true;
                            fe_reval.SetComponent(inLDARTag, inExtension);
                            fe_reval.ShowDialog();
                            break;
                        default:
                            FormEditTag fe_default = new FormEditTag(new Point(0, 0));
                            fe_default._allowOldTagRefresh = false;
                            fe_default.SetComponent(inLDARTag);
                            fe_default.ShowDialog();
                            break;
                    }
                }

                try
                {
                    ((MainForm)Tag).UpdateLocalComponentCount();
                }
                catch { }
            }
            else
            {
                if (string.IsNullOrEmpty(Properties.Settings.Default.TargetSite) || Properties.Settings.Default.TargetSite.Contains("Default"))
                {
                    //if we're here, use a form that's appropriate to the project
                    if (Globals.CurrentProjectData.LDARDatabaseType == "Guideware")
                    {

                        if (Globals.isProductChinese)
                        {
                            MainForm._editTag_GW_Chinese._allowOldTagRefresh = true;
                            MainForm._editTag_GW_Chinese.SetComponent(c.Id, null, null, null, null, null, true);
                            MainForm._editTag_GW_Chinese.ShowDialog();
                            MainForm._editTag_GW_Chinese.Focus();
                        }
                        else
                        {
                            //FormEditTag_GW fe = new FormEditTag_GW(new Point(0, 0));
                            MainForm._editTag_GW_Full._allowOldTagRefresh = true;
                            MainForm._editTag_GW_Full.SetComponent(c.Id, null, null, null, null, null, true);
                            MainForm._editTag_GW_Full.ShowDialog();
                            MainForm._editTag_GW_Full.Focus();
                        }
                    }
                    else
                    {
                        if (Globals.CurrentProjectData.ProjectType == LDARProjectType.EiMOC)
                        {
                            //FormEditTag_Full fe_full = new FormEditTag_Full(new Point(0, 0));
                            MainForm._editTag_Full._allowOldTagRefresh = true;
                            MainForm._editTag_Full.SetComponent(c.Id, null, null, null, null, null, true);
                            MainForm._editTag_Full.ShowDialog();
                        }
                        else
                        {
                            //FormEditTag fe = new FormEditTag(new Point(0, 0));
                            MainForm._editTag._allowOldTagRefresh = false;
                            MainForm._editTag.SetComponent(c.Id, null, null, null, null, null, true);
                            MainForm._editTag.ShowDialog();
                        }
                    }
                }
                else
                {
                    //if we're here, a form override has been specified - use that form instead
                    switch (Properties.Settings.Default.TargetSite)
                    {
                        case "TAG":
                            FormEditTag fe = new FormEditTag(new Point(0, 0));
                            fe._allowOldTagRefresh = false;
                            fe.SetComponent(c.Id, null, null, null, null, null, true);
                            fe.ShowDialog();
                            break;

                        case "MGV":
                            FormEditTag_Garyville fe_garyville = new FormEditTag_Garyville(new Point(0, 0));
                            fe_garyville._allowOldTagRefresh = false;
                            fe_garyville.SetComponent(c.Id, null, null, null, null, null, true);
                            fe_garyville.ShowDialog();
                            break;
                        case "MOC":
                            FormEditTag_Full fe_full = new FormEditTag_Full(new Point(0, 0));
                            fe_full.SetComponent(c.Id, null, null, null, null, null, true);
                            fe_full.ShowDialog();
                            break;
                        case "GW":
                            MainForm._editTag_GW_Full._allowOldTagRefresh = true;
                            MainForm._editTag_GW_Full.SetComponent(c.Id, null, null, null, null, null, true);
                            MainForm._editTag_GW_Full.ShowDialog();
                            MainForm._editTag_GW_Full.Focus();
                            break;
                        case "GW_REVAL":
                            MainForm._editTag_GW._allowOldTagRefresh = true;
                            MainForm._editTag_GW.SetComponent(c.Id, null, null, null, null, null, true);
                            MainForm._editTag_GW.ShowDialog();
                            MainForm._editTag_GW.Focus();
                            break;
                        case "REVAL":
                            FormEditTag_GW_Reval fe_reval = new FormEditTag_GW_Reval(new Point(0, 0));
                            fe_reval._allowOldTagRefresh = true;
                            fe_reval.SetComponent(c.Id, null, null, null, null, null, true);
                            fe_reval.ShowDialog();
                            break;
                        default:
                            FormEditTag fe_default = new FormEditTag(new Point(0, 0));
                            fe_default._allowOldTagRefresh = false;
                            fe_default.SetComponent(c.Id, null, null, null, null, null, true);
                            fe_default.ShowDialog();
                            break;
                    }
                }

                try
                {
                    ((MainForm)Tag).UpdateLocalComponentCount();
                }
                catch { }
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