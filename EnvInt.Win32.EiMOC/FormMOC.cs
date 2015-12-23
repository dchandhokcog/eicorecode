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
    public partial class FormMOC : Form
    {
        public string _currentComponentId = "";
        public string _currentDrawing = "";
        public double _currentBackground = 0;
        public double _currentReading = 0;
        public string _currentUser = "";
        public string _currentInstrument = "";
        public DateTime _currentInspectionDate = DateTime.Today;
        public bool _inspected = false;
        public bool _hasImage = false;


        public FormMOC(Point startPosition)
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

            _currentDrawing = drawing;

            //load component   
            TaggedComponent c = LocalData.GetComponent(id);
            if (c != null)
            {
                _currentComponentId = c.Id;
                _currentBackground = c.InspectionBackground;
                _currentInspectionDate = c.InspectionDate;
                _currentInstrument = c.InspectionInstrument;
                _currentReading = c.InspectionReading;
                _currentUser = c.InspectionInspector;
                _inspected = c.Inspected;
                labelInspection.Text = _currentReading + " PPM by " + _currentUser + " on " + _currentInspectionDate.ToString();
                textBoxLDARTag.Text = c.LDARTag;
                textBoxPreviousTag.Text = c.PreviousTag;
                textBoxLocation.Text = c.Location;
                textBoxDrawing.Text = c.Drawing;
                textBoxSize.Text = c.Size.ToString();
                textBoxMOCNumber.Text = c.MOCNumber;
                comboBoxUnit.Text = c.Unit;
                comboBoxState.Text = c.ChemicalState;
                comboBoxAccess.Text = c.Access;
                comboBoxStream.Text = c.Stream;
                comboBoxType.Text = c.ComponentType;
                //checkBoxAttachDrawing.Checked = c.AttachDrawing;


                if (c.Children != null)
                {
                    foreach (ChildComponent cc in c.Children)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = cc.ComponentType;
                        item.SubItems.Add(cc.LDARTag);
                        item.SubItems.Add(cc.PreviousTag);
                        item.SubItems.Add(cc.Location);
                        //item.SubItems.Add(cc.Inspected);
                        item.SubItems.Add(cc.InspectionInspector);
                        item.SubItems.Add(cc.InspectionDate.ToString());
                        item.SubItems.Add(cc.InspectionReading.ToString());
                        item.SubItems.Add(cc.InspectionBackground.ToString());
                        item.SubItems.Add(cc.InspectionInstrument);
                        listViewChildren.Items.Add(item);
                    }
                }

                string imageFile = Globals.WorkingFolder + "\\Images\\" + _currentComponentId + ".jpg";
                if (System.IO.File.Exists(imageFile))
                {
                    pictureBoxPhoto.Image = Image.FromFile(imageFile);
                    _hasImage = true;
                }

            }
            else
            {
                if (!String.IsNullOrEmpty(Properties.Settings.Default.LastComponentLocation)) textBoxLocation.Text = Properties.Settings.Default.LastComponentLocation;
                //if (!String.IsNullOrEmpty(Properties.Settings.Default.LastComponentLDARTag)) textBoxLDARTag.Text = Properties.Settings.Default.LastComponentLDARTag;
                if (!String.IsNullOrEmpty(Properties.Settings.Default.LastUnit)) comboBoxUnit.Text = Properties.Settings.Default.LastUnit;
                //if (!String.IsNullOrEmpty(Properties.Settings.Default.LastOldTag)) textBoxPreviousTag.Text = Properties.Settings.Default.LastOldTag;
                if (!String.IsNullOrEmpty(Properties.Settings.Default.LastType)) comboBoxType.Text = Properties.Settings.Default.LastType;
                if (!String.IsNullOrEmpty(Properties.Settings.Default.LastAccess)) comboBoxAccess.Text = Properties.Settings.Default.LastAccess;
                if (!String.IsNullOrEmpty(Properties.Settings.Default.LastStream)) comboBoxStream.Text = Properties.Settings.Default.LastStream;
                if (!String.IsNullOrEmpty(Properties.Settings.Default.LastState)) comboBoxState.Text = Properties.Settings.Default.LastState;
                if (!String.IsNullOrEmpty(Properties.Settings.Default.LastSize)) textBoxSize.Text = Properties.Settings.Default.LastSize;
                if (!String.IsNullOrEmpty(Properties.Settings.Default.LastMOCNumber)) textBoxMOCNumber.Text = Properties.Settings.Default.LastMOCNumber;
                if (!String.IsNullOrEmpty(Properties.Settings.Default.LastDrawing)) textBoxDrawing.Text = Properties.Settings.Default.LastDrawing;
                //checkBoxAttachDrawing.Checked = Properties.Settings.Default.LastAttachDrawing;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            saveTag(true);
        }

        private void saveTag(Boolean closeForm)
        {

            if (!validateFormData()) return;

            Properties.Settings.Default.LastComponentLocation = textBoxLocation.Text;
            Properties.Settings.Default.LastComponentLDARTag = textBoxLDARTag.Text;
            Properties.Settings.Default.LastAccess = comboBoxAccess.Text;
            Properties.Settings.Default.LastDrawing = textBoxDrawing.Text;
            Properties.Settings.Default.LastMOCNumber = textBoxMOCNumber.Text;
            Properties.Settings.Default.LastSize = textBoxSize.Text;
            Properties.Settings.Default.LastState = comboBoxState.Text;
            Properties.Settings.Default.LastStream = comboBoxStream.Text;
            Properties.Settings.Default.LastType = comboBoxType.Text;
            Properties.Settings.Default.LastUnit = comboBoxUnit.Text;
            Properties.Settings.Default.LastOldTag = textBoxPreviousTag.Text;
            //Properties.Settings.Default.LastAttachDrawing = checkBoxAttachDrawing.Checked;

            Properties.Settings.Default.Save();

            TaggedComponent c = new TaggedComponent();

            if (_inspected)
            {
                c.InspectionBackground = _currentBackground;
                c.InspectionDate = _currentInspectionDate;
                c.InspectionInspector = _currentUser;
                c.InspectionInstrument = _currentInstrument;
                c.InspectionReading = _currentReading;
                c.Inspected = true;
            }
            else
            {
                c.InspectionBackground = 0;
                c.InspectionDate = _currentInspectionDate;
                c.InspectionInspector = "";
                c.InspectionInstrument = "";
                c.InspectionReading = 0;
                c.Inspected = false;
            }

            c.Id = _currentComponentId;
            c.Unit = comboBoxUnit.Text;
            c.EngineeringTag = Guid.NewGuid().ToString();
            c.ComponentType = comboBoxType.Text;
            c.LDARTag = textBoxLDARTag.Text;
            c.PreviousTag = textBoxPreviousTag.Text;
            c.Location = textBoxLocation.Text;
            c.Access = comboBoxAccess.Text.ToString();
            c.ModifiedDate = DateTime.Now.ToString();
            //c.AttachDrawing = checkBoxAttachDrawing.Checked;
            if (Environment.UserName != null)
            {
                c.ModifiedBy = Environment.UserName;
            }
            c.Stream = comboBoxStream.Text;
            c.Drawing = textBoxDrawing.Text;
            c.ChemicalState = comboBoxState.Text;
            if (textBoxSize.Text != string.Empty) c.Size = double.Parse(textBoxSize.Text);
            c.Children = new List<ChildComponent>();
            c.MOCNumber = this.textBoxMOCNumber.Text;
            foreach (ListViewItem item in listViewChildren.Items)
            {
                c.Children.Add(new ChildComponent() { ComponentType = item.Text, LDARTag = item.SubItems[1].Text, PreviousTag = item.SubItems[2].Text, 
                    Location = item.SubItems[3].Text, Inspected = bool.Parse(item.SubItems[4].Text), InspectionInspector = item.SubItems[5].Text,
                    InspectionDate = DateTime.Parse(item.SubItems[6].Text), InspectionReading = double.Parse(item.SubItems[7].Text), 
                    InspectionBackground = double.Parse(item.SubItems[8].Text), InspectionInstrument = item.SubItems[9].Text});
            }

            LocalData.AddComponent(c);

            if (_hasImage)
            {
                //if photo is not the default, then save it in the Working folder under 'Images'
                string imagesFolder = Globals.WorkingFolder + "\\Images";
                if (!System.IO.Directory.Exists(imagesFolder))
                {
                    System.IO.Directory.CreateDirectory(imagesFolder);
                }
                pictureBoxPhoto.Image.Save(imagesFolder + "\\" + _currentComponentId + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            if (closeForm)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }

        }

        private bool validateFormData()
        {
            List<string> errorList = new List<string>();
            string errString = "";
            bool noErrors = true;

            if (textBoxLDARTag.Text == "") errorList.Add("Tag cannot be empty");
            if (textBoxLocation.Text == "") errorList.Add("Location cannot be empty");
            if (textBoxSize.Text == "") errorList.Add("Size cannot be empty");
            if (comboBoxState.Text == "") errorList.Add("State cannot be empty");
            if (comboBoxAccess.Text == "") errorList.Add("Access cannot be empty");
            if (comboBoxStream.Text == "") errorList.Add("Stream cannot be empty");
            if (comboBoxType.Text == "") errorList.Add("Type cannot be empty");
            if (comboBoxUnit.Text == "") errorList.Add("Unit cannot be empty");
            //if (LocalData.doesTagExist(textBoxLDARTag.Text)) errorList.Add("The new tag has already been documented");
            //if (LocalData.doesPreviousTagExist(textBoxPreviousTag.Text)) errorList.Add("The old tag has already been documented");

            if (errorList.Count() > 0)
            {
                noErrors = false;
                foreach (string err in errorList)
                {
                    errString = errString + err + Environment.NewLine;
                }
                MessageBox.Show(errString);
            }

            return noErrors;
 
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
            if (Globals.CurrentProjectData != null)
            {
                co.setTypeList(Globals.CurrentProjectData.LDARData.ComponentClassTypes);
            }
            co.InspectionBackground = _currentBackground;
            co.InspectionInspector = _currentUser;
            co.InspectionInstrument = _currentInstrument;
            DialogResult dr = co.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                ListViewItem item = new ListViewItem();
                item.Text = co.ComponentType;
                item.SubItems.Add("");
                item.SubItems.Add(co.PreviousTag);
                item.SubItems.Add(co.LocationDescription);
                item.SubItems.Add(co.Inspected.ToString());
                item.SubItems.Add(co.InspectionInspector);
                item.SubItems.Add(co.InspectionDate.ToString());
                item.SubItems.Add(co.InspectionReading.ToString());
                item.SubItems.Add(co.InspectionBackground.ToString());
                item.SubItems.Add(co.InspectionInstrument);
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

            //if this didn't work abort!
            if (comp == null) return;

            //update location description
            tagID = comp.ComponentTag.ToString();
            if (tagID == this.textBoxPreviousTag.Text)
            {
                this.textBoxLocation.Text = comp.LocationDescription.ToString();
            }

            //update drawing
            if (comp.Drawing != null) textBoxDrawing.Text = comp.Drawing;
            if (comp.Size != null) textBoxSize.Text = comp.Size;

            //update DTM/UTM
            string AccessText = "NTM";
            string ReasonText = String.Empty;

            categoryId = comp.ComponentCategoryId;
            LDARCategory ldCat = Globals.CurrentProjectData.LDARData.ComponentCategories.Where(c => c.ComponentCategoryID == categoryId).FirstOrDefault();
            reasonId = comp.ComponentReasonId;
            LDARReason ldRsn = Globals.CurrentProjectData.LDARData.ComponentReasons.Where(c => c.ComponentReasonID == reasonId).FirstOrDefault();

            if (ldCat != null)
            {
                if (tagID == this.textBoxPreviousTag.Text || categoryId != null)
                {
                    if (ldCat.CategoryCode == "U") AccessText = "UTM";
                    if (ldCat.CategoryCode == "D") AccessText = "DTM";
                    if (ldRsn != null) ReasonText = ldRsn.ReasonDescription;
                }
                if (string.IsNullOrEmpty(ReasonText))
                {
                    this.comboBoxAccess.Text = AccessText;
                }
                else
                {
                    this.comboBoxAccess.Text = AccessText + " - " + ReasonText;
                }
            }

            LDARProcessUnit ldUnit = Globals.CurrentProjectData.LDARData.ProcessUnits.Where(c => c.ProcessUnitId == comp.ProcessUnitId).FirstOrDefault();
            if (ldUnit != null) this.comboBoxUnit.Text = ldUnit.UnitDescription;
           
            LDARComponentStream ldStream = Globals.CurrentProjectData.LDARData.ComponentStreams.Where(c => c.ComponentStreamId == comp.ChemicalStreamId).FirstOrDefault();
            if (ldStream != null) this.comboBoxStream.Text = ldStream.StreamDescription;

            LDARChemicalState ldState = Globals.CurrentProjectData.LDARData.ChemicalStates.Where(c => c.ChemicalStateId == comp.ChemicalStateId).FirstOrDefault();
            if (ldState != null) this.comboBoxState.Text = ldState.ChemicalState;

            if (comp.ComponentTypeId != null)
            {
                LDARComponentClassType ldClassType = Globals.CurrentProjectData.LDARData.ComponentClassTypes.Where(c => c.ComponentClassId == comp.ComponentClassId && c.ComponentTypeId == comp.ComponentTypeId).FirstOrDefault();
                if (ldClassType != null) this.comboBoxType.Text = ldClassType.ComponentClass + " - " + ldClassType.ComponentType;
            }
            else
            {
                LDARComponentClassType ldClassType = Globals.CurrentProjectData.LDARData.ComponentClassTypes.Where(c => c.ComponentClassId == comp.ComponentClassId).FirstOrDefault();
                if (ldClassType != null) this.comboBoxType.Text = ldClassType.ComponentClass;
            }


            //TODO: Retrieve more data from original tag.
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
            fillCategoryReasonList();
            fillUnitList();
            fillStateList();
            fillStreamList();
            fillTypeList();
        }

        private void fillCategoryReasonList()
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

        private void fillTypeList()
        {
            List<string> typeList = new List<string>();

            if (Globals.CurrentProjectData != null)
            {
                this.comboBoxType.Items.Clear();
                foreach (LDARComponentClassType ldt in Globals.CurrentProjectData.LDARData.ComponentClassTypes)
                {
                    if (ldt.ComponentClass != null ) typeList.Add(ldt.ComponentClass + " - " + ldt.ComponentType);
                }
                typeList.Sort();
                foreach (string str in typeList)
                {
                    comboBoxType.Items.Add(str);
                }

                this.Text = "MOC Edit (LeakDAS Integrated)";
            }
        }

        private void fillUnitList()
        {
            if (Globals.CurrentProjectData != null)
            {
                this.comboBoxUnit.Items.Clear();
                foreach (LDARProcessUnit ldu in Globals.CurrentProjectData.LDARData.ProcessUnits)
                {
                    if (ldu.UnitDescription != null ) this.comboBoxUnit.Items.Add(ldu.UnitDescription);
                }
            }
        }

        private void fillStreamList()
        {
            if (Globals.CurrentProjectData != null)
            {
                this.comboBoxStream.Items.Clear();
                foreach (LDARComponentStream lds in Globals.CurrentProjectData.LDARData.ComponentStreams)
                {
                    if (lds.StreamDescription != null) this.comboBoxStream.Items.Add(lds.StreamDescription);
                }
            }
        }

        private void fillStateList()
        {
            if (Globals.CurrentProjectData != null)
            {
                this.comboBoxState.Items.Clear();
                foreach (LDARChemicalState lds in Globals.CurrentProjectData.LDARData.ChemicalStates)
                {
                    if (lds.ChemicalState != null) this.comboBoxState.Items.Add(lds.ChemicalState);
                }
            }
        }

        private void textBoxSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '.')
            {
                if (e.KeyChar == '.' & this.textBoxSize.Text.Contains(".")) e.KeyChar = char.MinValue;
            }
            else e.KeyChar = char.MinValue;
        }

        private void buttonNextComponent_Click(object sender, EventArgs e)
        {
            saveTag(false);
            _currentComponentId = Guid.NewGuid().ToString();
            this.textBoxLDARTag.Text = "";
            this.textBoxPreviousTag.Text = "";
            _inspected = false;
            labelInspection.Text = "Not Inspected";
            listViewChildren.Items.Clear();
        }

        private void buttonSetBackground_Click(object sender, EventArgs e)
        {
            string bg = string.Empty;
            double bgTemp = 0;

            DialogResult dr = Globals.InputBox("Background", "Enter Background Reading (PPM)", ref bg);

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    bgTemp = double.Parse(bg);
                    if (!(bgTemp >= 0 && bgTemp <= 1000000))
                    {
                        MessageBox.Show("Background must be a number between 0 and 100000");
                    }
                    else
                    {
                        _currentBackground = bgTemp;
                    }
                }
                catch
                {
                    MessageBox.Show("Background must be a number between 0 and 100000");
                }
            }
            
        }

        private void buttonInspect_Click(object sender, EventArgs e)
        {
            FormInspect ins = new FormInspect();
            ins.StartPosition = FormStartPosition.CenterScreen;
            ins.Background = _currentBackground;
            ins.Inspector = _currentUser;
            DialogResult dr = ins.ShowDialog();
            ins.TopMost = true;
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                _currentInspectionDate = DateTime.Now;
                _currentInstrument = ins.Instrument;
                _currentReading = ins.Reading;
                _currentUser = ins.Inspector;
                _currentBackground = ins.Background;
                _inspected = true;
                labelInspection.Text = ins.Reading + " PPM by " + ins.Inspector + " on " + _currentInspectionDate.ToString();
            }
        }

        private void pictureBoxPhoto_Click(object sender, EventArgs e)
        {
            FormAttachments fa = new FormAttachments();
            DialogResult dr = fa.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                if (fa.HasPicture)
                {
                    pictureBoxPhoto.Image = fa.CurrentPhoto;
                    _hasImage = true;
                }
                else
                {
                    pictureBoxPhoto.Image = pictureBoxPhoto.InitialImage;
                    _hasImage = false;
                }
            }
        }



    }
}
