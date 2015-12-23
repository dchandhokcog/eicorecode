using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Data;

namespace EnvInt.Win32.FieldTech.Containers
{
    public class ProjectConfiguration
    {
        public string Name { get; set; }
        public string Notes { get; set; }
        public List<string> ExtraFields { get; set; }
        public List<FieldConfiguration> FieldConfigurations { get; set; }
        public bool AllowInspections { get; set; }
        public string MainIndexField { get; set; } //unique id in target database ComponentID, etc.
        public string DocumentingTable { get; set; } //table we're documenting from Components, etc.
        public Dictionary<string, string> StoredSessionValues { get; set; }  //allows us to save values from session to session
        public ConfigurationDatabaseTypes ConfigurationDatabaseType { get; set; }
        public bool Default { get; set; }
        
        public enum ConfigurationDatabaseTypes
        {
            TaggedComponent = 1,
            LeakDAS = 2,
            Guideware = 3,
            Generic = 4
        }

        public ProjectConfiguration()
        {
            StoredSessionValues = new Dictionary<string, string>();
            ExtraFields = new List<string>();
            FieldConfigurations = new List<FieldConfiguration>();
        }

        public void setDefaultFieldConfiguration(ConfigurationDatabaseTypes ConfigurationDatabaseType = ConfigurationDatabaseTypes.TaggedComponent)
        {
            //set some default values for known project field configurations
            //TODO: add more field configuration templates

            if (ConfigurationDatabaseType == ConfigurationDatabaseTypes.TaggedComponent)
            {
                TaggedComponent ldc = new TaggedComponent();
                List<PropertyInfo> props = ldc.GetType().GetProperties().ToList();
                List<FieldConfiguration> fcList = new List<FieldConfiguration>();
                int orderNo = 0;

                foreach (PropertyInfo prop in props)
                {
                    FieldConfiguration fc = new FieldConfiguration();
                    fc.FieldName = prop.Name;
                    fc.FieldLabel = prop.Name;
                    fc.Order = orderNo;
                    fc.UserEditable = true;
                    fc.CarryForward = true;
                    fc.FieldEnabled = true;
                    fc.LimitToList = false;
                    fc.ChildField = false;
                    fc.MinEntryAreaSize = 200;
                    fc.DataType = prop.PropertyType.ToString();
                    fc.ValidateRegEx = string.Empty;
                    fcList.Add(fc);
                    orderNo += 1;
                }
                this.FieldConfigurations = fcList;
            }
        }

    }

    public class FieldConfiguration
    {
        public string FieldName { get; set; } //field name to be collected in underlying object
        public string FieldLabel { get; set; }  //label to be displayed next to entry
        public string DefaultValue { get; set; }  //Default to apply to new records enter a text or numeric value or one of the markers from below
                                                  //[AutoNumber] to automatically number based on record count, 
                                                  //[LastX] to carry forward with X,
                                                  //[FieldName] for last used value from that field or any combination with + sign, 
                                                  //[FromDrawing:FieldName] to grab value from underlying drawing valid field names are ID, CAD_ID, Size, Stream, State
        public int Order { get; set; }  //order field is displayed on grid control
        public bool UserEditable { get; set; } //allow user to edit?
        public bool CarryForward { get; set; } //carry values forward to next record
        public bool LimitToList { get; set; }  //limit comboboxes to list
        public bool SaveSession { get; set; } //Save field for next session
        public bool ChildField { get; set; } //when true, field is unique in child
        public bool FieldEnabled { get; set; }  //Enabled = show in form, Disabled = do not show
        public bool InspectionField { get; set; } //This field is for inspections
        public string RefreshFromTable { get; set; } //When a refresh button is pressed retrieve value from this table
        public string RefreshFromKey { get; set; } //When a refresh button is pressed retrieve value with this key field
        public string RefreshFromValue { get; set; } //when a refresh button is pressed, retrieve this value from lookup
        public string FormXY { get; set; }  //this represents the grid control position on the form where 1:1 means grid 1, column 1.  2:1 means grid2, column 1, etc.
        public string PicklistTableName { get; set; } //table to get picklist values from
        public string PicklistTableKey { get; set; } //picklist table key field
        public string PicklistTableValue { get; set; } //picklist table display field
        public string DataType { get; set; } //underlying data type
        public string ValidateRegEx { get; set; } //regular expression to compare against for validation
        public string ChildRetrievalRegEx { get; set; } //regular expression to find child records in target database based on current column
        public string UpdateFromField { get; set; } //A field name in this column will assume the typed values from the parent column plus an optional appendage (decimal points, etc)
        public int MinEntryAreaSize { get; set; } //minimum size in pixels of data entry area
        public ControlTypes ControlType { get; set; } //custom control type, see enum below

        public enum ControlTypes
        {
            TextBox = 1,
            TextBoxWithRefresh = 2,
            ComboBox = 3,
            DataGrid = 4,
            SubForm = 5,
            ImageCapture = 6,
            Label = 7,
            CheckBox = 8
        }

    }


}
