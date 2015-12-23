using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using EnvInt.Win32.FieldTech.Data;
using EnvInt.Win32.FieldTech.Containers;
using EnvInt.Win32.FieldTech.Dialogs;

using Community.CsharpSqlite.SQLiteClient;


namespace EnvInt.Win32.FieldTech
{
   
    public partial class FormEditTag_GW_Chinese : Form
    {
        public TaggedComponent _currentTaggedComponent = new TaggedComponent();
        public System.IO.FileStream _imageFileStream;
        public event EventHandler TagSaved;
        //public event EventHandler<FindObjectEventArgs> FindObject;
        public bool _allowOldTagRefresh = true;
        private BindingSource bindingSourceChildren = new BindingSource();
        private string _lastChildType = string.Empty;
        private ChildComponent _lastChild = new ChildComponent();
        private bool _newFlag = false;
        private string _defaultExtension = string.Empty;
        private bool currentFormChanged; 
        
        public FormEditTag_GW_Chinese(Point startPosition)
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

            comboBoxInstalled.SelectedIndex = 0;

            foreach (Control c in this.Controls)
            {
                if (c.GetType().ToString() == "System.Windows.Forms.TextBox" || c.GetType().ToString() == "System.Windows.Forms.ComboBox")
                {
                    c.KeyPress += FormEditTag_KeyPress;
                }
            }
            //this.locationDescriptionUserControl1.location = "";
            listViewChildren.Columns[3].Width = listViewChildren.Columns[4].Width = 0;
            locationDescriptionUserControl1.LocationDescriptionChanged += new EventHandler(locationDescriptionUserControl1_LocationChanged);
            locationDescriptionUserControl1.suspendUpdates = true;
        }

        void locationDescriptionUserControl1_LocationChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(locationDescriptionUserControl1.locationDescription))
                this.textBoxLocation.Text = locationDescriptionUserControl1.locationDescription;
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

            //svs talked to shane with this, we need static text for the Chinese EiMOC now instead of coming from GW
            /*foreach (LDAROption o in Globals.CurrentProjectData.LDARData.LDAROptions)
            {
                if (o.OptionName == "Location1Name") labelUnit.Text = o.OptionValue + ":";
                if (o.OptionName == "Location2Name") labelArea.Text = o.OptionValue + ":";
                if (o.OptionName == "Location3Name") labelEquipment.Text = o.OptionValue + ":";
            }*/

            if (Globals.CurrentProjectData.LDARTagStartChildrenNumber < 1) Globals.CurrentProjectData.LDARTagStartChildrenNumber = 1;
            _defaultExtension = Globals.getTagPoint(Globals.CurrentProjectData.LDARTagStartChildrenNumber - 1, Globals.CurrentProjectData.LDARTagPaddedZeros);
        }

 
        private void SetComponentNew(string id, string engineeringTag, string componentType, string stream, string drawing, string size, bool drawingTag)
        {

            _currentTaggedComponent = new TaggedComponent();
            _newFlag = true;
            _currentTaggedComponent.Id = id;
            _currentTaggedComponent.EngineeringTag = engineeringTag;
            
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


            if (!String.IsNullOrEmpty(Properties.Settings.Default.LastEquipment)) _currentTaggedComponent.Equipment = Properties.Settings.Default.LastEquipment;
            if (!String.IsNullOrEmpty(Properties.Settings.Default.LastUnit)) _currentTaggedComponent.Unit = Properties.Settings.Default.LastUnit;
            if (!String.IsNullOrEmpty(Properties.Settings.Default.LastState)) _currentTaggedComponent.ChemicalState = Properties.Settings.Default.LastState;
            if (!String.IsNullOrEmpty(Properties.Settings.Default.LastMOCNumber)) _currentTaggedComponent.MOCNumber = Properties.Settings.Default.LastMOCNumber;
            if (!String.IsNullOrEmpty(Properties.Settings.Default.LastManufacturer)) _currentTaggedComponent.Manufacturer = Properties.Settings.Default.LastManufacturer;
            if (!String.IsNullOrEmpty(Properties.Settings.Default.LastProperty)) _currentTaggedComponent.Property = Properties.Settings.Default.LastProperty;

            if (!String.IsNullOrEmpty(Properties.Settings.Default.LastComponentLocation)) _currentTaggedComponent.Location = Properties.Settings.Default.LastComponentLocation;
            if (!String.IsNullOrEmpty(Properties.Settings.Default.LastComponentLDARTag)) _currentTaggedComponent.LDARTag = getXTag(Properties.Settings.Default.LastComponentLDARTag);
            //if (!String.IsNullOrEmpty(Properties.Settings.Default.LastOldTag)) _currentTaggedComponent.PreviousTag = getXTag(Properties.Settings.Default.LastOldTag);
            if (!String.IsNullOrEmpty(Properties.Settings.Default.ModifiedBy)) _currentTaggedComponent.ModifiedBy = Properties.Settings.Default.ModifiedBy;
            
            //if (!String.IsNullOrEmpty(Properties.Settings.Default.LastAccess)) _currentTaggedComponent.Access = Properties.Settings.Default.LastAccess;
            _currentTaggedComponent.Access = string.Empty;
            _currentTaggedComponent.UTMReason = string.Empty;
            if (!String.IsNullOrEmpty(Properties.Settings.Default.LastArea)) _currentTaggedComponent.Area = Properties.Settings.Default.LastArea;
            _currentTaggedComponent.isDrawingTag = drawingTag;
            _currentTaggedComponent.AttachDrawing = false;
            _currentTaggedComponent.Inspected = false;
            _currentTaggedComponent.InspectionReading = -100;
            _currentTaggedComponent.InspectionBackground = -100;
            _currentTaggedComponent.InspectionInspector = "";
            _currentTaggedComponent.InspectionInstrument = "";
            _currentTaggedComponent.RouteSequence = LocalData.getNextRouteNumber();
            _currentTaggedComponent.Extension = _defaultExtension;
            _currentTaggedComponent.PreviousTagExtension = string.Empty;
            _currentTaggedComponent.ReferenceTagExtension = string.Empty;
            //checkBoxAttachDrawing.Checked = Properties.Settings.Default.LastAttachDrawing;
            checkForRelatedComponents();

            _currentTaggedComponent.CreatedDate = DateTime.Now.ToString();

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
                SetComponentNew(id,engineeringTag,componentType,stream,drawing,size,drawingTag);
            }
            currentFormChanged = false;
        }

        public void SetComponent(string LDARTag, string Extension)
        {
            resetFormData();

            SetComponentNew(Guid.NewGuid().ToString(), null, null, null, null, null, false);

            textBoxLDARTag.Text = LDARTag;
            textBoxExtension.Text = Extension;
            textBoxPreviousTag.Text = LDARTag;
            textBoxOTExtension.Text = Extension;

            refreshTagFromExisting(LDARTag, Extension, true);
            labelRelatedTags_Click(this, null);

            currentFormChanged = false;
        }

        private string getXTag(string nonXTag)
        {
            if (nonXTag != string.Empty)
            {
                return nonXTag.Substring(0, nonXTag.Length - 1) + "X";
            }
            else
            {
                return string.Empty;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (textBoxLDARTag.Text.Contains("X"))
            {
//                if (MessageBox.Show("Tag/Old Tag contains an 'X', continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                if (MessageBox.Show("标签/旧标记包含了一个“X” ，是否继续？Tag contains an 'X', continue?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
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
            Properties.Settings.Default.LastAccess = comboBoxDTM.Text;
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
            Properties.Settings.Default.LastEquipment = this.locationDescriptionUserControl1.equipmentText;
            Properties.Settings.Default.ModifiedBy = labelCurrentUser.Text;
           
            //Properties.Settings.Default.LastAttachDrawing = checkBoxAttachDrawing.Checked;

            Properties.Settings.Default.Save();

            _currentTaggedComponent.Unit = comboBoxUnit.Text;
            _currentTaggedComponent.EngineeringTag = labelCADID.Text;
            _currentTaggedComponent.ComponentType = comboBoxType.Text;
            _currentTaggedComponent.LDARTag = textBoxLDARTag.Text;
            _currentTaggedComponent.Extension = textBoxExtension.Text;
            _currentTaggedComponent.ReferenceTagExtension = textBoxRefExtension.Text;
            _currentTaggedComponent.PreviousTagExtension = textBoxOTExtension.Text;
            _currentTaggedComponent.PreviousTag = textBoxPreviousTag.Text;
            _currentTaggedComponent.Location = textBoxLocation.Text;
            _currentTaggedComponent.Access = comboBoxDTM.Text.ToString();
            _currentTaggedComponent.UTMReason = comboBoxUTM.Text;
            _currentTaggedComponent.ModifiedDate = DateTime.Now.ToString();
            _currentTaggedComponent.ReferenceTag = textBoxReferenceTag.Text;
            //c.AttachDrawing = checkBoxAttachDrawing.Checked;
            if (_currentTaggedComponent.ModifiedBy == null)
            {
                _currentTaggedComponent.ModifiedBy = Environment.UserName;
            }
            else
            {
                _currentTaggedComponent.ModifiedBy = labelCurrentUser.Text;
            }
            _currentTaggedComponent.Stream = comboBoxStream.Text;
            _currentTaggedComponent.Drawing = textBoxDrawing.Text;
            _currentTaggedComponent.ChemicalState = comboBoxState.Text;
            _currentTaggedComponent.Size = Globals.getSizeFromString(textBoxSize.Text);
            _currentTaggedComponent.Children = new List<ChildComponent>();
            _currentTaggedComponent.MOCNumber = this.textBoxMOCNumber.Text;
            _currentTaggedComponent.Area = comboBoxArea.Text;
            _currentTaggedComponent.Equipment = this.locationDescriptionUserControl1.equipmentText;
            _currentTaggedComponent.Manufacturer = comboBoxManufacturer.Text;
            _currentTaggedComponent.InstalledResponse = comboBoxInstalled.Text;
            _currentTaggedComponent.RouteSequence = Double.Parse(textBoxRouteNo.Text);

            if (!_currentTaggedComponent.Inspected)
            {
                _currentTaggedComponent.InspectionBackground = -100;
                _currentTaggedComponent.InspectionReading = -100;
                _currentTaggedComponent.InspectionInspector = "";
                _currentTaggedComponent.InspectionInstrument = "";
            }
			
            _currentTaggedComponent.Children.Clear();
            foreach (ListViewItem item in listViewChildren.Items)
            {
                _currentTaggedComponent.Children.Add(new ChildComponent() { ComponentType = item.Text, LDARTag = item.SubItems[1].Text, Extension = item.SubItems[2].Text, PreviousTag = item.SubItems[3].Text, PreviousTagExtension = item.SubItems[4].Text,
                    Location = item.SubItems[5].Text, Inspected = bool.Parse(item.SubItems[6].Text), InspectionInspector = item.SubItems[7].Text,
                    InspectionDate = DateTime.Parse(item.SubItems[8].Text), InspectionReading = double.Parse(item.SubItems[9].Text), 
                    InspectionBackground = double.Parse(item.SubItems[10].Text), InspectionInstrument = item.SubItems[11].Text, Size = Globals.getSizeFromString(item.SubItems[12].Text)});
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


            _lastChild.Location = string.Empty;

            MainForm._projectDirty = true;

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (currentFormChanged || listViewChildren.Items.Count > 0)
            {
                if (MessageBox.Show("您将要取消所有更改到这个标签。关闭的形式呢？ You are about to undo all changes to this tag. Close form anyway?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
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

        //private void buttonLocationBuilder_Click(object sender, EventArgs e)
        //{
        //    FormLocationBuilder lb = new FormLocationBuilder();
        //    DialogResult dr = lb.ShowDialog();
        //    if (dr == System.Windows.Forms.DialogResult.OK)
        //    {
        //        //this.locationDescriptionUserControl1.locationDescription = textBoxLocation.Text = lb.LocationDescriptor;
        //        this.locationDescriptionUserControl1.locationDescription = lb.LocationDescriptor;
        //    }
        //}

        private void buttonChildAdd_Click(object sender, EventArgs e)
        {
            FormChildObject_GW co = new FormChildObject_GW(textBoxLocation.Text);
            co.AllowInspections = true;
            co.InspectionBackground = _currentTaggedComponent.InspectionBackground;
            co.InspectionInspector = _currentTaggedComponent.InspectionInspector;
            co.InspectionInstrument = _currentTaggedComponent.InspectionInstrument;
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
                //co.PreviousTag = textBoxPreviousTag.Text;
            }
            DialogResult dr = co.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                ListViewItem item = new ListViewItem();
                item.Text = co.ComponentType;
                item.SubItems.Add("");
                item.SubItems.Add("");
                //item.SubItems.Add(co.PreviousTag);
                //item.SubItems.Add(co.PreviousTagExtension);
                item.SubItems.Add("");
                item.SubItems.Add("");
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
                _lastChild.PreviousTag = string.Empty;
                _lastChild.Size = co.Size;
                _lastChild.Location = co.LocationDescription;

            }

            SequenceChildTags();
        }

        private void SequenceChildTags()
        {
            int pad = Globals.CurrentProjectData.LDARTagPaddedZeros;
            int startat;

            if (!int.TryParse(textBoxExtension.Text, out startat))
            {
                startat = Globals.CurrentProjectData.LDARTagStartChildrenNumber;
            }
            else
            {
                //TODO: Parsing is successful, so we need to start child from Parent extension + 1
                startat++;
            }
            
            for (int i = 0; i < listViewChildren.Items.Count; i++)
            {
                listViewChildren.Items[i].SubItems[1].Text = textBoxLDARTag.Text;
                listViewChildren.Items[i].SubItems[2].Text = Globals.getTagPoint((startat + i), pad);
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
                //MessageBox.Show("Select a child component to delete.");
                MessageBox.Show("选择一个子组件删除。Select a child component to delete.");
            }
        }

        private void textBoxLDARTag_TextChanged(object sender, EventArgs e)
        {
            currentFormChanged = true;
            SequenceChildTags();
        }

        private void buttonRefreshOldTag_Click(object sender, EventArgs e)
        {

            refreshTagFromExisting(textBoxPreviousTag.Text, textBoxOTExtension.Text, true);
            checkForRelatedComponents();

        }

        private void refreshTagFromExisting(string OldTag, string extension, bool useRefTag = false)
        {
            string tagID = string.Empty;
            string categoryCode = string.Empty;
            string reasonDesc = string.Empty;
            int? categoryId = 0;
            int? reasonId = 0;

            locationDescriptionUserControl1.suspendUpdates = true;

            List<LDARComponent> comps = new List<LDARComponent>();
            LDARProcessUnit unitFilter = Globals.CurrentProjectData.LDARData.ProcessUnits.Where(c => c.UnitDescription == comboBoxUnit.Text).FirstOrDefault();
            

            //if no extension is declared, assume that the first extension in the order is the parent
            if (string.IsNullOrEmpty(extension))
            {
                //try to locate by unit first, if that doesn't work try globally
                if (unitFilter != null)
                {
                    comps = Globals.CurrentProjectData.LDARData.ExistingComponents.Where(c => c.ComponentTag == OldTag && c.Location1 == unitFilter.UnitCode).OrderBy(c => c.TagExtension).ToList();
                }

                if (comps.Count() == 0)
                {
                    comps = Globals.CurrentProjectData.LDARData.ExistingComponents.Where(c => c.ComponentTag == OldTag).OrderBy(c => c.TagExtension).ToList();
                }
            }
            else
            {
                //try to locate by unit first, if that doesn't work try globally
                if (unitFilter != null)
                {
                    comps = Globals.CurrentProjectData.LDARData.ExistingComponents.Where(c => c.ComponentTag == OldTag && c.TagExtension == extension && c.Location1 == unitFilter.UnitCode).ToList();
                }

                if (comps.Count() == 0)
                {
                    comps = Globals.CurrentProjectData.LDARData.ExistingComponents.Where(c => c.ComponentTag == OldTag && c.TagExtension == extension).ToList();
                }
            }


            LDARComponent comp;

           //handle the case that multiple results were found.  If no extension was declared, take first in the list, otherwise prompt the user since there's a duplicate.

            if (comps.Count > 1 && !string.IsNullOrEmpty(extension))
            {
                FormDuplicateSelection ds = new FormDuplicateSelection();
                ds._dups = comps;

                ds.ShowDialog();

                if (ds.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    comp = ds._selectedTag;
                }
                else
                {
                    return;
                }
            }
            else
            {
                comp = comps.FirstOrDefault();
            }

            //if this didn't work abort!
            if (comp == null) return;

            if (comp.POS)
            {
                MessageBox.Show("The old/reference tag has been permanently removed from service and cannot be edited");
                return;
            }

            //update location description
            tagID = comp.ComponentTag.ToString();

            LDAREquipment ldEquip = Globals.CurrentProjectData.LDARData.Equipment.Where(c => c.EquipmentCode == comp.Location3).FirstOrDefault();
            if (ldEquip != null) this.locationDescriptionUserControl1.equipmentText = ldEquip.EquipmentDescription;

            this.locationDescriptionUserControl1.locationDescription = comp.LocationDescription.ToString();
            this.textBoxLocation.Text = comp.LocationDescription;

            textBoxExtension.Text = comp.TagExtension;
            textBoxOTExtension.Text = comp.TagExtension;
            textBoxRefExtension.Text = comp.TagExtension;


            if (useRefTag)
            {
                textBoxRouteNo.Text = comp.RouteSequence.ToString();
                LocalData.LastRouteNo = comp.RouteSequence;
            }



            if (comp.Drawing != null && textBoxDrawing.BackColor != Color.FromKnownColor(KnownColor.Control)) textBoxDrawing.Text = comp.Drawing;

            if (comp.Size != null) textBoxSize.Text = comp.Size;

            //update DTM/UTM
            LDARReason DTMReason = Globals.CurrentProjectData.LDARData.ComponentReasons.Where(c => c.ComponentReasonID == comp.ComponentReasonId && c.ComponentCategoryID == 2).FirstOrDefault();
            LDARReason UTMReason = Globals.CurrentProjectData.LDARData.ComponentReasons.Where(c => c.ComponentReasonID == comp.ComponentUTMReasonId && c.ComponentCategoryID == 3).FirstOrDefault();

            if (DTMReason != null) comboBoxDTM.Text = DTMReason.ReasonDescription;
            if (UTMReason != null) comboBoxUTM.Text = UTMReason.ReasonDescription;

            LDARProcessUnit ldUnit = Globals.CurrentProjectData.LDARData.ProcessUnits.Where(c => c.UnitCode == comp.Location1).FirstOrDefault();
            if (ldUnit != null) this.comboBoxUnit.Text = ldUnit.UnitDescription;

            //if (Globals.CurrentProjectData.ProjectType == LDARProjectType.EiMOC)
            //{
                LDARComponentStream ldStream = Globals.CurrentProjectData.LDARData.ComponentStreams.Where(c => c.ComponentStreamId == comp.ChemicalStreamId).FirstOrDefault();
                if (ldStream != null && comboBoxStream.BackColor != Color.FromKnownColor(KnownColor.Control)) this.comboBoxStream.Text = ldStream.StreamDescription;
            //}

            LDARChemicalState ldState = Globals.CurrentProjectData.LDARData.ChemicalStates.Where(c => c.ChemicalStateId == comp.ChemicalStateId).FirstOrDefault();
            if (ldState != null) this.comboBoxState.Text = ldState.ChemicalState;

            LDARArea ldArea = Globals.CurrentProjectData.LDARData.Areas.Where(c => c.AreaCode == comp.Location2).FirstOrDefault();
            if (ldArea != null) comboBoxArea.Text = ldArea.AreaDescription;

            //TODO: Disabled for Shell Norco project
            //LDARManufacturer ldManufacturer = Globals.CurrentProjectData.LDARData.Manufacturers.Where(c => c.ManufacturerId == comp.ManufacturerID).FirstOrDefault();
            //if (ldManufacturer != null) comboBoxManufacturer.Text = ldManufacturer.ManufacturerCode;

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
            locationDescriptionUserControl1.suspendUpdates = false;
        }

        private void checkForRelatedComponents()
        {
            List<LDARComponent> matchedComponents;

            if (textBoxPreviousTag.Text != "" && Globals.CurrentProjectData.LDARData.ExistingComponents.Count > 0)
            {
                try
                {
                    LDARProcessUnit unitMatch = Globals.CurrentProjectData.LDARData.ProcessUnits.Where(c => c.UnitDescription == comboBoxUnit.Text).FirstOrDefault();
                    if (unitMatch != null)
                    {
                        matchedComponents = Globals.CurrentProjectData.LDARData.ExistingComponents.Where(c => c.ComponentTag == textBoxPreviousTag.Text && !c.POS && c.Location1 == unitMatch.UnitCode).OrderBy(c => c.TagExtension).ToList<LDARComponent>();
                    }
                    else
                    {
                        matchedComponents = Globals.CurrentProjectData.LDARData.ExistingComponents.Where(c => c.ComponentTag == textBoxPreviousTag.Text && !c.POS).OrderBy(c => c.TagExtension).ToList<LDARComponent>();
                    }

                    if (matchedComponents != null)
                    {
                        if (matchedComponents.Count > 1)
                        {
                            labelRelatedTags.Text = matchedComponents.Count() - 1 + " 有关";
                        }
                        else
                        {
                            labelRelatedTags.Text = "";
                        }
                    }
                }
                catch { }
            }
        }

        private void textBoxPreviousTag_TextChanged(object sender, EventArgs e)
        {

            //textBoxLDARTag.Text = textBoxPreviousTag.Text;
            
            if (Globals.CurrentProjectData == null) return;
			labelRelatedTags.Text = "";
            
            string tagID = string.Empty;

            LDARComponent comp;
            if (string.IsNullOrEmpty(textBoxOTExtension.Text))
            {
                comp = Globals.CurrentProjectData.LDARData.ExistingComponents.Where(c => c.ComponentTag == this.textBoxPreviousTag.Text && !c.POS).FirstOrDefault();
            }
            else
            {
                comp = Globals.CurrentProjectData.LDARData.ExistingComponents.Where(c => c.ComponentTag == this.textBoxPreviousTag.Text && !c.POS && c.TagExtension == this.textBoxOTExtension.Text).FirstOrDefault();
            }

            if (comp != null)
            {
                tagID = comp.ComponentTag.ToString();
                 if (tagID == this.textBoxPreviousTag.Text)
                {
                    this.buttonRefreshOldTag.Enabled = true;
                    pictureBoxOldTagOk.Visible = true;
                }
                else
                {
                    this.buttonRefreshOldTag.Enabled = false;
                    pictureBoxOldTagOk.Visible = false;
                }
            }
            else
            {
                this.buttonRefreshOldTag.Enabled = false;
                pictureBoxOldTagOk.Visible = false;
            }

        }

        private void FormEditObject_Load(object sender, EventArgs e)
        {
            //TODO: Some picklists are disabled through code for Shell Norco project, they need to be re-enabled for generic builds.
            
            fillCategoryReasonList();
            fillUnitList();
            fillEquipmentList();
            fillUTMList();
            fillStateList();
            fillStreamList();
            fillTypeList();
            fillAreaList();
            setDirectionList();
            //fillManufacturerList();
            currentFormChanged = false;
        }

        private void setDirectionList()
        {
            Dictionary<string, string> direction = new Dictionary<string, string>();
            direction.Add("", "");
            direction.Add("N", "N (Běi)");
            direction.Add("S", "S (Nán)");
            direction.Add("E", "E (Dōngfāng)");
            direction.Add("W", "W (Xīfāng)");
            direction.Add("EN", "EN (Dōngběi)");
            direction.Add("WN", "WN (Xīběi)");
            direction.Add("ES", "ES (Dōngnán)");
            direction.Add("WS", "WS (Xīnán)");
            this.locationDescriptionUserControl1.setDirectionList(direction);
        }

        private void fillCategoryReasonList()
        {
            List<string> crList = new List<string>();
            string accessText = string.Empty;


            if (Globals.CurrentProjectData != null)
            {
                this.comboBoxDTM.Items.Clear();
                foreach (LDARReason ldr in Globals.CurrentProjectData.LDARData.ComponentReasons.Where(c => c.ComponentCategoryID == 2))
                {
                    if (ldr.showInTablet)
                    {
                        if (!crList.Contains(accessText)) crList.Add(ldr.ReasonDescription);
                    }
                }

                this.comboBoxDTM.Items.Add("");

                foreach (string str in crList)
                {
                    this.comboBoxDTM.Items.Add(str);
                }
            }
        }

        private void fillUTMList()
        {
            List<string> crList = new List<string>();
            string accessText = string.Empty;


            if (Globals.CurrentProjectData != null)
            {
                this.comboBoxUTM.Items.Clear();
                foreach (LDARReason ldr in Globals.CurrentProjectData.LDARData.ComponentReasons.Where(c => c.ComponentCategoryID == 3))
                {
                    if (ldr.showInTablet)
                    {
                        if (!crList.Contains(accessText)) crList.Add(ldr.ReasonDescription);
                    }
                }

                this.comboBoxUTM.Items.Add("");

                foreach (string str in crList)
                {
                    this.comboBoxUTM.Items.Add(str);
                }
            }
        }


        private void fillEquipmentList()
        {
            List<string> eqList = new List<string>();

            locationDescriptionUserControl1.suspendUpdates = true;
            
            if (Globals.CurrentProjectData != null)
            {
                this.comboBoxEquipment.Items.Clear();
                foreach (LDAREquipment lde in Globals.CurrentProjectData.LDARData.Equipment.Where(c => c.showInTablet))
                {
                    if (!eqList.Contains(lde.EquipmentDescription)) eqList.Add(lde.EquipmentDescription);
                }
                foreach (string str in eqList)
                {
                    this.comboBoxEquipment.Items.Add(str);
                }
                this.locationDescriptionUserControl1.setEquipmentList(eqList);

                locationDescriptionUserControl1.suspendUpdates = false;
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
                    if (ldt.ComponentClass != null && ldt.showInTablet ) typeList.Add(ldt.ComponentClass + " - " + ldt.ComponentType);
                }

                if (typeList.Count == 0)
                {
                    //we have to have parents, so if none are selected select all
                    foreach (LDARComponentClassType ldt in Globals.CurrentProjectData.LDARData.ComponentClassTypes)
                    {
                        if (ldt.ComponentClass != null && ldt.showInTablet) typeList.Add(ldt.ComponentClass + " - " + ldt.ComponentType);
                    }
                }

                typeList.Sort();
                foreach (string str in typeList)
                {
                    comboBoxType.Items.Add(str);
                }

                this.Text = "Component Edit (Guideware Integrated)";
            }
        }

        private void fillUnitList()
        {
            if (Globals.CurrentProjectData != null)
            {
                this.comboBoxUnit.Items.Clear();
                foreach (LDARProcessUnit ldu in Globals.CurrentProjectData.LDARData.ProcessUnits)
                {
                    if (ldu.UnitDescription != null && ldu.showInTablet ) this.comboBoxUnit.Items.Add(ldu.UnitDescription);
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
            if (Globals.CurrentProjectData != null)
            {
                this.comboBoxManufacturer.Items.Clear();
                foreach (LDARManufacturer ldm in Globals.CurrentProjectData.LDARData.Manufacturers)
                {
                    if (ldm.ManufacturerCode != null && ldm.showInTablet) comboBoxManufacturer.Items.Add(ldm.ManufacturerCode);
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
                //if (MessageBox.Show("Tag/Old Tag contains an 'X', continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                if (MessageBox.Show("标签/旧标记包含了一个“X” ，是否继续？Tag/Old Tag contains an 'X', continue?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
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
            //_currentTaggedComponent = new TaggedComponent();
            //_currentTaggedComponent.Id = Guid.NewGuid().ToString();
            //_currentTaggedComponent.Children = new List<ChildComponent>();
            locationDescriptionUserControl1.suspendUpdates = true;
            locationDescriptionUserControl1.locationDescription = string.Empty;
            this.textBoxLDARTag.Text = getXTag(Properties.Settings.Default.LastComponentLDARTag);
            this.textBoxPreviousTag.Text = getXTag(Properties.Settings.Default.LastOldTag);
            pictureBoxEditMode.Visible = false;
            buttonDeleteTag.Enabled = false;
            comboBoxInstalled.SelectedIndex = 0;
            checkBoxOOS.Checked = false;
            pictureBoxPhoto.Image = pictureBoxPhoto.InitialImage;
            labelInspection.Text = "无";
            listViewChildren.Items.Clear();
        }

        private void setFormValues(bool existingTag)
        {
            if (existingTag)
            {
                pictureBoxEditMode.Visible = true;
                buttonDeleteTag.Enabled = true;
            }
            else
            {
                pictureBoxEditMode.Visible = false;
                buttonDeleteTag.Enabled = false;
                pictureBoxPhoto.Image = pictureBoxPhoto.InitialImage;
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
                labelInspection.Text = _currentTaggedComponent.InspectionReading + " PPM 由 " + _currentTaggedComponent.InspectionInspector;
            }
            else
            {
                labelInspection.Text = "无";
            }


            textBoxMOCNumber.Text = _currentTaggedComponent.MOCNumber;
            comboBoxUnit.Text = _currentTaggedComponent.Unit;
            comboBoxManufacturer.Text = _currentTaggedComponent.Manufacturer;
            comboBoxState.Text = _currentTaggedComponent.ChemicalState;


            textBoxDrawing.Text = _currentTaggedComponent.Drawing;
            textBoxLDARTag.Text = _currentTaggedComponent.LDARTag;
            textBoxExtension.Text = _currentTaggedComponent.Extension;
            textBoxOTExtension.Text = _currentTaggedComponent.PreviousTagExtension;
            textBoxRefExtension.Text = _currentTaggedComponent.ReferenceTagExtension;
            textBoxPreviousTag.Text = _currentTaggedComponent.PreviousTag;
            //this.locationDescriptionUserControl1.locationDescription = textBoxLocation.Text = _currentTaggedComponent.Location;
            this.locationDescriptionUserControl1.equipmentText = _currentTaggedComponent.Equipment;
            this.locationDescriptionUserControl1.locationDescription = _currentTaggedComponent.Location;
            textBoxLocation.Text = _currentTaggedComponent.Location;
            textBoxSize.Text = _currentTaggedComponent.Size.ToString();
            comboBoxDTM.Text = _currentTaggedComponent.Access;
            comboBoxUTM.Text = _currentTaggedComponent.UTMReason;
            comboBoxStream.Text = _currentTaggedComponent.Stream;
            comboBoxType.Text = _currentTaggedComponent.ComponentType;
            comboBoxArea.Text = _currentTaggedComponent.Area;
            textBoxReferenceTag.Text = _currentTaggedComponent.ReferenceTag;
            textBoxRouteNo.Text = _currentTaggedComponent.RouteSequence.ToString();

            if (_currentTaggedComponent.InstalledResponse == null)
            {
                comboBoxInstalled.SelectedItem = 0;
            }
            else
            {
                comboBoxInstalled.Text = _currentTaggedComponent.InstalledResponse;
            }
            
            //checkBoxAttachDrawing.Checked = c.AttachDrawing;

            if (_currentTaggedComponent.EngineeringTag != null && _currentTaggedComponent.isDrawingTag)
            {
                labelCADID.Text = _currentTaggedComponent.EngineeringTag;
            }
            else
            {
                labelCADID.Text = "None";
            }

            checkBoxOOS.Checked = _currentTaggedComponent.TagOOS || _currentTaggedComponent.TagPOS;

            if (_currentTaggedComponent.Children != null)
            {
                foreach (ChildComponent cc in _currentTaggedComponent.Children)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = cc.ComponentType;
                    item.SubItems.Add(cc.LDARTag);
                    item.SubItems.Add(cc.Extension);
                    item.SubItems.Add(cc.PreviousTag);
                    item.SubItems.Add(cc.PreviousTagExtension);
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

            locationDescriptionUserControl1.suspendUpdates = false;
 
        }

        private void buttonSetBackground_Click(object sender, EventArgs e)
        {
            string bg = string.Empty;
            double bgTemp = 0;

            DialogResult dr = Globals.InputBox("背景 Background", "输入背景读数（PPM）Enter Background Reading (PPM)", ref bg);

            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    bgTemp = double.Parse(bg);
                    if (!(bgTemp >= 0 && bgTemp <= 1000000))
                    {
                        //MessageBox.Show("Background must be a number between 0 and 100000");
                        MessageBox.Show("背景必须是一个介于0和100000 Background must be a number between 0 and 100000");
                    }
                    else
                    {
                        _currentTaggedComponent.InspectionBackground = bgTemp;
                        MainForm._lastBackground = bgTemp;
                    }
                }
                catch
                {
                    //MessageBox.Show("Background must be a number between 0 and 100000");
                    MessageBox.Show("背景必须是一个介于0和100000 Background must be a number between 0 and 100000");
                }
            }
            
        }

         private void buttonInspect_Click(object sender, EventArgs e)
        {
            FormInspect ins = new FormInspect();
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

            ins.StartPosition = FormStartPosition.CenterScreen;

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
                    labelInspection.Text = "无";
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
                    labelInspection.Text = ins.Reading + " PPM 由 " + ins.Inspector;
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
            if ( _imageFileStream != null) _imageFileStream.Dispose();
            Hide();
            e.Cancel = true;
        }

        private bool validateFormData()
        {
            List<string> errorList = new List<string>();
            string errString = "";
            bool dataValid = true;

            if (textBoxLDARTag.Text == "") errorList.Add("标签不能为空 Tag cannot be empty");
            if (textBoxExtension.Text == "") errorList.Add("标签扩展不能为空 Tag Extension cannot be empty");
            if (textBoxLocation.Text == "") errorList.Add("位置不能为空 Location cannot be empty");
            if (textBoxSize.Text == "") errorList.Add("大小不能为空 Size cannot be empty");
            if (comboBoxState.Text == "") errorList.Add("国家不能为空 State cannot be empty");
            //if (comboBoxDTM.Text == "") errorList.Add("Access cannot be empty");
            if (comboBoxStream.Text == "") errorList.Add("流不能为空 Stream cannot be empty");
            if (comboBoxType.Text == "") errorList.Add("类型不能为空 Type cannot be empty");
            if (comboBoxUnit.Text == "") errorList.Add(labelUnit.Text.Replace(":", "") + " 不能为空 cannot be empty");
            if (this.locationDescriptionUserControl1.equipmentText == "") errorList.Add(labelEquipment.Text.Replace(":", "") + " 不能为空 cannot be empty");
            if (comboBoxArea.Text == "") errorList.Add(labelArea.Text.Replace(":", "") + " 不能为空 cannot be empty");
            if (LocalData.doesTagExist(textBoxLDARTag.Text, _currentTaggedComponent.Id)) errorList.Add("新的标签已被记录在案 The new tag has already been documented");
            if(!Properties.Settings.Default.AllowDuplicateLocation) if (LocalData.doesLocationDescriptionExist(textBoxLocation.Text, _currentTaggedComponent.Id)) errorList.Add("位置描述已被使用 The location description has already been used");
            //if (textBoxPreviousTag.Text != "")
            //{
            //    if (LocalData.doesPreviousTagExist(textBoxPreviousTag.Text, _currentTaggedComponent.Id)) errorList.Add("旧标签已经被记录在案 The old tag has already been documented");
            //}
            if (Globals.getSizeFromString(textBoxSize.Text) == -1) errorList.Add("大小无效 Invalid size");

            if (!string.IsNullOrEmpty(textBoxPreviousTag.Text))
            {
                if (!(textBoxLDARTag.Text == textBoxPreviousTag.Text && textBoxExtension.Text == textBoxOTExtension.Text))
                {
                    LDARComponent ldc = Globals.CurrentProjectData.LDARData.ExistingComponents.Where(c => c.ComponentTag == textBoxLDARTag.Text && c.TagExtension == textBoxExtension.Text).FirstOrDefault();

                    if (ldc != null)
                    {
                        //errorList.Add("该记录的标签已经存在的目标数据库中 The documented tag already exists in the target database");
                        if (ldc.POS)
                        {
                            errorList.Add("旧标签不能被编辑，因为它已被永久地从服务中删除 Old tag cannot be edited since it has been permanently removed from service");
                            return true;
                        }

                        DialogResult docResult = MessageBox.Show("该记录的标签已经存在的目标数据库中，利用现有的信息编辑现有的标记？ The documented tag already exists in the target database, edit existing tag using current information?", "编辑现有的标签？ Edit Existing Tag?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (docResult == System.Windows.Forms.DialogResult.Yes)
                        {
                            textBoxPreviousTag.Text = textBoxLDARTag.Text;
                            textBoxOTExtension.Text = textBoxExtension.Text;
                            checkForRelatedComponents();
                            if (labelRelatedTags.Text != "")
                            {
                                DialogResult relatedResult = MessageBox.Show("加载现有的子组件？ Load existing child components?", "负载儿童 Load Children", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (relatedResult == System.Windows.Forms.DialogResult.Yes) labelRelatedTags_Click(this, null);
                                return false;
                            }
                            return true;
                        }
                        else
                        {
                            errorList.Add("该记录的标签已经存在的目标数据库中 The documented tag already exists in the target database");
                        }
                    }
                }
            }

            List<string> locList = new List<string>();
            foreach (ListViewItem itm in listViewChildren.Items)
            {
                if (itm.SubItems[5].Text == textBoxLocation.Text)
                {
                    if (!Properties.Settings.Default.AllowDuplicateLocation) errorList.Add("重复定位的描述存在 Duplicate location descriptions exist");
                    break;
                }

                if (!locList.Contains(itm.SubItems[5].Text))
                    locList.Add(itm.SubItems[5].Text);
                else
                {
                    if (!Properties.Settings.Default.AllowDuplicateLocation) errorList.Add("重复定位的描述存在 Duplicate location descriptions exist");
                    break;
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

            return dataValid;

        }
        
        private void listViewChildren_DoubleClick(object sender, EventArgs e)
        {
            ChildComponent child = new ChildComponent();
            if (listViewChildren.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewChildren.SelectedItems[0];
                child.ComponentType = item.SubItems[0].Text;
                child.Inspected = bool.Parse(item.SubItems[6].Text);
                child.InspectionBackground = double.Parse(item.SubItems[10].Text);
                child.InspectionDate = DateTime.Parse(item.SubItems[8].Text);
                child.InspectionInspector = item.SubItems[7].Text;
                child.InspectionInstrument = item.SubItems[11].Text;
                child.InspectionReading = double.Parse(item.SubItems[9].Text);
                child.LDARTag = item.SubItems[1].Text;
                child.Extension = item.SubItems[2].Text;
                child.Location = item.SubItems[5].Text;
                child.PreviousTag = item.SubItems[3].Text;
                child.PreviousTagExtension = item.SubItems[4].Text;
                child.Size = Globals.getSizeFromString(item.SubItems[12].Text);

                FormChildObject_GW cc = new FormChildObject_GW(textBoxLocation.Text);
                cc.AllowInspections = true;
                cc.EditMode = true;
                cc.setComponent(child);
                if (cc.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    item.Text = cc.ComponentType;
                    item.SubItems[1].Text = textBoxLDARTag.Text;
                    item.SubItems[3].Text = cc.PreviousTag;
                    item.SubItems[4].Text = cc.PreviousTagExtension;
                    item.SubItems[5].Text = cc.LocationDescription;
                    item.SubItems[6].Text = cc.Inspected.ToString();
                    item.SubItems[7].Text = cc.InspectionInspector;
                    item.SubItems[8].Text = cc.InspectionDate.ToString();
                    item.SubItems[9].Text = cc.InspectionReading.ToString();
                    item.SubItems[10].Text = cc.InspectionBackground.ToString();
                    item.SubItems[11].Text = cc.InspectionInstrument;
                    item.SubItems[12].Text = cc.Size.ToString();
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
            refreshTagFromExisting(this.textBoxReferenceTag.Text, this.textBoxRefExtension.Text, false);
        }

        private void textBoxReferenceTag_TextChanged(object sender, EventArgs e)
        {
            if (Globals.CurrentProjectData == null) return;

            string tagID = string.Empty;

            LDARComponent comp;
            if (string.IsNullOrEmpty(textBoxRefExtension.Text))
            {
                comp = Globals.CurrentProjectData.LDARData.ExistingComponents.Where(c => c.ComponentTag == this.textBoxReferenceTag.Text && !c.POS).FirstOrDefault();
            }
            else
            {
                comp = Globals.CurrentProjectData.LDARData.ExistingComponents.Where(c => c.ComponentTag == this.textBoxReferenceTag.Text && !c.POS && c.TagExtension == this.textBoxRefExtension.Text).FirstOrDefault();
            }

            if (comp != null)
            {
                tagID = comp.ComponentTag.ToString();
                if (tagID == this.textBoxReferenceTag.Text)
                {
                    this.buttonRefreshReferenceTag.Enabled = true;
                    this.buttonSetRouteAfterRef.Enabled = true;

                }
                else
                {
                    this.buttonRefreshReferenceTag.Enabled = false;
                    this.buttonSetRouteAfterRef.Enabled = false;

                }
            }
            else
            {
                this.buttonRefreshReferenceTag.Enabled = false;
                this.buttonSetRouteAfterRef.Enabled = false;

            }
        }

        private void labelRelatedTags_Click(object sender, EventArgs e)
        {
            if (listViewChildren.Items.Count > 0)
            {
                //if (MessageBox.Show("Clear existing children and refresh from LDAR Data?", "Confirm", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                if (MessageBox.Show("清除现有的儿童和LDAR数据刷新？", "确认", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
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
                    LDARProcessUnit unitMatch = Globals.CurrentProjectData.LDARData.ProcessUnits.Where(c => c.UnitDescription == comboBoxUnit.Text).FirstOrDefault();
                    if (unitMatch != null)
                    {
                        matchedComponents = Globals.CurrentProjectData.LDARData.ExistingComponents.Where(c => c.ComponentTag == textBoxPreviousTag.Text && !c.POS && c.Location1 == unitMatch.UnitCode).OrderBy(c => c.TagExtension).ToList<LDARComponent>();
                    }
                    else
                    {
                        matchedComponents = Globals.CurrentProjectData.LDARData.ExistingComponents.Where(c => c.ComponentTag == textBoxPreviousTag.Text && !c.POS).OrderBy(c => c.TagExtension).ToList<LDARComponent>();
                    }
                    bool proceed = true;
                    if (matchedComponents.Count > 10)
                    {
                        //DialogResult dr = MessageBox.Show("More than 5 tags match your old tag criteria, continue?","Warning",MessageBoxButtons.YesNo);
                        DialogResult dr = MessageBox.Show("超过5个标签符合您的旧标签的标准，是否继续？ More than 10 tags match your old tag criteria, continue?", "警告", MessageBoxButtons.YesNo);
                        if (dr == System.Windows.Forms.DialogResult.No) proceed = false;
                    }
                    if (matchedComponents.Count > 0 && proceed)
                    {
                        bool isParent = true;
                        foreach (LDARComponent ldc in matchedComponents.OrderBy(c => c.TagExtension))
                        {
                            if (isParent)
                            {
                                //we're skipping the first component in the order, assuming it's the parent
                                isParent = false;
                            }
                            else
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
                                //if the lookup didn't work, don't leave it empty
                                if (string.IsNullOrEmpty(classTypeText))
                                    item.Text = "UNKNOWN";
                                else
                                    item.Text = classTypeText;
                                item.SubItems.Add(textBoxLDARTag.Text);
                                item.SubItems.Add(ldc.TagExtension);
                                item.SubItems.Add(ldc.ComponentTag);
                                item.SubItems.Add(ldc.TagExtension);
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
                        }
                        //SequenceChildTags();
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
                //if (MessageBox.Show("Delete tag " + textBoxLDARTag.Text + "?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                if (MessageBox.Show("删除标签 " + textBoxLDARTag.Text + "?", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    LocalData.RemoveComponent(_currentTaggedComponent);
                    Hide();
                }
            }

            try
            {
                TagSaved(this, EventArgs.Empty);
            }
            catch (Exception ex) { }
        }

        private void checkBoxOOS_Click(object sender, EventArgs e)
        {
            FormOOS oos = new FormOOS();

            oos.TagOOS = _currentTaggedComponent.TagOOS;
            oos.TagOOSReason = _currentTaggedComponent.TagOOSReason;
            oos.TagPOS = _currentTaggedComponent.TagPOS;
            oos.TagPOSReason = _currentTaggedComponent.TagPOSReason;

            oos.ShowDialog();

            if (oos.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                _currentTaggedComponent.TagOOS = oos.TagOOS;
                _currentTaggedComponent.TagOOSReason = oos.TagOOSReason;
                _currentTaggedComponent.TagPOS = oos.TagPOS;
                _currentTaggedComponent.TagPOSReason = oos.TagPOSReason;
            }

            if (_currentTaggedComponent.TagOOS || _currentTaggedComponent.TagPOS)
            {
                checkBoxOOS.Checked = true;
            }
            else
            {
                checkBoxOOS.Checked = false;
            }
        }

        private void buttonSetRouteAfter_Click(object sender, EventArgs e)
        {
            //this code has moved to Globals.getNextRouteNumber
            
            //List<LDARComponent> matchedComponents = new List<LDARComponent>();
            //matchedComponents = Globals.CurrentProjectData.LDARData.ExistingComponents.Where(c => c.ComponentTag == textBoxReferenceTag.Text).OrderBy(c => c.ComponentTag).ToList<LDARComponent>();
            //bool proceed = true;
            //if (matchedComponents.Count > 5)
            //{

            //    DialogResult dr = MessageBox.Show("More than 5 tags match your old tag criteria, continue?", "Warning", MessageBoxButtons.YesNo);
            //    if (dr == System.Windows.Forms.DialogResult.No) proceed = false;
            //}

            //if (proceed && matchedComponents.Count > 0)
            //{
            //    DialogResult = MessageBox.Show("Route current component after " + matchedComponents.Last().ComponentTag + "?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (DialogResult == System.Windows.Forms.DialogResult.Yes)
            //    {
            //        double nextNo = matchedComponents.Max(c => c.RouteSequence);
            //        _currentTaggedComponent.RouteSequence = nextNo + LocalData.RouteAddNo;
            //        LocalData.LastRouteNo = nextNo + LocalData.RouteAddNo;
            //        MessageBox.Show("Route sequence set.  All new tags will now be routed after this component");
            //    }
            //}

                double nextRoute = 0.0;
                nextRoute = LocalData.getNextRouteNumber(textBoxReferenceTag.Text, comboBoxUnit.Text);
                this.textBoxRouteNo.Text = nextRoute.ToString();
                LocalData.LastRouteNo = nextRoute;

        }

        private void comboBoxInstalled_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBoxInstalled.SelectedIndex == 0 || comboBoxInstalled.SelectedIndex == 1)
            {
                if (_currentTaggedComponent.InstalledDate != null || _currentTaggedComponent.HCServiceDate != null)
                {
                    //DialogResult dr = MessageBox.Show("Clear Installed/HC Service Dates?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    DialogResult dr = MessageBox.Show("清除安装/ HC服务日期？ Clear Installed/HC Service Dates?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        _currentTaggedComponent.InstalledDate = null;
                        _currentTaggedComponent.HCServiceDate = null;
                    }
                }
            }
            else
            {
                if (comboBoxInstalled.SelectedIndex == 3)
                {
                    _currentTaggedComponent.HCServiceDate = DateTime.Now;
                }
                _currentTaggedComponent.InstalledDate = DateTime.Now;
            }
        }

        private void buttonShowInDrawing_Click(object sender, EventArgs e)
        {
            //FindObjectEventArgs args = new FindObjectEventArgs();

            //args.ObjectID = _currentTaggedComponent.Id;

            //FindObject(this, args);
        }

        private void textBoxOTExtension_TextChanged(object sender, EventArgs e)
        {



            textBoxPreviousTag_TextChanged(this, null);            
        }

        private void textBoxRefExtension_TextChanged(object sender, EventArgs e)
        {
            textBoxReferenceTag_TextChanged(this, null);
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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void labelCurrentUser_Click(object sender, EventArgs e)
        {

        }

        private void listViewChildren_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox_DataChanged(object sender, EventArgs e)
        {
            currentFormChanged = true;
        }

        private void comboBoxType_DataChanged(object sender, EventArgs e)
        {
            currentFormChanged = true;
            //this.locationDescriptionUserControl1.setComponentType((sender as ComboBox).Text);
        }

    }

    //public class FindObjectEventArgs : EventArgs
    //{
    //    public string ObjectID { get; set; }
    //}
}