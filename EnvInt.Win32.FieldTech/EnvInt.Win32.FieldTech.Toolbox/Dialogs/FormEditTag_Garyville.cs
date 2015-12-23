using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;
using System.Windows.Forms;

using EnvInt.Win32.FieldTech.Data;
using EnvInt.Win32.FieldTech.Containers;
using EnvInt.Win32.FieldTech.Dialogs;

using Community.CsharpSqlite.SQLiteClient;
using EnvInt.Win32.FieldTech.Library;

namespace EnvInt.Win32.FieldTech
{
    public partial class FormEditTag_Garyville : Form
    {
        public TaggedComponent _currentTaggedComponent = new TaggedComponent();
        public System.IO.FileStream _imageFileStream;
        public event EventHandler TagSaved;
        public bool _allowOldTagRefresh = true;
        private BindingSource bindingSourceChildren = new BindingSource();
        private string _lastChildType = string.Empty;
        private ChildComponent _lastChild = new ChildComponent();
        private bool _newFlag = false;
        private bool currentFormChanged = false;


        public FormEditTag_Garyville(Point startPosition)
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


            foreach (Control c in this.Controls)
            {
                if (c.GetType().ToString() == "System.Windows.Forms.TextBox" || c.GetType().ToString() == "System.Windows.Forms.ComboBox")
                {
                    c.KeyPress += FormEditTag_KeyPress;
                }
            }
        }

        public void setFormProperties(bool drawingTag)
        {
            if (drawingTag)
            {
                buttonNextComponent.Visible = false;
            }


            else
            {
                buttonNextComponent.Visible = true;
            }

            this.buttonRefreshOldTag.Visible = _allowOldTagRefresh;
        }


        private void SetComponentNew(string id, string engineeringTag, string componentType, string stream, string drawing, string size, bool drawingTag)
        {
            _currentTaggedComponent.Id = id;
            _currentTaggedComponent.EngineeringTag = engineeringTag;
            _newFlag = true;

            if (String.IsNullOrEmpty(drawing))
            {
                if (!String.IsNullOrEmpty(Properties.Settings.Default.LastType)) _currentTaggedComponent.Drawing = Properties.Settings.Default.LastDrawing;
            }
            else
            {
                _currentTaggedComponent.Drawing = drawing;
            }
            if (String.IsNullOrEmpty(componentType))
            {
                if (!String.IsNullOrEmpty(Properties.Settings.Default.LastType)) _currentTaggedComponent.ComponentType = Properties.Settings.Default.LastType;
            }
            else
            {
                _currentTaggedComponent.ComponentType = componentType;
            }

            if (String.IsNullOrEmpty(stream))
            {
                if (!String.IsNullOrEmpty(Properties.Settings.Default.LastStream)) _currentTaggedComponent.Stream = Properties.Settings.Default.LastStream;
            }
            else
            {
                _currentTaggedComponent.Stream = stream;
            }

            if (String.IsNullOrEmpty(size))
            {
                if (!String.IsNullOrEmpty(Properties.Settings.Default.LastSize))
                {
                    if (!String.IsNullOrEmpty(Properties.Settings.Default.LastSize)) _currentTaggedComponent.Size = Globals.getSizeFromString(Properties.Settings.Default.LastSize);
                }
            }
            else
            {
                _currentTaggedComponent.Size = Globals.getSizeFromString(size);
            }

            //TODO: Disabled for Shell Norco project
            //if (!String.IsNullOrEmpty(Properties.Settings.Default.LastUnit)) _currentTaggedComponent.Unit = Properties.Settings.Default.LastUnit;
            comboBoxUnit.Text = string.Empty;
            //if (!String.IsNullOrEmpty(Properties.Settings.Default.LastState)) _currentTaggedComponent.ChemicalState = Properties.Settings.Default.LastState;
            comboBoxState.Text = string.Empty;
            //if (!String.IsNullOrEmpty(Properties.Settings.Default.LastMOCNumber)) _currentTaggedComponent.MOCNumber = Properties.Settings.Default.LastMOCNumber;
            textBoxMOCNumber.Text = string.Empty;
            //if (!String.IsNullOrEmpty(Properties.Settings.Default.LastManufacturer)) _currentTaggedComponent.Manufacturer = Properties.Settings.Default.LastManufacturer;
            //TODO: Custom property for Marathon Garyville
            comboBoxManufacturer.Text = "OTHER";
            //if (!String.IsNullOrEmpty(Properties.Settings.Default.LastProperty)) _currentTaggedComponent.Property = Properties.Settings.Default.LastProperty;
            textBoxProperty.Text = string.Empty;

            if (!String.IsNullOrEmpty(Properties.Settings.Default.LastComponentLocation)) _currentTaggedComponent.Location = Properties.Settings.Default.LastComponentLocation;
            if (!String.IsNullOrEmpty(Properties.Settings.Default.LastComponentLDARTag)) _currentTaggedComponent.LDARTag = getXTag(Properties.Settings.Default.LastComponentLDARTag);
            if (!String.IsNullOrEmpty(Properties.Settings.Default.LastOldTag)) _currentTaggedComponent.PreviousTag = getXTag(Properties.Settings.Default.LastOldTag);
            //if (!String.IsNullOrEmpty(Properties.Settings.Default.LastAccess)) _currentTaggedComponent.Access = Properties.Settings.Default.LastAccess;
            _currentTaggedComponent.Access = "NTM";

            if (!String.IsNullOrEmpty(Properties.Settings.Default.LastArea)) _currentTaggedComponent.Area = Properties.Settings.Default.LastArea;
            _currentTaggedComponent.isDrawingTag = drawingTag;
            _currentTaggedComponent.AttachDrawing = false;
            _currentTaggedComponent.Inspected = false;

            _currentTaggedComponent.RouteSequence = LocalData.getNextRouteNumber();
            //checkBoxAttachDrawing.Checked = Properties.Settings.Default.LastAttachDrawing;
            checkForRelatedComponents();
            if (string.IsNullOrEmpty(_currentTaggedComponent.ModifiedBy))
            {
                _currentTaggedComponent.ModifiedBy = Environment.UserName;
            }
            else
            {
                _currentTaggedComponent.ModifiedBy = _currentTaggedComponent.ModifiedBy;
            }

            setFormValues(false);
        }


        public void SetComponent(string id, string engineeringTag, string componentType, string stream, string drawing, string size, bool drawingTag)
        {

            resetFormData();

            //grey out fields that are provided by drawing

            if (String.IsNullOrEmpty(drawing))
            {
                textBoxDrawing.BackColor = Color.FromKnownColor(KnownColor.Window);
            }
            else
            {
                textBoxDrawing.BackColor = Color.FromKnownColor(KnownColor.Control);
            }
            if (String.IsNullOrEmpty(size))
            {
                textBoxSize.BackColor = Color.FromKnownColor(KnownColor.Window);
            }
            else
            {
                textBoxSize.BackColor = Color.FromKnownColor(KnownColor.Control);
            }
            if (String.IsNullOrEmpty(componentType))
            {
                comboBoxType.BackColor = Color.FromKnownColor(KnownColor.Window);
            }
            else
            {
                comboBoxType.BackColor = Color.FromKnownColor(KnownColor.Control);
            }

            if (String.IsNullOrEmpty(stream))
            {
                comboBoxStream.BackColor = Color.FromKnownColor(KnownColor.Window);
            }
            else
            {
                comboBoxStream.BackColor = Color.FromKnownColor(KnownColor.Control);
            }

            setFormProperties(drawingTag);

            //load component   
            TaggedComponent c = LocalData.GetComponent(id);
            if (c != null)
            {
                _currentTaggedComponent = c;
                setFormValues(true);
            }
            else
            {
                SetComponentNew(id, engineeringTag, componentType, stream, drawing, size, drawingTag);
            }

        }

        public void SetComponent(string LDARTag)
        {
            resetFormData();

            SetComponentNew(Guid.NewGuid().ToString(), null, null, null, null, null, false);

            textBoxLDARTag.Text = LDARTag;
            textBoxPreviousTag.Text = LDARTag;

            refreshTagFromExisting(LDARTag, "", true);

        }

        private string getXTag(string nonXTag)
        {
            if (nonXTag != string.Empty)
            {
                return clsCommonFunctions.getXTag(nonXTag);
            }
            else
            {
                return string.Empty;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (textBoxLDARTag.Text.Contains("X") || textBoxPreviousTag.Text.Contains("X"))
            {
                if (MessageBox.Show("Tag/Old Tag contains an 'X', continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }

            if (validateFormData())
            {
                saveTag();
                Hide();
            }
        }

        public void saveCurrentTag()
        {
            MainForm._projectDirty = true;
            if (validateFormData())
            {
                saveTag();
                resetFormData();
            }
        }

        private void addToPicklist()
        {

            //TODO: This doesn't work yet, because currently type is expected (something after the dash) but in reality, sometimes type is not defined.

            if (comboBoxType.Text.Contains("-"))
            {
                string ldClass = comboBoxType.Text.Split('-')[0].Trim().ToUpper();
                string ldType = comboBoxType.Text.Split('-')[1].Trim().ToUpper();
                string ldClassType = ldClass + " - " + ldType;
                LDARComponentClassType ldarClassType = Globals.CurrentProjectData.LDARData.ComponentClassTypes.Where(c => c.ComponentClass == ldClass && c.TypeDescription == ldType).FirstOrDefault();
                if (ldarClassType == null)
                {
                    //maybe we can get it by class only?
                    ldarClassType = Globals.CurrentProjectData.LDARData.ComponentClassTypes.Where(c => c.ComponentClass == ldClass).FirstOrDefault();
                }
                //if it's still null, let's add it to the list
                if (ldarClassType == null)
                {
                    ldarClassType = new LDARComponentClassType();
                    ldarClassType.ClassDescription = ldClass;
                    ldarClassType.ComponentClass = ldClass;
                    ldarClassType.ComponentClassId = -1;
                    ldarClassType.ComponentType = ldType;
                    ldarClassType.ComponentTypeId = -1;
                    ldarClassType.TypeDescription = ldType;
                    Globals.CurrentProjectData.LDARData.ComponentClassTypes.Add(ldarClassType);
                }
            }
            else
            {
                LDARComponentClassType ldarClassType = Globals.CurrentProjectData.LDARData.ComponentClassTypes.Where(c => c.ComponentClass == comboBoxType.Text).FirstOrDefault();
                //if it's still null, let's add it to the list
                if (ldarClassType == null)
                {
                    ldarClassType = new LDARComponentClassType();
                    ldarClassType.ClassDescription = comboBoxType.Text;
                    ldarClassType.ComponentClass = comboBoxType.Text;
                    ldarClassType.ComponentClassId = -1;
                    ldarClassType.ComponentType = string.Empty;
                    Globals.CurrentProjectData.LDARData.ComponentClassTypes.Add(ldarClassType);
                }
            }
            fillTypeList();
        }


        private void saveTag()
        {

            //TODO: enable adding to picklist once the kinks are worked out.  see addToPicklist() for more details. 
            //addToPicklist();

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
            Properties.Settings.Default.LastArea = comboBoxArea.Text;
            Properties.Settings.Default.LastManufacturer = comboBoxManufacturer.Text;
            Properties.Settings.Default.LastProperty = textBoxProperty.Text;

            //Properties.Settings.Default.LastAttachDrawing = checkBoxAttachDrawing.Checked;

            Properties.Settings.Default.Save();

            if (_currentTaggedComponent.Inspected)
            {
                _currentTaggedComponent.InspectionBackground = _currentTaggedComponent.InspectionBackground;
                _currentTaggedComponent.InspectionDate = _currentTaggedComponent.InspectionDate;
                _currentTaggedComponent.InspectionInspector = _currentTaggedComponent.InspectionInspector;

                _currentTaggedComponent.InspectionInstrument = _currentTaggedComponent.InspectionInstrument;
                _currentTaggedComponent.InspectionReading = _currentTaggedComponent.InspectionReading;
                _currentTaggedComponent.Inspected = true;
            }
            else
            {
                _currentTaggedComponent.InspectionBackground = -100;
                _currentTaggedComponent.InspectionDate = _currentTaggedComponent.InspectionDate;
                _currentTaggedComponent.InspectionInspector = "";
                _currentTaggedComponent.InspectionInstrument = "";
                _currentTaggedComponent.InspectionReading = -100;
                _currentTaggedComponent.Inspected = false;
            }

            _currentTaggedComponent.Id = _currentTaggedComponent.Id;
            _currentTaggedComponent.Unit = comboBoxUnit.Text;
            _currentTaggedComponent.EngineeringTag = labelCADID.Text;
            _currentTaggedComponent.ComponentType = comboBoxType.Text;
            _currentTaggedComponent.LDARTag = textBoxLDARTag.Text;
            _currentTaggedComponent.PreviousTag = textBoxPreviousTag.Text;
            _currentTaggedComponent.Location = textBoxLocation.Text;
            _currentTaggedComponent.Access = comboBoxAccess.Text.ToString();
            _currentTaggedComponent.ModifiedDate = DateTime.Now.ToString();
            _currentTaggedComponent.isDrawingTag = _currentTaggedComponent.isDrawingTag;
            _currentTaggedComponent.ReferenceTag = textBoxReferenceTag.Text;
            //c.AttachDrawing = checkBoxAttachDrawing.Checked;
            if (_currentTaggedComponent.ModifiedBy != null)
            {
                _currentTaggedComponent.ModifiedBy = _currentTaggedComponent.ModifiedBy;
            }
            else
            {
                _currentTaggedComponent.ModifiedBy = Environment.UserName;
                _currentTaggedComponent.ModifiedBy = Environment.UserName;
            }
            _currentTaggedComponent.Stream = comboBoxStream.Text;
            _currentTaggedComponent.Drawing = textBoxDrawing.Text;
            _currentTaggedComponent.ChemicalState = comboBoxState.Text;
            _currentTaggedComponent.Size = Globals.getSizeFromString(textBoxSize.Text);

            _currentTaggedComponent.Children = new List<ChildComponent>();
            _currentTaggedComponent.MOCNumber = this.textBoxMOCNumber.Text;
            _currentTaggedComponent.Area = comboBoxArea.Text;

            _currentTaggedComponent.Manufacturer = comboBoxManufacturer.Text;
            _currentTaggedComponent.Property = textBoxProperty.Text;
            _currentTaggedComponent.RouteSequence = Double.Parse(textBoxRouteNo.Text);
            LocalData.LastRouteNo = _currentTaggedComponent.RouteSequence;

            foreach (ListViewItem item in listViewChildren.Items)
            {
                _currentTaggedComponent.Children.Add(new ChildComponent()
                {
                    ComponentType = item.Text,
                    LDARTag = item.SubItems[1].Text,
                    PreviousTag = item.SubItems[2].Text,
                    Location = item.SubItems[3].Text,
                    Inspected = bool.Parse(item.SubItems[4].Text),
                    InspectionInspector = item.SubItems[5].Text,
                    InspectionDate = DateTime.Parse(item.SubItems[6].Text),
                    InspectionReading = double.Parse(item.SubItems[7].Text),
                    InspectionBackground = double.Parse(item.SubItems[8].Text),
                    InspectionInstrument = item.SubItems[9].Text,
                    Size = Globals.getSizeFromString(item.SubItems[10].Text)
                });
            }



            if (_currentTaggedComponent.AttachDrawing)
            {
                //if photo is not the default, then save it in the Working folder under 'Images'
                string imagesFolder = Globals.WorkingFolder + "\\Images";
                if (!System.IO.Directory.Exists(imagesFolder))
                {
                    System.IO.Directory.CreateDirectory(imagesFolder);
                }
                try
                {
                    string imageFile = Globals.WorkingFolder + "\\Images\\" + _currentTaggedComponent.Id.Replace(':', '_') + ".jpg";
                    //pictureBoxPhoto.Image.Save(imagesFolder + "\\" + _currentComponentId.Replace(':', '_') + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    //                    if (_imageFileStream == null)
                    //                    {
                    _imageFileStream = new System.IO.FileStream(imageFile, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
                    //                    }
                    //                    else
                    //                    {

                    _imageFileStream.Position = 0;
                    //                    }
                    pictureBoxPhoto.Image.Save(_imageFileStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                catch (Exception ex1)
                {
                    Globals.LogError("Error saving image file", ex1.Message, ex1.StackTrace);
                }

            }

            if (_newFlag)
            {
                LocalData.AddComponent(_currentTaggedComponent);
                _newFlag = false;
            }
            else
            {
                LocalData.SaveComponents();
            }

            try
            {
                TagSaved(this, EventArgs.Empty);
            }
            catch (Exception ex) { }

            _lastChild.Location = string.Empty;

            MainForm._projectDirty = true;

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (currentFormChanged || listViewChildren.Items.Count > 0)
            {
                if (MessageBox.Show("You are about to undo all changes to this tag. Close form anyway?", "Lose Changes?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                {
                    _currentTaggedComponent = new TaggedComponent();
                    resetFormData();
                    Hide();
                }
            }
            else
            {
                Hide();
            }
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
            if (textBoxLDARTag.Text == "")
            {
                MessageBox.Show("Tag cannot be empty.");
                return;
            }
            else
            {
                string resultMsg = "";
                if (!(clsCommonFunctions.ValidTagFormat(textBoxLDARTag.Text, ref resultMsg)))
                {
                    MessageBox.Show(resultMsg);
                    return;
                }
            }

            FormChildObject co = new FormChildObject(textBoxLocation.Text);
            co.AllowInspections = false;
            co.InspectionBackground = _currentTaggedComponent.InspectionBackground;
            co.InspectionInspector = _currentTaggedComponent.InspectionInspector;
            co.InspectionInstrument = _currentTaggedComponent.InspectionInstrument;
            co.LDARTag = getNextChild();
            co.AllowEditTag = Properties.Settings.Default.AllowChildTagEdit;
            co.EditMode = false;
            if (!string.IsNullOrEmpty(_lastChild.ComponentType)) co.ComponentType = _lastChild.ComponentType;
            if (string.IsNullOrEmpty(_lastChild.Location))
            {
                co.LocationDescription = textBoxLocation.Text;
            }
            else
            {
                co.LocationDescription = _lastChild.Location;
            }
            co.Size = Globals.getSizeFromString(textBoxSize.Text);

            if (textBoxPreviousTag.Text != "")
            {
                //TODO: Disabling copying old tag by default, but this needs to be an option
                //co.PreviousTag = textBoxPreviousTag.Text + "." + Globals.getTagPoint(listViewChildren.Items.Count + 1, 2);
            }
            DialogResult dr = co.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                ListViewItem item = new ListViewItem();
                item.Text = co.ComponentType;
                item.SubItems.Add(co.LDARTag);

                item.SubItems.Add(co.PreviousTag);
                item.SubItems.Add(co.LocationDescription);
                item.SubItems.Add(co.Inspected.ToString());
                item.SubItems.Add(co.InspectionInspector);
                item.SubItems.Add(co.InspectionDate.ToString());
                item.SubItems.Add(co.InspectionReading.ToString());
                item.SubItems.Add(co.InspectionBackground.ToString());
                item.SubItems.Add(co.InspectionInstrument);
                item.SubItems.Add(co.Size.ToString());
                listViewChildren.Items.Add(item);
                _lastChildType = co.ComponentType;
                _lastChild.ComponentType = co.ComponentType;
                _lastChild.Inspected = co.Inspected;
                _lastChild.InspectionBackground = co.InspectionBackground;
                _lastChild.InspectionDate = co.InspectionDate;
                _lastChild.InspectionInstrument = co.InspectionInstrument;
                _lastChild.InspectionInspector = co.InspectionInspector;
                _lastChild.PreviousTag = co.PreviousTag;
                _lastChild.Size = co.Size;
                _lastChild.Location = co.LocationDescription;

            }

            SequenceChildTags();
        }

        private void SequenceChildTags()
        {

            if (Properties.Settings.Default.AllowChildTagEdit)
            {

                foreach (ListViewItem lvi in listViewChildren.Items)
                {
                    lvi.SubItems[1].Text = Globals.getBaseTag(textBoxLDARTag.Text) + "." + Globals.getTagPoint(lvi.SubItems[1].Text);
                }

                List<ListViewItem> tmpList = new List<ListViewItem>();

                foreach (ListViewItem lvi in listViewChildren.Items)
                {
                    ListViewItem tmpItem = new ListViewItem();
                    tmpItem.Text = lvi.Text;
                    for (int i = 1; i < lvi.SubItems.Count; i++)
                    {
                        tmpItem.SubItems.Add(lvi.SubItems[i].Text);
                    }
                    tmpList.Add(tmpItem);
                }

                listViewChildren.Items.Clear();

                foreach (ListViewItem lvi in tmpList.OrderBy(c => c.SubItems[1].Text))
                {
                    listViewChildren.Items.Add(lvi);
                }
            }
            else
            {
                for (int i = 0; i < listViewChildren.Items.Count; i++)
                {
                    listViewChildren.Items[i].SubItems[1].Text = textBoxLDARTag.Text + "." + Globals.getTagPoint((i + Globals.CurrentProjectData.LDARTagStartChildrenNumber), Globals.CurrentProjectData.LDARTagPaddedZeros);

                }
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

            refreshTagFromExisting(this.textBoxPreviousTag.Text, "", true);
            checkForRelatedComponents();

        }

        private void refreshTagFromExisting(string OldTag, string extension, bool useRefTag = false)
        {
            string tagID = string.Empty;
            string categoryCode = string.Empty;
            string reasonDesc = string.Empty;
            int? categoryId = 0;
            int? reasonId = 0;

            LDARComponent comp = Globals.CurrentProjectData.LDARData.ExistingComponents.Where(c => c.ComponentTag == OldTag).FirstOrDefault();

            //if this didn't work abort!
            if (comp == null) return;

            //update location description
            tagID = comp.ComponentTag.ToString();
            if (tagID == OldTag)
            {
                this.textBoxLocation.Text = comp.LocationDescription.ToString();
            }

            //update drawing

            //TODO: Disabled for Shell Norco project
            //if (comp.Drawing != null && textBoxDrawing.BackColor != Color.FromKnownColor(KnownColor.Control)) textBoxDrawing.Text = comp.Drawing;
            //if (comp.compProperty != null) textBoxProperty.Text = comp.compProperty;

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
                if (tagID == OldTag || categoryId != null)
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

            if (useRefTag)
            {
                textBoxRouteNo.Text = comp.RouteSequence.ToString();
                LocalData.LastRouteNo = comp.RouteSequence;
            }
            //TODO: Disabled for Shell Norco project
            //LDARProcessUnit ldUnit = Globals.CurrentProjectData.LDARData.ProcessUnits.Where(c => c.ProcessUnitId == comp.ProcessUnitId).FirstOrDefault();
            //if (ldUnit != null) this.comboBoxUnit.Text = ldUnit.UnitDescription;

            if (Globals.CurrentProjectData.ProjectType == LDARProjectType.EiMOC)
            {

                LDARComponentStream ldStream = Globals.CurrentProjectData.LDARData.ComponentStreams.Where(c => c.ComponentStreamId == comp.ChemicalStreamId).FirstOrDefault();
                if (ldStream != null && comboBoxStream.BackColor != Color.FromKnownColor(KnownColor.Control)) this.comboBoxStream.Text = ldStream.StreamDescription;
            }


            //TODO: Disabled for Shell Norco project
            //LDARChemicalState ldState = Globals.CurrentProjectData.LDARData.ChemicalStates.Where(c => c.ChemicalStateId == comp.ChemicalStateId).FirstOrDefault();
            //if (ldState != null) this.comboBoxState.Text = ldState.ChemicalState;

            LDARArea ldArea = Globals.CurrentProjectData.LDARData.Areas.Where(c => c.AreaId == comp.AreaID).FirstOrDefault();
            if (ldArea != null) comboBoxArea.Text = ldArea.AreaDescription;



            //TODO: Custom logic for Marathon Garyville
            string tmpManuf = string.Empty;
            LDARManufacturer ldManufacturer = Globals.CurrentProjectData.LDARData.Manufacturers.Where(c => c.ManufacturerId == comp.ManufacturerID).FirstOrDefault();
            if (ldManufacturer != null)
            {
                tmpManuf = ldManufacturer.ManufacturerCode;
                if (comboBoxManufacturer.Items.Contains(tmpManuf))
                {
                    comboBoxManufacturer.Text = tmpManuf;
                }
                else
                {
                    comboBoxManufacturer.Text = "OTHER";
                }
            }
            else
            {
                comboBoxManufacturer.Text = "OTHER";
            }

            if (!_currentTaggedComponent.isDrawingTag)
            {
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
            }

            checkForRelatedComponents();

        }

        private void checkForRelatedComponents()
        {
            if (textBoxPreviousTag.Text != "" && Globals.CurrentProjectData.LDARData.ExistingComponents.Count > 0)
            {
                try
                {
                    Regex tagSearch = new Regex(@"^(" + textBoxPreviousTag.Text + @"[\.]\w*)");
                    //int relatedComponents = Globals.CurrentProjectData.LDARData.ExistingComponents.Where(c => c.ComponentTag.Contains(textBoxPreviousTag.Text) && c.ComponentTag != textBoxPreviousTag.Text).Count();
                    int relatedComponents = Globals.CurrentProjectData.LDARData.ExistingComponents.Where(c => tagSearch.IsMatch(c.ComponentTag) && c.ComponentTag != textBoxPreviousTag.Text).Count();
                    if (relatedComponents > 0)
                    {
                        labelRelatedTags.Text = relatedComponents.ToString() + " Related";
                    }
                    else
                    {
                        labelRelatedTags.Text = "";
                    }
                }
                catch { }
            }
        }

        private void textBoxPreviousTag_TextChanged(object sender, EventArgs e)
        {

            if (Globals.CurrentProjectData == null) return;

            labelRelatedTags.Text = "";

            string tagID = string.Empty;
            LDARComponent comp = Globals.CurrentProjectData.LDARData.ExistingComponents.Where(c => c.ComponentTag == this.textBoxPreviousTag.Text).FirstOrDefault();
            if (comp != null)
            {
                tagID = comp.ComponentTag.ToString();
                if (tagID == this.textBoxPreviousTag.Text)
                {
                    this.buttonRefreshOldTag.Enabled = true;

                    if (!_allowOldTagRefresh) pictureBoxOldTagOk.Visible = true;
                }
                else
                {
                    this.buttonRefreshOldTag.Enabled = false;

                    if (!_allowOldTagRefresh) pictureBoxOldTagOk.Visible = false;
                }
            }
            else
            {
                this.buttonRefreshOldTag.Enabled = false;

                if (!_allowOldTagRefresh) pictureBoxOldTagOk.Visible = false;
            }

        }

        private void FormEditObject_Load(object sender, EventArgs e)
        {


            fillCategoryReasonList();
            fillUnitList();
            fillStateList();
            fillStreamList();
            fillTypeList();
            fillAreaList();
            fillManufacturerList();
        }

        private void fillCategoryReasonList()
        {
            List<string> crList = new List<string>();
            string accessText = string.Empty;

            //this should always be an option
            crList.Add("NTM");
            //crList.Add("DTM - >2 Meters");
            //crList.Add("DTM - 8 Foot Ladder");
            //crList.Add("DTM - Extension Ladder");
            //crList.Add("DTM - Harness");
            //crList.Add("DTM - Inaccessible Component");
            //crList.Add("DTM - Scaffold");
            //crList.Add("UTM - Dangerous Location");
            //crList.Add("UTM - Inaccessible Location");

            if (Globals.CurrentProjectData != null)
            {
                this.comboBoxAccess.Items.Clear();


                foreach (LDARCategory ldc in Globals.CurrentProjectData.LDARData.ComponentCategories)
                {
                    foreach (LDARReason ldr in Globals.CurrentProjectData.LDARData.ComponentReasons)
                    {
                        if (ldr.ComponentCategoryID == ldc.ComponentCategoryID && ldc.showInTablet && ldr.showInTablet)
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
                foreach (LDARComponentClassType ldt in Globals.CurrentProjectData.LDARData.ComponentClassTypes.Where(c => c.parentType))
                {
                    if (ldt.ComponentTypeId == 0)
                    {
                        if (ldt.ComponentClass != null && ldt.showInTablet) typeList.Add(ldt.ComponentClass);
                    }
                    else
                    {
                        if (ldt.ComponentClass != null && ldt.showInTablet) typeList.Add(ldt.ComponentClass + " - " + ldt.ComponentType);
                    }
                }

                if (typeList.Count == 0)
                {
                    //we have to have parents, so if none are selected select all
                    foreach (LDARComponentClassType ldt in Globals.CurrentProjectData.LDARData.ComponentClassTypes)
                    {
                        if (ldt.ComponentTypeId == 0)
                        {
                            if (ldt.ComponentClass != null && ldt.showInTablet) typeList.Add(ldt.ComponentClass);
                        }
                        else
                        {
                            if (ldt.ComponentClass != null && ldt.showInTablet) typeList.Add(ldt.ComponentClass + " - " + ldt.ComponentType);
                        }
                    }
                }

                typeList.Sort();
                foreach (string str in typeList)
                {
                    comboBoxType.Items.Add(str);
                }

                this.Text = "Component Edit (LeakDAS Integrated)";
            }
        }

        private void fillUnitList()
        {
            if (Globals.CurrentProjectData != null)
            {
                this.comboBoxUnit.Items.Clear();
                foreach (LDARProcessUnit ldu in Globals.CurrentProjectData.LDARData.ProcessUnits)
                {
                    if (ldu.UnitDescription != null && ldu.showInTablet) this.comboBoxUnit.Items.Add(ldu.UnitDescription);
                }
            }
        }

        private void fillAreaList()
        {
            if (Globals.CurrentProjectData != null)
            {
                this.comboBoxArea.Items.Clear();
                foreach (LDARArea lda in Globals.CurrentProjectData.LDARData.Areas)
                {
                    if (lda.AreaDescription != null && lda.showInTablet) comboBoxArea.Items.Add(lda.AreaDescription);
                }
            }
        }

        private void fillManufacturerList()
        {


            //TODO: Overriding this for Marathon Garyville

            //if (Globals.CurrentProjectData != null)
            //{
            //    this.comboBoxManufacturer.Items.Clear();
            //    foreach (LDARManufacturer ldm in Globals.CurrentProjectData.LDARData.Manufacturers)
            //    {
            //        if (ldm.ManufacturerCode != null && ldm.showInTablet) comboBoxManufacturer.Items.Add(ldm.ManufacturerCode);
            //    }
            //}

            this.comboBoxManufacturer.Items.Clear();

            comboBoxManufacturer.Items.Add("VOGT");
            comboBoxManufacturer.Items.Add("SMITH");
            comboBoxManufacturer.Items.Add("VELAN");
            comboBoxManufacturer.Items.Add("BONNEY FORGE");
            comboBoxManufacturer.Items.Add("CRANE");
            comboBoxManufacturer.Items.Add("FISHER");
            comboBoxManufacturer.Items.Add("KITZ");
            comboBoxManufacturer.Items.Add("POWELL");
            comboBoxManufacturer.Items.Add("WALWORTH");
            comboBoxManufacturer.Items.Add("SWAGELOCK");
            comboBoxManufacturer.Items.Add("FLOW SERVE");
            comboBoxManufacturer.Items.Add("JERGERSON");
            comboBoxManufacturer.Items.Add("OMB");
            comboBoxManufacturer.Items.Add("SWI");
            comboBoxManufacturer.Items.Add("PK");
            comboBoxManufacturer.Items.Add("APOLLO");
            comboBoxManufacturer.Items.Add("JAMESBURY");
            comboBoxManufacturer.Items.Add("NEWAY");
            comboBoxManufacturer.Items.Add("OTHER");
            this.comboBoxManufacturer.Sorted = true;
        }

        private void fillStreamList()
        {
            if (Globals.CurrentProjectData != null)
            {
                this.comboBoxStream.Items.Clear();
                foreach (LDARComponentStream lds in Globals.CurrentProjectData.LDARData.ComponentStreams)
                {
                    if (lds.StreamDescription != null && lds.showInTablet) this.comboBoxStream.Items.Add(lds.StreamDescription);
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
                    if (lds.ChemicalState != null && lds.showInTablet) this.comboBoxState.Items.Add(lds.ChemicalState);
                }
            }
        }

        private void textBoxSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == '/')
            {
                if (e.KeyChar == '.' & this.textBoxSize.Text.Contains(".")) e.KeyChar = char.MinValue;
                if (e.KeyChar == '/' & this.textBoxSize.Text.Contains("/")) e.KeyChar = char.MinValue;
                if (e.KeyChar == '/' & this.textBoxSize.Text.Contains(".")) e.KeyChar = char.MinValue;
                if (e.KeyChar == '.' & this.textBoxSize.Text.Contains("/")) e.KeyChar = char.MinValue;
            }
            else e.KeyChar = char.MinValue;
        }

        private void buttonNextComponent_Click(object sender, EventArgs e)
        {
            if (textBoxLDARTag.Text.Contains("X") || textBoxPreviousTag.Text.Contains("X"))
            {
                if (MessageBox.Show("Tag/Old Tag contains an 'X', continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }
            if (validateFormData())
            {
                saveTag();
                resetFormData();
                SetComponentNew(Guid.NewGuid().ToString(), "None", "", "", "", "", false);
            }
        }

        private void resetFormData()
        {

            _currentTaggedComponent = new TaggedComponent();
            _currentTaggedComponent.Id = Guid.NewGuid().ToString();

            _currentTaggedComponent.Children = new List<ChildComponent>();
            this.textBoxLDARTag.Text = getXTag(Properties.Settings.Default.LastComponentLDARTag);
            this.textBoxPreviousTag.Text = getXTag(Properties.Settings.Default.LastOldTag);


            if (Globals.CurrentProjectData.LDARTAGFormat != null)
            {
                if (Globals.CurrentProjectData.LDARTAGFormat != "None")
                {
                    this.picInfo.Visible = true;
                    this.lblTagFormat.Visible = true;
                    this.lblTagFormat.Text = "Tag Format: " + Globals.CurrentProjectData.LDARTAGFormat;
                    ToolTip toolTip1 = new ToolTip();
                    toolTip1.SetToolTip(this.textBoxLDARTag, "Tag Format: " + Globals.CurrentProjectData.LDARTAGFormat);

                    ToolTip toolTip2 = new ToolTip();
                    toolTip2.SetToolTip(this.label4, "Tag Format: " + Globals.CurrentProjectData.LDARTAGFormat);

                    ToolTip toolTip3 = new ToolTip();
                    toolTip3.SetToolTip(this.picInfo, "Tag Format: " + Globals.CurrentProjectData.LDARTAGFormat);
                }
                else
                {
                    this.picInfo.Visible = false;
                    this.lblTagFormat.Visible = false;
                    this.lblTagFormat.Text = "";
                }
            }
            else
            {
                this.picInfo.Visible = false;
                this.lblTagFormat.Visible = false;
                this.lblTagFormat.Text = "";
            }

            this.lblTagFormat.Visible = false;

            pictureBoxEditMode.Visible = false;
            buttonDeleteTag.Enabled = false;


            pictureBoxPhoto.Image = pictureBoxPhoto.InitialImage;
            labelInspection.Text = "None";
            listViewChildren.Items.Clear();
        }

        private void setFormValues(bool existingTag)
        {

            if (existingTag)
            {
                pictureBoxEditMode.Visible = true;
                buttonDeleteTag.Enabled = true;
                currentFormChanged = false;
            }
            else
            {
                pictureBoxEditMode.Visible = false;
                buttonDeleteTag.Enabled = false;
                pictureBoxPhoto.Image = pictureBoxPhoto.InitialImage;
                currentFormChanged = true;
            }

            textBoxDrawing.Text = _currentTaggedComponent.Drawing;
            if (string.IsNullOrEmpty(_currentTaggedComponent.ModifiedBy))
            {
                labelCurrentUser.Text = Environment.UserName;
                _currentTaggedComponent.ModifiedBy = Environment.UserName;
            }
            else
            {
                labelCurrentUser.Text = _currentTaggedComponent.ModifiedBy;
            }

            if (_currentTaggedComponent.Inspected)
            {
                labelInspection.Text = _currentTaggedComponent.InspectionReading + " PPM by " + _currentTaggedComponent.InspectionInspector + " on " + _currentTaggedComponent.InspectionDate.ToString();
            }
            else
            {
                labelInspection.Text = "None";
            }

            //TODO: disabled for Shell Norco project

            //textBoxMOCNumber.Text = _currentTaggedComponent.MOCNumber;
            textBoxMOCNumber.Text = string.Empty;
            //comboBoxUnit.Text = _currentTaggedComponent.Unit;
            comboBoxUnit.Text = string.Empty;
            //comboBoxManufacturer.Text = _currentTaggedComponent.Manufacturer;
            comboBoxManufacturer.Text = "OTHER";
            //textBoxProperty.Text = _currentTaggedComponent.Property;
            textBoxProperty.Text = string.Empty;
            //comboBoxState.Text = _currentTaggedComponent.ChemicalState;
            comboBoxState.Text = string.Empty;
            textBoxDrawing.Text = _currentTaggedComponent.Drawing;
            textBoxLDARTag.Text = _currentTaggedComponent.LDARTag;
            textBoxPreviousTag.Text = _currentTaggedComponent.PreviousTag;
            textBoxLocation.Text = _currentTaggedComponent.Location;
            textBoxSize.Text = _currentTaggedComponent.Size.ToString();
            comboBoxAccess.Text = _currentTaggedComponent.Access;
            comboBoxStream.Text = _currentTaggedComponent.Stream;
            comboBoxType.Text = _currentTaggedComponent.ComponentType;
            comboBoxArea.Text = _currentTaggedComponent.Area;
            textBoxRouteNo.Text = _currentTaggedComponent.RouteSequence.ToString();
            textBoxReferenceTag.Text = _currentTaggedComponent.ReferenceTag;

            //checkBoxAttachDrawing.Checked = c.AttachDrawing;

            if (_currentTaggedComponent.EngineeringTag != null && _currentTaggedComponent.isDrawingTag)
            {
                labelCADID.Text = _currentTaggedComponent.EngineeringTag;
            }
            else
            {
                labelCADID.Text = "None";
            }

            if (_currentTaggedComponent.Children != null)
            {
                foreach (ChildComponent cc in _currentTaggedComponent.Children)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = cc.ComponentType;
                    item.SubItems.Add(cc.LDARTag);

                    item.SubItems.Add(cc.PreviousTag);
                    item.SubItems.Add(cc.Location);
                    item.SubItems.Add(cc.Inspected.ToString());
                    item.SubItems.Add(cc.InspectionInspector);
                    item.SubItems.Add(cc.InspectionDate.ToString());
                    item.SubItems.Add(cc.InspectionReading.ToString());
                    item.SubItems.Add(cc.InspectionBackground.ToString());
                    item.SubItems.Add(cc.InspectionInstrument);
                    item.SubItems.Add(cc.Size.ToString());
                    listViewChildren.Items.Add(item);
                }
            }
            else
            {
                listViewChildren.Clear();
            }

            string imageFile = Globals.WorkingFolder + "\\Images\\" + _currentTaggedComponent.Id.Replace(':', '_') + ".jpg";

            try
            {

                if (System.IO.File.Exists(imageFile))
                {
                    _imageFileStream = new System.IO.FileStream(imageFile, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
                    _imageFileStream.Position = 0;
                    pictureBoxPhoto.Image = Image.FromStream(_imageFileStream);
                    _currentTaggedComponent.AttachDrawing = true;
                }
            }
            catch (Exception ex)
            {
                Globals.LogError("Cannot open image", ex.Message, ex.StackTrace);
            }

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
                        _currentTaggedComponent.InspectionBackground = bgTemp;
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
            if (!_currentTaggedComponent.Inspected)
            {
                _currentTaggedComponent.InspectionBackground = MainForm._lastBackground;
                _currentTaggedComponent.InspectionInspector = MainForm._lastInspector;
                _currentTaggedComponent.InspectionInstrument = MainForm._lastInstrument;
            }
            else
            {
                ins.EditExisting = true;
                ins.Reading = _currentTaggedComponent.InspectionReading;
            }
            ins.Background = _currentTaggedComponent.InspectionBackground;
            ins.Inspector = _currentTaggedComponent.InspectionInspector;
            ins.Instrument = _currentTaggedComponent.InspectionInstrument;
            DialogResult dr = ins.ShowDialog();
            ins.TopMost = true;
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                if (ins.RemoveInspection)
                {
                    _currentTaggedComponent.Inspected = false;
                    _currentTaggedComponent.InspectionBackground = -100;
                    _currentTaggedComponent.InspectionReading = -100;
                    _currentTaggedComponent.InspectionInspector = "";
                    _currentTaggedComponent.InspectionInstrument = "";
                    labelInspection.Text = "None";
                }
                else
                {
                    _currentTaggedComponent.InspectionDate = DateTime.Now;
                    _currentTaggedComponent.InspectionInstrument = ins.Instrument;
                    _currentTaggedComponent.InspectionReading = ins.Reading;
                    _currentTaggedComponent.InspectionInspector = ins.Inspector;
                    _currentTaggedComponent.InspectionBackground = ins.Background;
                    _currentTaggedComponent.Inspected = true;
                    MainForm._lastBackground = ins.Background;
                    MainForm._lastInspector = ins.Inspector;
                    MainForm._lastInstrument = ins.Instrument;
                    labelInspection.Text = ins.Reading + " PPM by" + ins.Inspector;
                }
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
                    _currentTaggedComponent.AttachDrawing = true;
                }
                else
                {
                    pictureBoxPhoto.Image = pictureBoxPhoto.InitialImage;
                    _currentTaggedComponent.AttachDrawing = false;
                }
            }
        }

        private void FormEditTag_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_imageFileStream != null) _imageFileStream.Dispose();
            Hide();
            e.Cancel = true;
        }

        private bool validateFormData()
        {
            List<string> errorList = new List<string>();
            string errString = "";
            bool dataValid = true;

            //TODO: Some checks are disabled through code for Shell Norco project, they need to be re-enabled for generic builds.


            if (textBoxLDARTag.Text == "")
            {
                errorList.Add("Tag cannot be empty");
            }
            else
            {
                string resultMsg = "";
                if (!(clsCommonFunctions.ValidTagFormat(textBoxLDARTag.Text, ref resultMsg)))
                {
                    errorList.Add(resultMsg);
                }
            }

            if (textBoxLocation.Text == "") errorList.Add("Location cannot be empty");
            if (textBoxSize.Text == "") errorList.Add("Size cannot be empty");
            //if (comboBoxState.Text == "") errorList.Add("State cannot be empty");
            if (comboBoxAccess.Text == "") errorList.Add("Access cannot be empty");
            if (comboBoxStream.Text == "") errorList.Add("Stream cannot be empty");
            if (comboBoxType.Text == "") errorList.Add("Type cannot be empty");
            //if (comboBoxUnit.Text == "") errorList.Add("Unit cannot be empty");

            if (LocalData.doesTagExist(textBoxLDARTag.Text, _currentTaggedComponent.Id)) errorList.Add("The new tag has already been documented");
            if (!Properties.Settings.Default.AllowDuplicateLocation) if (LocalData.doesLocationDescriptionExist(textBoxLocation.Text, _currentTaggedComponent.Id)) errorList.Add("The location description has already been used");
            if (textBoxPreviousTag.Text != "")
            {
                if (LocalData.doesPreviousTagExist(textBoxPreviousTag.Text, _currentTaggedComponent.Id)) errorList.Add("The old tag has already been documented");
            }
            if (Globals.getSizeFromString(textBoxSize.Text) == -1) errorList.Add("Invalid size");

            if (!string.IsNullOrEmpty(textBoxPreviousTag.Text))
            {
                LDARComponent targetOldTag = Globals.CurrentProjectData.LDARData.ExistingComponents.Where(c => c.ComponentTag == textBoxPreviousTag.Text).FirstOrDefault();
                if (targetOldTag == null) errorList.Add("Previous tag doesn't exist in target database");
            }

            if (!Properties.Settings.Default.AllowDuplicateLocation)
            {
                List<string> locList = new List<string>();
                foreach (ListViewItem itm in listViewChildren.Items)
                {

                    if (itm.SubItems[3].Text == textBoxLocation.Text)
                    {
                        errorList.Add("Duplicate location descriptions exist");
                        break;
                    }

                    if (!locList.Contains(itm.SubItems[3].Text))
                        locList.Add(itm.SubItems[3].Text);
                    else
                    {
                        errorList.Add("Duplicate location descriptions exist");
                        break;
                    }
                }
            }

            if (errorList.Count() > 0)
            {
                dataValid = false;
                foreach (string err in errorList)
                {
                    errString = errString + err + Environment.NewLine;
                }
                MessageBox.Show(errString);
            }
            else
            {
                //additional warnings here
                if (string.IsNullOrEmpty(textBoxPreviousTag.Text))
                {
                    DialogResult WarningDR = MessageBox.Show("You have not documented an Old Tag.  This will be a Title V deviation.  Are you sure?", "Confirm No Old Tag", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (WarningDR == DialogResult.No) dataValid = false;
                }
            }

            return dataValid;

        }

        private void listViewChildren_DoubleClick(object sender, EventArgs e)
        {
            ChildComponent child = new ChildComponent();
            if (listViewChildren.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewChildren.SelectedItems[0];
                child.ComponentType = item.SubItems[0].Text;
                child.Inspected = bool.Parse(item.SubItems[4].Text);
                child.InspectionBackground = double.Parse(item.SubItems[8].Text);
                child.InspectionDate = DateTime.Parse(item.SubItems[6].Text);
                child.InspectionInspector = item.SubItems[5].Text;
                child.InspectionInstrument = item.SubItems[9].Text;
                child.InspectionReading = double.Parse(item.SubItems[7].Text);
                child.LDARTag = item.SubItems[1].Text;

                child.Location = item.SubItems[3].Text;
                child.PreviousTag = item.SubItems[2].Text;
                child.Size = Globals.getSizeFromString(item.SubItems[10].Text);

                FormChildObject cc = new FormChildObject(textBoxLocation.Text);
                cc.AllowInspections = false;
                cc.setComponent(child);
                cc.EditMode = true;
                cc.AllowEditTag = Properties.Settings.Default.AllowChildTagEdit;

                if (cc.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    item.Text = cc.ComponentType;
                    item.SubItems[0].Text = cc.ComponentType;
                    item.SubItems[1].Text = cc.LDARTag;
                    item.SubItems[2].Text = cc.PreviousTag;
                    item.SubItems[3].Text = cc.LocationDescription;
                    item.SubItems[4].Text = cc.Inspected.ToString();
                    item.SubItems[5].Text = cc.InspectionInspector;
                    item.SubItems[6].Text = cc.InspectionDate.ToString();
                    item.SubItems[7].Text = cc.InspectionReading.ToString();
                    item.SubItems[8].Text = cc.InspectionBackground.ToString();
                    item.SubItems[9].Text = cc.InspectionInstrument;
                    item.SubItems[10].Text = cc.Size.ToString();
                }

                SequenceChildTags();
            }

        }

        private void FormEditTag_VisibleChanged(object sender, EventArgs e)
        {
            if (_imageFileStream != null) _imageFileStream.Dispose();
        }

        private void pictureBoxOldTagOk_Click(object sender, EventArgs e)
        {
            if (pictureBoxOldTagOk.Visible)
            {
                checkForRelatedComponents();
            }
        }


        private void buttonCurrentUser_Click(object sender, EventArgs e)
        {
            TechnicianDialog td = new TechnicianDialog();

            td.selectedTech = _currentTaggedComponent.ModifiedBy;

            if (td.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _currentTaggedComponent.ModifiedBy = td.selectedTech;
                labelCurrentUser.Text = _currentTaggedComponent.ModifiedBy;
            }
        }

        private void FormEditTag_KeyPress(object sender, KeyPressEventArgs e)
        {

            currentFormChanged = true;

            if (Properties.Settings.Default.ForceUpperCase)
            {
                if (char.IsLetter(e.KeyChar))
                {
                    e.KeyChar = char.ToUpper(e.KeyChar);
                }
            }
        }

        private void buttonRefreshReferenceTag_Click(object sender, EventArgs e)
        {
            refreshTagFromExisting(this.textBoxReferenceTag.Text, "", false);
        }

        private void textBoxReferenceTag_TextChanged(object sender, EventArgs e)
        {
            if (Globals.CurrentProjectData == null) return;

            string tagID = string.Empty;
            LDARComponent comp = Globals.CurrentProjectData.LDARData.ExistingComponents.Where(c => c.ComponentTag == this.textBoxReferenceTag.Text).FirstOrDefault();
            if (comp != null)
            {
                tagID = comp.ComponentTag.ToString();
                if (tagID == this.textBoxReferenceTag.Text)
                {
                    this.buttonRefreshReferenceTag.Enabled = true;

                }
                else
                {
                    this.buttonRefreshReferenceTag.Enabled = false;

                }
            }
            else
            {
                this.buttonRefreshReferenceTag.Enabled = false;

            }
        }

        private void labelRelatedTags_Click(object sender, EventArgs e)
        {

            if (listViewChildren.Items.Count > 0)
            {
                if (MessageBox.Show("Clear existing children and refresh from LDAR Data?", "Confirm", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    listViewChildren.Items.Clear();
                }
                else
                {
                    return;
                }
            }

            if (textBoxPreviousTag.Text != "" && Globals.CurrentProjectData.LDARData.ExistingComponents.Count > 0)
            {
                try
                {
                    List<LDARComponent> matchedComponents = new List<LDARComponent>();
                    Regex tagSearch = new Regex(@"^(" + textBoxPreviousTag.Text + @"[\.]\w*)");
                    matchedComponents = Globals.CurrentProjectData.LDARData.ExistingComponents.Where(c => tagSearch.IsMatch(c.ComponentTag) && c.ComponentTag != textBoxPreviousTag.Text).OrderBy(c => c.ComponentTag).ToList<LDARComponent>();
                    //matchedComponents = Globals.CurrentProjectData.LDARData.ExistingComponents.Where(c => c.ComponentTag.Contains(textBoxPreviousTag.Text) && c.ComponentTag != textBoxPreviousTag.Text).OrderBy(c => c.ComponentTag).ToList<LDARComponent>();                    bool proceed = true;
                    bool proceed = true;
                    if (matchedComponents.Count > 5)
                    {
                        DialogResult dr = MessageBox.Show("More than 5 tags match your old tag criteria, continue?", "Warning", MessageBoxButtons.YesNo);
                        if (dr == System.Windows.Forms.DialogResult.No) proceed = false;
                    }
                    if (matchedComponents.Count > 0 && proceed)
                    {
                        foreach (LDARComponent ldc in matchedComponents)
                        {
                            ListViewItem item = new ListViewItem();
                            string classTypeText = string.Empty;
                            if (ldc.ComponentTypeId != null)
                            {
                                LDARComponentClassType ldClassType = Globals.CurrentProjectData.LDARData.ComponentClassTypes.Where(c => c.ComponentClassId == ldc.ComponentClassId && c.ComponentTypeId == ldc.ComponentTypeId).FirstOrDefault();
                                if (ldClassType != null) classTypeText = ldClassType.ComponentClass + " - " + ldClassType.ComponentType;
                            }
                            else
                            {
                                LDARComponentClassType ldClassType = Globals.CurrentProjectData.LDARData.ComponentClassTypes.Where(c => c.ComponentClassId == ldc.ComponentClassId).FirstOrDefault();
                                if (ldClassType != null) classTypeText = ldClassType.ComponentClass;
                            }

                            item.Text = classTypeText;
                            item.SubItems.Add(textBoxLDARTag.Text + "." + Globals.getPointFromExistingTag(ldc.ComponentTag));
                            //item.SubItems.Add(Globals.getNextTagWithPoint(textBoxLDARTag.Text, Globals.CurrentProjectData.LDARTagStartChildrenNumber, Globals.CurrentProjectData.LDARTagPaddedZeros));
                            item.SubItems.Add(ldc.ComponentTag);
                            item.SubItems.Add(ldc.LocationDescription);
                            item.SubItems.Add("False");
                            item.SubItems.Add("");
                            item.SubItems.Add(DateTime.Now.ToString());
                            item.SubItems.Add("0");
                            item.SubItems.Add("0");
                            item.SubItems.Add("");
                            item.SubItems.Add(ldc.Size);
                            listViewChildren.Items.Add(item);
                            _lastChildType = classTypeText;
                        }

                        SequenceChildTags();
                    }
                }
                catch { }
            }

        }

        private void textBoxLDARTag_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '"' || e.KeyChar == '\\') e.KeyChar = char.MinValue;
        }

        private void textBoxPreviousTag_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '"' || e.KeyChar == '\\') e.KeyChar = char.MinValue;
        }

        private void buttonDeleteTag_Click(object sender, EventArgs e)
        {
            if (pictureBoxEditMode.Visible)
            {
                if (MessageBox.Show("Delete tag " + textBoxLDARTag.Text + "?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    LocalData.RemoveComponent(_currentTaggedComponent);
                    Hide();
                }
            }
        }

        private void buttonRouteAfterSelect_Click(object sender, EventArgs e)
        {
            string routeAfter = string.Empty;
            DialogResult dr = Globals.InputBox("Route After", "Please enter the route after tag you wish to use:", ref routeAfter);

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                double nextRoute = 0.0;
                nextRoute = LocalData.getNextRouteNumber(routeAfter, comboBoxUnit.Text);
                this.textBoxRouteNo.Text = nextRoute.ToString();
                LocalData.LastRouteNo = nextRoute;
            }
        }

        private void textBoxRouteNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == '/')
            {
                if (e.KeyChar == '.' & this.textBoxRouteNo.Text.Contains(".")) e.KeyChar = char.MinValue;
                if (e.KeyChar == '/' & this.textBoxRouteNo.Text.Contains("/")) e.KeyChar = char.MinValue;
                if (e.KeyChar == '/' & this.textBoxRouteNo.Text.Contains(".")) e.KeyChar = char.MinValue;
                if (e.KeyChar == '.' & this.textBoxRouteNo.Text.Contains("/")) e.KeyChar = char.MinValue;
            }
            else e.KeyChar = char.MinValue;
        }

        private void labelCADID_TextChanged(object sender, EventArgs e)
        {
            if (labelCADID.Text != "None")
            {
                labelCADID.BackColor = Color.LightGreen;
            }
            else
            {
                labelCADID.BackColor = MainForm.DefaultBackColor;
            }
        }
        private string getNextChild()
        {

            string lastTag = textBoxLDARTag.Text;

            if (listViewChildren.Items.Count > 0)
            {
                List<string> childTags = new List<string>();
                foreach (ListViewItem lvi in listViewChildren.Items)
                {
                    childTags.Add(lvi.SubItems[1].Text);
                }

                lastTag = childTags.Max();
            }

            return Globals.getNextTagWithPoint(lastTag, Globals.CurrentProjectData.LDARTagStartChildrenNumber, Globals.CurrentProjectData.LDARTagPaddedZeros);
        }

        private void picInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show(clsCommonFunctions.getFormatMessage(Convert.ToString(this.lblTagFormat.Text)));
            return;
        }
    }
}


















































