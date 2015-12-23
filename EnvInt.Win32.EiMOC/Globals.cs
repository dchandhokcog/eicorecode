using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Community.CsharpSqlite.SQLiteClient;

using EnvInt.Win32.EiMOC.Data;
using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.EiMOC
{
    public static class Globals
    {
        public static string LastLocation;
        public static List<string> ProjectComponents = new List<string>();
        public static List<string> ProjectComponentTypes = new List<string>();
        public static Guid ApplicationGuid = new Guid("4C915D8D-3EE6-4083-ADDA-A1A297CFEC18");
        public static Dictionary<string, Point> DialogLocations = new Dictionary<string, Point>();
        public static ProjectData CurrentProjectData;
        public static bool CurrentProjectDataDirty;


        public static string WorkingFolder
        {
            get
            {
                string exeFolder = Environment.CurrentDirectory;
                return exeFolder + "\\Working";
            }
        }

        public static string BackupFolder
        {
            get
            {
                string exeFolder = Environment.CurrentDirectory;
                return exeFolder + "\\Backups";
            }
        }

        public static SqliteConnection GetDbConnection()
        {
            SqliteConnection conn = new SqliteConnection();

            string dbFilename = CurrentProjectData.ProjectName + ".db";
            string cs = string.Format("Version=3,uri=file:{0}", dbFilename);
            Console.WriteLine("Set connection String: {0}", cs);
            conn.ConnectionString = cs;
            if (!System.IO.File.Exists(dbFilename))
            {
                //doesn't exist... we have to copy our version there...

            }
            return conn;
        }

        public static DataTable QueryDb(SqliteConnection conn, string queryString)
        {
            SqliteCommand command = new SqliteCommand(queryString, conn);
            DataTable dataTable = new DataTable();
            SqliteDataAdapter dataAdapter = new SqliteDataAdapter();
            dataAdapter.SelectCommand = command;
            dataAdapter.Fill(dataTable);
            return dataTable;
        }


        public static void LogError(string msg, string source, string stackTrace)
        {
            //EiServiceReference.MainServiceClient client = new EiServiceReference.MainServiceClient();
           // client.SendErrorAsync(msg, source, stackTrace, System.Environment.UserName, System.Environment.MachineName, String.Empty, GetWindowsVersion(), String.Empty, "FieldTechToolbox", GetCurrentAssemblyVersion(), String.Empty);
        }

        private static string GetWindowsVersion()
        {
            return Environment.OSVersion.Version.ToString();
        }

        public static string GetCurrentAssemblyVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
    }
}
