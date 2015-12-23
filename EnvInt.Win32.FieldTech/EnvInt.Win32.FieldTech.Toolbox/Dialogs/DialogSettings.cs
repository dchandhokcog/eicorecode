using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using EnvInt.Win32.FieldTech.Data;
using EnvInt.Win32.FieldTech.Common;
using EnvInt.Win32.FieldTech.Dialogs;

namespace EnvInt.Win32.FieldTech
{
    public partial class DialogSettings : Form
    {

        private MainForm _mainForm;
        private bool suspendWarnings = false;

        public DialogSettings()
        {
            InitializeComponent();
            SetFormProperties();
        }

        public DialogSettings(MainForm mainForm)
        {
            _mainForm = mainForm;
            InitializeComponent();
            suspendWarnings = true;
            SetFormProperties();
            suspendWarnings = false;
        }

        private void SetFormProperties()
        {
            if (Globals.isProductChinese)
            {
                checkBoxAllowChildEdit.Checked = false;
                checkBoxAllowChildEdit.Enabled = false;
                comboBoxOverride.Enabled = false;
            }
            else
            {
                checkBoxAllowChildEdit.Checked = true;
                checkBoxAllowChildEdit.Enabled = true;
                comboBoxOverride.Enabled = true;
            }
            checkBoxChangetoChinese.Checked = Properties.Settings.Default.isProductChinese;
            checkBoxOpenLastProject.Checked = Properties.Settings.Default.OpenLastProject;
            checkBoxShowSplash.Checked = Properties.Settings.Default.ShowSplash;
            checkBoxForceUppercase.Checked = Properties.Settings.Default.ForceUpperCase;
            textBoxDeviceIdentifier.Text = Properties.Settings.Default.DeviceIdentifier;
            comboBoxOverride.Text = Properties.Settings.Default.TargetSite;
            textBoxAutoSaveInterval.Text = Properties.Settings.Default.AutoSaveInterval.ToString();
            checkBoxDuplicateLocation.Checked = Properties.Settings.Default.AllowDuplicateLocation;
            comboBoxAnalyzerType.Text = Properties.Settings.Default.AnalyzerType;
            checkBoxAllowChildEdit.Checked = Properties.Settings.Default.AllowChildTagEdit;
            buttonConnectAnalyzer.Enabled = MainForm.bluetoothAvailable;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

            int rndTagGenCount = 0;
            if (int.TryParse(comboBoxOverride.Text, out rndTagGenCount))
            {
                if (MessageBox.Show("Add " + rndTagGenCount.ToString() + " random test components?  This will add the number of components specified in 'Target Site' to your list of currently tagged components!", "Generate Test Data", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    string passwd = string.Empty;
                    Globals.InputBox("Enter password", "Enter Password", ref passwd);
                    if (passwd == "Int3ll3ct!")
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        for (int x = 1; x <= rndTagGenCount; x++)
                        {
                            LocalData.AddComponent(FileUtilities.getRandomComponent());
                        }

                        Cursor.Current = Cursors.Arrow;
                        MessageBox.Show(rndTagGenCount.ToString() + " Random Tags Added");
                        MainForm._projectDirty = true;
                    }
                }
            }
            
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (checkBoxChangetoChinese.Checked)
            {
                Globals.isProductChinese = true;
                Globals.setCurrentCulture();
            }
            else
            {
                Globals.isProductChinese = false;
                Globals.setCurrentCulture();
            }
            Properties.Settings.Default.ShowSplash = checkBoxShowSplash.Checked;
            Properties.Settings.Default.OpenLastProject = checkBoxOpenLastProject.Checked;
            Properties.Settings.Default.ForceUpperCase = checkBoxForceUppercase.Checked;
            Properties.Settings.Default.DeviceIdentifier = textBoxDeviceIdentifier.Text;
            Properties.Settings.Default.TargetSite = comboBoxOverride.Text;
            Properties.Settings.Default.AllowDuplicateLocation = checkBoxDuplicateLocation.Checked;
            Properties.Settings.Default.isProductChinese = checkBoxChangetoChinese.Checked;
            Properties.Settings.Default.AnalyzerType = comboBoxAnalyzerType.Text;
            Properties.Settings.Default.AllowChildTagEdit = checkBoxAllowChildEdit.Checked;
            try
            {
                Properties.Settings.Default.AutoSaveInterval = int.Parse(textBoxAutoSaveInterval.Text);
            }
            catch { }

            Properties.Settings.Default.Save();
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void buttonShowWorking_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", Globals.WorkingFolder);
        }

        private void DialogSettings_Load(object sender, EventArgs e)
        {
            comboBoxOverride.Items.Clear();
            
            if (Globals.CurrentProjectData != null)
            {
                if (Globals.CurrentProjectData.LDARDatabaseType.Contains("LeakDAS"))
                {
                    comboBoxOverride.Items.Add("Use Default Form For Project Type");
                    comboBoxOverride.Items.Add("MOC");
                    comboBoxOverride.Items.Add("TAG");
                    comboBoxOverride.Items.Add("REVAL");
                    comboBoxOverride.Items.Add("MGV");
                }
                else
                {
                    comboBoxOverride.Items.Add("Use Default Form For Project Type");
                    comboBoxOverride.Items.Add("GW");
                    comboBoxOverride.Items.Add("GW_REVAL");
                }
            }
            else
            { 
                comboBoxOverride.Items.Add("Use Default Form For Project Type");
                comboBoxOverride.Items.Add("MOC");
                comboBoxOverride.Items.Add("TAG");
                comboBoxOverride.Items.Add("REVAL");
                comboBoxOverride.Items.Add("MGV");
                comboBoxOverride.Items.Add("GW");
                comboBoxOverride.Items.Add("GW_REVAL");
            }
        }

        private void buttonConnectAnalyzer_Click(object sender, EventArgs e)
        {
            //FormConnectAnalyzer fConnect = new FormConnectAnalyzer(_mainForm);

            //fConnect.ShowDialog();

            if (comboBoxAnalyzerType.Text == "PHX21")
            {
                MainForm.phxConnect.ShowConnectForm(new Point(50, 50));
            }
            if (comboBoxAnalyzerType.Text == "C2")
            {
                MainForm.TVAConnect.ShowConnectForm(new Point(50, 50));
            }
            if (comboBoxAnalyzerType.Text == "WDA")
            {
                MainForm.WDAConnect.ShowConnectForm(new Point(50, 50));
            }
        }

        private void comboBoxAnalyzerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DialogResult dr;

            if (suspendWarnings) return;
            
            if (!checkBoxChangetoChinese.Checked)
            {
                dr = MessageBox.Show("Changing analyzer type will reset all connections, continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            else
            {
                dr = MessageBox.Show("更改分析类型将重置所有连接，是否继续？ Changing analyzer type will reset all connections, continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }

            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                _mainForm.resetPHX();
                _mainForm.resetWDA();
                _mainForm.resetTVA();
            }
        }
    }
}
