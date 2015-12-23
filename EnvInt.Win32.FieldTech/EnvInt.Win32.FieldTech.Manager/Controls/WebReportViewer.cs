using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace EnvInt.Win32.FieldTech.Migrate.Controls
{
    public partial class WebReportViewer : UserControl
    {

        public string url = string.Empty;

        public string HTMLSource
        {
            set
            { webBrowser1.Url = new Uri(value);}
        }
        
        public WebReportViewer()
        {
            InitializeComponent();
        }

        private void WebReportViewer_Load(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void WebReportViewer_VisibleChanged(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowPrintPreviewDialog();
        }
    }
}
