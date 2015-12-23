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
    public partial class DialogViewTags : Form
    {
        MainForm _currentMainForm;
        DataView _currentDataView = new DataView();

        public DialogViewTags()
        {
            InitializeComponent();
            LoadComponents();
        }

        private void LoadComponents()
        {
            
            if (Globals.CurrentProjectData.LDARDatabaseType == "LeakDAS")
            {

                dataGridView.DataSource = LocalData.GetComponentsAsTable(string.Empty, string.Empty, string.Empty);
                //_currentDataView = LocalData.GetComponentsAsTable(string.Empty, string.Empty, string.Empty);
                foreach (DataGridViewColumn dc in dataGridView.Columns)
                {
                    dc.Visible = false;
                }
                dataGridView.Columns["LDARTag"].Visible = true;
                dataGridView.Columns["LDARTag"].HeaderText = "New Tag";
                dataGridView.Columns["LDARTag"].DisplayIndex = 0;
                dataGridView.Columns["PreviousTag"].Visible = true;
                dataGridView.Columns["PreviousTag"].HeaderText = "Old Tag";
                dataGridView.Columns["PreviousTag"].DisplayIndex = 1;
                dataGridView.Columns["ComponentType"].Visible = true;
                dataGridView.Columns["ComponentType"].HeaderText = "Type";
                dataGridView.Columns["Location"].Visible = true;
                dataGridView.Columns["Size"].Visible = true;
                dataGridView.Columns["Drawing"].Visible = true;
                dataGridView.Columns["RouteSequence"].Visible = true;
                dataGridView.Columns["RouteSequence"].HeaderText = "Route";
                DataGridViewCellStyle routeStyle = new DataGridViewCellStyle();
                routeStyle.Format = "N5";
                dataGridView.Columns["RouteSequence"].DefaultCellStyle = routeStyle;

                textBoxEquipment.Visible = false;
                labelEquipment.Visible = false;

            }
            else
            {

                dataGridView.DataSource = LocalData.GetComponentsAsTable(string.Empty, string.Empty, string.Empty);
                foreach (DataGridViewColumn dc in dataGridView.Columns)
                {
                    dc.Visible = false;
                }
                dataGridView.Columns["LDARTag"].Visible = true;
                dataGridView.Columns["LDARTag"].HeaderText = "New Tag";
                dataGridView.Columns["LDARTag"].DisplayIndex = 0;
                dataGridView.Columns["Extension"].Visible = true;
                dataGridView.Columns["Extension"].DisplayIndex = 1;
                dataGridView.Columns["PreviousTag"].Visible = true;
                dataGridView.Columns["PreviousTag"].HeaderText = "Old Tag";
                dataGridView.Columns["PreviousTag"].DisplayIndex = 2;
                dataGridView.Columns["PreviousTagExtension"].Visible = true;
                dataGridView.Columns["PreviousTagExtension"].DisplayIndex = 3;
                dataGridView.Columns["ComponentType"].Visible = true;
                dataGridView.Columns["ComponentType"].HeaderText = "Type";
                dataGridView.Columns["Location"].Visible = true;
                dataGridView.Columns["Size"].Visible = true;
                dataGridView.Columns["Drawing"].Visible = true;
                dataGridView.Columns["RouteSequence"].Visible = true;
                dataGridView.Columns["RouteSequence"].HeaderText = "Route";
                dataGridView.Columns["Equipment"].Visible = true;
                DataGridViewCellStyle routeStyle = new DataGridViewCellStyle();
                routeStyle.Format = "N5";
                dataGridView.Columns["RouteSequence"].DefaultCellStyle = routeStyle;

                textBoxEquipment.Visible = true;
                labelEquipment.Visible = true;

               
            }
            //check for Chinese version
            if (Globals.isProductChinese)
            {
                dataGridView.Columns["LDARTag"].HeaderText = "新标签";
                dataGridView.Columns["Extension"].HeaderText = "延期";
                dataGridView.Columns["ComponentType"].HeaderText = "类型";
                dataGridView.Columns["Location"].HeaderText = "地点";
                dataGridView.Columns["Drawing"].HeaderText = "画画";
                dataGridView.Columns["Size"].HeaderText = "尺寸";
                dataGridView.Columns["Equipment"].HeaderText = "设备";
                dataGridView.Columns["RouteSequence"].HeaderText = "航线";
            }

        }


        private void launchEditForm()
        {
            if (this.dataGridView.SelectedRows.Count > 0)
            {

                //can't open a child
                //if (listViewGWTags.SelectedItems[0].Tag.ToString().StartsWith("ChildComponent")) return;

                TaggedComponent c = LocalData.GetComponent(dataGridView.SelectedRows[0].Cells[0].Value.ToString());
                if (c != null)
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
                    LoadComponents();
                    ((MainForm)Tag).UpdateLocalComponentCount();
                }
                else
                {
                    MessageBox.Show("Can't Find Component.");
                }
            }
        }

        private void dataGridViewGuideware_DoubleClick(object sender, EventArgs e)
        {
            launchEditForm();
        }

        private void dataGridViewLeakDAS_DoubleClick(object sender, EventArgs e)
        {
            launchEditForm();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView.DataSource = LocalData.GetComponentsAsTable(textBoxTagFilter.Text, textBoxLocationFilter.Text, textBoxEquipment.Text);
        }

        private void textBoxLocationFilter_TextChanged(object sender, EventArgs e)
        {
            dataGridView.DataSource = LocalData.GetComponentsAsTable(textBoxTagFilter.Text, textBoxLocationFilter.Text, textBoxEquipment.Text);
        }

        private void textBoxEquipment_TextChanged(object sender, EventArgs e)
        {
            dataGridView.DataSource = LocalData.GetComponentsAsTable(textBoxTagFilter.Text, textBoxLocationFilter.Text, textBoxEquipment.Text);
        }
    }
}
