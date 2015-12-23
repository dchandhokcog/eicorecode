using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization.Json;
using System.IO;

using MouseKeyboardActivityMonitor.Controls;
using MouseKeyboardActivityMonitor.WinApi;
using MouseKeyboardActivityMonitor;
using System.Windows.Input;

using BOKControls;
using WDA_Connect;
using TVA_Connect;
using phx21;

using AdCommon;
using AxExpressViewerDll;
//using EPlotRenderer;

using EnvInt.Win32.FieldTech.Containers;
using EnvInt.Win32.FieldTech.Data;
using EnvInt.Win32.FieldTech.Library;
using EnvInt.Win32.FieldTech.Common;
using EnvInt.Win32.FieldTech.HelperClasses;
using Ionic.Zip;

namespace EnvInt.Win32.FieldTech
{
    public partial class MainForm : Form
    {

        private List<Control> _highlightControls = new List<Control>();
        //TODO: separate form for shell norco, this should be dynamic!
        public static FormEditTag _editTag = new FormEditTag(new Point(10, 40));
        public static FormEditTag_Full _editTag_Full = new FormEditTag_Full(new Point(10, 40));
        public static FormEditTag_Garyville _editTag_Garyville = new FormEditTag_Garyville(new Point(10, 40));
        public static FormEditTag_GW_Reval _editTag_GW = new FormEditTag_GW_Reval(new Point(10, 40));
        public static FormEditTag_GW_Full _editTag_GW_Full = new FormEditTag_GW_Full(new Point(10, 40));
        public static FormEditTag_GW_Chinese _editTag_GW_Chinese = new FormEditTag_GW_Chinese(new Point(10, 40));
        private bool _freehandActive = false;
        public static bool _projectDirty = false;
        public static bool _eidSaving = false;
        public static bool _eidLoading = false;
        public static bool _lastSaveSuccess = true;
        public static double _lastBackground = 0;
        public static string _lastInspector = string.Empty;
        public static string _lastInstrument = string.Empty;
        private string _commandLineFileName = "";
        //ADR objects are now global to the main form in an effort to reduce memory leaks
        private ECompositeViewer.IAdECompositeViewer3 _LocalCompositeViewer;
        private ECompositeViewer.IAdSection _LocalIAdSection;
        private ECompositeViewer.IAdContent _LocalContent;
        private ECompositeViewer.IAdContent2 _LocalContent2;
        private ECompositeViewer.IAdContent3 _LocalContent3;
        private AdCommon.IAdCollection _LocalIAdCollection;
        private AdCommon.IAdObject _LocalIAdObject;
        private int _drawingSecondsElapsed = 0;
        private bool _mouseButtonDown = false;
        private string _formTitle = "";
        private bool _doubleClickRegistered = false;

        //forbidden zone stuff
        private List<Rectangle> forbiddenZones = new List<Rectangle>();
        private Bitmap _paletteControlsImage;
        private Bitmap _paletteControlsImage2;
        private Bitmap _paletteControlsImage3;
        private Bitmap _paletteControlsImage4;
        private Bitmap _paletteControlsImageX;
        private Bitmap _paletteControlsImagePin;
        private Bitmap _paletteControlsImageContext;
        private Bitmap _paletteControlsImageContext2;
        private Bitmap _paletteControlsImagePin2;
        private IAdPageObjectNode5 _currentPageNode;
        private MouseHookListener _mouseListener;


        //Bluetooth integration
        public static Phx21Connect phxConnect;
        public static WDA_Connect.BlueConnect WDAConnect;
        public static TVA_Connect.BlueConnect TVAConnect;
        public static bool bluetoothAvailable = false;

        public BackgroundWorker forbiddenZoneWorker = new BackgroundWorker();


        public MainForm()
        {
            InitializeComponent();
            adrContainer.Control.OnSelectObjectEx += axCExpressViewerControl1_OnSelectObjectEx;
            adrContainer.Control.OnSelectObject += axCExpressViewerControl1_OnSelectObject;
            adrContainer.Control.OnExecuteCommandEx += axCExpressViewerControl1_OnExecuteCommandEx;
            adrContainer.Control.OnEndLoadItem += axCExpressViewerControl1_OnEndLoadItem;
            adrContainer.Control.OnLButtonUp += axCExpressViewerControl1_OnMouseButtonUp;
            adrContainer.Control.OnLButtonDown += axCExpressViewerControl1_OnLButtonDown;
            adrContainer.Control.OnLButtonDblClick += axCExpressViewerControl1_OnLButtonDblClick;
            //adrContainer.Control.OnExecuteURL += axCExpressViewerControl1_OnExecuteURL;
            
            //adrContainer.Control.OnMouseMove += axCExpressViewerControl1_OnMouseMove;
            //adrContainer.Control.OnOverObjectEx += new IAdViewerEvents_OnOverObjectExEventHandler(Control_OnOverObjectEx);
            //adrContainer.Control.OnEndDraw += axCExpressViewerControl1_OnEndDraw;
            adrContainer.Control.OnUpdateUiItem += axCExpressViewerControl1_OnUpdateUiItem;


            UpdateLocalComponentCount();

            setFormTitle(string.Empty);

            //TODO: separate form for shell norco, this should be dynamic!
            _editTag.VisibleChanged += new EventHandler(this.fEditTag_TagSaved);
            _editTag_Full.VisibleChanged += new EventHandler(this.fEditTag_TagSaved);
            _editTag_Garyville.VisibleChanged += new EventHandler(this.fEditTag_TagSaved);
            _editTag_GW.VisibleChanged += new EventHandler(this.fEditTag_TagSaved);
            _editTag_GW_Full.VisibleChanged += new EventHandler(this.fEditTag_TagSaved);
            _editTag_GW_Chinese.VisibleChanged += new EventHandler(this.fEditTag_TagSaved);
            //_editTag_GW.FindObject += new EventHandler<FindObjectEventArgs>(this.fEditTag_FindObject);


            //DirectoryInfo di = new DirectoryInfo("c:\\users\\devin\\desktop\\SCRCDrawings");
            //foreach (FileInfo f in di.GetFiles())
            //{
            //    LoadDWFFile(f.FullName);
            //}
            Globals.setCurrentCulture();
        }

        //void Control_OnShowUiItem(object sender, IAdViewerEvents_OnShowUiItemEvent e)
        //{
        //    FindForbiddenZones();
        //}

        void Control_OnOverObjectEx(object sender, IAdViewerEvents_OnOverObjectExEvent e)
        {
            System.Diagnostics.Debug.WriteLine("MouseOver Event: " + e.bObjectID.ToString());
        }

        public MainForm(string fileName)
        {
            InitializeComponent();
            adrContainer.Control.OnSelectObjectEx += axCExpressViewerControl1_OnSelectObjectEx;
            adrContainer.Control.OnSelectObject += axCExpressViewerControl1_OnSelectObject;
            adrContainer.Control.OnExecuteCommandEx += axCExpressViewerControl1_OnExecuteCommandEx;
            adrContainer.Control.OnEndLoadItem += axCExpressViewerControl1_OnEndLoadItem;
            adrContainer.Control.OnLButtonUp += axCExpressViewerControl1_OnMouseButtonUp;
            adrContainer.Control.OnLButtonDown += axCExpressViewerControl1_OnLButtonDown;
            adrContainer.Control.OnLButtonDblClick += axCExpressViewerControl1_OnLButtonDblClick;
            //adrContainer.Control.OnExecuteURL += axCExpressViewerControl1_OnExecuteURL;
            //adrContainer.Control.OnMouseMove += axCExpressViewerControl1_OnMouseMove;
            //adrContainer.Control.OnEndDraw += axCExpressViewerControl1_OnEndDraw;
            adrContainer.Control.OnUpdateUiItem += axCExpressViewerControl1_OnUpdateUiItem;


            UpdateLocalComponentCount();
            _commandLineFileName = fileName;

            _editTag.VisibleChanged += new EventHandler(this.fEditTag_TagSaved);
            _editTag_Full.VisibleChanged += new EventHandler(this.fEditTag_TagSaved);
            _editTag_Garyville.VisibleChanged += new EventHandler(this.fEditTag_TagSaved);
            _editTag_GW.VisibleChanged += new EventHandler(this.fEditTag_TagSaved);
            _editTag_GW_Full.VisibleChanged += new EventHandler(this.fEditTag_TagSaved);
            _editTag_GW_Chinese.VisibleChanged += new EventHandler(this.fEditTag_TagSaved);
            //_editTag_GW.FindObject += new EventHandler<FindObjectEventArgs>(this.fEditTag_FindObject);
            Globals.setCurrentCulture();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Globals.isProductChinese = Properties.Settings.Default.isProductChinese;
            Globals.setCurrentCulture();
            
            if (checkForCleanExit())
            {
                if (string.IsNullOrEmpty(_commandLineFileName))
                {
                    timerSplash.Enabled = true;
                }
                else
                {
                    LoadEiD(_commandLineFileName);
                }
            }

            try
            {
                //load bitmaps we're using to compare to buttons on adr control we want to disable
                _paletteControlsImage = Properties.Resources.ADRPaletteControls;
                _paletteControlsImage2 = Properties.Resources.ADRPaletteControls2;
                _paletteControlsImageX = Properties.Resources.ADRPaletteControlX;
                _paletteControlsImageContext = Properties.Resources.ADRPaletteControlContext;
                _paletteControlsImagePin = Properties.Resources.ADRPaletteControlsPin;
                _paletteControlsImageContext2 = Properties.Resources.ADRPaletteControlContext2;
                _paletteControlsImage3 = Properties.Resources.ADRPaletteControls3;
                _paletteControlsImage4 = Properties.Resources.ADRPaletteControls4;
                _paletteControlsImagePin2 = Properties.Resources.ADRPaletteControlsPin2;

            }
            catch (Exception ex)
            {
                Globals.LogError(ex.Message, ex.Source, ex.StackTrace);
            }

            try
            {
                phxConnect = new Phx21Connect(this);
                WDAConnect = new WDA_Connect.BlueConnect(this);
                TVAConnect = new TVA_Connect.BlueConnect(this);
                bluetoothAvailable = true;
            }
            catch (Exception ex)
            {
                bluetoothAvailable = false;
            }
            
        }

        public void resetPHX()
        {
            phxConnect = new Phx21Connect(this);
        }

        public void resetWDA()
        {
            WDAConnect = new WDA_Connect.BlueConnect(this);
        }

        public void resetTVA()
        {
            TVAConnect = new TVA_Connect.BlueConnect(this);
        }

        private void axCExpressViewerControl1_OnUpdateUiItem(object sender, IAdViewerEvents_OnUpdateUiItemEvent e)
        {
            System.Diagnostics.Debug.WriteLine("UpdateUi Event: " + e.bstrItemName + " : " + e.ToString());
        }

        //private void axCExpressViewerControl1_OnEndDraw(object sender, IAdViewerEvents_OnEndDrawEvent e)
        //{
        //    //System.Diagnostics.Debug.WriteLine("EndDraw Event: " + e.ToString());
        //}

        //private void axCExpressViewerControl1_OnMouseMove(object sender, IAdViewerEvents_OnMouseMoveEvent e)
        //{
        //    //System.Diagnostics.Debug.WriteLine("Mouse Move Event: " + e.nX.ToString() + ":" + e.nY.ToString());
        //}

        private void axCExpressViewerControl1_OnLButtonDown(object sender, IAdViewerEvents_OnLButtonDownEvent e)
        {
            _LocalIAdSection = _LocalCompositeViewer.Section;
            _LocalContent = _LocalIAdSection.Content;
            _LocalContent2 = _LocalIAdSection.Content;
            _LocalContent3 = _LocalIAdSection.Content;
            _LocalIAdCollection = _LocalContent.get_Objects(1); //for selected object
            
            try
            {
                System.Diagnostics.Debug.WriteLine("Mouse Button down" + e.nX.ToString() + ":" + e.nY.ToString());
                _mouseButtonDown = true;

                if (_freehandActive)
                {
                    _drawingSecondsElapsed = 0;
                    timerMarkupDelay.Enabled = true;
                }
            }
            catch { }

        }

        //private void axCExpressViewerControl1_OnExecuteURL(object sender, IAdViewerEvents_OnExecuteURLEvent e)
        //{
        //    try
        //    {
        //        System.Diagnostics.Debug.WriteLine("url:" + e.pIAdPageLink.ToString() + ":" + e.nIndex.ToString());
        //    }
        //    catch { }

        //}

        private void axCExpressViewerControl1_OnLButtonDblClick(object sender, IAdViewerEvents_OnLButtonDblClickEvent e)
        {
            if (_freehandActive) return;

            if (isObjectSelected() == string.Empty) return;

            if (_editTag.Visible) return;
            if (_editTag_Garyville.Visible) return;
            if (_editTag_Full.Visible) return;
            if (_editTag_GW.Visible) return;
            if (_editTag_GW_Chinese.Visible) return;
            if (_editTag_GW_Full.Visible) return;

            _editTag_GW.TopMost = true;
            toolStripButtonTag_Click(this, null);
            _doubleClickRegistered = true;
        }
        
        private void axCExpressViewerControl1_OnMouseButtonUp(object sender, IAdViewerEvents_OnLButtonUpEvent e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Mouse Button up" + e.nX.ToString() + ":" + e.nY.ToString());
                System.Diagnostics.Debug.WriteLine("Freehand Active = " + _freehandActive.ToString());

                if (_freehandActive) _projectDirty = true;
                _drawingSecondsElapsed = 0;
                _mouseButtonDown = false;
            }
            catch { }
        }

        private void axCExpressViewerControl1_OnEndLoadItem(object sender, IAdViewerEvents_OnEndLoadItemEvent e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("EndLoadItem - " + e.bstrItemName);
                if (e.bstrItemName == "SECTION")
                {
                    int total = _LocalCompositeViewer.Sections.Count();
                    double current = _LocalCompositeViewer.Section.Order;
                    int currentInt = (int)current;
                    toolStripLabelDrawingNumber.Text = currentInt.ToString() + " of " + total.ToString();

                    if (currentInt == 1)
                    {
                        toolStripButtonACADPrevious.Enabled = false;
                    }
                    else
                    {
                        toolStripButtonACADPrevious.Enabled = true;
                    }

                    if (currentInt == total)
                    {
                        toolStripButtonACADNext.Enabled = false;
                    }
                    else
                    {
                        toolStripButtonACADNext.Enabled = true;
                    }

                }
            }
            catch { }
        }

        private void fEditTag_TagSaved(object sender, EventArgs e)
        {
            UpdateLocalComponentCount();

            if (Globals.DialogLocations.ContainsKey(this.Name))
            {
                Globals.DialogLocations[this.Name] = new Point(_editTag.Top, _editTag.Left);
            }
            else
            {
                Globals.DialogLocations.Add(this.Name, new Point(_editTag.Top, _editTag.Left));
            }

        }

        //private void fEditTag_FindObject(object sender, FindObjectEventArgs e)
        //{
        //    //string id = e.ObjectID;

        //    //SetSelectedObject(id);
        //}

        private void timerSplash_Tick(object sender, EventArgs e)
        {
            timerSplash.Enabled = false;

            bool openLastProject = Properties.Settings.Default.OpenLastProject;
            if (openLastProject)
            {
                string lastProjectDrawing = Properties.Settings.Default.LastProject;
                string lastEiD = Properties.Settings.Default.LastEiD;
                Properties.Settings.Default.LastEiD = "";
                Properties.Settings.Default.Save();

                if (String.IsNullOrEmpty(lastEiD))
                {
                    if (System.IO.File.Exists("SampleProject.eid"))
                    {
                        LoadEiD(System.IO.Path.GetFullPath("SampleProject.eid"));
                    }
                }
                else
                {
                    if (System.IO.File.Exists(lastEiD))
                    {
                        LoadEiD(System.IO.Path.GetFullPath(lastEiD));
                    }
                    else
                    {
                        if (System.IO.Path.GetExtension(lastProjectDrawing).ToLower() == ".pdf")
                        {
                            LoadPDFFile(System.IO.Path.GetFullPath(lastProjectDrawing));
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(lastProjectDrawing)) LoadDWFFile(System.IO.Path.GetFullPath(lastProjectDrawing));
                        }
                    }
                }
            }

            //make sure we have a valid device identifier, if not, autocreate one
            if (String.IsNullOrEmpty(Properties.Settings.Default.DeviceIdentifier))
            {
                Properties.Settings.Default.DeviceIdentifier = "Tablet" + DateTime.Now.Millisecond.ToString("000");
                Properties.Settings.Default.Save();
            }
            //TODO: This shoudl be removed at some point, I put it here to change any of the old default tablet identifiers to something
            //shorter and sweeter
            if (Properties.Settings.Default.DeviceIdentifier.StartsWith("SP_"))
            {
                Properties.Settings.Default.DeviceIdentifier = "Tablet" + DateTime.Now.Millisecond.ToString("000");
                Properties.Settings.Default.Save();
            }

            if (Properties.Settings.Default.ShowSplash)
            {
                Splash s = new Splash();
                DialogResult dr = s.ShowDialog();
            }
        }

        private void LoadDWFFile(string dwfFile)
        {
            timerAutoSave.Enabled = false;

            adrContainer.Control.SourcePath = "";

            if (_projectDirty)
            {
                if (MessageBox.Show("Save current project?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (!saveEid(Properties.Settings.Default.LastEiD)) return;
                }
            }

            Globals.cleanWorkingDirectory();

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                //this isn't a manager project, so let's clean up
                clearProjectData();
                Globals.CurrentProjectData.ProjectType = LDARProjectType.FieldTechToolbox;

                adrContainer.Control.SourcePath = dwfFile;
                _LocalCompositeViewer = (ECompositeViewer.IAdECompositeViewer3)adrContainer.Control.ECompositeViewer;

                //load any existing component data into the UI
                LocalData.Initialize(dwfFile);
                UpdateLocalComponentCount();
                //LoadComponentLists();

                //start with SELECT command
                adrContainer.Control.ExecuteCommand("SELECT");

                Properties.Settings.Default.LastProject = dwfFile;
                Properties.Settings.Default.LastEiD = "";
                Properties.Settings.Default.Save();
                setFormTitle(System.IO.Path.GetFileNameWithoutExtension(dwfFile));
                UpdateDrawingIndexLabel(0);
                toolStripButtonACADPrevious.Enabled = false;
                setMode();
            }
            catch (Exception ex)
            {
                //Cursor.Current = Cursors.Default;
                //MessageBox.Show("There was an error loading this drawing set. Try re-plotting from AutoCAD.");
                //Globals.LogError(ex.Message, "EnvInt.Win32.FieldTech.MainForm.LoadDWFFile", ex.StackTrace);
            }
            Cursor.Current = Cursors.Default;
        }

        private void LoadPDFFile(string pdfFile)
        {
                   
            timerAutoSave.Enabled = false;

            if (_projectDirty)
            {
                if (MessageBox.Show("Save current project?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (!saveEid(Properties.Settings.Default.LastEiD)) return;
                }
            }

            Globals.cleanWorkingDirectory();

            Cursor.Current = Cursors.WaitCursor;

            try
            {
                //this isn't a manager project, so let's clean up
                clearProjectData();
                Globals.CurrentProjectData.ProjectType = LDARProjectType.EiMOC;

                adrContainer.Control.SourcePath = pdfFile;

                //load any existing component data into the UI
                LocalData.Initialize(pdfFile);
                UpdateLocalComponentCount();
                //LoadComponentLists();

                Properties.Settings.Default.LastProject = pdfFile;
                Properties.Settings.Default.LastEiD = "";
                Properties.Settings.Default.Save();
                setFormTitle(System.IO.Path.GetFileNameWithoutExtension(pdfFile));
                UpdateDrawingIndexLabel(0);
                toolStripButtonACADPrevious.Enabled = false;
                setMode();
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("There was an error loading this drawing set. Try re-plotting from AutoCAD.");
                Globals.LogError(ex.Message, "EnvInt.Win32.FieldTech.MainForm.LoadPDFFile", ex.StackTrace);
            }
            Cursor.Current = Cursors.Default;
        }

        private void clearProjectData()
        {
            if (Globals.CurrentProjectData != null)
            {
                Globals.CurrentProjectData.LDARData.Areas.Clear();
                Globals.CurrentProjectData.LDARData.ChemicalStates.Clear();
                Globals.CurrentProjectData.LDARData.ComponentCategories.Clear();
                Globals.CurrentProjectData.LDARData.ComponentClassTypes.Clear();
                Globals.CurrentProjectData.LDARData.ComponentReasons.Clear();
                Globals.CurrentProjectData.LDARData.ComponentStreams.Clear();
                Globals.CurrentProjectData.LDARData.ExistingComponents.Clear();
                Globals.CurrentProjectData.LDARData.LocationPlants.Clear();
                Globals.CurrentProjectData.LDARData.Manufacturers.Clear();
                Globals.CurrentProjectData.LDARData.PressureServices.Clear();
                Globals.CurrentProjectData.LDARData.ProcessUnits.Clear();
                Globals.CurrentProjectData.LDARData.Technicians.Clear();
                Globals.CurrentProjectData.ProjectId = Guid.NewGuid();
                Globals.CurrentProjectData.ProjectName = "Unnamed Project";
                Globals.CurrentProjectData.ProjectType = LDARProjectType.FieldTechToolbox;
                Globals.CurrentProjectData.ProjectVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                Globals.CurrentProjectData.ProjectPath = Globals.WorkingFolder;
                Globals.CurrentProjectData.CADPackages = new List<CADPackage>();
                if (!string.IsNullOrEmpty(adrContainer.Control.SourcePath))
                {
                    CADPackage cp = new CADPackage();
                    cp.FileName = adrContainer.Control.SourcePath;
                    cp.LastRefreshed = DateTime.Now;
                    cp.PackageId = new System.Guid().ToString();
                    Globals.CurrentProjectData.CADPackages.Add(cp);
                }
            }
            else
            {
                Globals.CurrentProjectData = new ProjectData();
            }
        }

        private void LoadComponentLists()
        {
            Globals.ProjectComponents.Clear();
            Globals.ProjectComponentTypes.Clear();

            //ECompositeViewer.IAdECompositeViewer compositeViewer;
            //AdCommon.IAdCollection sections;
            ECompositeViewer.IAdSection currentSection;

            //_LocalCompositeViewer = (ECompositeViewer.IAdECompositeViewer)adrContainer.Control.ECompositeViewer;
            //_LocalIAdSection = _LocalCompositeViewer.Sections;

            uint nRed = 255, nGreen = 0, nBlue = 0;
            _LocalCompositeViewer.ObjectSelectionColor = 65536 * nBlue + 256 * nGreen + nRed;

            foreach (ECompositeViewer.IAdSection Section in _LocalCompositeViewer.Sections)
            {
                //Set section 
                //compositeViewer.Section = Section;
                //Get section 
                _LocalContent = Section.Content;
                _LocalContent2 = Section.Content;
                _LocalContent3 = Section.Content;
                _LocalIAdCollection = _LocalContent.get_Objects(0);

                ECompositeViewer.IAdContent ObjectContent = (ECompositeViewer.IAdContent)Section.Content;

                //Create a user collection of all objects.  Collection as such is not named or unnamed, it depends on whether the
                //items added into the user collection are named or unnamed.
                AdCommon.IAdUserCollection MyObjectsNamedCollection = (AdCommon.IAdUserCollection)ObjectContent.CreateUserCollection();
                //Get all the objects in the current section
                AdCommon.IAdCollection MyObjects = (AdCommon.IAdCollection)ObjectContent.get_Objects(0);
                AdCommon.IAdCollection MyObject2 = (AdCommon.IAdCollection)ObjectContent.get_Objects(0);

                //0 is to return all objects 
                foreach (ECompositeViewer.IAdObject MyObject in _LocalContent.get_Objects(0))
                {

                    
                    object dur = _LocalCompositeViewer._ViewerParams;

                    //testNode.Selected = true;

                    AdCommon.IAdCollection MyObjectProperties = (AdCommon.IAdCollection)MyObject.Properties;
                    AdCommon.IAdCollection MyObjectProperties2 = (AdCommon.IAdCollection)MyObject.Properties;
                    AdCommon.IAdCollection MyObjectProperties3 = (AdCommon.IAdCollection)MyObject.Properties;
                    foreach (AdCommon.IAdProperty MyObjectProperty in MyObjectProperties)
                    {
                        //IntPtr hDC = PlatformInvokeUSER32.GetWindowDC(MyObject.Handle.ToInt32()); 
                        //CompositeViewer.DrawToDC(hDC.ToInt32(), 10, 0, 250, 250);
                        string targetRow = string.Empty;
                        
                        if ((MyObjectProperty.Name.ToLower() == "tag"))
                        {
                            string componentTag = MyObjectProperty.Value;
                            //if (!Globals.ProjectComponents.Contains(componentTag))
                            //{
                            //    Globals.ProjectComponents.Add(componentTag);
                            //}
                            targetRow += "Tag:" + MyObjectProperty.Value + ",";
                        }

                        if ((MyObjectProperty.Name.ToLower() == "description"))
                        {
                            string componentType = MyObjectProperty.Value;
                            if (!Globals.ProjectComponentTypes.Contains(componentType))
                            {
                                Globals.ProjectComponentTypes.Add(componentType);
                            }
                            targetRow += "description:" + MyObjectProperty.Value + ",";
                        }

                        _LocalIAdCollection = _LocalContent2.get_Objects(1);
                        AdCommon.IAdObject2 obj = (AdCommon.IAdObject2)MyObject;
                        targetRow += "id:" + obj.Id;

                        _LocalIAdCollection = _LocalContent3.get_Objects(1);
                        AdCommon.IAdObject3 obj3 = (AdCommon.IAdObject3)MyObject;
                        targetRow += "Label:" + obj3.Label;
                       

                        if (targetRow.StartsWith("Tag")) Globals.ProjectComponents.Add(targetRow);
                    }
                }
            }

            System.IO.File.AppendAllLines("c:\\users\\devin\\desktop\\scrcObjects.txt", Globals.ProjectComponents);

            Globals.ProjectComponents = Globals.ProjectComponents.OrderBy(c => c).ToList();
        }

        private void SetSelectedObject(string id)
        {
            ECompositeViewer.IAdDocument doc = _LocalCompositeViewer.DocumentHandler.Document;

            foreach (ECompositeViewer.IAdPage page in doc.Pages)
            {
                foreach (ECompositeViewer.IAdPageObjectNode5 node in page.ObjectNodes)
                {
                    try
                    {
                        string tmpStr = id.Split(':').Last().Replace(":","").Trim();
                        if (node.Number == int.Parse(tmpStr))
                        {
                            node.Center2DObjectToView();
                        }
                    }
                    catch { }
                }
            }
            
       }


        private void axCExpressViewerControl1_OnSelectObject(object sender, AxExpressViewerDll.IAdViewerEvents_OnSelectObjectEvent e)
        {

            string strResult = "";
            //First parameter : piAdPageObjectNode 
            IAdPageObjectNode5 objPageObjectNode = (IAdPageObjectNode5)e.pIAdPageObjectNode;
            if (objPageObjectNode != null)
            {
                strResult = "Object node name is " + objPageObjectNode.Name;
                //MessageBox.Show(strResult);
                _currentPageNode = (dynamic)((IAdPageObjectNode5)e.pIAdPageObjectNode);
            }

            //TODO: separate form for shell norco, this should be dynamic!

            if (_editTag.Visible)
            {
                if (MessageBox.Show("Save current tag?","Save?",MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    _editTag.saveCurrentTag();
                    _editTag.Hide();
                }
                else
                {
                    _editTag.Hide();
                }
            }

            if (_editTag_Full.Visible)
            {
                if (MessageBox.Show("Save current tag?", "Save?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    _editTag_Full.saveCurrentTag();
                    _editTag_Full.Hide();
                }
                else
                {
                    _editTag_Full.Hide();
                }
            }

            if (_editTag_Garyville.Visible)
            {
                if (MessageBox.Show("Save current tag?", "Save?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    _editTag_Garyville.saveCurrentTag();
                    _editTag_Garyville.Hide();
                }
                else
                {
                    _editTag_Garyville.Hide();
                }
            }

            if (_editTag_GW.Visible)
            {
                if (MessageBox.Show("Save current tag?", "Save?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    _editTag_GW.saveCurrentTag();
                    _editTag_GW.Hide();
                }
                else
                {
                    _editTag_GW.Hide();
                }
            }

       }

        private void axCExpressViewerControl1_OnExecuteCommandEx(object sender, IAdViewerEvents_OnExecuteCommandExEvent e)
        {

            System.Diagnostics.Debug.WriteLine("ExecuteCommandEx: " + e.ToString());
            int result = 0;
            switch (e.bstrItemType)
            {
                case "SELECT":
                    toolStripButtonACADSelect.Checked = true;
                    toolStripButtonACADPan.Checked = false;
                    toolStripButtonACADExtents.Checked = false;
                    toolStripButtonACADZoomWindow.Checked = false;
                    break;
                case "PAN":
                    toolStripButtonACADSelect.Checked = false;
                    toolStripButtonACADPan.Checked = true;
                    toolStripButtonACADExtents.Checked = false;
                    toolStripButtonACADZoomWindow.Checked = false;
                    break;
                case "FITTOWINDOW":
                    toolStripButtonACADSelect.Checked = false;
                    toolStripButtonACADPan.Checked = false;
                    toolStripButtonACADExtents.Checked = true;
                    toolStripButtonACADZoomWindow.Checked = false;
                    break;
                case "ZOOMRECT":
                    toolStripButtonACADSelect.Checked = false;
                    toolStripButtonACADPan.Checked = false;
                    toolStripButtonACADExtents.Checked = false;
                    toolStripButtonACADZoomWindow.Checked = true;
                    break;
                case "NEXT":
                    result = UpdateDrawingIndexLabel(1);
                    toolStripButtonACADNext.Enabled = result <= 0;
                    toolStripButtonACADPrevious.Enabled = true;
                    break;
                case "PREVIOUS":
                    result = UpdateDrawingIndexLabel(-1);
                    toolStripButtonACADPrevious.Enabled = result >= 0;
                    toolStripButtonACADNext.Enabled = true;
                    break;
                default:
                    System.Diagnostics.Debug.WriteLine("OnCommandEx - " + e.bstrItemType);
                    //MessageBox.Show(e.bstrItemType);
                    break;
            }

            if (e.bstrItemType == "SHAPETOOLS_FREEHAND" || e.bstrItemType.Contains("LINEWEIGHT") || e.bstrItemType == "SHAPETOOLS_FREEHAND_HIGHLIGHTER")
            {
                _freehandActive = true;
                _drawingSecondsElapsed = 0;
                timerMarkupDelay.Enabled = true;
            }
            else
            {
                if (!(e.bstrItemType == "SHOWTAB" || e.bstrItemType == "FOCUSWINDOW"))
                {
                    if (!forbiddenZoneWorker.IsBusy) forbiddenZoneWorker.RunWorkerAsync();
                    _freehandActive = false;
                }
            }

        }


        public void ExecuteCommand(string command, string parameter1, string parameter2)
        {
            adrContainer.Control.ExecuteCommand(command);
        }

        public void ExecuteSearch(string term)
        {
            adrContainer.Control.ExecuteCommand("SEARCHBUTTON");

        }

        private void axCExpressViewerControl1_OnSelectObjectEx(object sender, AxExpressViewerDll.IAdViewerEvents_OnSelectObjectExEvent e)
        {
           
        }


        private int UpdateDrawingIndexLabel(int offset)
        {
            //ECompositeViewer.IAdECompositeViewer compositeViewer = (ECompositeViewer.IAdECompositeViewer)adrContainer.Control.ECompositeViewer;
            //AdCommon.IAdCollection sections = (AdCommon.IAdCollection)compositeViewer.Sections;
            //AdCommon.IAdSection section = (AdCommon.IAdSection)compositeViewer.Section;
            _LocalIAdCollection = _LocalCompositeViewer.Sections;
            _LocalIAdSection = _LocalCompositeViewer.Section;
            int index = ((int)_LocalIAdSection.Order) + offset;
            toolStripLabelDrawingNumber.Text = index.ToString() + " of " + _LocalIAdCollection.Count.ToString();
            if (index >= _LocalIAdCollection.Count) return 1;
            else if (index <= 1) return -1;
            return 0;
        }

        public void UpdateLocalComponentCount()
        {
            int parentCount = LocalData.GetParentComponentCount();
            int childCount = LocalData.GetChildComponentCount();

            toolStripComponentCounter.ParentCount = parentCount;
            toolStripComponentCounter.ChildCount = childCount;
            //toolStripLabelSummary.Text = "Parent: " + parentCount.ToString() + " Child: " + childCount.ToString();
            //toolStripLabelSummary.ForeColor = Color.Black;
            //if (parentCount > 0 || childCount > 0) toolStripLabelSummary.ForeColor = Color.Red;
        }

        private void toolStripButtonOpenProject_Click(object sender, EventArgs e)
        {
            timerAutoSave.Enabled = false;
            OpenFileDialog oFileDialog = new OpenFileDialog();
            oFileDialog.Filter = "FieldTech Toolbox Files (*.dwf;*.eid;*.pdf)|*.dwf;*.eid;*.pdf|DWF files (*.dwf)|*.dwf|FTT Manager Data Files|*.eid|PDF Files|*.pdf|All files (*.*)|*.*";
            oFileDialog.FilterIndex = 1;
            DialogResult dr = oFileDialog.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                string fName = System.IO.Path.GetExtension(oFileDialog.FileName).ToLower();
                switch (fName)
                {
                    case ".dwf":
                        LoadDWFFile(oFileDialog.FileName);
                        break;
                    case ".eid":
                        LoadEiD(oFileDialog.FileName);
                        break;
                    case ".eie":
                        LoadEiD(oFileDialog.FileName);
                        break;
                    case ".pdf":
                        LoadPDFFile(oFileDialog.FileName);
                        break;
                    default:
                        LoadDWFFile(oFileDialog.FileName);
                        break;
                }
            }
            else
            {
                if (Globals.CurrentProjectData != null && !string.IsNullOrEmpty(Properties.Settings.Default.LastEiD))
                {
                    timerAutoSave.Enabled = true;
                }
            }
        }

        private void saveCurrentDrawing()
        {
            if (Globals.CurrentProjectData.CADProjectPath == null) return;

            //to save markups, save .pdf out as .dwf and update references.
            string originalName = Globals.CurrentProjectData.CADProjectPath;
            string originalExtension = Path.GetExtension(Globals.CurrentProjectData.CADProjectPath);
            string newName = Path.GetFileNameWithoutExtension(Globals.CurrentProjectData.CADProjectPath) + ".dwf";

            if (originalExtension.ToLower() == ".pdf")
            {
                adrContainer.Control.SaveAs(Globals.WorkingFolder + "\\" + newName);
                adrContainer.Control.SourcePath = "";
                Globals.CurrentProjectData.CADProjectPath = Globals.WorkingFolder + "\\" + newName;
                Globals.CurrentProjectData.CADPackages.FirstOrDefault().FileName = newName;
                Globals.CurrentProjectData.CADPackages.FirstOrDefault().LocalName = newName;
                adrContainer.Control.SourcePath = Globals.CurrentProjectData.CADProjectPath;
                try
                {
                    File.Delete(Globals.WorkingFolder + "\\" + Path.GetFileName(originalName));
                    File.Delete(Globals.WorkingFolder + "\\project.json");
                    File.AppendAllText(Globals.WorkingFolder + "\\project.json", FileUtilities.SerializeObject<ProjectData>(Globals.CurrentProjectData));
                }
                catch (Exception ex)
                {
                    Globals.LogError(ex.Message, ex.Source, ex.StackTrace);
                }
                _projectDirty = true;
            }
            else
            {
                adrContainer.Control.SaveAs(Globals.CurrentProjectData.CADProjectPath);
            }
        }

        private bool saveEid(string fileName = "", bool skipDrawingSave = false)
        {

            if (LocalData.ProjectData == null)
            {
                MessageBox.Show("No project is open!");
                return false;
            }

            //can't save if we're still loading
            if (_eidLoading) return false;

            _eidSaving = true;
            saveNotification(true);
            timerAutoSave.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;

            if (!skipDrawingSave) saveCurrentDrawing();

            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = "FieldTech Data files (*.eid)|*.eid|All files (*.*)|*.*";
            fd.FilterIndex = 1;

            if (fileName == "")
            {
                DialogResult dr = fd.ShowDialog();
                fileName = fd.FileName;
                if (dr == System.Windows.Forms.DialogResult.Cancel)
                {
                    Cursor.Current = Cursors.Arrow;
                    if (Globals.CurrentProjectData != null && !string.IsNullOrEmpty(Properties.Settings.Default.LastEiD))
                    {
                        timerAutoSave.Enabled = true;
                    }
                    saveNotification(false);
                    return false;
                }
            }

            //JMA: Manager does not put a 'project.id' file into the .zip because the ProjectId is contained in the projectdata inside the .zip
            //storing it in two places would be bad either way.
            //instead... so, when saving, we just compare against the value in the project data...
            //string targetFileProjectId = FileUtilities.GetProjectId(fileName);
            //DW: Switched this back because it wasn't working for some reason
            string targetFileProjectId = string.Empty;
            bool targetExists = File.Exists(fileName);
            if (targetExists)
            {
                using (ZipFile oldzip = new ZipFile(fileName))
                {
                    targetFileProjectId = FileUtilities.GetZipEntryAsText(oldzip, "project.id");
                    oldzip.Dispose();
                }
            }
            else
            {
                //we're creating a new .eid here, so just append the current project id
                targetFileProjectId = Globals.CurrentProjectData.ProjectId.ToString();
            }


            if (targetFileProjectId != Globals.CurrentProjectData.ProjectId.ToString())
            {
                if (MessageBox.Show("The target file is from a different project, continue?", "Warning", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                {
                    timerAutoSave.Enabled = false;
                    _lastSaveSuccess = false;
                    saveNotification(false);
                    return false;
                }
            }


            try
            {
                if (File.Exists(fileName)) File.Delete(fileName);
                if (!File.Exists(Globals.WorkingFolder + "\\project.json"))
                { 
                    //if we're here, the project.json doesn't exist for this project, probably opened a dwf/pdf and saving as .eid
                    File.AppendAllText(Globals.WorkingFolder + "\\project.json", FileUtilities.SerializeObject<ProjectData>(Globals.CurrentProjectData));
                }
                //zip the entire 'working' folder
                using (ZipFile zip = new ZipFile(fileName))
                {
                    zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestSpeed;
                    zip.AddDirectory(Globals.WorkingFolder, "");
                    //add pid from project if one doesn't already exist.
                    ZipEntry pid = zip.Entries.Where(c => c.FileName == "project.id").FirstOrDefault();
                    if (pid != null) zip.RemoveEntry("project.id");
                    zip.AddEntry("project.id", Encoding.ASCII.GetBytes(Globals.CurrentProjectData.ProjectId.ToString()));
                    if (File.Exists(Globals.ErrorLog))
                    {
                        ZipEntry errLog = zip.Entries.Where(c => c.FileName == System.IO.Path.GetFileName(Globals.ErrorLog)).FirstOrDefault();
                        if (errLog != null) zip.RemoveEntry(errLog);
                        zip.AddFile(Globals.ErrorLog, "\\");
                    }
                    zip.Save();
                    _lastSaveSuccess = true;
                    //Globals.CurrentProjectData.CADProjectPath = fileName;
                    Properties.Settings.Default.LastEiD = fileName;
                    Properties.Settings.Default.Save();
                    setFormTitle(fileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error saving the file to disk: " + ex.Message);
                Globals.LogError(ex.Message, "EnvInt.Win32.FieldTech.MainForm.saveEid", ex.StackTrace);
                _lastSaveSuccess = false;
            }

            _eidSaving = false;
            saveNotification(false);
            if (_lastSaveSuccess) timerAutoSave.Enabled = true;
            Cursor.Current = Cursors.Arrow;

            return true;
        }

        private void LoadEiD(string fName, bool skipExtraction = false)
        {

            //we're assuming everything is in the .eid now, so save data if necessary then clear working directory            
            if (_projectDirty)
            {
                if (MessageBox.Show("Save current project?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (!saveEid(Properties.Settings.Default.LastEiD)) return;
                }
            }

            _eidLoading = true;
            timerAutoSave.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            FormPleaseWait fWait = new FormPleaseWait();
            fWait.Show();
            Application.DoEvents();

            //AdrContainer should be cleared first too... in case it is attached to something already
            adrContainer.Control.SourcePath = "";
            if (!skipExtraction) Globals.cleanWorkingDirectory();
            
            try
            {
                using (ZipFile zip = ZipFile.Read(fName))
                {
                    //FileUtilities.checkProjectVersion(zip, Application.ProductVersion.ToString());
                    ProjectData loadingProjectData = null;

                    if (!skipExtraction) zip.ExtractAll(Globals.WorkingFolder);

                    //loadingProjectData = FileUtilities.DeserializeObject<ProjectData>(FileUtilities.GetZipEntryAsText(zip, "project.json"));
                    loadingProjectData = FileUtilities.DeserializeProjectData(Globals.WorkingFolder + "\\project.json");

                    Globals.CurrentProjectData = loadingProjectData;
                    //load any existing component data into the UI
                    LocalData.LoadFromProject(Globals.CurrentProjectData);
                    UpdateLocalComponentCount();
                    Properties.Settings.Default.LastEiD = fName;
                    Properties.Settings.Default.Save();

                    //put a device.id file in working *always*
                    //this only reflects the last device to open the file!
                    if (System.IO.File.Exists(Globals.WorkingFolder + "\\device.id"))
                    {
                        System.IO.File.Delete(Globals.WorkingFolder + "\\device.id");
                    }
                    System.IO.File.WriteAllText(Globals.WorkingFolder + "\\device.id", LocalData.DeviceIdentifier, Encoding.UTF8);

                    //put a project.id file in working if it didn't come from the .eid
                    if (!System.IO.File.Exists(Globals.WorkingFolder + "\\project.id"))
                    {
                        System.IO.File.WriteAllText(Globals.WorkingFolder + "\\project.id", Globals.CurrentProjectData.ProjectId.ToString(), Encoding.UTF8);
                    }

                    string drawingEntry = string.Empty;
                    if (Globals.CurrentProjectData.CADPackages != null)
                    {
                        CADPackage defaultCADPackage = Globals.CurrentProjectData.CADPackages.FirstOrDefault();
                        if (defaultCADPackage.LocalName == "" || defaultCADPackage.LocalName == null)
                        {
                            drawingEntry = Path.GetFileName(Globals.CurrentProjectData.CADPackages.FirstOrDefault().FileName);
                        }
                        else
                        {
                            drawingEntry = Path.GetFileName(Globals.CurrentProjectData.CADPackages.FirstOrDefault().LocalName);
                        }
                    }

                    if (System.IO.File.Exists(Globals.WorkingFolder + "\\" + drawingEntry))
                    {
                        //this is good... save the entry here...
                        Globals.CurrentProjectData.CADProjectPath = Globals.WorkingFolder + "\\" + drawingEntry;
                        //remove tmp file if it exists (not sure why it ever does)
                        if (System.IO.File.Exists(Globals.CurrentProjectData.CADProjectPath + ".tmp")) System.IO.File.Delete(Globals.CurrentProjectData.CADProjectPath + ".tmp");
                        adrContainer.Control.SourcePath = Globals.CurrentProjectData.CADProjectPath; // JMA: This seems to be wrong, fixed. Globals.CurrentProjectData.CADPackages.FirstOrDefault().FileName;
                        _LocalCompositeViewer = (dynamic)(ECompositeViewer.IAdECompositeViewer3)adrContainer.Control.ECompositeViewer;
                        Properties.Settings.Default.LastEiD = fName;
                        Properties.Settings.Default.Save();
                        UpdateDrawingIndexLabel(0);
                        toolStripButtonACADPrevious.Enabled = false;
                        if (System.IO.Path.GetExtension(Globals.CurrentProjectData.CADProjectPath).ToLower() == ".dwf")
                        {
                            //start with SELECT command
                            adrContainer.Control.ExecuteCommand("SELECT");
                        }
                        setMode();
                        setFormTitle(System.IO.Path.GetFileNameWithoutExtension(fName));

                        if (Properties.Settings.Default.AutoSaveInterval > 0) timerAutoSave.Interval = Properties.Settings.Default.AutoSaveInterval;
                        _lastSaveSuccess = true;
                        File.WriteAllText(Globals.WorkingFolder + "\\lastfile.txt", fName.ToString(), Encoding.UTF8);
                        timerAutoSave.Enabled = true;
                        checkZeroSettings();
                        checkInitialEditFormAssignment();
                        //LoadComponentLists();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open project: " + ex.Message);
                Globals.LogError(ex.Message, "EnvInt.Win32.FieldTech.MainForm.LoadEiD", ex.StackTrace);
                Properties.Settings.Default.LastEiD = "";
                Properties.Settings.Default.Save();
                timerAutoSave.Enabled = false;
            }

            _eidLoading = false;
            timerAutoSave.Enabled = true;
            fWait.Close();
            Cursor.Current = Cursors.Arrow;
        }

        public void checkInitialEditFormAssignment()
        {
            if (Globals.CurrentProjectData.LDARDatabaseType.Contains("Guideware"))
            {
                if (!Properties.Settings.Default.TargetSite.Contains("GW"))
                {
                    Properties.Settings.Default.TargetSite = "Use Default Form For Project Type";
                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                if (Properties.Settings.Default.TargetSite.Contains("GW"))
                {
                    Properties.Settings.Default.TargetSite = "Use Default Form For Project Type";
                    Properties.Settings.Default.Save();
                }                
            }
            
        }

        //private void LoadEiD(string fName)
        //{
        //    try
        //    {
        //        using (ZipFile zip = ZipFile.Read(fName))
        //        {
        //            //FileUtilities.checkProjectVersion(zip, Application.ProductVersion.ToString());
        //            ProjectData loadingProjectData = null;
        //            //foreach (ZipEntry e in zip)
        //            //{
        //            //    if (e.FileName.ToLower() == "project.json")
        //            //    {7
        //            //        using (var ms = new MemoryStream())
        //            //        {
        //            //            e.Extract(ms);
        //            //            ms.Position = 0;
        //            //            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ProjectData));
        //            //            loadingProjectData = (ProjectData)ser.ReadObject(ms);
        //            //            break;
        //            //        }
        //            //    }
        //            //}

        //            loadingProjectData = FileUtilities.DeserializeObject<ProjectData>(FileUtilities.GetZipEntryAsText(zip, "project.json"));

        //            //look for existing working project..
        //            string idFile = Globals.WorkingFolder + "\\project.id";
        //            if (File.Exists(idFile))
        //            {
        //                string projectIdString = System.IO.File.ReadAllText(idFile);
        //                if (!String.IsNullOrEmpty(projectIdString))
        //                {
        //                    Guid projectId = Guid.Empty;
        //                    if (Guid.TryParse(projectIdString, out projectId))
        //                    {
        //                        if (projectId != loadingProjectData.ProjectId || fName != Properties.Settings.Default.LastEiD)
        //                        {
        //                            //the user is trying to open a project that is a different ID than what 
        //                            //is in the working directory currently.
        //                            DialogResult dr = MessageBox.Show("FieldTech has files remaining from a different project. Loading this project will remove all data from the previous project. You should consider exporting the existing project data before opening a new project. \r\n\r\nAre you sure you would like to erase all existing project data?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
        //                            if (dr == System.Windows.Forms.DialogResult.No)
        //                            {
        //                                return;
        //                            }
        //                            if (dr == System.Windows.Forms.DialogResult.Yes)
        //                            {
        //                                //delete it to make sure we are clean...
        //                                if (System.IO.Directory.Exists(Globals.WorkingFolder))
        //                                {
        //                                    adrContainer.Control.SourcePath = string.Empty;
        //                                    System.IO.Directory.Delete(Globals.WorkingFolder, true);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }

        //            //if we are here, then the new project to open seems to be the same project as before. This is okay.
        //            // 1. reload the FTTP projec
        //            // 2. extract the CAD Document again
        //            // 3. reload the existing data.csv file if it exists

        //            Globals.CurrentProjectData = loadingProjectData;
        //            //load any existing component data into the UI
        //            LocalData.LoadFromProject(Globals.CurrentProjectData);
        //            UpdateLocalComponentCount();
        //            Properties.Settings.Default.LastEiD = fName;
        //            Properties.Settings.Default.Save();

        //            //create working if it does not exist
        //            if (!System.IO.Directory.Exists(Globals.WorkingFolder))
        //            {
        //                System.IO.Directory.CreateDirectory(Globals.WorkingFolder);
        //            }
        //            if (!System.IO.Directory.Exists(Globals.WorkingFolder + "\\Images"))
        //            {
        //                System.IO.Directory.CreateDirectory(Globals.WorkingFolder + "\\Images");
        //            }

        //            //put a device.id file in working *always*
        //            System.IO.File.WriteAllText(Globals.WorkingFolder + "\\device.id", LocalData.DeviceIdentifier);

        //            //put a project.id file in working *always*
        //            System.IO.File.WriteAllText(Globals.WorkingFolder + "\\project.id", Globals.CurrentProjectData.ProjectId.ToString());

        //            ZipEntry projectEntry = zip.Entries.Where(e => e.FileName == Path.GetFileName("project.json")).FirstOrDefault();
        //            if (projectEntry != null)
        //            {
        //                projectEntry.Extract(Globals.WorkingFolder, ExtractExistingFileAction.OverwriteSilently);
        //            }

        //            ZipEntry drawingEntry = zip.Entries.Where(e => e.FileName == Path.GetFileName(Globals.CurrentProjectData.CADPackages.FirstOrDefault().FileName)).FirstOrDefault();

        //            if (drawingEntry != null)
        //            {
        //                //AdrContainer should be cleared first too... in case it is attached to something already
        //                adrContainer.Control.SourcePath = "";
        //                //this is good... save the entry here...
        //                Globals.CurrentProjectData.CADProjectPath = Globals.WorkingFolder + "\\" + drawingEntry.FileName;
        //                //remove tmp file if it exists (not sure why it ever does)
        //                if (System.IO.File.Exists(Globals.CurrentProjectData.CADProjectPath + ".tmp")) System.IO.File.Delete(Globals.CurrentProjectData.CADProjectPath + ".tmp");
        //                //only overwrite the cad package if it doesn't already exist.
        //                if (!System.IO.File.Exists(Globals.WorkingFolder + "\\" + Path.GetFileName(Globals.CurrentProjectData.CADPackages.FirstOrDefault().FileName)))
        //                {
        //                    drawingEntry.Extract(Globals.WorkingFolder, ExtractExistingFileAction.OverwriteSilently);
        //                }
        //                adrContainer.Control.SourcePath = Globals.CurrentProjectData.CADProjectPath; // JMA: This seems to be wrong, fixed. Globals.CurrentProjectData.CADPackages.FirstOrDefault().FileName;
        //                Properties.Settings.Default.LastEiD = fName;
        //                Properties.Settings.Default.Save();
        //                UpdateDrawingIndexLabel(0);
        //                toolStripButtonACADPrevious.Enabled = false;
        //                if (System.IO.Path.GetExtension(Globals.CurrentProjectData.CADProjectPath).ToLower() == ".dwf")
        //                {
        //                    //start with SELECT command
        //                    adrContainer.Control.ExecuteCommand("SELECT");
        //                }
        //                setMode();
        //                setFormTitle(System.IO.Path.GetFileNameWithoutExtension(fName));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Unable to open project: " + ex.Message);
        //    }
        //}

        public void toolStripButtonTag_Click(object sender, EventArgs e)
        {
            
            if (_LocalIAdSection == null)
            {
                MessageBox.Show("No project is open.");
            }
            else
            {
                _LocalIAdSection = _LocalCompositeViewer.Section;
                _LocalContent = _LocalIAdSection.Content;
                _LocalContent2 = _LocalIAdSection.Content;
                _LocalContent3 = _LocalIAdSection.Content;
                _LocalIAdCollection = _LocalContent.get_Objects(1); //for selected object

                if (_LocalIAdCollection.Count > 0)
                {
                    _LocalIAdObject = (IAdObject)_LocalIAdCollection[1];

                    string id = "";
                    string tag = "";
                    string description = "";
                    string str = "";
                    string stream = "";
                    string size = "";
                    string drawing = _LocalIAdSection.Title;

                    foreach (AdCommon.IAdProperty prop in (IAdCollection)_LocalIAdObject.Properties)
                    {
                        if (prop.Name.ToLower() == "tag") tag = Globals.textCleaner(prop.Value);
                        if (prop.Name.ToLower() == "description") description = Globals.textCleaner(prop.Value);
                        if (prop.Name.ToLower() == "stream") stream = Globals.textCleaner(prop.Value);
                        if (prop.Name.ToLower() == "size") size = Globals.textCleaner(prop.Value);
                        str = str + "Name: " + prop.Name + " Value: " + prop.Value + Environment.NewLine;
                    }

                    _LocalIAdCollection = _LocalContent2.get_Objects(1);
                    foreach (ECompositeViewer.IAdObject2 o in _LocalIAdCollection)
                    {
                        id = o.Id;
                    }
                    //MessageBox.Show(str);

                    AdCommon.IAdCollection obj3 = _LocalContent3.get_Objects(1);

                    //Don't allow non-ldar types to be documented
                    bool showInvalidTypeMsg = false;
                    if (description.Contains("PRIMARY LINE")) showInvalidTypeMsg = true;
                    if (description.Contains("INSTRUMENT SUPPLY")) showInvalidTypeMsg = true;
                    if (description.Contains("SECONDARY LINE")) showInvalidTypeMsg = true;
                    if (description.Contains("CAPILLARY TUBE")) showInvalidTypeMsg = true;

                    if (showInvalidTypeMsg)
                    {
                        MessageBox.Show("Cannot document " + description);
                        return;
                    }

                    //FormEditTag eo = new FormEditTag(GetFormEditPosition());

                    //TODO: separate form for shell norco, this should be dynamic!
                    if (string.IsNullOrEmpty(Properties.Settings.Default.TargetSite) || Properties.Settings.Default.TargetSite.Contains("Default"))
                    {
                        //if we're here, use a form that's appropriate to the project
                        if (Globals.CurrentProjectData.LDARDatabaseType == "Guideware")
                        {
                            if (Globals.isProductChinese)
                            {
                                _editTag_GW_Chinese._allowOldTagRefresh = true;
                                _editTag_GW_Chinese.SetComponent(id, tag, description, stream, drawing, size, true);
                                _editTag_GW_Chinese.Show();
                                _editTag_GW_Chinese.Focus();
                            }
                            else
                            {
                                _editTag_GW_Full._allowOldTagRefresh = true;
                                _editTag_GW_Full.SetComponent(id, tag, description, stream, drawing, size, true);
                                _editTag_GW_Full.Show();
                                _editTag_GW_Full.Focus();
                            }
                        }
                        else
                        {
                            if (Globals.CurrentProjectData.ProjectType == LDARProjectType.EiMOC)
                            {
                                _editTag_Full.SetComponent(id, tag, description, stream, drawing, size, true);
                                _editTag_Full.Show();
                            }
                            else
                            {
                                _editTag._allowOldTagRefresh = false;
                                _editTag.SetComponent(id, tag, description, stream, drawing, size, true);
                                _editTag.Show();
                            }
                        }
                    }
                    else
                    {
                        //if we're here, a form override has been specified - use that form instead
                        switch (Properties.Settings.Default.TargetSite)
                        {
                            case "TAG":
                                _editTag._allowOldTagRefresh = false;
                                _editTag.SetComponent(id, tag, description, stream, drawing, size, true);
                                _editTag.Show();
                                break;

                            case "MGV":
                                _editTag_Garyville.SetComponent(id, tag, description, stream, drawing, size, true);
                                _editTag_Garyville.Show();
                                break;

                            case "MOC":
                                _editTag_Full.SetComponent(id, tag, description, stream, drawing, size, true);
                                _editTag_Full.Show();
                                break;

                            case "GW_REVAL":
                                _editTag_GW._allowOldTagRefresh = true;
                                _editTag_GW.SetComponent(id, tag, description, stream, drawing, size, true);
                                _editTag_GW.Show();
                                _editTag_GW.Focus();
                                break;

                            case "GW":
                                _editTag_GW_Full._allowOldTagRefresh = true;
                                _editTag_GW_Full.SetComponent(id, tag, description, stream, drawing, size, true);
                                _editTag_GW_Full.Show();
                                _editTag_GW_Full.Focus();
                                break;

                            case "REVAL":
                                _editTag_GW._allowOldTagRefresh = true;
                                _editTag_GW.SetComponent(id, tag, description, stream, drawing, size, true);
                                _editTag_GW.Show();
                                _editTag_GW.Focus();
                                break;

                            default:
                                _editTag._allowOldTagRefresh = false;
                                _editTag.SetComponent(id, tag, description, stream, drawing, size, true);
                                _editTag.Show();
                                break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No object was selected in the drawing. Select a component first to tag it, or use the * (star) tag button to tag a component not located on the drawing.");
                }
            }
        }

        private Point GetFormEditPosition()
        {
            Point pos = new Point(panelMain.Top, this.panelMain.Width - 700);
            if (Globals.DialogLocations.ContainsKey("FormEditTag"))
            {
                pos = Globals.DialogLocations["FormEditTag"];
            }
            return pos;
        }




        private void toolStripButtonExport_Click(object sender, EventArgs e)
        {
            saveEid("");
        }

        //private void toolStripButtonExport_Click(object sender, EventArgs e)
        //{
        //    //export an EIE file... that will contain the following items..
        //    // 1. The EIP Project (for reference)
        //    // 2. The CSV file of tagging activities
        //    // 3. The CAD Package File (.pdf or .dwf typically)
        //    // 4. Any photos taken on this tagging event

        //    if (LocalData.ProjectData == null)
        //    {
        //        MessageBox.Show("No data to export!");
        //        return;
        //    }

        //    saveDWFToEID();
        //    SaveFileDialog fd = new SaveFileDialog();
        //    //fd.InitialDirectory = System.IO.Path.GetDirectoryName(LocalData.ProjectData);
        //    fd.Filter = "FieldTech Export files (*.eie)|*.eie|All files (*.*)|*.*";
        //    fd.FilterIndex = 1;
        //    string baseFileName = LocalData.ProjectData.ProjectName + "_" + LocalData.DeviceIdentifier + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00");
        //    string finalFileName = baseFileName;

        //    for (int i = 0; i < 10; i++)
        //    {
        //        if (System.IO.File.Exists(finalFileName))
        //        {
        //            finalFileName = baseFileName + "_" + i.ToString();
        //        }
        //        else
        //        {
        //            break;
        //        }
        //    }
        //    fd.FileName = finalFileName;
        //    DialogResult dr = fd.ShowDialog();

        //    if (dr == System.Windows.Forms.DialogResult.OK)
        //    {
        //        if (File.Exists(fd.FileName)) File.Delete(fd.FileName);
        //        //zip the entire 'working' folder
        //        using (ZipFile zip = new ZipFile(fd.FileName))
        //        {
        //            zip.AddDirectory(Globals.WorkingFolder, "");
        //            zip.Save();
        //        }

        //        ////export to Excel here...
        //        //List<TaggedComponent> components = LocalData.GetComponents();

        //        //List<string> csv = new List<string>();
        //        //csv.Add(new TaggedComponent().GetHeader());
        //        //foreach (TaggedComponent component in components)
        //        //{
        //        //    csv.Add(component.ToString());
        //        //    csv.AddRange(component.GetChildrenAsComponents());
        //        //}

        //        //System.IO.File.WriteAllText(fd.FileName, String.Join("\r\n", csv.ToArray()));

        //        DialogResult dr2 = MessageBox.Show("Clear \"Current Tagged Components?\" Data will exclusively exist in export if you select YES. Selecting NO may lead to duplicate information in LDAR database.", "Reset Data On Device", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        //        if (dr2 == System.Windows.Forms.DialogResult.Yes)
        //        {
        //            LocalData.BackupAndClear();
        //            UpdateLocalComponentCount();
        //        }
        //    }

        //}

        //private void toolStripButton4_CheckStateChanged(object sender, EventArgs e)
        //{
        //    if (toolStripButton4.Checked)
        //    {
        //        int pixelsHeight = 50;
        //        int pixelsWidth = 200;
        //        TransparentPanel p = new TransparentPanel();
        //        p.Height = pixelsHeight;
        //        p.Width = pixelsWidth;
        //        p.BackColor = Color.Yellow;
        //        p.AutoSize = false;
        //        //p.Parent = this;
        //        p.Top = axCExpressViewerControl1.Top + 100;
        //        p.Left = axCExpressViewerControl1.Left + 100;
        //        this.Controls.Add(p);
        //        p.BringToFront();
        //        highlightControls.Add(p);
        //    }
        //    else
        //    {
        //        foreach (Control c in highlightControls)
        //        {
        //            this.Controls.Remove(c);
        //        }
        //        highlightControls.Clear();
        //    }    
        //}

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //throw new ArgumentException("The parameter was invalid");
        }

        private void toolStripButtonNonDrawingTag_Click(object sender, EventArgs e)
        {

            //ECompositeViewer.IAdECompositeViewer compvwr = (ECompositeViewer.IAdECompositeViewer)adrContainer.Control.ECompositeViewer;
            //ECompositeViewer.IAdSection sec = (ECompositeViewer.IAdSection)compvwr.Section;

            //_LocalCompositeViewer = (ECompositeViewer.IAdECompositeViewer)adrContainer.Control.ECompositeViewer;

            if (_LocalIAdSection == null)
            {
                MessageBox.Show("No project is open.");
            }
            else
            {
                try
                {

                    _LocalIAdSection = _LocalCompositeViewer.Section;
                    _LocalContent = _LocalIAdSection.Content;
                    _LocalContent2 = _LocalIAdSection.Content;
                    _LocalContent3 = _LocalIAdSection.Content;
                    _LocalIAdCollection = _LocalContent.get_Objects(1); //for selected object
                    _LocalIAdObject = (IAdObject)_LocalIAdCollection[1];

                    string id = "";
                    string tag = "";
                    string description = "";
                    string str = "";
                    string stream = "";
                    string size = "";
                    string drawing = _LocalIAdSection.Title;

                    if (_LocalIAdCollection.Count > 0)
                    {

                        foreach (AdCommon.IAdProperty prop in (IAdCollection)_LocalIAdObject.Properties)
                        {
                            if (prop.Name.ToLower() == "tag") tag = Globals.textCleaner(prop.Value);
                            if (prop.Name.ToLower() == "description") description = Globals.textCleaner(prop.Value);
                            if (prop.Name.ToLower() == "stream") stream = Globals.textCleaner(prop.Value);
                            if (prop.Name.ToLower() == "size") size = Globals.textCleaner(prop.Value);
                            str = str + "Name: " + prop.Name + " Value: " + prop.Value + Environment.NewLine;
                        }

                        DialogResult dr = MessageBox.Show("An object (" + description + ", Tag: " + tag + ") is selected.  Document it instead?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);

                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        {
                            toolStripButtonTag_Click(this, null);
                        }
                        else
                        {
                            showNonDrawingTagDialog(_LocalIAdSection.Title);
                        }
                    }
                    else
                    {
                        showNonDrawingTagDialog(_LocalIAdSection.Title);
                    }
                }
                catch
                {
                    showNonDrawingTagDialog("");
                }
            }
        }

        private void showNonDrawingTagDialog(string title)
        {
            string drawing = title;
            //FormEditTag eo = new FormEditTag(GetFormEditPosition());
            Point pos = new Point(panelMain.Top, this.panelMain.Width - _editTag.Width - 100);
            if (Globals.DialogLocations.ContainsKey(_editTag.Name))
            {
                pos = Globals.DialogLocations[_editTag.Name];
            }

            //TODO: separate form for shell norco, this should be dynamic!

            if (string.IsNullOrEmpty(Properties.Settings.Default.TargetSite) || Properties.Settings.Default.TargetSite.Contains("Default"))
            {
                //if we're here, use a form that's appropriate to the project
                if (Globals.CurrentProjectData.LDARDatabaseType == "Guideware")
                {
                    if (Globals.isProductChinese)
                    {
                        _editTag_GW_Chinese._allowOldTagRefresh = true;
                        _editTag_GW_Chinese.SetComponent(Guid.NewGuid().ToString(), null, null, null, drawing, null, false);
                        _editTag_GW_Chinese.Show();
                    }
                    else
                    {
                        _editTag_GW_Full._allowOldTagRefresh = true;
                        _editTag_GW_Full.SetComponent(Guid.NewGuid().ToString(), null, null, null, drawing, null, false);
                        _editTag_GW_Full.Show();
                    }
                }
                else
                {
                    if (Globals.CurrentProjectData.ProjectType == LDARProjectType.EiMOC)
                    {
                        _editTag_Full.SetComponent(Guid.NewGuid().ToString(), null, null, null, drawing, null, false);
                        _editTag_Full.Show();
                    }
                    else
                    {
                        _editTag._allowOldTagRefresh = false;
                        _editTag.SetComponent(Guid.NewGuid().ToString(), null, null, null, drawing, null, false);
                        _editTag.Show();
                    }
                }
            }
            else
            {
                //if we're here, a form override has been specified - use that form instead
                switch (Properties.Settings.Default.TargetSite)
                {
                    case "TAG":
                        _editTag._allowOldTagRefresh = false;
                        _editTag.SetComponent(Guid.NewGuid().ToString(), null, null, null, drawing, null, false);
                        _editTag.Show();
                        break;
                    case "MGV":
                        _editTag_Garyville.SetComponent(Guid.NewGuid().ToString(), null, null, null, drawing, null, false);
                        _editTag_Garyville.Show();
                        break;
                    case "MOC":
                        _editTag_Full.SetComponent(Guid.NewGuid().ToString(), null, null, null, drawing, null, false);
                        _editTag_Full.Show();
                        break;
                    case "GW":
                        _editTag_GW_Full._allowOldTagRefresh = true;
                        _editTag_GW_Full.SetComponent(Guid.NewGuid().ToString(), null, null, null, drawing, null, false);
                        _editTag_GW_Full.Show();
                        break;
                    case "GW_REVAL":
                        _editTag_GW._allowOldTagRefresh = true;
                        _editTag_GW.SetComponent(Guid.NewGuid().ToString(), null, null, null, drawing, null, false);
                        _editTag_GW.Show();
                        break;
                    case "REVAL":
                        _editTag_GW._allowOldTagRefresh = true;
                        _editTag_GW.SetComponent(Guid.NewGuid().ToString(), null, null, null, drawing, null, false);
                        _editTag_GW.Show();
                        break;
                    default:
                        _editTag._allowOldTagRefresh = false;
                        _editTag.SetComponent(Guid.NewGuid().ToString(), null, null, null, drawing, null, false);
                        _editTag.Show();
                        break;
                }
            }

        }



        private void toolStripButtonSettings_Click(object sender, EventArgs e)
        {
            DialogSettings ds = new DialogSettings(this);
            ds.ShowDialog();

            if (ds.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                timerAutoSave.Interval = Properties.Settings.Default.AutoSaveInterval;
            }
        }

        private void toolStripButtonViewTags_Click(object sender, EventArgs e)
        {
            DialogViewTags dv = new DialogViewTags();
            dv.Tag = this;
            dv.ShowDialog();
        }

        private void toolStripButtonOldTags_Click(object sender, EventArgs e)
        {
            FormOldTags ot = new FormOldTags(this);
            ot.ShowDialog();
        }

        private void toolStripButtonACADSelect_Click(object sender, EventArgs e)
        {
            try
            {
                adrContainer.Control.ExecuteCommand("SELECT");
                toolStripButtonACADPan.Checked = false;
                toolStripButtonACADExtents.Checked = false;
                toolStripButtonACADZoomWindow.Checked = false;
                toolStripButtonACADFreehand.Checked = false;
                toolStripButtonDrawHeavy.Checked = false;
            }
            catch { }
        }

        private void toolStripButtonACADPan_Click(object sender, EventArgs e)
        {
            try
            {
                adrContainer.Control.ExecuteCommand("PAN");
                toolStripButtonACADSelect.Checked = false;
                toolStripButtonACADExtents.Checked = false;
                toolStripButtonACADZoomWindow.Checked = false;
                toolStripButtonACADFreehand.Checked = false;
                toolStripButtonDrawHeavy.Checked = false;
            }
            catch { }
        }

        private void toolStripButtonACADExtents_Click(object sender, EventArgs e)
        {
            try
            {
                adrContainer.Control.ExecuteCommand("FITTOWINDOW");
                toolStripButtonACADSelect.Checked = false;
                toolStripButtonACADPan.Checked = false;
                toolStripButtonACADZoomWindow.Checked = false;
                toolStripButtonACADFreehand.Checked = false;
                toolStripButtonDrawHeavy.Checked = false;
            }
            catch { }
        }

        private void toolStripButtonACADZoomWindow_Click(object sender, EventArgs e)
        {
            try
            {
                adrContainer.Control.ExecuteCommand("ZOOMRECT");
                toolStripButtonACADSelect.Checked = false;
                toolStripButtonACADPan.Checked = false;
                toolStripButtonACADExtents.Checked = false;
                toolStripButtonACADFreehand.Checked = false;
                toolStripButtonDrawHeavy.Checked = false;
            }
            catch { }
        }

        private void toolStripButtonACADPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                adrContainer.Control.ExecuteCommand("PREVIOUS");
            }
            catch { }
        }

        private void toolStripButtonACADNext_Click(object sender, EventArgs e)
        {
            try
            {
                adrContainer.Control.ExecuteCommand("NEXT");
            }
            catch { }
        }

        private void toolStripButtonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                adrContainer.Control.ExecuteCommand("SEARCHBUTTON");
            }
            catch { }
        }

        private void toolStripButtonACADFreehand_Click(object sender, EventArgs e)
        {
            try
            {
                adrContainer.Control.ExecuteCommand("SHAPETOOLS_FREEHAND");
                adrContainer.Control.ExecuteCommand("LINEWEIGHT_ITEM_1_00");
                toolStripButtonACADSelect.Checked = false;
                toolStripButtonACADPan.Checked = false;
                toolStripButtonACADExtents.Checked = false;
                toolStripButtonACADZoomWindow.Checked = false;
                toolStripButtonDrawHeavy.Checked = false;
                _projectDirty = true;
            }
            catch { }
        }

        private void toolStripButtonACADPalettes_Click(object sender, EventArgs e)
        {
            //hide all palettes
            //_LocalCompositeViewer = (ECompositeViewer.IAdECompositeViewer)adrContainer.Control.ECompositeViewer;
            dynamic commands;

            try
            {
                commands = _LocalCompositeViewer.Commands;
                _LocalCompositeViewer.NavigationBarVisible = true;
                _LocalCompositeViewer.ToolbarVisible = true;
            }
            catch 
            {
                return;
            }

            try { commands("DATABAND").Toggled = false; }
            catch { }
            try { commands("SEARCHBAND").Toggled = false; }
            catch { }
            try { commands("VIEWSBAND").Toggled = false; }
            catch { }
            try { commands("TEXTBAND").Toggled = false; }
            catch { }
            try { commands("MODELBAND").Toggled = false; }
            catch { }
            try { commands("MARKUPPROPERTIESBAND").Toggled = false; }
            catch { }
            try { commands("MARKUPSBAND").Toggled = false; }
            catch { }
            try { commands("LISTVIEWBAND").Toggled = false; }
            catch { }
            try { commands("LAYERSBAND").Toggled = false; }
            catch { }
            try { commands("SECTIONPROPERTIESBAND").Toggled = false; }
            catch { }
            try { commands("OBJECTPROPERTIESBAND").Toggled = false; }
            catch { }
            try { commands("THUMBNAILSBAND").Toggled = false; }
            catch { }
        }

        private void setMode()
        {

            //make sure project type is set correctly
            if (System.IO.Path.GetExtension(Globals.CurrentProjectData.CADProjectPath).ToLower() == ".pdf")
            {
                Globals.CurrentProjectData.ProjectType = LDARProjectType.EiMOC;
            }
            
            if (Globals.CurrentProjectData.ProjectType == LDARProjectType.EiMOC)
            {
                toolStripButtonTag.Visible = false;
            }
            else
            {
                toolStripButtonTag.Visible = true;
            }

        }

        private void setFormTitle(string fName)
        {
            string formTitle = string.Empty;

            //if (Globals.CurrentProjectData != null)
            //{
            //    if (Globals.CurrentProjectData.ProjectType == LDARProjectType.EiMOC)
            //        formTitle = formTitle + "EiMOC";
            //    else
            //        formTitle = formTitle + "FieldTech Toolbox";
            //}
            //else
            //{
                if (Properties.Settings.Default.MOCMode)
                {
                    formTitle = formTitle + "EiMOC";
                    toolStripButtonTag.Visible = false;
                }
                else
                {
                    formTitle = formTitle + "FieldTech Toolbox";
                    toolStripButtonTag.Visible = true;
                }
            //}


            //odd minor revisions will display development version
            //if (Globals.isDevelopmentVersion()) formTitle = formTitle + " (beta)";

            formTitle = formTitle + " " + Globals.GetCurrentAssemblyVersion();

            if (fName != string.Empty) formTitle = formTitle + " - " + System.IO.Path.GetFileName(fName);

            this.Text = formTitle;
            _formTitle = formTitle;

        }

        private void toolStripButtonHelp_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Path.GetDirectoryName(Application.ExecutablePath) + "\\FieldTechToolbox_User_Manual.docx"); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open help file" + ex.Message);
            }
        }

        private void toolStripButtonACADRotate_Click(object sender, EventArgs e)
        {

            FormRotateOption ro = new FormRotateOption();
            ro.ShowDialog();

            try
            {
                if (ro.Tag.ToString() == "Left")
                    adrContainer.Control.ExecuteCommand("ROTATE_90");
                else
                    adrContainer.Control.ExecuteCommand("ROTATE_270");
            }
            catch { }
                      
            toolStripButtonACADSelect.Checked = false;
            toolStripButtonACADPan.Checked = false;
            toolStripButtonACADExtents.Checked = false;
        }

        private void toolStripLabelDrawingNumber_Click(object sender, EventArgs e)
        {
            adrContainer.Control.ExecuteCommand("LISTVIEWBAND");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            //make sure the file is completely loaded before we allow form to close
            if (_eidLoading)
            {
                e.Cancel = true;
                return;
            }
            
            //give a little time in case we need it for file save to complete.
            int saveSleeper = 0;
            while (saveSleeper < 10 && _eidSaving)
            {
                System.Threading.Thread.Sleep(100);
                saveSleeper++;
            }
            
            timerAutoSave.Enabled = false;
            if (adrContainer.Control.SourcePath != null) saveCurrentDrawing();
            if (_projectDirty)
            {
                DialogResult dr = MessageBox.Show("Save project?", "Save", MessageBoxButtons.YesNo);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    saveEid(Properties.Settings.Default.LastEiD);
                }
                else
                {
                    //it's not dirty anymore if they didn't elect to save
                    _projectDirty = false;
                    _lastSaveSuccess = true;
                }
            }
            adrContainer.Control.SourcePath = "";
            //only clean working directory if original source exists and last save was successful
            if (System.IO.File.Exists(Properties.Settings.Default.LastEiD) && _lastSaveSuccess)
            {
                Globals.cleanWorkingDirectory();
            }
        }

        private void toolStripButtonDrawHeavy_Click(object sender, EventArgs e)
        {
            try
            {
                adrContainer.Control.ExecuteCommand("SHAPETOOLS_FREEHAND_HIGHLIGHTER");
                adrContainer.Control.ExecuteCommand("LINEWEIGHT_ITEM_20_00");
                _projectDirty = true;
                toolStripButtonACADSelect.Checked = false;
                toolStripButtonACADPan.Checked = false;
                toolStripButtonACADExtents.Checked = false;
                toolStripButtonACADZoomWindow.Checked = false;
                toolStripButtonACADFreehand.Checked = false;
            }
            catch { }
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            if (Globals.CurrentProjectData != null)
            {
                saveEid(Properties.Settings.Default.LastEiD);
            }
            else
            {
                MessageBox.Show("No project is open!");
            }
        }

        private void timerAutoSave_Tick(object sender, EventArgs e)
        {
            if (Globals.CurrentProjectData != null)
            {
                try
                {
                    //we have to disable the timer so that it doesn't clobber itself if the user does not respond right away
                    if (!_eidLoading && !_eidSaving && _lastSaveSuccess)
                    {
                        saveEid(Properties.Settings.Default.LastEiD, true);
                    }
                    else
                    {
                        timerAutoSave.Enabled = false; 
                    }
                }
                catch { }
            }
        }

        private bool checkForCleanExit()
        {

            int fileCount = 0;
            bool silentRecovery = false;

            try
            {
                fileCount = System.IO.Directory.GetFiles(Globals.WorkingFolder).Where(c => c.Contains("json") || c.Contains("dwf") || c.Contains("csv")).FirstOrDefault().Count();
            }
            catch { }
            
            if (fileCount > 0)
            {
                Globals.LogError("File recovery initiated", "File Recovery", "");
                //attempt silent recovery
                if (File.Exists(Globals.WorkingFolder + "\\lastfile.txt"))
                {

                    try
                    {
                        Properties.Settings.Default.LastEiD = File.ReadAllText(Globals.WorkingFolder + "\\lastfile.txt");
                        Properties.Settings.Default.Save();
                        LoadEiD(Properties.Settings.Default.LastEiD, true);
                        saveEid(Properties.Settings.Default.LastEiD);
                        silentRecovery = true;
                    }
                    catch (Exception e2)
                    {
                        Globals.LogError("Silent recovery failed! " + e2.Message , e2.Source, e2.StackTrace);
                    }
                }
                if (!silentRecovery)
                {
                    DialogResult dr = MessageBox.Show("FieldTech Toolbox did not exit cleanly the last time it was used, try to reload data from previous project?  If you select 'No' you risk losing data that has been collected!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        _projectDirty = false;
                        MessageBox.Show("Please select an .eid file to save the recovered data to");
                        SaveFileDialog fd = new SaveFileDialog();
                        fd.Filter = "FieldTech Data files (*.eid)|*.eid|All files (*.*)|*.*";
                        fd.FilterIndex = 1;
                        DialogResult saveResult = fd.ShowDialog();
                        if (saveResult == System.Windows.Forms.DialogResult.Cancel)
                        {
                            MessageBox.Show("No .eid file selected to recover to.  FieldTech Toolbox will now close");
                            _lastSaveSuccess = false;
                            Application.Exit();
                        }
                        Properties.Settings.Default.LastEiD = fd.FileName;
                        Properties.Settings.Default.Save();
                        LoadEiD(Properties.Settings.Default.LastEiD, true);
                        saveEid(Properties.Settings.Default.LastEiD);
                    }
                    else
                    {
                        MessageBox.Show("No project is loaded, FieldTech Toolbox will now close");
                        Globals.cleanWorkingDirectory();
                        _lastSaveSuccess = false;
                        Properties.Settings.Default.LastEiD = "";
                        Properties.Settings.Default.Save();
                        Application.Exit();
                    }
                }
            }
            return true;
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {

            // Note: for an application hook, use the AppHooker class instead
            _mouseListener = new MouseHookListener(new AppHooker());

            // The listener is not enabled by default
            _mouseListener.Enabled = true;

            // Set the event handler
            // recommended to use the Extended handlers, which allow input suppression among other additional information
            _mouseListener.MouseDownExt += MouseListener_MouseDownExt;

            forbiddenZoneWorker.DoWork += new DoWorkEventHandler(forbiddenZoneWorker_DoWork);
        }

        void forbiddenZoneWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            FindForbiddenZones();
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            _mouseListener.Dispose();
        }


        private void MouseListener_MouseDownExt(object sender, MouseEventExtArgs e)
        {
            //here we're trapping the mouse down event so we can figure out if a user is trying to click "forbidden buttons" on the adr control

            int relativeX = e.X - this.Location.X;
            int relativeY = e.Y - this.Location.Y;

            if (forbiddenZones.Count > 0)
            {
                foreach (Rectangle rect in forbiddenZones)
                {
                    if (relativeX >= rect.X && relativeX <= rect.X + 60)
                    {
                        if (relativeY >= rect.Y && relativeY <= rect.Y + 20)
                        {
                            e.Handled = true;
                            Console.WriteLine("Click cancelled");
                            toolStripButtonACADPalettes_Click(this, null);
                        }
                    }
                }
            }

            return;

            if (_LocalIAdSection == null)
            {
                //skip if nothing is loaded, it fouls up our ADR control
                return;
            }

            if (_doubleClickRegistered)
            {
                //also skip if someone double-clicked an object
                return;
            }

            if (_freehandActive) return;

            Console.WriteLine(_freehandActive.ToString());
            Console.WriteLine(string.Format("MouseDown: \t{0}; \t System Timestamp: \t{1}", e.Button, e.Timestamp));

            int x = e.X;
            int y = e.Y;

            Console.WriteLine("Clicked: " + x.ToString() + ":" + y.ToString() + " - Window Parms: " + _LocalCompositeViewer._ClientWindow.ToString());

            if (x < 50 || y < 20)
            {
                return;
            }

            Bitmap screenPart = Globals.GetScreenImage(new Point(x - 70, y - 30));

            Rectangle searchRect = Globals.searchBitmap(_paletteControlsImage, screenPart, 0);
            Rectangle searchRect2 = Globals.searchBitmap(_paletteControlsImage2, screenPart, 0);

            Rectangle searchRectX = Globals.searchBitmap(_paletteControlsImageX, screenPart, 0);
            Rectangle searchRectPin = Globals.searchBitmap(_paletteControlsImagePin, screenPart, 0);
            Rectangle searchRectContext = Globals.searchBitmap(_paletteControlsImageContext, screenPart, 0);
            Rectangle searchRectContext2 = Globals.searchBitmap(_paletteControlsImageContext2, screenPart, 0);
            Rectangle searchRectPin2 = Globals.searchBitmap(_paletteControlsImagePin2, screenPart, 0);


            Clipboard.SetImage(screenPart);


            if (searchRect.X > 1 || searchRect2.X > 1)
            {
                Console.WriteLine("Found match!");
                e.Handled = true;
                toolStripButtonACADPalettes_Click(this, null);
            }

            int iconSearchResults = 0;

            if (searchRectX.X > 1) iconSearchResults += 1;
            if (searchRectPin.X > 1) iconSearchResults += 1;
            if (searchRectContext.X > 1) iconSearchResults += 1;
            if (searchRectContext2.X > 1) iconSearchResults += 1;
            if (searchRectPin2.X > 1) iconSearchResults += 1;


            if (iconSearchResults > 1)
            {
                Console.WriteLine("Near Found match!");
                toolStripButtonACADPalettes_Click(this, null);
                e.Handled = true;
            }
        }

        private void FindForbiddenZones()
        {

            try
            {
                Console.WriteLine("Finding forbidden zones");

                forbiddenZones.Clear();
                foreach (Control c in adrContainer.Control.Controls)
                {
                    if (c.Name.StartsWith("hp")) c.Dispose();
                }

                Bitmap screenPart = Globals.GetWholeScreenImage(this.Location, this.Size);

                forbiddenZones.AddRange(Globals.searchBitmapAll(_paletteControlsImageX, screenPart, 0));
                forbiddenZones.AddRange(Globals.searchBitmapAll(_paletteControlsImagePin, screenPart, 0));
                forbiddenZones.AddRange(Globals.searchBitmapAll(_paletteControlsImage, screenPart, 0));
                forbiddenZones.AddRange(Globals.searchBitmapAll(_paletteControlsImage2, screenPart, 0));
                forbiddenZones.AddRange(Globals.searchBitmapAll(_paletteControlsImage3, screenPart, 0));
                forbiddenZones.AddRange(Globals.searchBitmapAll(_paletteControlsImage4, screenPart, 0));

                //forbiddenZones.Add(Globals.findLeftExclusionZone(screenPart));
               
                //foreach (Rectangle rct in forbiddenZones)
                //{
                //    Panel newPanel = new Panel();
                //    newPanel.BackColor = Color.Transparent;
                //    newPanel.Size = new Size(75, 1000);
                //    newPanel.Name = "hp_" + Guid.NewGuid().ToString();
                //    newPanel.Location = new Point(rct.X - 55, adrContainer.Control.Location.Y);
                //    newPanel.Parent = this.adrContainer.Control;
                //    adrContainer.Control.Refresh();
                //}

                Console.WriteLine("Finding forbidden zones done");

                Clipboard.SetImage(screenPart);
            }
            catch { }

        }

        private void timerMarkupDelay_Tick(object sender, EventArgs e)
        {
            
            if (_drawingSecondsElapsed > 5 && !_mouseButtonDown)
            {
                saveNotification(true);
                saveCurrentDrawing();
                _drawingSecondsElapsed = 0;
                timerMarkupDelay.Enabled = false;
                saveNotification(false);
            }
            
            if (_drawingSecondsElapsed < Properties.Settings.Default.AutoSaveInterval)
            {
                _drawingSecondsElapsed += 1;
            }
            else
            {
                _drawingSecondsElapsed = 0;
                if (!_freehandActive) timerMarkupDelay.Enabled = false;
            }
        }

        private void saveNotification(bool enableNotify)
        {
            if (enableNotify)
            {
                this.Text = _formTitle + " (Saving...)";
            }
            else
            {
                this.Text = _formTitle;
            }
 
        }

        private void toolStripComponentCounter_Click(object sender, EventArgs e)
        {
            //LoadComponentLists();
        }

        private void checkZeroSettings()
        {
            //this function handles the case where the padded zeros/child start at values aren't set in the project
            
            if (!Globals.CurrentProjectData.ZeroStatusSet)
            {
                if (Globals.CurrentProjectData.LDARDatabaseName.Contains("LeakDAS"))
                {
                    Globals.CurrentProjectData.LDARTagPaddedZeros = 0;
                    Globals.CurrentProjectData.LDARTagStartChildrenNumber = 1;
                    Globals.CurrentProjectData.ZeroStatusSet = true;
                }
                else
                {
                    Globals.CurrentProjectData.LDARTagPaddedZeros = 3;
                    Globals.CurrentProjectData.LDARTagStartChildrenNumber = 1;
                    Globals.CurrentProjectData.ZeroStatusSet = true;
                }
            }

            LocalData.RouteAddNo = (double)1 / Math.Pow(10, Globals.CurrentProjectData.LDARRoutePaddedZeros);
        }

        private void MainForm_LocationChanged(object sender, EventArgs e)
        {
            //FindForbiddenZones();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            //FindForbiddenZones();
        }

        public string isObjectSelected()
        {

            //checks for object selected in ADR, return string value representing the object or empty string if nothing selected.

            if (_LocalIAdSection == null)
            {
                MessageBox.Show("No project is open.");
            }
            else
            {
                try
                {

                    _LocalIAdSection = _LocalCompositeViewer.Section;
                    _LocalContent = _LocalIAdSection.Content;
                    _LocalContent2 = _LocalIAdSection.Content;
                    _LocalContent3 = _LocalIAdSection.Content;
                    _LocalIAdCollection = _LocalContent.get_Objects(1); //for selected object
                    _LocalIAdObject = (IAdObject)_LocalIAdCollection[1];

                    string id = "";
                    string tag = "";
                    string description = "";
                    string str = "";
                    string stream = "";
                    string size = "";
                    string drawing = _LocalIAdSection.Title;

                    if (_LocalIAdCollection.Count > 0)
                    {

                        foreach (AdCommon.IAdProperty prop in (IAdCollection)_LocalIAdObject.Properties)
                        {
                            if (prop.Name.ToLower() == "tag") tag = Globals.textCleaner(prop.Value);
                            if (prop.Name.ToLower() == "description") description = Globals.textCleaner(prop.Value);
                            if (prop.Name.ToLower() == "stream") stream = Globals.textCleaner(prop.Value);
                            if (prop.Name.ToLower() == "size") size = Globals.textCleaner(prop.Value);
                            str = str + "Name: " + prop.Name + " Value: " + prop.Value + Environment.NewLine;
                        }

                        return tag + '\t' + description + '\t' + stream + '\t' + size;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                catch
                {
                    return string.Empty;
                }
            }

            return string.Empty;
        }
    }
}
