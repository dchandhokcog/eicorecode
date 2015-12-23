using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using EnvInt.Win32.FieldTech.Containers;
using EnvInt.Win32.EiMOC.Data;

namespace EnvInt.Win32.EiMOC
{
    public partial class DialogViewTags : Form
    {
        public bool showMOC = false;
        
        public DialogViewTags()
        {
            InitializeComponent();
            LoadComponents();
        }

        private void listViewComponents_DoubleClick(object sender, EventArgs e)
        {
            if (listViewComponents.SelectedItems.Count > 0)
            {
                ListViewItem item = listViewComponents.SelectedItems[0];
                TaggedComponent c = LocalData.GetComponent(item.Tag.ToString());
                if (c != null)
                {
                    if (showMOC)
                    {
                        FormMOC fe = new FormMOC(new Point(0, 0));
                        fe.SetComponent(c.Id, c.EngineeringTag, c.ComponentType, c.Stream, c.Drawing);
                        fe.ShowDialog();
                        LoadComponents();
                        ((MainForm)Tag).UpdateLocalComponentCount();
                    }
                    else
                    {
                        FormEditObject fe = new FormEditObject(new Point(0, 0));
                        fe.SetComponent(c.Id, c.EngineeringTag, c.ComponentType, c.Stream, c.Drawing);
                        fe.ShowDialog();
                        LoadComponents();
                        //refresh the main form for fun
                        ((MainForm)Tag).UpdateLocalComponentCount();
                    }
                }
                else
                {
                    MessageBox.Show("Can't Find Component.");
                }
            }
        }

        private void LoadComponents()
        {
            listViewComponents.Items.Clear();
            List<TaggedComponent> components = LocalData.GetComponents();
            foreach (TaggedComponent component in components)
            {
                ListViewItem item = new ListViewItem();
                item.SubItems[0].Text = component.LDARTag;
                item.SubItems.Add(component.PreviousTag);
                item.SubItems.Add(component.EngineeringTag);
                item.SubItems.Add(component.ComponentType);
                item.SubItems.Add(component.Children.Count().ToString());
                item.Tag = component.Id;
                listViewComponents.Items.Add(item);
            }
        }
    }
}
