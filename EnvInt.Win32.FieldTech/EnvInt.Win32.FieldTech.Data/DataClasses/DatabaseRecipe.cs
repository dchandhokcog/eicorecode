// Environmental Intellect, LLC Copyright 2015
// Design by: Devin Williams

using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Xml;
using System.IO;

namespace EnvInt.Win32.FieldTech.Data
{

    // A "Database Recipe" represents the collected knowledge about a target database system that we need to perform edits
    // This class also includes methods for saving and retrieving and import/export of recipe definitions
    
    public class DatabaseRecipe
    {
        public string RecipeName { get; set; }
        public string RecipeVersion { get; set; }
        public string RecipeDescription { get; set; }
        public string InputTableName { get; set; }
        public DBConnectionType TargetDatabaseType { get; set; }
        public DBConnection TargetDatabase { get; set; }
        public DateTime LastRefresh { get; set; }
        public DataTable TableList;  //list of tables to create in "local database" and optionally import from target database
        public DataTable DestinationTableList; //list of tables to return data to destination and input tables from local database
        public DataTable SQLList; //list of queries to represent reports and QC checks
        public DataTable ExtraFieldsList; //list of fields to add to local database for tracking various things
        public DataTable InputFieldDefinitions; //definitions of how to input data during offline work

        public DatabaseRecipe()
        {
            TableList = new DataTable();
            TableList.TableName = "Tables";
            TableList.Columns.Add("TableName", typeof(string));
            TableList.Columns.Add("ImportFromTarget", typeof(bool));
            TableList.Columns.Add("SourceConnectionString", typeof(string));
            TableList.Columns.Add("SourceDatabaseType", typeof(string));
            TableList.Columns.Add("LastRefresh", typeof(DateTime));
            TableList.Columns.Add("SQL", typeof(string));

            SQLList = new DataTable();
            SQLList.TableName = "Views";
            SQLList.Columns.Add("ViewName", typeof(string));
            SQLList.Columns.Add("SQL", typeof(string));
            SQLList.Columns.Add("IsQCQuery", typeof(bool));

            ExtraFieldsList = new DataTable();
            ExtraFieldsList.TableName = "ExtraFields";
            ExtraFieldsList.Columns.Add("TableName", typeof(string));
            ExtraFieldsList.Columns.Add("FieldName", typeof(string));
            ExtraFieldsList.Columns.Add("FieldType", typeof(string));

            DestinationTableList = new DataTable();
            DestinationTableList.TableName = "DestinationTables";
            DestinationTableList.Columns.Add("SourceTableName", typeof(string));
            DestinationTableList.Columns.Add("DestinationTableName", typeof(string));

            InputFieldDefinitions = new DataTable();
            InputFieldDefinitions.TableName = "InputFieldDefinitions";
            InputFieldDefinitions.Columns.Add("FieldName", typeof(string)); //field name to be collected in underlying object
            InputFieldDefinitions.Columns.Add("FieldLabel", typeof(string));  //label to be displayed next to entry
            InputFieldDefinitions.Columns.Add("DefaultValue", typeof(string));  //Default to apply to new records enter a text or numeric value or one of the markers from below
                                                      //[AutoNumber] to automatically number based on record count, 
                                                      //[LastX] to carry forward with X,
                                                      //[FieldName] for last used value from that field or any combination with + sign, 
                                                      //[FromDrawing:FieldName] to grab value from underlying drawing valid field names are ID, CAD_ID, Size, Stream, State
            InputFieldDefinitions.Columns.Add("Order", typeof(int));  //order field is displayed on grid control
            InputFieldDefinitions.Columns.Add("UserEditable", typeof(bool)); //allow user to edit?
            InputFieldDefinitions.Columns.Add("CarryForward", typeof(bool)); //carry values forward to next record
            InputFieldDefinitions.Columns.Add("LimitToList", typeof(bool));  //limit comboboxes to list
            InputFieldDefinitions.Columns.Add("SaveSession", typeof(bool)); //Save field for next session
            InputFieldDefinitions.Columns.Add("ChildField", typeof(bool)); //when true, field is unique in child
            InputFieldDefinitions.Columns.Add("FieldEnabled", typeof(bool));  //Enabled = show in form, Disabled = do not show
            InputFieldDefinitions.Columns.Add("InspectionField", typeof(bool)); //This field is for inspections
            InputFieldDefinitions.Columns.Add("RefreshFromTable", typeof(string)); //When a refresh button is pressed retrieve value from this table
            InputFieldDefinitions.Columns.Add("RefreshFromKey", typeof(string)); //When a refresh button is pressed retrieve value with this key field
            InputFieldDefinitions.Columns.Add("RefreshFromValue", typeof(string)); //when a refresh button is pressed, retrieve this value from lookup
            InputFieldDefinitions.Columns.Add("FormXY", typeof(string));  //this represents the grid control position on the form where 1:1 means grid 1, column 1.Columns.Add(" 2:1 means grid2, column 1, etc.Columns.Add("
            InputFieldDefinitions.Columns.Add("PicklistTableName", typeof(string)); //table to get picklist values from
            InputFieldDefinitions.Columns.Add("PicklistTableKey", typeof(string)); //picklist table key field
            InputFieldDefinitions.Columns.Add("PicklistTableValue", typeof(string)); //picklist table display field
            InputFieldDefinitions.Columns.Add("DataType", typeof(string)); //underlying data type
            InputFieldDefinitions.Columns.Add("ValidateRegEx", typeof(string)); //regular expression to compare against for validation
            InputFieldDefinitions.Columns.Add("ChildRetrievalRegEx", typeof(string)); //regular expression to find child records in target database based on current column
            InputFieldDefinitions.Columns.Add("UpdateFromField", typeof(string)); //A field name in this column will assume the typed values from the parent column plus an optional appendage (decimal points, etc)
            InputFieldDefinitions.Columns.Add("MinEntryAreaSize", typeof(int)); //minimum size in pixels of data entry area
            InputFieldDefinitions.Columns.Add("ControlType", typeof(string)); //custom control type
        }
        
        public void getTablesFromCSV(string FileNameTables)
        {
            TableList.Clear();
            
            string recipeCsv = File.ReadAllText(FileNameTables);

            List<string> tableFileLines = recipeCsv.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

            bool headerRow = true;

            foreach (string tableDef in tableFileLines)
            {
                if (!headerRow)
                {
                    if (!string.IsNullOrEmpty(tableDef))
                    {
                        string tblName = tableDef.Split('\t')[0];
                        string sql = tableDef.Split('\t')[5];
                        string targetImport = tableDef.Split('\t')[1];
                        bool isTargetImport = true;
                        bool.TryParse(targetImport, out isTargetImport);
                        DataRow newRow = TableList.NewRow();
                        newRow["TableName"] = tblName;
                        newRow["ImportFromTarget"] = isTargetImport;
                        newRow["SourceConnectionString"] = "";
                        newRow["SourceDatabaseType"] = "";
                        newRow["LastRefresh"] = DateTime.MinValue;
                        newRow["SQL"] = sql;
                        TableList.Rows.Add(newRow);
                    }
                }
                else
                {
                    headerRow = false;
                }
            }
        }

        public void getSQLFromCSV(string FileNameViews)
        {
            SQLList.Clear();

            string recipeCsv = File.ReadAllText(FileNameViews);

            List<string> tableFileLines = recipeCsv.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

            bool headerRow = true;

            foreach (string tableDef in tableFileLines)
            {
                if (!headerRow)
                {
                    if (!string.IsNullOrEmpty(tableDef))
                    {
                        string tblName = tableDef.Split('\t')[0];
                        string sql = tableDef.Split('\t')[1];
                        string isQC = tableDef.Split('\t')[2];
                        DataRow newRow = SQLList.NewRow();
                        newRow["ViewName"] = tblName;
                        newRow["SQL"] = sql;
                        newRow["IsQCQuery"] = bool.Parse(isQC);
                        SQLList.Rows.Add(newRow);
                    }
                }
                else
                {
                    headerRow = false;
                }
            }
        }

        public void getFieldDefinitionsFromCSV(string FileNameRelations)
        {
            InputFieldDefinitions.Clear();

            string recipeCsv = File.ReadAllText(FileNameRelations);

            List<string> tableFileLines = recipeCsv.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

            bool headerRow = true;

            foreach (string tableDef in tableFileLines)
            {
                if (!headerRow)
                {
                    if (!string.IsNullOrEmpty(tableDef))
                    {
                        string FieldName = tableDef.Split('\t')[0];
                        string FieldLabel = tableDef.Split('\t')[1];
                        string DefaultValue = tableDef.Split('\t')[2];
                        int Order = int.Parse(tableDef.Split('\t')[3]);
                        bool UserEditable = bool.Parse(tableDef.Split('\t')[4]);
                        bool CarryForward = bool.Parse(tableDef.Split('\t')[5]);
                        bool LimitToList = bool.Parse(tableDef.Split('\t')[6]);
                        bool SaveSession = bool.Parse(tableDef.Split('\t')[7]);
                        bool ChildField = bool.Parse(tableDef.Split('\t')[8]);
                        bool FieldEnabled = bool.Parse(tableDef.Split('\t')[9]);
                        bool InspectionField = bool.Parse(tableDef.Split('\t')[10]);
                        string RefreshFromTable = tableDef.Split('\t')[11];
                        string RefreshFromKey = tableDef.Split('\t')[12];
                        string RefreshFromValue = tableDef.Split('\t')[13];
                        string FormXY = tableDef.Split('\t')[14];
                        string PicklistTableName = tableDef.Split('\t')[15];
                        string PicklistTableKey = tableDef.Split('\t')[16];
                        string PicklistTableValue = tableDef.Split('\t')[17];
                        string DataType = tableDef.Split('\t')[18];
                        string ValidateRegEx = tableDef.Split('\t')[19];
                        string ChildRetrievalRegEx = tableDef.Split('\t')[20];
                        string UpdateFromField = tableDef.Split('\t')[21];
                        int MinEntryAreaSize = int.Parse(tableDef.Split('\t')[22]);
                        string ControlType = tableDef.Split('\t')[23];

                        DataRow newRow = InputFieldDefinitions.NewRow();
                        newRow["FieldName"] = FieldName;
                        newRow["FieldLabel"] = FieldLabel;
                        newRow["DefaultValue"] = DefaultValue;
                        newRow["Order"] = Order;
                        newRow["UserEditable"] = UserEditable;
                        newRow["CarryForward"] = CarryForward;
                        newRow["LimitToList"] = LimitToList;
                        newRow["SaveSession"] = SaveSession;
                        newRow["ChildField"] = ChildField;
                        newRow["FieldEnabled"] = FieldEnabled;
                        newRow["InspectionField"] = InspectionField;
                        newRow["RefreshFromTable"] = RefreshFromTable;
                        newRow["RefreshFromKey"] = RefreshFromKey;
                        newRow["RefreshFromValue"] = RefreshFromValue;
                        newRow["FormXY"] = FormXY;
                        newRow["PicklistTableName"] = PicklistTableName;
                        newRow["PicklistTableKey"] = PicklistTableKey;
                        newRow["PicklistTableValue"] = PicklistTableValue;
                        newRow["DataType"] = DataType;
                        newRow["ValidateRegEx"] = ValidateRegEx;
                        newRow["ChildRetrievalRegEx"] = ChildRetrievalRegEx;
                        newRow["UpdateFromField"] = UpdateFromField;
                        newRow["MinEntryAreaSize"] = MinEntryAreaSize;
                        newRow["ControlType"] = ControlType;
                        InputFieldDefinitions.Rows.Add(newRow);
                    }
                }
                else
                {
                    headerRow = false;
                }
            }
        }


        public void SaveRecipeToXML(string FileName)
        {
            DataSetSQLExport.SaveObjectAsXMLFile<DatabaseRecipe>(this, FileName);
        }

        public void LoadRecipeFromXML(string FileName)
        {
            DatabaseRecipe loadedRecipe;

            loadedRecipe = (DatabaseRecipe)DataSetSQLExport.LoadObjectFromXMLFile<DatabaseRecipe>(FileName);

            this.TableList = loadedRecipe.TableList;
            this.SQLList = loadedRecipe.SQLList;
            this.RecipeName = loadedRecipe.RecipeName;
            this.InputFieldDefinitions = loadedRecipe.InputFieldDefinitions;

            this.RecipeName = loadedRecipe.RecipeName;
            this.RecipeVersion = loadedRecipe.RecipeVersion;
            this.RecipeDescription = loadedRecipe.RecipeDescription;
            this.InputTableName = loadedRecipe.InputTableName;
            this.TargetDatabaseType = loadedRecipe.TargetDatabaseType;
            this.TargetDatabase = loadedRecipe.TargetDatabase;
            this.LastRefresh = loadedRecipe.LastRefresh;

        }

        public void loadDataSetFromRecipe(ref DataSet ds, SqlConnection loadFromSQLConnection)
        {
            string errMsg = string.Empty;

            ds.Tables.Clear();

            foreach (DataRow tbl in TableList.Rows)
            {
                SqlDataAdapter da = new SqlDataAdapter(tbl["SQL"].ToString(), loadFromSQLConnection);
                DataTable newDt = new DataTable();
                newDt.TableName = tbl["TableName"].ToString();
                da.Fill(newDt);
                ds.Tables.Add(newDt);
            }
        }
    }
}
