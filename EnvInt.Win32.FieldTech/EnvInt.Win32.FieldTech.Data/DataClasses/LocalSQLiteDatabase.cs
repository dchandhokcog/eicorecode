using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data;

namespace EnvInt.Win32.FieldTech.Data
{
    public class LocalSQLiteDatabase
    {
        public string _SQLiteFileLocation { get; set; }
        public DatabaseRecipe _dbRecipe { get; set; }
        public DBConnection _dbIncomingConn { get; set; }
        public DBConnection _dbOutgoingConn { get; set; }
        public BackgroundWorker _importWorker = new BackgroundWorker();
        public DataSetSQLExport _SQLExporter = new DataSetSQLExport();

        public LocalSQLiteDatabase()
        {            _importWorker.DoWork += new DoWorkEventHandler(_importWorker_DoWork);
            _importWorker.WorkerReportsProgress = true;
            _importWorker.WorkerSupportsCancellation = true;
        }

        public void CreateLocalDatabase()
        {
            if (_dbIncomingConn == null) return;
            if (string.IsNullOrEmpty(_SQLiteFileLocation)) return;
            _importWorker.RunWorkerAsync();
        }

        public void CreateLocalDatabase(string EIDFileLocation)
        {

            _dbIncomingConn = new DBConnection();
            _dbIncomingConn.ConnectionName = "EID Import";
            _dbIncomingConn.ConnectionType = DBConnectionType.Legacy;
            _dbIncomingConn.FileLocation = EIDFileLocation;
            
            if (_dbIncomingConn == null) return;
            _importWorker.RunWorkerAsync();
        }

        private void CreateLocalWithRecipe()
        {
            DBConnectionManager connMan = new DBConnectionManager();

            DatabaseRecipe importRecipe = new DatabaseRecipe();

            _SQLExporter.ConnType = DBConnectionType.SQLite;
            _SQLExporter.FileName = _SQLiteFileLocation;

            int rowCtr = 0;

            //import tables
            foreach (DataRow tableRow in _dbRecipe.TableList.Rows)
            {
                rowCtr++;
                _SQLExporter.xferTableName = tableRow["TableName"].ToString();
                double rc = rowCtr;
                double rt = importRecipe.TableList.Rows.Count;
                double pDone = (rc / rt) * 100;
                int percentDone = Convert.ToInt32(pDone);
                _importWorker.ReportProgress(percentDone, "Saving Table: " + _SQLExporter.xferTableName);
                DataSet newDataSet = new DataSet();
                IDataAdapter newDA = _dbIncomingConn.getDBAdapter(tableRow["SQL"].ToString());
                newDA.Fill(newDataSet);
                if (newDataSet.Tables.Count > 0) _SQLExporter.SendToConnection(newDataSet.Tables[0]);
            }

            //import views
            foreach (DataRow tableRow in importRecipe.SQLList.Rows)
            {
                rowCtr++;
                _SQLExporter.xferTableName = tableRow["ViewName"].ToString();
                _importWorker.ReportProgress(99, "Creating View: " + _SQLExporter.xferTableName);
                _SQLExporter.createSQLiteView(tableRow["SQL"].ToString());
            }

            WriteRecipe();

            //dt.ConnType = DataTransfer.ConnectionTypes.Firebird;
            //dt.DatabaseName = "c:\\users\\devin\\desktop\\testdb.fdb";
        }

        private void CreateLocalWithLegacyImport()
        {
            DBConnectionManager connMan = new DBConnectionManager();
            LegacyDataConvert oldDataConvert = new LegacyDataConvert();

            _SQLExporter.ConnType = DBConnectionType.SQLite;
            _SQLExporter.FileName = _SQLiteFileLocation;

            DataSet importedData = oldDataConvert.getDataSetFromEIP(_dbIncomingConn.FileLocation);

            int rowCtr = 0;

            //import tables
            foreach (DataTable tbl in importedData.Tables)
            {
                rowCtr++;
                _SQLExporter.xferTableName = tbl.TableName;
                double rc = rowCtr;
                double rt = importedData.Tables.Count;
                double pDone = (rc / rt) * 100;
                int percentDone = Convert.ToInt32(pDone);
                _importWorker.ReportProgress(percentDone, "Saving Table: " + _SQLExporter.xferTableName);
                _SQLExporter.SendToConnection(tbl);
            }
            _dbRecipe = oldDataConvert._localRecipe;
            WriteRecipe();
        }

        void _importWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            switch (_dbIncomingConn.ConnectionType)
            {
                case DBConnectionType.Legacy:
                    CreateLocalWithLegacyImport();
                    break;
                default:
                    CreateLocalWithRecipe();
                    break;
            }

        }

        public void WriteRecipe()
        {
            _SQLExporter.ConnType = DBConnectionType.SQLite;
            _SQLExporter.FileName = _SQLiteFileLocation;

            _SQLExporter.xferTableName = "RECIPE_Definition";
            _SQLExporter.SendToConnection(DataSetSQLExport.GetObjectAsPropertyTable<DatabaseRecipe>(_dbRecipe, "RECIPE_Definition"));
            _SQLExporter.xferTableName = "RECIPE_TableList";
            _SQLExporter.SendToConnection(_dbRecipe.TableList);

            _SQLExporter.xferTableName = "RECIPE_SQLList";
            _SQLExporter.SendToConnection(_dbRecipe.SQLList);

            _SQLExporter.xferTableName = "RECIPE_InputFieldDefinitions";
            _SQLExporter.SendToConnection(_dbRecipe.InputFieldDefinitions);

            _SQLExporter.xferTableName = "RECIPE_ExtraFieldsList";
            _SQLExporter.SendToConnection(_dbRecipe.ExtraFieldsList);

            _SQLExporter.xferTableName = "RECIPE_DestinationTableList";
            _SQLExporter.SendToConnection(_dbRecipe.ExtraFieldsList);

        }
    }
}
