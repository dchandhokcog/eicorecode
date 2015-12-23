using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnvInt.Win32.FieldTech.Controls
{
    public partial class ComponentSearch : UserControl
    {
        public ComponentSearch()
        {
            InitializeComponent();
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            ToolStrip toolStrip = this.Parent as ToolStrip;
            if (toolStrip != null)
            {
                MainForm mainForm = toolStrip.Parent as MainForm;
                if (mainForm != null)
                {
                    mainForm.ExecuteSearch(textBoxTerm.Text);
                }
            }
        }
    }
}
