using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

using EnvInt.Win32.FieldTech.Common;
using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Data
{
    class LegacyDataConvert
    {
        private DataSet _localDataSet = new DataSet();
        private ProjectData _localProjectData = new ProjectData();
        private List<ProjectTags> _localProjectTags = new List<ProjectTags>();
        public DatabaseRecipe _localRecipe = new DatabaseRecipe();

        public DataSet getDataSetFromEIP(string fileLocation)
        {
            _localDataSet.Clear();
            LoadDataFromEIP(fileLocation);
            importProjectData();
            importTags();
            CreateDatabaseRecipe();

            return _localDataSet;
        }

        public void importProjectData()
        {
            _localDataSet.Tables.Add(DataSetSQLExport.GetObjectAsPropertyTable<ProjectData>(_localProjectData, "ProjectData"));
            _localDataSet.Tables.Add(DataSetSQLExport.GetObjectListAsTable<CADPackage>(_localProjectData.CADPackages, "CADPackages"));
            _localDataSet.Tables.Add(DataSetSQLExport.GetObjectListAsTable<CADPackage>(_localProjectData.MarkedDrawings, "MarkedDrawings"));

            _localDataSet.Tables.Add(DataSetSQLExport.GetObjectListAsTable<LDARArea>(_localProjectData.LDARData.Areas, "Areas"));
            _localDataSet.Tables.Add(DataSetSQLExport.GetObjectListAsTable<LDARChemicalState>(_localProjectData.LDARData.ChemicalStates, "ChemicalStates"));
            _localDataSet.Tables.Add(DataSetSQLExport.GetObjectListAsTable<LDARComplianceGroup>(_localProjectData.LDARData.ComplianceGroups, "ComplianceGroups"));
            _localDataSet.Tables.Add(DataSetSQLExport.GetObjectListAsTable<LDARComponent>(_localProjectData.LDARData.ExistingComponents, "ExistingComponents"));
            _localDataSet.Tables.Add(DataSetSQLExport.GetObjectListAsTable<LDARCategory>(_localProjectData.LDARData.ComponentCategories, "ComponentCategories"));
            _localDataSet.Tables.Add(DataSetSQLExport.GetObjectListAsTable<LDARComponentClassType>(_localProjectData.LDARData.ComponentClassTypes, "ComponentClassTypes"));
            _localDataSet.Tables.Add(DataSetSQLExport.GetObjectListAsTable<LDARComponentStream>(_localProjectData.LDARData.ComponentStreams, "ComponentStreams"));
            _localDataSet.Tables.Add(DataSetSQLExport.GetObjectListAsTable<LDAREquipment>(_localProjectData.LDARData.Equipment, "Equipment"));
            _localDataSet.Tables.Add(DataSetSQLExport.GetObjectListAsTable<LDARLocationPlant>(_localProjectData.LDARData.LocationPlants, "LocationPlants"));
            _localDataSet.Tables.Add(DataSetSQLExport.GetObjectListAsTable<LDARManufacturer>(_localProjectData.LDARData.Manufacturers, "Manufacturers"));
            _localDataSet.Tables.Add(DataSetSQLExport.GetObjectListAsTable<LDAROOSDescription>(_localProjectData.LDARData.OOSDescriptions, "OOSDescriptions"));
            _localDataSet.Tables.Add(DataSetSQLExport.GetObjectListAsTable<LDAROption>(_localProjectData.LDARData.LDAROptions, "LDAROptions"));
            _localDataSet.Tables.Add(DataSetSQLExport.GetObjectListAsTable<LDARPressureService>(_localProjectData.LDARData.PressureServices, "PressureServices"));
            _localDataSet.Tables.Add(DataSetSQLExport.GetObjectListAsTable<LDARProcessUnit>(_localProjectData.LDARData.ProcessUnits, "ProcessUnits"));
            _localDataSet.Tables.Add(DataSetSQLExport.GetObjectListAsTable<LDARReason>(_localProjectData.LDARData.ComponentReasons, "ComponentReasons"));
            _localDataSet.Tables.Add(DataSetSQLExport.GetObjectListAsTable<LDARRegulation>(_localProjectData.LDARData.Regulations, "Regulations"));
            _localDataSet.Tables.Add(DataSetSQLExport.GetObjectListAsTable<LDARTechnician>(_localProjectData.LDARData.Technicians, "Technicians"));
        }

        public void importTags()
        {
            List<TaggedComponent> allTagsList = new List<TaggedComponent>();
            foreach (ProjectTags pt in _localProjectTags)
            {
                allTagsList.AddRange(pt.getAllAsTaggedComponent());
            }

            _localDataSet.Tables.Add(DataSetSQLExport.GetObjectListAsTable<TaggedComponent>(allTagsList, "ProjectTags"));
        }

        public void LoadDataFromEIP(string fileLocation)
        {

            try
            {
                Ionic.Zip.ZipFile EIP = new Ionic.Zip.ZipFile(fileLocation);
                string projectTxt = FileUtilities.GetZipEntryAsText(EIP, "project.json");
                string tagsTxt = FileUtilities.GetZipEntryAsText(EIP, "tags.json");
                _localProjectData = FileUtilities.DeserializeObject<ProjectData>(projectTxt);
                _localProjectTags = FileUtilities.DeserializeObject<List<ProjectTags>>(tagsTxt);
            }
            catch { }
        }

        public void CreateDatabaseRecipe()
        {
            string ExecutableFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).ToString().Replace("file:\\", "");
            string RecipeFile = ExecutableFolder + "\\Recipes\\TaggedComponent LeakDAS.xml";

            _localRecipe.LoadRecipeFromXML(RecipeFile);
            
            _localRecipe.TargetDatabaseType = DBConnectionType.SQLServer;
            _localRecipe.InputTableName = "ProjectTags";
            DBConnection dbc = new DBConnection();
            dbc.ConnectionName = _localProjectData.ProjectName;
            dbc.ConnectionType = DBConnectionType.SQLServer;
            dbc.Database = _localProjectData.LDARDatabaseName;
            dbc.Server = _localProjectData.LDARDatabaseServer;
            dbc.Password = _localProjectData.LDARDatabasePassword;
            dbc.UserName = _localProjectData.LDARDatabaseUsername;
            dbc.UseTrustedConnection = _localProjectData.LDARDatabaseAuthentication == "Windows Authentication";

            foreach (DataRow dr in _localRecipe.TableList.Rows)
            {
                dr["SourceConnectionString"] = dbc.getConnectionString();
            }

            int cNo = 0;
            foreach (DataColumn dc in _localDataSet.Tables[_localRecipe.InputTableName].Columns)
            {
                cNo++;
                DataRow newFDRow = _localRecipe.InputFieldDefinitions.NewRow();
                newFDRow["FieldName"] = dc.ColumnName;
                newFDRow["FieldLabel"] = dc.ColumnName;
                newFDRow["Order"] = cNo;
                newFDRow["CarryForward"] = true;
                newFDRow["LimitToList"] = false;
                newFDRow["ChildField"] = false;
                newFDRow["FieldEnabled"] = true;
                newFDRow["InspectionField"] = false;
                newFDRow["DataType"] = dc.DataType.ToString();
                _localRecipe.InputFieldDefinitions.Rows.Add(newFDRow);
            }
        }
    }
}
