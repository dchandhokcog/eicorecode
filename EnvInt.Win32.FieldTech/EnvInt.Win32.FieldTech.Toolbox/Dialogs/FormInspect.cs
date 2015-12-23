using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using phx21;
using BOKControls;

using EnvInt.Win32.FieldTech.Data;
using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech
{
    public partial class FormInspect : Form
    {

        private string WDAMessage = string.Empty;
        private bool DeviceError = false;

        public string Inspector
        {
            get
            {
                return comboInspectionUser.Text;
            }
            set
            {
                if (value != null)
                {
                    comboInspectionUser.Text = value.ToString();
                }
                else
                {
                    comboInspectionUser.Text = Environment.UserName;
                }
            }
        }

        public double Reading
        {
            get
            {
                double reading = 0;
                double.TryParse(textBoxReading.Text, out reading);
                return reading;
            }
            set
            {
                textBoxReading.Text = value.ToString();
            }
        }

        public bool RemoveInspection
        {
            get
            {
                return checkBoxRemoveInspection.Checked;
            }
        }

        public string Instrument
        {
            get
            {
                return textBoxInstrument.Text;
            }
            set
            {
                if (value != null)
                {
                    textBoxInstrument.Text = value.ToString();
                }
                else
                {
                    textBoxInstrument.Text = "";
                }
            }
        }

        public double Background
        {
            get
            {
                return double.Parse(textBoxBackground.Text);
            }
            set
            {
                if (value != null)
                {
                    textBoxBackground.Text = value.ToString();
                }
                else
                {
                    textBoxBackground.Text = "";
                }
            }
        }

        public bool EditExisting { get; set; }
        
        public FormInspect()
        {
            InitializeComponent();
            MainForm.phxConnect.DataReceived += new phx21.Phx21Connect.DataReceivedEventHandler(phxConnect_DataReceived);
            MainForm.WDAConnect.AnalyzerReadingReceived += new WDA_Connect.BlueConnect.AnalyzerReadingReceivedEventHandler(WDAConnect_AnalyzerReadingReceived);
            MainForm.WDAConnect.AnalyzerMessageReceived += new WDA_Connect.BlueConnect.AnalyzerMessageReceivedEventHandler(WDAConnect_AnalyzerMessageReceived);
            MainForm.TVAConnect.AnalyzerReadingReceived += new TVA_Connect.BlueConnect.AnalyzerReadingReceivedEventHandler(TVAConnect_AnalyzerReadingReceived);
            MainForm.TVAConnect.AnalyzerMessageReceived += new TVA_Connect.BlueConnect.AnalyzerMessageReceivedEventHandler(TVAConnect_AnalyzerMessageReceived);

        }

        void TVAConnect_AnalyzerMessageReceived(TVA_Connect.MessageType msgType, string Msg)
        {
            if (msgType == TVA_Connect.MessageType.FIDPID)
            {
                try
                {
                    //Analyzer PPM
                    //textBoxAnalyzerPPM.Text = Msg;
                    //Analyzer Error

                    //Calculate Net
                    if (MainForm.TVAConnect.CurrentBackground > -100)
                    {
                        double NetValue = double.Parse(Msg) - MainForm.TVAConnect.CurrentBackground;
                        if (NetValue < 0)
                        {
                            NetValue = 0;
                            //textBoxAnalyzerPPM.Text = NetValue.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    textBoxAnalyzerPPM.Text = ex.Message;
                }
 
            }
        }

        void TVAConnect_AnalyzerReadingReceived(double PPMValue, TVA_Connect.ErrorCodes ErrorCode)
        {
            try
            {
                //Analyzer PPM
                textBoxAnalyzerPPM.Text = PPMValue.ToString();
                //Analyzer Error
                TVA_Connect.ErrorCodes mError = ErrorCode;

                if (mError == TVA_Connect.ErrorCodes.PHXERROR_NONE)
                {
                    double NetValue = 0.0;
                    //Calculate Net
                    if (MainForm.TVAConnect.CurrentBackground > -100)
                    {
                        NetValue = PPMValue - MainForm.TVAConnect.CurrentBackground;
                        if (NetValue < 0)
                        {
                            NetValue = 0;
                        }
                    }
                    else
                    {
                        //if no background is set, just show PPM value
                        NetValue = PPMValue;
                    }

                    //Analyzer PPM
                    this.textBoxAnalyzerPPM.Text = string.Format("{0:N3}", NetValue);
                    double maxVal = 0.0;
                    double.TryParse(textBoxReading.Text, out maxVal);
                    if (maxVal < NetValue)
                    {
                        textBoxReading.Text = string.Format("{0:N3}", NetValue);
                    }
                }
                else
                {
                    switch (mError)
                    {
                        case TVA_Connect.ErrorCodes.PHX_ERROR_ERR:
                            textBoxAnalyzerPPM.Text = "Error";
                            break;
                        case TVA_Connect.ErrorCodes.PHX_ERROR_FLAMEDOUT:
                            textBoxAnalyzerPPM.Text = "Flameout";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void WDAConnect_AnalyzerMessageReceived(WDA_Connect.MessageTypes msgType, string Msg)
        {
            WDAMessage = Msg;
        }

        void WDAConnect_AnalyzerReadingReceived(double PPMValue, WDA_Connect.ErrorCodes ErrorCode)
        {
            try
            {
                //Analyzer Error
                WDA_Connect.ErrorCodes mError = ErrorCode;

                if (mError == WDA_Connect.ErrorCodes.WDAERROR_NONE)
                {
                    WDAMessage = mError.ToString();

                    double NetValue = 0.0;
                    //Calculate Net
                    if (MainForm.WDAConnect.CurrentBackground > -100)
                    {
                        NetValue = PPMValue - MainForm.WDAConnect.CurrentBackground;
                        if (NetValue < 0)
                        {
                            NetValue = 0;
                        }
                    }
                    else
                    {
                        //if no background is set, just show PPM value
                        NetValue = PPMValue;
                    }

                    //Analyzer PPM
                    this.textBoxAnalyzerPPM.Text = string.Format("{0:N3}", NetValue);
                    double maxVal = 0.0;
                    double.TryParse(textBoxReading.Text, out maxVal);
                    if (maxVal < NetValue)
                    {
                        textBoxReading.Text = string.Format("{0:N3}", NetValue);
                    }
                }
                else
                {
                    switch (mError)
                    {
                        case WDA_Connect.ErrorCodes.WDA_ERROR_ERR:
                            textBoxAnalyzerPPM.Text = "Error";
                            break;
                        case WDA_Connect.ErrorCodes.WDA_ERROR_FLAMEDOUT:
                            textBoxAnalyzerPPM.Text = "Flameout";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void phxConnect_DataReceived(double PPMValue, int ErrorCode, int BackgroundPPM)
        {
            if (ErrorCode < 1)
            {
                this.textBoxAnalyzerPPM.Text = string.Format("{0:N3}", PPMValue);
                double maxVal = 0.0;
                double.TryParse(textBoxReading.Text, out maxVal);
                if (maxVal < PPMValue)
                {
                    textBoxReading.Text = string.Format("{0:N3}", PPMValue);
                }
            }
            else
            {
                if (Globals.isProductChinese)
                {
                    switch (ErrorCode)
                    {
                        case 1:
                            this.textBoxAnalyzerPPM.Text = "熄火";
                            break;
                        case 2:
                            this.textBoxAnalyzerPPM.Text = "不良校准";
                            break;
                        case 3:
                            this.textBoxAnalyzerPPM.Text = "连接关闭";
                            break;
                        case 4:
                            this.textBoxAnalyzerPPM.Text = "PHX21 错误";
                            break;
                        default:
                            this.textBoxAnalyzerPPM.Text = "PHX21 错误";
                            break;
                    }
                }
                else
                {
                    switch (ErrorCode)
                    {
                        case 1:
                            this.textBoxAnalyzerPPM.Text = "Flame Out";
                            break;
                        case 2:
                            this.textBoxAnalyzerPPM.Text = "Bad Calibration";
                            break;
                        case 3:
                            this.textBoxAnalyzerPPM.Text = "Connection Closed";
                            break;
                        case 4:
                            this.textBoxAnalyzerPPM.Text = "PHX21 Error";
                            break;
                        default:
                            this.textBoxAnalyzerPPM.Text = "PHX21 Error";
                            break;
                    }
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void FormInspect_Load(object sender, EventArgs e)
        {

            this.TopMost = true;

            pictureBoxEditMode.Visible = EditExisting;

            comboInspectionUser.Items.Clear();

            if (Globals.CurrentProjectData != null)
            {
                foreach (LDARTechnician tech in Globals.CurrentProjectData.LDARData.Technicians)
                {
                    comboInspectionUser.Items.Add(tech.Name.ToString());
                }
               
            }
        }

        private void textBoxBackground_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == '.')
            {
                if (e.KeyChar == '.' & this.textBoxBackground.Text.Contains(".")) e.KeyChar = char.MinValue;
            }
            else e.KeyChar = char.MinValue;
        }

        private void textBoxReading_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;

            if (char.IsDigit(e.KeyChar) || e.KeyChar == '.')
            {
                if (e.KeyChar == '.' & this.textBoxReading.Text.Contains(".")) e.KeyChar = char.MinValue;
            }
            else e.KeyChar = char.MinValue;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (validateForm())
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }

        }

        private bool validateForm()
        {
            
            //skip this is we're just removing an inspection
            if (checkBoxRemoveInspection.Checked) return true;
            
            bool formValid = true;
            List<string> errList = new List<string>();
            string errorMsg = string.Empty;
            if(Globals.isProductChinese)
            {
                if (comboInspectionUser.Text == "") errList.Add("检查员不能为空 Inspector cannot be empty");
                if (textBoxBackground.Text == "") errList.Add("背景不能为空 Background cannot be empty");
                if (textBoxReading.Text == "") errList.Add("读书不能为空 Reading cannot be empty");
                if (textBoxInstrument.Text == "") errList.Add("仪器不能为空 Instrument cannot be empty ");
                if (DeviceError) errList.Add("这个检查不能因保存到分析仪的错误 This inspection can't be saved due to an analyzer error");
            }
            else
            {
                if (comboInspectionUser.Text == "") errList.Add("Inspector cannot be empty");
                if (textBoxBackground.Text == "") errList.Add("Background cannot be empty");
                if (textBoxReading.Text == "") errList.Add("Reading cannot be empty");
                if (textBoxInstrument.Text == "") errList.Add("Instrument cannot be empty");
                if (DeviceError) errList.Add("This inspection can't be saved due to an analyzer error");
            }
            
            try
            {
                double bg = double.Parse(textBoxBackground.Text);
                
                if (Globals.isProductChinese)
                {
                    if (bg < 0 || bg > 999999) errList.Add("背景必须介于0和百万 Background must be between 0 and 1000000");
                }
                else
                {
                    if (bg < 0 || bg > 999999) errList.Add("Background must be between 0 and 1000000");
                }
            }
            catch
            {
                
                if (Globals.isProductChinese)
                {
                    errList.Add("背景值无效 Background value is invalid");
                }
                else
                {
                    errList.Add("Background value is invalid");
                }
            }

            try
            {
                double bg = double.Parse(textBoxReading.Text);
                if (Globals.isProductChinese)
                {
                    if (bg < 0 || bg > 999999) errList.Add("阅读必须在0到1000000 Reading must be between 0 and 1000000");
                }
                else
                {
                    if (bg < 0 || bg > 999999) errList.Add("Reading must be between 0 and 1000000");
                }
            }
            catch
            {
                if (Globals.isProductChinese)
                {
                    errList.Add("读值无效 Reading value is invalid");
                }
                else
                {
                    errList.Add("Reading value is invalid");
                }
            }

            if (errList.Count() > 0)
            {
                formValid = false;
                foreach (string str in errList)
                {
                    errorMsg = errorMsg + str + Environment.NewLine; 
                }
                MessageBox.Show(errorMsg);
            }

            return formValid;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRemoveInspection.Checked)
            {
                comboInspectionUser.Enabled = false;
                textBoxBackground.Enabled = false;
                textBoxInstrument.Enabled = false;
                textBoxBackground.Enabled = false;
                textBoxReading.Enabled = false;
            }
            else
            {
                comboInspectionUser.Enabled = true;
                textBoxBackground.Enabled = true;
                textBoxInstrument.Enabled = true;
                textBoxBackground.Enabled = true;
                textBoxReading.Enabled = true;
            }
        }

        private void buttonResetReading_Click(object sender, EventArgs e)
        {
            textBoxReading.Text = "0";
        }

        private void buttonDeviceSettings_Click(object sender, EventArgs e)
        {

            switch (Properties.Settings.Default.AnalyzerType)
            {
                case "PHX21":
                    MainForm.phxConnect.ShowConnectForm(new Point(50, 50));
                    break;
                case "C2":
                    MainForm.TVAConnect.ShowConnectForm(new Point(50, 50));
                    break;
                case "WDA":
                    MainForm.WDAConnect.ShowConnectForm(new Point(50, 50));
                    break;
            }
            
        }

        private void textBoxAnalyzerPPM_TextChanged(object sender, EventArgs e)
        {
            
            //here we're testing to see if a connected analyzer produces an error.  if so, invalidate the inspection
            if (string.IsNullOrEmpty(textBoxAnalyzerPPM.Text))
            {
                DeviceError = false;
                return;
            }
            
            double x = 0;
            if (!double.TryParse(textBoxAnalyzerPPM.Text, out x))
            {
                DeviceError = true;
            }
            else
            {
                DeviceError = false;
            }
        }

        private void FormInspect_VisibleChanged(object sender, EventArgs e)
        {
            DeviceError = false;
        }

    }
}
