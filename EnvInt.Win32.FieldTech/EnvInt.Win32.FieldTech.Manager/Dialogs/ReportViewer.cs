using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

using EnvInt.Win32.FieldTech.Manager.HelperClasses;
using EnvInt.Win32.FieldTech.Containers;
using EnvInt.Win32.FieldTech.Common;
using EnvInt.Win32.FieldTech.Manager.Dialogs;
using EnvInt.Win32.FieldTech.Migrate.Controls;

namespace EnvInt.Win32.FieldTech.Manager.Dialogs
{
    public partial class ReportViewer : Form
    {

        public DataTable localData { get; set; }
        public WebReportViewer localReportViewer = new WebReportViewer();        

        public ReportViewer()
        {
            InitializeComponent();
            localReportViewer.Dock = DockStyle.Fill;
            localReportViewer.Parent = this;
        }

        public ReportViewer(string FileLocation)
        {
            InitializeComponent();
            localReportViewer.Dock = DockStyle.Fill;
            localReportViewer.HTMLSource = FileLocation;
            localReportViewer.Parent = this;
        }

    }
}
