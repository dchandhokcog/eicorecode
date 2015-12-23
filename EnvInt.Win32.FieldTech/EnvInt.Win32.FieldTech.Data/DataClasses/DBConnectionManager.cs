using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Data.SqlClient;
using System.Data.SQLite;
using FirebirdSql.Data.FirebirdClient;

namespace EnvInt.Win32.FieldTech.Data
{
    public class DBConnectionManager
    {
        public List<DBConnection> ConnectionList { get; set; }
        public string SavedConnectionsFileLocation { get; set; }
        public DatabaseRecipe ImportRecipe { get; set; }

        public DBConnectionManager()
        {

            string AppDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\FieldTechMigrate";
            
            if (!Directory.Exists(AppDataDir))
            {
                try
                {
                    Directory.CreateDirectory(AppDataDir);
                }
                catch { }
            }

            SavedConnectionsFileLocation = AppDataDir + "\\savedconnections.xml";

            ImportRecipe = new DatabaseRecipe();

            ConnectionList = new List<DBConnection>();
        }
        
        public DBConnection getConnectionByName(string connectionName)
        {
            return ConnectionList.Where(c => c.ConnectionName == connectionName).FirstOrDefault();
        }

        public DBConnection getCurrentConnection()
        {
            return ConnectionList.Where(c => c.InUse == true).FirstOrDefault();
        }

        public string getConnectionStringByName(string connectionName)
        {
            DBConnection currentConnection = ConnectionList.Where(c => c.ConnectionName == connectionName).FirstOrDefault();

            if (currentConnection == null) return string.Empty;

            return currentConnection.getConnectionString();
        }

        public void setCurrentConnectionByName(string connectionName)
        {
            foreach (DBConnection dbc in ConnectionList)
            {
                if (dbc.ConnectionName == connectionName)
                {
                    dbc.InUse = true;
                }
                else
                {
                    dbc.InUse = false;
                }
            }
        }

        public string saveConnections()
        {
            return DataSetSQLExport.SaveObjectAsXMLFile<List<DBConnection>>(ConnectionList, SavedConnectionsFileLocation);
        }

        public bool loadConnections()
        {

            if (!File.Exists(SavedConnectionsFileLocation)) return false;
            
            try
            {
                object import = DataSetSQLExport.LoadObjectFromXMLFile<List<DBConnection>>(SavedConnectionsFileLocation);
                if (import != null)
                {
                    ConnectionList = (List<DBConnection>)import;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }

    public class DBConnection
    {
        public string ConnectionName { get; set; }
        public DBConnectionType ConnectionType { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }
        public string Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FileLocation { get; set; }
        public bool UseTrustedConnection { get; set; }
        public bool InUse { get; set; }
        public DateTime LastConnection { get; set; }
        public string LastUser { get; set; }

        public IDbConnection getDBConnection()
        {
            IDbConnection dbConn;
            
            switch (ConnectionType)
            {
               
                case DBConnectionType.SQLServer:
                    dbConn = new SqlConnection(getConnectionString());
                    break;
                case DBConnectionType.SQLite:
                    dbConn = new SQLiteConnection(getConnectionString());
                    break;
                case DBConnectionType.Firebird:
                    dbConn = new FbConnection(getConnectionString());
                    break;
                default:
                    dbConn = new SqlConnection(getConnectionString());
                    break;
            }

            return dbConn;
        }

        public IDataAdapter getDBAdapter(string SQL)
        {
            IDataAdapter dbAdapt;
            
            switch (ConnectionType)
            {
                case DBConnectionType.SQLServer:
                    dbAdapt = new SqlDataAdapter(SQL, getConnectionString());
                    break;
                case DBConnectionType.SQLite:
                    dbAdapt = new SQLiteDataAdapter(SQL, getConnectionString());
                    break;
                case DBConnectionType.Firebird:
                    dbAdapt = new FbDataAdapter(SQL, getConnectionString());
                    break;
                default:
                    dbAdapt = new SqlDataAdapter(SQL, getConnectionString());
                    break;
            }

            return dbAdapt;
        }

        public string getConnectionString()
        {
            string ConnectionString = string.Empty;

            switch (ConnectionType)
            {
                case DBConnectionType.SQLServer:
                    if (UseTrustedConnection)
                    {
                        ConnectionString = "Data Source=" + Server + ";Initial Catalog=" + Database + ";Trusted_Connection=true";
                    }
                    else
                    {
                        ConnectionString = "Data Source=" + Server + "; Initial Catalog=" + Database + "; User Id=" + UserName + "; Password=" + Password + ";";
                    }
                    break;
                case DBConnectionType.SQLite:
                    ConnectionString = "Data Source=" + FileLocation + ";Version=3;DateTimeFormat=CurrentCulture;";
                    break;
                case DBConnectionType.Firebird:
                    ConnectionString = "User=SYSDBA;Password=masterkey;Database=" + FileLocation + ";DataSource=localhost;" +
                                                    "Port=3050;Dialect=3;Charset=NONE;Role=;Connection lifetime=15;Pooling=true;" +
                                                    "MinPoolSize=0;MaxPoolSize=50;Packet Size=8192;ServerType=1;";
                    break;
                default:
                    break;
            }

            return ConnectionString;
        }
    }

    public enum DBConnectionType
    {
        SQLServer = 1,
        SQLite = 2,
        Firebird = 3,
        XML = 4,
        CSV = 5,
        Legacy = 6
    }
}
