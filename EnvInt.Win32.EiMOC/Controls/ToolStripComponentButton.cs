using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.Drawing.Design;

namespace EnvInt.Win32.EiMOC.Controls
{
    [DefaultProperty("Items")]
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public class ToolStripComponentButton : ToolStripControlHost
    {
        public ToolStripComponentButton() : base(new Button() { Size = new System.Drawing.Size(48,48)})
        {

            
        }
    }
}
