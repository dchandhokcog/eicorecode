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
    public class ToolStripComponentCount : ToolStripControlHost
    {
        public ToolStripComponentCount()
            : base(new ComponentCountControl())
        {
        }

        [Browsable(false)]
        public ComponentCountControl ComponentCountControl
        {
            get { return base.Control as ComponentCountControl; }
        }

        public int ParentCount
        {
            get { return ComponentCountControl.ParentCount; }
            set { ComponentCountControl.ParentCount = value; }
        }

        public int ChildCount
        {
            get { return ComponentCountControl.ChildCount; }
            set { ComponentCountControl.ChildCount = value; }
        }

    }
}
