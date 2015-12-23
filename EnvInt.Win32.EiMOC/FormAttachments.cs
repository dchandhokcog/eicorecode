using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DirectShowLib;

using EnvInt.Win32.EiMOC.Library;

namespace EnvInt.Win32.EiMOC
{
    public partial class FormAttachments : Form
    {
        private Capture _camera = null;
        private IntPtr _ip = IntPtr.Zero;
        private bool _hasPicture = false;
        private bool _hasFile = false;
        private Image _emptyImage = null;
        private bool _cameraPreview = false;
        private Size _cameraSize = new Size(640, 480);

        public FormAttachments()
        {
            InitializeComponent();
            _emptyImage = new Bitmap(pictureBoxPhoto.Image);

            //enumerate Video Input filters and add them to comboBox1
            foreach (DsDevice ds in DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice))
            {
                comboBoxSources.Items.Add(ds.Name);
            }

            if (comboBoxSources.Items.Count == 0)
            {
                MessageBox.Show("No camera devices were found on this system.", "Camera", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }

            comboBoxSources.SelectedIndex = 0;

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);

            if (_ip != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(_ip);
                _ip = IntPtr.Zero;
            }
        }

        public bool HasFile
        {
            get { return _hasFile; }
        }

        public bool HasPicture
        {
            get { return _hasPicture; }
        }

        public Image CurrentPhoto
        {
            get { return pictureBoxPhoto.Image; }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            pictureBoxPhoto.Image = _emptyImage;
            _hasPicture = false;
        }

        private void buttonCamera_Click(object sender, EventArgs e)
        {
            if (!_cameraPreview)
            {
                int VIDEOWIDTH = _cameraSize.Width; // Depends on video device caps
                int VIDEOHEIGHT = _cameraSize.Height; // Depends on video device caps
                const int VIDEOBITSPERPIXEL = 24; // BitsPerPixel values determined by device

                _camera = new Capture(comboBoxSources.SelectedIndex, VIDEOWIDTH, VIDEOHEIGHT, VIDEOBITSPERPIXEL, pictureBoxPhoto);
                buttonCamera.Text = "&Snap";
                _cameraPreview = true;
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;

                // Release any previous buffer
                if (_ip != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(_ip);
                    _ip = IntPtr.Zero;
                }

                // capture image
                _ip = _camera.Click();
                Bitmap b = new Bitmap(_camera.Width, _camera.Height, _camera.Stride, PixelFormat.Format24bppRgb, _ip);
                _camera.Dispose();
                _camera = null;
                // If the image is upsidedown
                b.RotateFlip(RotateFlipType.RotateNoneFlipY);
                pictureBoxPhoto.Image = b;

                buttonCamera.Text = "&Preview";
                _cameraPreview = false;
                _hasPicture = true;

                Cursor.Current = Cursors.Default;
            }
        }

        private List<string> GetAllAvailableResolution(DsDevice vidDev)
        {
           try
           {
             int hr, bitCount = 0;

             IBaseFilter sourceFilter = null;

             var m_FilterGraph2 = new FilterGraph() as IFilterGraph2;
             hr = m_FilterGraph2.AddSourceFilterForMoniker(vidDev.Mon, null, vidDev.Name, out sourceFilter);
             var pRaw2 = DsFindPin.ByCategory(sourceFilter, PinCategory.Capture, 0);
             var AvailableResolutions = new List<string>();

             VideoInfoHeader v = new VideoInfoHeader();
             IEnumMediaTypes mediaTypeEnum;
             hr = pRaw2.EnumMediaTypes(out mediaTypeEnum);

             AMMediaType[] mediaTypes = new AMMediaType[1];
             IntPtr fetched = IntPtr.Zero;
             hr = mediaTypeEnum.Next(1, mediaTypes, fetched);

             while (fetched != null && mediaTypes[0] != null)
             {
               Marshal.PtrToStructure(mediaTypes[0].formatPtr, v);
               if (v.BmiHeader.Size != 0 && v.BmiHeader.BitCount != 0)
               {
                 if (v.BmiHeader.BitCount > bitCount)
                 {
                   AvailableResolutions.Clear();
                   bitCount = v.BmiHeader.BitCount;
                 }
                 AvailableResolutions.Add(v.BmiHeader.Width +"x"+ v.BmiHeader.Height);
               }
               hr = mediaTypeEnum.Next(1, mediaTypes, fetched);
             }
             return AvailableResolutions;
           }
           catch (Exception ex)
           {
             MessageBox.Show(ex.Message);
             return new List<string>();
           }
        }

        private void FormAttachments_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_camera != null)
            {
                _camera.Dispose();
            }

            if (_ip != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(_ip);
                _ip = IntPtr.Zero;
            }
        }

        private void comboBoxSources_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DsDevice ds in DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice))
            {
                if (ds.Name == comboBoxSources.SelectedItem.ToString())
                {
                    int finalWidth = 0;
                    int finalHeight = 0;
                    List<string> resolutions = GetAllAvailableResolution(ds);
                    foreach (string resolution in resolutions)
                    {

                        string[] rs = resolution.Split('x');
                        if (rs.Length == 2)
                        {
                            int width = 0;
                            int height = 0;
                            int.TryParse(rs[0], out width);
                            int.TryParse(rs[1], out height);

                            if (height > 0 && width > 0 && height > finalHeight && width > finalWidth)
                            {
                                finalWidth = width;
                                finalHeight = height;
                            }
                        }
                    }

                    _cameraSize = new Size(finalWidth, finalHeight);
                }
            }
        }

    }
}
