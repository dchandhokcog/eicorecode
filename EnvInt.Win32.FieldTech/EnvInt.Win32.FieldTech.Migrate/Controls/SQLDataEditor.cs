using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

using EnvInt.Win32.FieldTech.Data;

namespace EnvInt.Win32.FieldTech.Migrate.Controls
{
    public partial class SQLDataEditor : UserControl
    {

        private DataSet _localData = new DataSet();
        public SQLiteDataAdapter _localAdapter = new SQLiteDataAdapter();
        public SQLiteCommand _localCommand = new SQLiteCommand();
        public SQLiteConnection _localConnection { get; set; }
        public string TableName { get; set; }
        private DataSetSQLExport dt = new DataSetSQLExport();

        public string SQL 
        {
            get
            {
                return fastColoredTextBoxSQL.Text;
            }
            set
            {
                fastColoredTextBoxSQL.Text = value;
            }
        }
        
        public SQLDataEditor()
        {
            InitializeComponent();
        }

        private void SQLDataEditor_Load(object sender, EventArgs e)
        {
            if (_localConnection != null)
            {
                _localCommand = new SQLiteCommand("select * from " + TableName, _localConnection);
                //SQLiteCommand classTblCmd = new SQLiteCommand("select * from ComponentClass", _localConnection);
                //SQLiteDataAdapter classDA = new SQLiteDataAdapter(classTblCmd);
                _localAdapter = new SQLiteDataAdapter(_localCommand);
                _localData.Tables.Add(new DataTable(TableName));
                //_localData.Tables.Add(new DataTable("ComponentClass"));

                _localAdapter.Fill(_localData.Tables[TableName]);
                //classDA.Fill(_localData.Tables["ComponentClass"]);
                //_localData.Relations.Add(new DataRelation("ClassRelation",_localData.Tables["ComponentClass"].Columns["ComponentClassID"],_localData.Tables[TableName].Columns["ComponentClassID"]));
                SQLiteCommandBuilder cmdBuild = new SQLiteCommandBuilder(_localAdapter);
                _localConnection.Close();
                
                fastColoredTextBoxSQL.Text = "select * from " + TableName;
                bindingSource1.DataSource = _localData.Tables[TableName];
                bindingNavigator1.BindingSource = bindingSource1;
                dataGridViewContents.DataSource = bindingSource1;
                //DataGridViewComboBoxColumn dc = new DataGridViewComboBoxColumn();
                //dc.DataSource = _localData.Tables["ComponentClass"];
                //dc.Name = "Class";
                //dc.DisplayMember = "ClassDescription";
                //dc.ValueMember = "ComponentClassID";
                //dc.DataPropertyName = "ComponentClassID";
                //dataGridViewContents.Columns.Add(dc);
                //dataGridViewContents.Columns.Remove("ComponentClassID");
                Application.DoEvents();
            }

        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            _localConnection.Open();
            _localAdapter.Update(_localData.Tables[TableName]);
            _localConnection.Close();
        }

    }
}
