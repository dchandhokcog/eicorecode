using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EnvInt.Win32.EiMOC.Data;
using EnvInt.Win32.FieldTech.Containers;

using Community.CsharpSqlite.SQLiteClient;

namespace EnvInt.Win32.EiMOC
{
    public partial class FormEditObject : Form
    {
        public string _currentComponentId = "";
        public string _currentDrawing = "";
        public double _currentBackground = 0;

        public FormEditObject(Point startPosition)
        {
            InitializeComponent();

            string locationSelector = Properties.Settings.Default.LocationSelector.ToLower();
            if (locationSelector == "advanced")
            {
                buttonLocationBuilder.Visible = true;
                textBoxLocation.Width = textBoxLDARTag.Width;
            }
            else
            {
                buttonLocationBuilder.Visible = false;
                //textBoxLocation.Width = comboBoxReason.Left - textBoxLocation.Left - 10;
            }

            if (startPosition != null)
            {
                //this.Top = startPosition.X;
                //this.Left = startPosition.Y;
            }
        }

        public void SetComponent(string id, string engineeringTag, string componentType, string stream, string drawing)
        {
            _currentComponentId = id;

            if (String.IsNullOrEmpty(engineeringTag))
            {
                textBoxEngineeringTag.BackColor = Color.FromKnownColor(KnownColor.Window);
            }
            else
            {
                textBoxEngineeringTag.BackColor = Color.FromKnownColor(KnownColor.Control);
                textBoxEngineeringTag.Text = engineeringTag;
            }

            if (String.IsNullOrEmpty(componentType))
            {
                textBoxComponentType.BackColor = Color.FromKnownColor(KnownColor.Window);
            }
            else
            {
                textBoxComponentType.BackColor = Color.FromKnownColor(KnownColor.Control);
                textBoxComponentType.Text = componentType;
            }

            if (String.IsNullOrEmpty(stream))
            {
                textBoxStream.BackColor = Color.FromKnownColor(KnownColor.Window);
            }
            else
            {
                textBoxStream.BackColor = Color.FromKnownColor(KnownColor.Control);
                textBoxStream.Text = stream;
            }

            if (!String.IsNullOrEmpty(Properties.Settings.Default.LastComponentLocation)) textBoxLocation.Text = Properties.Settings.Default.LastComponentLocation;
            if (!String.IsNullOrEmpty(Properties.Settings.Default.LastComponentLDARTag)) textBoxLDARTag.Text = Properties.Settings.Default.LastComponentLDARTag;

            //comboBoxReason.SelectedIndex = 0;
            comboBoxAccess.SelectedIndex = 0;
            _currentDrawing = drawing;

            //load component   
            TaggedComponent c = LocalData.GetComponent(id);
            if (c != null)
            {
                _currentComponentId = c.Id;
                textBoxLDARTag.Text = c.LDARTag;
                textBoxPreviousTag.Text = c.PreviousTag;
                textBoxLocation.Text = c.Location;
                if (!String.IsNullOrEmpty(c.Access))
                {
                    int i = comboBoxAccess.FindStringExact(c.Access);
                    if (i >= 0) comboBoxAccess.SelectedIndex = i;
                    else comboBoxAccess.SelectedIndex = 0;
                }
                /*
                if (!String.IsNullOrEmpty(c.AccessReason))
                {
                    int i = comboBoxReason.SelectedIndex = comboBoxReason.FindStringExact(c.AccessReason);
                    if (i >= 0) comboBoxReason.SelectedIndex = i;
                    else comboBoxReason.SelectedIndex = 0;
                }*/
                textBoxStream.Text = c.Stream;

                if (c.Children != null)
                {
                    foreach (ChildComponent cc in c.Children)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = cc.ComponentType;
                        item.SubItems.Add(cc.LDARTag);
                        item.SubItems.Add(cc.PreviousTag);
                        item.SubItems.Add(cc.Location);
                        listViewChildren.Items.Add(item);
                    }
                }
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.LastComponentLocation = textBoxLocation.Text;
            Properties.Settings.Default.LastComponentLDARTag = textBoxLDARTag.Text;
            Properties.Settings.Default.Save();

            TaggedComponent c = new TaggedComponent();
            c.Id = _currentComponentId;
            c.EngineeringTag = textBoxEngineeringTag.Text;
            c.ComponentType = textBoxComponentType.Text;
            c.LDARTag = textBoxLDARTag.Text;
            c.PreviousTag = textBoxPreviousTag.Text;
            c.Location = textBoxLocation.Text;
            if (comboBoxAccess.SelectedItem != null) c.Access = comboBoxAccess.SelectedItem.ToString();
            //if (comboBoxReason.SelectedItem != null) c.AccessReason = comboBoxReason.SelectedItem.ToString();
            c.ModifiedDate = DateTime.Now.ToString();
            if (Environment.UserName != null)
            {
                c.ModifiedBy = Environment.UserName;
            }
            c.Stream = textBoxStream.Text;
            c.Drawing = _currentDrawing;
            c.Children = new List<ChildComponent>();
            foreach (ListViewItem item in listViewChildren.Items)
            {
                c.Children.Add(new ChildComponent() { ComponentType = item.Text, LDARTag = item.SubItems[1].Text, PreviousTag = item.SubItems[2].Text, Location = item.SubItems[3].Text });
            }

            LocalData.AddComponent(c);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonLocationBuilder_Click(object sender, EventArgs e)
        {
            FormLocationBuilder lb = new FormLocationBuilder();
            DialogResult dr = lb.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                textBoxLocation.Text = lb.LocationDescriptor;
            }
        }

        private void buttonChildAdd_Click(object sender, EventArgs e)
        {
            FormChildObject co = new FormChildObject(textBoxLocation.Text);
            DialogResult dr = co.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                ListViewItem item = new ListViewItem();
                item.Text = co.ComponentType;
                item.SubItems.Add("");
                item.SubItems.Add(co.PreviousTag);
                item.SubItems.Add(co.LocationDescription);
                listViewChildren.Items.Add(item);
            }

            SequenceChildTags();
        }

        private void SequenceChildTags()
        {
            for (int i = 0; i < listViewChildren.Items.Count; i++)
            {
                listViewChildren.Items[i].SubItems[1].Text = textBoxLDARTag.Text + "." + (i + 1).ToString();
            }
        }

        private void buttonChildRemove_Click(object sender, EventArgs e)
        {
            if (listViewChildren.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in listViewChildren.SelectedItems)
                {
                    listViewChildren.Items.Remove(item);
                }
            }
            else
            {
                MessageBox.Show("Select a child component to delete.");
            }
        }

        private void textBoxEngineeringTag_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBoxLDARTag_TextChanged(object sender, EventArgs e)
        {
            SequenceChildTags();
        }

        private void buttonRefreshOldTag_Click(object sender, EventArgs e)
        {
            string tagID = string.Empty;
            string categoryCode = string.Empty;
            string reasonDesc = string.Empty;
            int? categoryId = 0;
            int? reasonId = 0;
                        
            LDARComponent comp = Globals.CurrentProjectData.LDARData.ExistingComponents.Where(c => c.ComponentTag == this.textBoxPreviousTag.Text).FirstOrDefault();

            //update location description
            if (comp != null)
            {
                tagID = comp.ComponentTag.ToString();
                if (tagID == this.textBoxPreviousTag.Text)
                {
                    this.textBoxLocation.Text = comp.LocationDescription.ToString();
                }
            }

            //update DTM/UTM
            string AccessText = "NTM";
            string ReasonText = String.Empty;

            if (comp != null)
            {
                categoryId = comp.ComponentCategoryId;
                LDARCategory ldCat = Globals.CurrentProjectData.LDARData.ComponentCategories.Where(c => c.ComponentCategoryID == categoryId).FirstOrDefault();
                reasonId = comp.ComponentReasonId;
                LDARReason ldRsn = Globals.CurrentProjectData.LDARData.ComponentReasons.Where(c => c.ComponentReasonID == reasonId).FirstOrDefault();

                if (ldCat == null) return;
                
                if (tagID == this.textBoxPreviousTag.Text || categoryId != null)
                {
                    if (ldCat.CategoryCode == "U") AccessText = "UTM";
                    if (ldCat.CategoryCode == "D") AccessText = "DTM";
                    if (ldRsn != null) ReasonText = ldRsn.ReasonDescription;
                }
            }
            if (string.IsNullOrEmpty(ReasonText))
            {
                this.comboBoxAccess.Text = AccessText;
            }
            else
            {
                this.comboBoxAccess.Text = AccessText + " - " + ReasonText;
            }
            //this.comboBoxReason.Text = ReasonText;
        }


        private void textBoxPreviousTag_TextChanged(object sender, EventArgs e)
        {
            
            if (Globals.CurrentProjectData == null) return;
            
            string tagID = string.Empty;
            LDARComponent comp = Globals.CurrentProjectData.LDARData.ExistingComponents.Where(c => c.ComponentTag == this.textBoxPreviousTag.Text).FirstOrDefault();
            if (comp != null)
            {
                tagID = comp.ComponentTag.ToString();
                if (tagID == this.textBoxPreviousTag.Text)
                {
                    this.buttonRefreshOldTag.Enabled = true;
                }
                else
                {
                    this.buttonRefreshOldTag.Enabled = false;
                }
            }
            else
            {
                this.buttonRefreshOldTag.Enabled = false;
            }

        }

        private void FormEditObject_Load(object sender, EventArgs e)
        {
            getCategoryReasonList();            
        }

        private void getCategoryReasonList()
        {
            List<string> crList = new List<string>();
            string accessText = string.Empty;

            //this should always be an option
            crList.Add("NTM");

            if (Globals.CurrentProjectData != null)
            {
                this.comboBoxAccess.Items.Clear();
                foreach (LDARCategory ldc in Globals.CurrentProjectData.LDARData.ComponentCategories)
                {
                    foreach (LDARReason ldr in Globals.CurrentProjectData.LDARData.ComponentReasons)
                    {
                        if (ldr.ComponentCategoryID == ldc.ComponentCategoryID)
                        {
                            accessText = ldc.CategoryCode + "TM - " + ldr.ReasonDescription;
                            if (!crList.Contains(accessText)) crList.Add(accessText);
                        }
                    }
                }
                foreach (string str in crList)
                {
                    this.comboBoxAccess.Items.Add(str);
                }
            }
        }

        private void buttonSetBackground_Click(object sender, EventArgs e)
        {

        }



    }
}
