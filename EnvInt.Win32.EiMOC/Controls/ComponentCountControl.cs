using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnvInt.Win32.EiMOC.Controls
{
    public partial class ComponentCountControl : UserControl
    {
        private int _parentCount = 0;
        private int _childCount = 0;
        private Color _emptyColor = Color.Black;
        private Color _nonEmptyColor = Color.Red;

        public ComponentCountControl()
        {
            InitializeComponent();
        }

        public int ParentCount
        {
            get
            {
                return _parentCount;
            }
            set
            {
                _parentCount = value;
                UpdateSummary();
                UpdateColor();
            }
        }

        public int ChildCount
        {
            get
            {
                return _childCount;
            }
            set
            {
                _childCount = value;
                UpdateSummary();
                UpdateColor();
            }
        }

        private void UpdateSummary()
        {
            labelSummary.Text = "Parent: " + _parentCount.ToString() + " Child: " + _childCount.ToString();
        }

        private void UpdateColor()
        {
            if (_parentCount > 0 || _childCount > 0)
            {
                labelSummary.ForeColor = _nonEmptyColor;
            }
            else
            {
                labelSummary.ForeColor = _emptyColor;
            }
        }
    }
}
