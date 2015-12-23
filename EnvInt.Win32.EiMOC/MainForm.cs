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

using AdCommon;
using AxExpressViewerDll;
//using EPlotRenderer;

using EnvInt.Win32.FieldTech.Containers;
using EnvInt.Win32.EiMOC.Data;
using EnvInt.Win32.EiMOC.Library;
using Ionic.Zip;

namespace EnvInt.Win32.EiMOC
{
  public partial class MainForm : Form
  {

      private List<Control> highlightControls = new List<Control>();

    public MainForm()
      {
          InitializeComponent();
          adrContainer.Control.OnSelectObjectEx += axCExpressViewerControl1_OnSelectObjectEx;
          adrContainer.Control.OnSelectObject += axCExpressViewerControl1_OnSelectObject;
          adrContainer.Control.OnExecuteCommandEx += axCExpressViewerControl1_OnExecuteCommandEx;
          adrContainer.Control.OnExecuteURL += new IAdViewerEvents_OnExecuteURLEventHandler(axCExpressViewerControl1_OnExecuteURL);
          adrContainer.Control.OnMButtonDblClick += new IAdViewerEvents_OnMButtonDblClickEventHandler(axCExpressViewerControl1_OnMButtonDblClick);

          timerSplash.Enabled = true;
          UpdateLocalComponentCount();

          Text = "EiMOC - " + Globals.GetCurrentAssemblyVersion();
          

      }

      private void axCExpressViewerControl1_OnMButtonDblClick(object sender, AxExpressViewerDll.IAdViewerEvents_OnMButtonDblClickEvent e)
      { 
          string strResult = ""; 
          //First parameter : nX 
          int intX = e.nX; 
          strResult = intX.ToString (); 

          //Second parameter : nY 
          int intY = e.nY; 
          strResult = intY.ToString (); 

          //Third parameter : pHandled 
          AdCommon.IAdToggle pHandled = (AdCommon.IAdToggle) e.pHandled; 
          if (pHandled != null)  
          { 
              pHandled.State = true; 
              strResult = "No effect.  Middle button is used for zooming"; 

          }
      }

    private void axCExpressViewerControl1_OnExecuteURL(object sender, AxExpressViewerDll.IAdViewerEvents_OnExecuteURLEvent e)
    {
        string strResult = "";
        //First parameter : piAdPageLink 
        IAdPageLink mv_Link = (IAdPageLink)e.pIAdPageLink;
        if (mv_Link != null)
        {
            strResult = "The DWF file contains " + mv_Link.Count.ToString() + " hyperlink(s)";
            //lstLog.Items.Insert(0, strResult);
        }
        //Second parameter : nIndex  
        int intIdx = e.nIndex;
        strResult = "Index is " + intIdx.ToString();
        //lstLog.Items.Insert(0, strResult);
        //Third parameter : pHandled 
        AdCommon.IAdToggle pHandled = (AdCommon.IAdToggle)e.pHandled;
        if (pHandled != null)
        {
            pHandled.State = true;
            strResult = "URL shouldn't execute when invoked";
            //lstLog.Items.Insert(0, strResult);
        }
    }



    private void timerSplash_Tick(object sender, EventArgs e)
    {
        timerSplash.Enabled = false;

        //TODO: This needs to handle manager project files also

        bool openLastProject = Properties.Settings.Default.OpenLastProject;
        if (openLastProject)
        {
            string lastFTTD = Properties.Settings.Default.LastFTTD;

            if (String.IsNullOrEmpty(lastFTTD))
            {
                if (System.IO.File.Exists("SampleProject.fttd"))
                {
                    LoadFTTD(System.IO.Path.GetFullPath("SampleProject.fttd"));
                }
            }
            else
            {
                if (System.IO.File.Exists(lastFTTD))
                {
                    LoadFTTD(System.IO.Path.GetFullPath(lastFTTD));
                }
            }
        }

        //make sure we have a valid device identifier, if not, autocreate one
        if (String.IsNullOrEmpty(Properties.Settings.Default.DeviceIdentifier))
        {
            Properties.Settings.Default.DeviceIdentifier = "SP_" + DateTime.Now.Year.ToString("yy") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + "_" + DateTime.Now.Millisecond.ToString("000");
            Properties.Settings.Default.Save();
        }

        if (Properties.Settings.Default.ShowSplash)
        {
            Splash s = new Splash();
            DialogResult dr = s.ShowDialog();
        }
    }

    //private void LoadComponentLists()
    //{
    //    Globals.ProjectComponents.Clear();
    //    Globals.ProjectComponentTypes.Clear();

    //    ECompositeViewer.IAdECompositeViewer compositeViewer;
    //    AdCommon.IAdCollection sections;
    //    ECompositeViewer.IAdSection currentSection;

    //    compositeViewer = (ECompositeViewer.IAdECompositeViewer)adrContainer.Control.ECompositeViewer;
    //    sections = (AdCommon.IAdCollection) compositeViewer.Sections;

    //    uint nRed = 255, nGreen = 0, nBlue = 0;
    //    compositeViewer.ObjectSelectionColor = 65536 * nBlue + 256 * nGreen + nRed;
        
        //foreach (ECompositeViewer.IAdSection Section in sections)
        //{ 
        //    //Set section 
        //    compositeViewer.Section = Section;  
        //    //Get section 
        //    currentSection = (ECompositeViewer.IAdSection) compositeViewer.Section;

        //    ECompositeViewer.IAdContent ObjectContent = (ECompositeViewer.IAdContent)Section.Content;

        //    //Create a user collection of all objects.  Collection as such is not named or unnamed, it depends on whether the
        //    //items added into the user collection are named or unnamed.
        //    AdCommon.IAdUserCollection MyObjectsNamedCollection = (AdCommon.IAdUserCollection)ObjectContent.CreateUserCollection(); 
        //    //Get all the objects in the current section
        //    AdCommon.IAdCollection MyObjects = (AdCommon.IAdCollection)ObjectContent.get_Objects(0);

        //    //0 is to return all objects 
        //    foreach (ECompositeViewer.IAdObject MyObject in MyObjects)
        //    {
        //        AdCommon.IAdCollection MyObjectProperties = (AdCommon.IAdCollection)MyObject.Properties;	
        //        foreach (AdCommon.IAdProperty MyObjectProperty in MyObjectProperties)	
        //        {
        //            //IntPtr hDC = PlatformInvokeUSER32.GetWindowDC(MyObject.Handle.ToInt32()); 
        //            //CompositeViewer.DrawToDC(hDC.ToInt32(), 10, 0, 250, 250);

            
        //            if ( (MyObjectProperty.Name.ToLower() == "tag")  )		
        //            {
        //                string componentTag = MyObjectProperty.Value;
        //                if (!Globals.ProjectComponents.Contains(componentTag))
        //                {
        //                    Globals.ProjectComponents.Add(componentTag);
        //                }
        //            }

        //            if ((MyObjectProperty.Name.ToLower() == "description"))
        //            {
        //                string componentType = MyObjectProperty.Value;
        //                if (!Globals.ProjectComponentTypes.Contains(componentType))
        //                {
        //                    Globals.ProjectComponentTypes.Add(componentType);
        //                }
        //            }
        //        }
        //    }
        //}
    //    Globals.ProjectComponents = Globals.ProjectComponents.OrderBy(c => c).ToList();
    //}

    
    private void axCExpressViewerControl1_OnSelectObject(object sender, AxExpressViewerDll.IAdViewerEvents_OnSelectObjectEvent e)
    { 
        string strResult = ""; 
        //First parameter : piAdPageObjectNode 
        IAdPageObjectNode objPageObjectNode = (IAdPageObjectNode) e.pIAdPageObjectNode; 
        if (objPageObjectNode != null)  
        { 
            strResult = "Object node name is " + objPageObjectNode.Name;
            //MessageBox.Show(strResult);
        }
    }

    private void axCExpressViewerControl1_OnExecuteCommandEx(object sender, IAdViewerEvents_OnExecuteCommandExEvent e)
    {
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
                //MessageBox.Show(e.bstrItemType);
                break;
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
        ECompositeViewer.IAdECompositeViewer compositeViewer = (ECompositeViewer.IAdECompositeViewer)adrContainer.Control.ECompositeViewer;
        AdCommon.IAdCollection sections = (AdCommon.IAdCollection)compositeViewer.Sections;
        AdCommon.IAdSection section = (AdCommon.IAdSection)compositeViewer.Section;
        if (section != null)
        {
            int index = ((int)section.Order) + offset;
            toolStripLabelDrawingNumber.Text = index.ToString() + " of " + sections.Count.ToString();

            if (index >= sections.Count) return 1;
            else if (index <= 1) return -1;
        }
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

    //private void DWF_Property_Click(object sender, EventArgs e)
    //{
      
    //  ECompositeViewer.IAdECompositeViewer CompositeViewer;
    //  AdCommon.IAdCollection Sections;

    //  CompositeViewer = (ECompositeViewer.IAdECompositeViewer)axCExpressViewerControl1.ECompositeViewer;
    //  Sections = (AdCommon.IAdCollection)CompositeViewer.Sections;
    //  MessageBox.Show("Pages Count : " + Sections.Count);

    //  //Loop through Sections Collection using foreach
    //  foreach (ECompositeViewer.IAdSection Section in Sections)
    //  {        
    //    MessageBox.Show(Section.Title);
    //    MessageBox.Show("Order of the section \"" + Section.Title + "\" is " + Section.Order);
    //  }
    //}

    //private void button_hideObj_Click(object sender, EventArgs e)
    //{
    //  try
    //  {
    //    ECompositeViewer.IAdECompositeViewer compvwr = (ECompositeViewer.IAdECompositeViewer)axCExpressViewerControl1.ECompositeViewer;

    //    ECompositeViewer.IAdSection sec = (ECompositeViewer.IAdSection)compvwr.Section;
    //    ECompositeViewer.IAdContent con = (ECompositeViewer.IAdContent)sec.Content;

    //    AdCommon.IAdCollection adobj = (AdCommon.IAdCollection)con.get_Objects(1); //for selected object
    //    AdCommon.IAdObject obj = (AdCommon.IAdObject)adobj[1];

    //    compvwr.ExecuteCommand("HIDE");        
    //  }
    //  catch
    //  {
    //    // TODO: Add your error handling here.
    //  }
    //}

    //private void objCountBtn_Click(object sender, EventArgs e)
    //{
    //  try
    //  {
    //    ECompositeViewer.IAdECompositeViewer compvwr = (ECompositeViewer.IAdECompositeViewer)axCExpressViewerControl1.ECompositeViewer;

    //    EPlotViewer.IAdEPlotViewer2 docHand = compvwr.DocumentHandler;
    //    Object objNodes = docHand.ObjectNodes;

    //    //String msgString = " ";
    //    //msgString = msgString + "Object Count : " + objNodes.count().ToString();

    //    // This section is related to Blog post -
    //    // 
        
    //    ECompositeViewer.IAdECompositeViewer CompositeViewer;
    //    EPlotViewer.IAdEPlotSection PlotSection;
    //    AdCommon.IAdPageView View;
    //    ECompositeViewer.IAdSection SectionChk;
    //    ECompositeViewer.IAdSectionType SectionTypeChk;
    //    CompositeViewer = (ECompositeViewer.IAdECompositeViewer) axCExpressViewerControl1.ECompositeViewer;      
    //    SectionChk = (ECompositeViewer.IAdSection) CompositeViewer.Section;
    //    SectionTypeChk = (ECompositeViewer.IAdSectionType) SectionChk.SectionType;  
        
    //    if (SectionTypeChk.Name == "com.autodesk.dwf.ePlot")
    //     { 
    //      PlotSection = (EPlotViewer.IAdEPlotSection) CompositeViewer.Section;
    //      View = (AdCommon.IAdPageView)PlotSection.View;  
    //      //Set View - interchange Left with Bottom and Top with Right 
    //      PlotSection.SetView (View.Bottom, View.Left, View.Top,  View.Right);
    //    }          

    //  }
    //  catch
    //  {
    //    // TODO: Add your error handling here.
    //  }
    //}

    private void toolStripButtonOpenProject_Click(object sender, EventArgs e)
    {
        OpenFileDialog oFileDialog = new OpenFileDialog();
        oFileDialog.Filter = "FieldTech Data Files|*.fttd|All files (*.*)|*.*";
        oFileDialog.FilterIndex = 1;
        DialogResult dr = oFileDialog.ShowDialog();
        if (dr == System.Windows.Forms.DialogResult.OK)
        {
            string fName = System.IO.Path.GetExtension(oFileDialog.FileName).ToLower();

            switch (fName)
            {
                case ".fttd":
                    LoadFTTD(oFileDialog.FileName);
                    break;
                case ".pdf":
                    //LoadPDF(oFileDialog.FileName);
                    break;
                default:
                    MessageBox.Show("Unknown file type. Cannot open.", "Open Project", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }
    }

    private void LoadFTTD(string fName)
    {
            try
            {
                using (ZipFile zip = ZipFile.Read(fName))
                {
                    ProjectData loadingProjectData = null; 
                    foreach (ZipEntry e in zip)
                    {
                        if (e.FileName.ToLower() == "project.json")
                        {
                            using (var ms = new MemoryStream())
                            {
                                e.Extract(ms);
                                ms.Position = 0;
                                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ProjectData));
                                loadingProjectData = (ProjectData)ser.ReadObject(ms);
                                break;
                            }
                        }
                    }

                    //look for existing working project..
                    string idFile = Globals.WorkingFolder + "\\project.id";
                    if (File.Exists(idFile))
                    {
                        string projectIdString = System.IO.File.ReadAllText(idFile);
                        if (!String.IsNullOrEmpty(projectIdString))
                        {
                            Guid projectId = Guid.Empty;
                            if (Guid.TryParse(projectIdString, out projectId))
                            {
                                if (projectId != loadingProjectData.ProjectId)
                                {
                                    //the user is trying to open a project that is a different ID than what 
                                    //is in the working directory currently.
                                    DialogResult dr = MessageBox.Show("FieldTech has files remaining from a different project. Loading this project will remove all data from the previous project. You should consider exporting the existing project data before opening a new project. \r\n\r\nAre you sure you would like to erase all existing project dat?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                                    if (dr == System.Windows.Forms.DialogResult.No)
                                    {
                                        return;
                                    }
                                    if (dr == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        //delete it to make sure we are clean...
                                        if (System.IO.Directory.Exists(Globals.WorkingFolder))
                                        {
                                            System.IO.Directory.Delete(Globals.WorkingFolder, true);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //if we are here, then the new project to open seems to be the same project as before. This is okay.
                    // 1. reload the FTTP projec
                    // 2. extract the CAD Document again
                    // 3. reload the existing data.csv file if it exists

                    Globals.CurrentProjectData = loadingProjectData;
                    //load any existing component data into the UI
                    LocalData.LoadFromProject(Globals.CurrentProjectData);
                    UpdateLocalComponentCount();
                    Properties.Settings.Default.LastFTTD = fName;
                    Properties.Settings.Default.Save();

                    //create working if it does not exist
                    if (!System.IO.Directory.Exists(Globals.WorkingFolder))
                    {
                        System.IO.Directory.CreateDirectory(Globals.WorkingFolder);
                    }
                    if (!System.IO.Directory.Exists(Globals.WorkingFolder + "\\Images"))
                    {
                        System.IO.Directory.CreateDirectory(Globals.WorkingFolder + "\\Images");
                    }

                    //put a device.id file in working *always*
                    System.IO.File.WriteAllText(Globals.WorkingFolder + "\\device.id", LocalData.DeviceIdentifier);

                    //put a project.id file in working *always*
                    System.IO.File.WriteAllText(Globals.WorkingFolder + "\\project.id", Globals.CurrentProjectData.ProjectId.ToString());

                    ZipEntry projectEntry = zip.Entries.Where(e => e.FileName == Path.GetFileName("project.json")).FirstOrDefault();
                    if (projectEntry != null)
                    {
                        projectEntry.Extract(Globals.WorkingFolder, ExtractExistingFileAction.OverwriteSilently);
                    }

                    ZipEntry drawingEntry = zip.Entries.Where(e => e.FileName == Path.GetFileName(Globals.CurrentProjectData.CADDrawingPackage)).FirstOrDefault();
                    if (drawingEntry != null)
                    {

                        //this is good... save the entry here...
                        drawingEntry.Extract(Globals.WorkingFolder, ExtractExistingFileAction.OverwriteSilently);

                        Globals.CurrentProjectData.CADDrawingPackage = Globals.WorkingFolder + "\\" + drawingEntry.FileName;
                        adrContainer.Control.SourcePath = Globals.CurrentProjectData.CADDrawingPackage;
                        Properties.Settings.Default.LastFTTD = fName;
                        Properties.Settings.Default.Save();
                        Text = "EiMOC - " + System.IO.Path.GetFileNameWithoutExtension(fName);
                        UpdateDrawingIndexLabel(0);
                        toolStripButtonACADPrevious.Enabled = false;
                    }
                }

                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open project: " + ex.Message);
            }
    }

    //private void LoadPDF(string pdfFile)
    //{
    //    Cursor.Current = Cursors.WaitCursor;
    //    try
    //    {
    //        adrContainer.Control.SourcePath = pdfFile;

    //        //load any existing component data into the UI
    //        LocalData.LoadFromProject(pdfFile);
    //        UpdateLocalComponentCount();
    //        LoadComponentLists();

    //        Properties.Settings.Default.LastProject = pdfFile;
    //        Properties.Settings.Default.Save();
    //        Text = "EiMOC - " + pdfFile;
    //        UpdateDrawingIndexLabel(0);
    //        toolStripButtonACADPrevious.Enabled = false;
    //    }
    //    catch (Exception ex)
    //    {
    //        Cursor.Current = Cursors.Default;
    //        MessageBox.Show("There was an error loading this drawing set. Try re-plotting from AutoCAD.");
    //    }
    //    Cursor.Current = Cursors.Default;
    //}
      
    private void toolStripButtonTag_Click(object sender, EventArgs e)
    {
        ECompositeViewer.IAdECompositeViewer compvwr = (ECompositeViewer.IAdECompositeViewer)adrContainer.Control.ECompositeViewer;
        ECompositeViewer.IAdSection sec = (ECompositeViewer.IAdSection)compvwr.Section;

        if (sec == null)
        {
            MessageBox.Show("No project is open.");
        }
        else
        {
            ECompositeViewer.IAdContent con = (ECompositeViewer.IAdContent)sec.Content;
            ECompositeViewer.IAdContent2 con2 = (ECompositeViewer.IAdContent2)sec.Content;
            ECompositeViewer.IAdContent3 con3 = (ECompositeViewer.IAdContent3)sec.Content;
            AdCommon.IAdCollection adobj = (AdCommon.IAdCollection)con.get_Objects(1); //for selected object

            if (adobj.Count > 0)
            {
                AdCommon.IAdObject obj = (AdCommon.IAdObject)adobj[1];

                string id = "";
                string tag = "";
                string description = "";
                string str = "";
                string stream = "";
                string drawing = sec.Title;

                foreach (AdCommon.IAdProperty prop in (AdCommon.IAdCollection)obj.Properties)
                {
                    if (prop.Name.ToLower() == "tag") tag = prop.Value;
                    if (prop.Name.ToLower() == "description") description = prop.Value;
                    if (prop.Name.ToLower() == "stream") stream = prop.Value;
                    str = str + "Name: " + prop.Name + " Value: " + prop.Value + Environment.NewLine;
                }

                AdCommon.IAdCollection obj2 = (AdCommon.IAdCollection) con2.get_Objects(1);
                foreach (ECompositeViewer.IAdObject2 o in obj2)
                {
                    id = o.Id;
                }
                //MessageBox.Show(str);

                AdCommon.IAdCollection obj3 = (AdCommon.IAdCollection)con3.get_Objects(1);


                FormEditObject eo = new FormEditObject(GetFormEditPosition());
                
                eo.SetComponent(id, tag, description, stream, drawing);
                DialogResult dr = eo.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    UpdateLocalComponentCount();

                    if (Globals.DialogLocations.ContainsKey(this.Name))
                    {
                        Globals.DialogLocations[this.Name] = new Point(eo.Top, eo.Left);
                    }
                    else
                    {
                        Globals.DialogLocations.Add(this.Name, new Point(eo.Top, eo.Left));
                    }
                }
            }
            else
            {
                MessageBox.Show("No Object");
            }
        }
    }

    private Point GetFormEditPosition()
    {
        Point pos = new Point(panelMain.Top, this.panelMain.Width - 700);
        if (Globals.DialogLocations.ContainsKey("FormEditObject"))
        {
            pos = Globals.DialogLocations["FormEditObject"];
        }
        return pos;
    }

    private Point GetFormMOCPosition()
    {
        Point pos = new Point(panelMain.Top, this.panelMain.Width - 700);
        if (Globals.DialogLocations.ContainsKey("FormMOC"))
        {
            pos = Globals.DialogLocations["FormMOC"];
        }
        return pos;
    }

    private void toolStripButtonExport_Click(object sender, EventArgs e)
    {
        //export an FTTD file... that will contain the following items..
        // 1. The FTTP Project (for reference)
        // 2. The CSV file of tagging activities
        // 3. The CAD Package File (.pdf or .dwf typically)
        // 4. Any photos taken on this tagging event

        SaveFileDialog fd = new SaveFileDialog();
        //fd.InitialDirectory = System.IO.Path.GetDirectoryName(LocalData.ProjectData);
        fd.Filter = "FieldTech Export files (*.ftte)|*.ftte|All files (*.*)|*.*";
        fd.FilterIndex = 1;
        string baseFileName = LocalData.ProjectData.ProjectName + "_" + LocalData.DeviceIdentifier + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00");
        string finalFileName = baseFileName;

        for (int i = 0; i < 10; i++)
        {
            if (System.IO.File.Exists(finalFileName))
            {
                finalFileName = baseFileName + "_" + i.ToString();
            }
            else
            {
                break;
            }
        }
        fd.FileName = finalFileName;
        DialogResult dr = fd.ShowDialog();

        if (dr == System.Windows.Forms.DialogResult.OK)
        {
            //zip the entire 'working' folder
            using (ZipFile zip = new ZipFile(fd.FileName))
            {
                zip.AddDirectory(Globals.WorkingFolder, "");
                zip.Save();
            }

            ////export to Excel here...
            //List<TaggedComponent> components = LocalData.GetComponents();

            //List<string> csv = new List<string>();
            //csv.Add(new TaggedComponent().GetHeader());
            //foreach (TaggedComponent component in components)
            //{
            //    csv.Add(component.ToString());
            //    csv.AddRange(component.GetChildrenAsComponents());
            //}

            //System.IO.File.WriteAllText(fd.FileName, String.Join("\r\n", csv.ToArray()));

            DialogResult dr2 = MessageBox.Show("Would you like to reset saved tag information now? Answering YES will make the export File the only source of that information. No, will cause the same data to be exported again and may lead to duplicate information in your LDAR database.", "Reset Data On Device", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr2 == System.Windows.Forms.DialogResult.Yes)
            {
                LocalData.BackupAndClear();
                UpdateLocalComponentCount();
            }
        }
    }

    private void toolStripButton5_Click(object sender, EventArgs e)
    {
        //throw new ArgumentException("The parameter was invalid");
    }

    private void toolStripButtonNonDrawingTag_Click(object sender, EventArgs e)
    {

        ECompositeViewer.IAdECompositeViewer compvwr = (ECompositeViewer.IAdECompositeViewer)adrContainer.Control.ECompositeViewer;
        ECompositeViewer.IAdSection sec = (ECompositeViewer.IAdSection)compvwr.Section;
        if (sec == null)
        {
            MessageBox.Show("No project is open.");
        }
        else
        {
            string drawing = sec.Title;
            FormEditObject eo = new FormEditObject(GetFormEditPosition());
            Point pos = new Point(panelMain.Top, this.panelMain.Width - eo.Width - 100);
            if (Globals.DialogLocations.ContainsKey(eo.Name))
            {
                pos = Globals.DialogLocations[eo.Name];
            }
            eo.SetComponent(Guid.NewGuid().ToString(),null, null, null, drawing);
            DialogResult dr = eo.ShowDialog();
            UpdateLocalComponentCount();
        }
    }

    private void toolStripButtonSettings_Click(object sender, EventArgs e)
    {
        DialogSettings ds = new DialogSettings();
        ds.ShowDialog();
        LocalData.DeviceIdentifier = Properties.Settings.Default.DeviceIdentifier;
    }

    private void toolStripButtonViewTags_Click(object sender, EventArgs e)
    {
        DialogViewTags dv = new DialogViewTags();
        dv.showMOC = toolStripButtonMOC.Enabled;
        dv.Tag = this;
        dv.ShowDialog();
    }

    private void toolStripButtonOldTags_Click(object sender, EventArgs e)
    {
        FormOldTags ot = new FormOldTags();
        ot.ShowDialog();
    }

    private void toolStripButtonACADSelect_Click(object sender, EventArgs e)
    {
        adrContainer.Control.ExecuteCommand("SELECT");
        toolStripButtonACADPan.Checked = false;
        toolStripButtonACADExtents.Checked = false;
        toolStripButtonACADZoomWindow.Checked = false;
    }

    private void toolStripButtonACADPan_Click(object sender, EventArgs e)
    {
        adrContainer.Control.ExecuteCommand("PAN");
        toolStripButtonACADSelect.Checked = false;
        toolStripButtonACADExtents.Checked = false;
        toolStripButtonACADZoomWindow.Checked = false;
    }

    private void toolStripButtonACADExtents_Click(object sender, EventArgs e)
    {
        adrContainer.Control.ExecuteCommand("FITTOWINDOW");
        toolStripButtonACADSelect.Checked = false;
        toolStripButtonACADPan.Checked = false;
        toolStripButtonACADZoomWindow.Checked = false;
    }

    private void toolStripButtonACADZoomWindow_Click(object sender, EventArgs e)
    {
        adrContainer.Control.ExecuteCommand("ZOOMRECT");
        toolStripButtonACADSelect.Checked = false;
        toolStripButtonACADPan.Checked = false;
        toolStripButtonACADExtents.Checked = false;
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
        }
        catch { }
    }

    private void toolStripButtonACADPalettes_Click(object sender, EventArgs e)
    {
        //hide all palettes
        ECompositeViewer.IAdECompositeViewer compositeViewer = (ECompositeViewer.IAdECompositeViewer)adrContainer.Control.ECompositeViewer;
        dynamic commands = compositeViewer.Commands;
        try { commands("DATABAND").Toggled = false; } catch { }
        try { commands("SEARCHBAND").Toggled = false; } catch { }
        try { commands("VIEWSBAND").Toggled = false; } catch { }
        try { commands("TEXTBAND").Toggled = false; } catch { }
        try { commands("MODELBAND").Toggled = false; } catch { }
        try { commands("MARKUPPROPERTIESBAND").Toggled = false; } catch { }
        try { commands("MARKUPBAND").Toggled = false; } catch { }
        try { commands("LISTVIEWBAND").Toggled = false; } catch { }
        try { commands("LAYERSBAND").Toggled = false; } catch { }
        try { commands("SECTIONPROPERTIESBAND").Toggled = false; } catch { }
        try { commands("OBJECTPROPERTIESBAND").Toggled = false; } catch { }
    }

    private void toolStripButtonMOC_Click(object sender, EventArgs e)
    {
        ECompositeViewer.IAdECompositeViewer compvwr = (ECompositeViewer.IAdECompositeViewer)adrContainer.Control.ECompositeViewer;
        ECompositeViewer.IAdSection sec = (ECompositeViewer.IAdSection)compvwr.Section;
        if (sec == null)
        {
            MessageBox.Show("No project is open.");
        }
        else
        {
            string drawing = sec.Title;
            FormMOC eo = new FormMOC(GetFormMOCPosition());
            Point pos = new Point(panelMain.Top, this.panelMain.Width - eo.Width - 100);
            if (Globals.DialogLocations.ContainsKey(eo.Name))
            {
                pos = Globals.DialogLocations[eo.Name];
            }
            eo.SetComponent(Guid.NewGuid().ToString(), null, null, null, drawing);
            DialogResult dr = eo.ShowDialog();
            UpdateLocalComponentCount();
        }

    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (Globals.CurrentProjectData != null)
        {
            
        }
    }

  }
}
