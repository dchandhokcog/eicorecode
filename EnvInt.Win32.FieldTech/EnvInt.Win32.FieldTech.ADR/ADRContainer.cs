using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EnvInt.Win32.FieldTech.ADR
{
    public partial class ADRContainer : UserControl
    {
        public ADRContainer()
        {
            InitializeComponent();
        }

        public AxExpressViewerDll.AxCExpressViewerControl Control
        {
            get
            {
                return axCExpressViewerControl1;
            }
        }
    }
}
