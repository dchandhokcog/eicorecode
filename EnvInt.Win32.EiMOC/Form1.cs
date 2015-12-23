using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AdCommon;
using AxExpressViewerDll;
using EPlotRenderer;

using EnvInt.Win32.FieldTech.Containers;
using EnvInt.Win32.FieldTech.Data;
using EnvInt.Win32.FieldTech.Library;


namespace EnvInt.Win32.FieldTech
{
  public partial class Form1 : Form
  {

      private List<Control> highlightControls = new List<Control>();

    public Form1()
    {
        InitializeComponent();
        axCExpressViewerControl1.OnSelectObjectEx += axCExpressViewerControl1_OnSelectObjectEx;
        axCExpressViewerControl1.OnSelectObject += axCExpressViewerControl1_OnSelectObject;

        timerSplash.Enabled = true;
        LocalData.Initialize();
        UpdateLocalComponentCount();

        Text = "Field Tech Toolbox - " + Globals.GetCurrentAssemblyVersion();
        
    }

    private void timerSplash_Tick(object sender, EventArgs e)
    {
        timerSplash.Enabled = false;
        Splash s = new Splash();
        DialogResult dr = s.ShowDialog();

        bool openSampleProject = Properties.Settings.Default.OpenSampleProject;
        if (openSampleProject)
        {
            if (System.IO.File.Exists("SampleProject.dwf"))
            {
                LoadProject(System.IO.Path.GetFullPath("SampleProject.dwf"));
            }
        }
    }

    private void LoadProject(string dwfFile)
    {
        axCExpressViewerControl1.SourcePath = dwfFile;
        Globals.ProjectFile = dwfFile;
        Globals.ProjectName = System.IO.Path.GetFileNameWithoutExtension(dwfFile);
        Globals.ProjectPath = System.IO.Path.GetDirectoryName(dwfFile);
        LocalData.ProjectFile = dwfFile;

        LoadComponentList();
    }

    private void LoadComponentList()
    {
        Globals.ProjectComponents.Clear();

        ECompositeViewer.IAdECompositeViewer compositeViewer;
        AdCommon.IAdCollection sections;
        ECompositeViewer.IAdSection currentSection; 
        
        compositeViewer = (ECompositeViewer.IAdECompositeViewer) axCExpressViewerControl1.ECompositeViewer;
        sections = (AdCommon.IAdCollection) compositeViewer.Sections;

        uint nRed = 255, nGreen = 0, nBlue = 0;
        compositeViewer.ObjectSelectionColor = 65536 * nBlue + 256 * nGreen + nRed;
        
        foreach (ECompositeViewer.IAdSection Section in sections)
        { 
            //Set section 
            compositeViewer.Section = Section;  
            //Get section 
            currentSection = (ECompositeViewer.IAdSection) compositeViewer.Section;

            ECompositeViewer.IAdContent ObjectContent = (ECompositeViewer.IAdContent)Section.Content;

            //Create a user collection of all objects.  Collection as such is not named or unnamed, it depends on whether the
            //items added into the user collection are named or unnamed.
            AdCommon.IAdUserCollection MyObjectsNamedCollection = (AdCommon.IAdUserCollection)ObjectContent.CreateUserCollection(); 
            //Get all the objects in the current section
            AdCommon.IAdCollection MyObjects = (AdCommon.IAdCollection)ObjectContent.get_Objects(0);

            //0 is to return all objects 
            foreach (ECompositeViewer.IAdObject MyObject in MyObjects)
            {
                AdCommon.IAdCollection MyObjectProperties = (AdCommon.IAdCollection)MyObject.Properties;	
                foreach (AdCommon.IAdProperty MyObjectProperty in MyObjectProperties)	
                {
                    //IntPtr hDC = PlatformInvokeUSER32.GetWindowDC(MyObject.Handle.ToInt32()); 
                    //CompositeViewer.DrawToDC(hDC.ToInt32(), 10, 0, 250, 250);

            
                    if ( (MyObjectProperty.Name == "Tag")  )		
                    {
                        string componentTag = MyObjectProperty.Value;
                        if (!Globals.ProjectComponents.Contains(componentTag))
                        {
                            Globals.ProjectComponents.Add(componentTag);
                        }
                    }	
                }
            }
        }

        Globals.ProjectComponents = Globals.ProjectComponents.OrderBy(c => c).ToList();
    }

    
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

    private void axCExpressViewerControl1_OnSelectObjectEx(object sender, AxExpressViewerDll.IAdViewerEvents_OnSelectObjectExEvent e)
    {
        
    }

    private void UpdateLocalComponentCount()
    {
        int count = LocalData.GetComponentCount();
        toolStripLabelSummary.Text = count.ToString() + " components tagged on device.";
        toolStripLabelSummary.ForeColor = Color.Black;
        if (count > 0) toolStripLabelSummary.ForeColor = Color.Red;
    }

    private void DWF_Property_Click(object sender, EventArgs e)
    {
      
      ECompositeViewer.IAdECompositeViewer CompositeViewer;
      AdCommon.IAdCollection Sections;

      CompositeViewer = (ECompositeViewer.IAdECompositeViewer)axCExpressViewerControl1.ECompositeViewer;
      Sections = (AdCommon.IAdCollection)CompositeViewer.Sections;
      MessageBox.Show("Pages Count : " + Sections.Count);

      //Loop through Sections Collection using foreach
      foreach (ECompositeViewer.IAdSection Section in Sections)
      {        
        MessageBox.Show(Section.Title);
        MessageBox.Show("Order of the section \"" + Section.Title + "\" is " + Section.Order);
      }
    }

    

    private void button_hideObj_Click(object sender, EventArgs e)
    {
      try
      {
        ECompositeViewer.IAdECompositeViewer compvwr =
(ECompositeViewer.IAdECompositeViewer)axCExpressViewerControl1.ECompositeViewer;

        ECompositeViewer.IAdSection sec = (ECompositeViewer.IAdSection)compvwr.Section;
        ECompositeViewer.IAdContent con = (ECompositeViewer.IAdContent)sec.Content;

        AdCommon.IAdCollection adobj = (AdCommon.IAdCollection)con.get_Objects(1); //for selected object
        AdCommon.IAdObject obj = (AdCommon.IAdObject)adobj[1];

        compvwr.ExecuteCommand("HIDE");        

      }
      catch
      {
        // TODO: Add your error handling here.
      }
    }

    private void objCountBtn_Click(object sender, EventArgs e)
    {
      try
      {
        ECompositeViewer.IAdECompositeViewer compvwr = (ECompositeViewer.IAdECompositeViewer)axCExpressViewerControl1.ECompositeViewer;

        EPlotViewer.IAdEPlotViewer2 docHand = compvwr.DocumentHandler;
        Object objNodes = docHand.ObjectNodes;

        //String msgString = " ";
        //msgString = msgString + "Object Count : " + objNodes.count().ToString();

        // This section is related to Blog post -
        // 
        
        ECompositeViewer.IAdECompositeViewer CompositeViewer;
        EPlotViewer.IAdEPlotSection PlotSection;
        AdCommon.IAdPageView View;
        ECompositeViewer.IAdSection SectionChk;
        ECompositeViewer.IAdSectionType SectionTypeChk;
        CompositeViewer = (ECompositeViewer.IAdECompositeViewer) axCExpressViewerControl1.ECompositeViewer;      
        SectionChk = (ECompositeViewer.IAdSection) CompositeViewer.Section;
        SectionTypeChk = (ECompositeViewer.IAdSectionType) SectionChk.SectionType;  
        
        if (SectionTypeChk.Name == "com.autodesk.dwf.ePlot")
         { 
          PlotSection = (EPlotViewer.IAdEPlotSection) CompositeViewer.Section;
          View = (AdCommon.IAdPageView)PlotSection.View;  
          //Set View - interchange Left with Bottom and Top with Right 
          PlotSection.SetView (View.Bottom, View.Left, View.Top,  View.Right);
        }          

      }
      catch
      {
        // TODO: Add your error handling here.
      }
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
        OpenFileDialog oFileDialog = new OpenFileDialog();
        oFileDialog.ShowDialog();
        LoadProject(oFileDialog.FileName);
    }

    private void toolStripButton3_Click(object sender, EventArgs e)
    {
        ECompositeViewer.IAdECompositeViewer compvwr = (ECompositeViewer.IAdECompositeViewer)axCExpressViewerControl1.ECompositeViewer;
        ECompositeViewer.IAdSection sec = (ECompositeViewer.IAdSection)compvwr.Section;
        ECompositeViewer.IAdContent con = (ECompositeViewer.IAdContent)sec.Content;

        AdCommon.IAdCollection adobj = (AdCommon.IAdCollection)con.get_Objects(1); //for selected object
        if (adobj.Count > 0)
        {
            AdCommon.IAdObject obj = (AdCommon.IAdObject)adobj[1];

            string tag = "";
            string description = "";
            string str = "";
            foreach (AdCommon.IAdProperty prop in (AdCommon.IAdCollection)obj.Properties)
            {
                if (prop.Name.ToLower() == "tag") tag = prop.Value;
                if (prop.Name.ToLower() == "description") description = prop.Value;
                str = str + "Name: " + prop.Name + " Value: " + prop.Value + Environment.NewLine;
            }
            //MessageBox.Show(str);
            FormEditObject eo = new FormEditObject();
            eo.SetComponent(tag, description);
            DialogResult dr = eo.ShowDialog();
            UpdateLocalComponentCount();
            
        }
        else
        {
            MessageBox.Show("No Object");
        }
    }

    private void toolStripButton2_Click(object sender, EventArgs e)
    {
        SaveFileDialog fd = new SaveFileDialog();
        fd.InitialDirectory = System.IO.Path.GetFullPath(LocalData.ProjectFile);
        fd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
        fd.FilterIndex = 1;

        DialogResult dr = fd.ShowDialog();

        if (dr == System.Windows.Forms.DialogResult.OK)
        {

            //export to Excel here...
            List<TaggedComponent> components = LocalData.GetComponents();

            List<string> csv = new List<string>();
            foreach (TaggedComponent component in components)
            {
                csv.Add(component.ToString());
            }

            System.IO.File.WriteAllText(fd.FileName, String.Join("\r\n", csv.ToArray()));

        }
    }

    private void toolStripButton4_CheckStateChanged(object sender, EventArgs e)
    {
        if (toolStripButton4.Checked)
        {
            int pixelsHeight = 50;
            int pixelsWidth = 200;

            TransparentPanel p = new TransparentPanel();
            p.Height = pixelsHeight;
            p.Width = pixelsWidth;
            p.BackColor = Color.Yellow;
            p.AutoSize = false;
            //p.Parent = this;


            p.Top = axCExpressViewerControl1.Top + 100;
            p.Left = axCExpressViewerControl1.Left + 100;


            this.Controls.Add(p);
            p.BringToFront();

            highlightControls.Add(p);
        }
        else
        {
            foreach (Control c in highlightControls)
            {
                this.Controls.Remove(c);
            }
            highlightControls.Clear();
        }
        
    }

    private void toolStripButton5_Click(object sender, EventArgs e)
    {
        throw new ArgumentException("The parameter was invalid");
    }

    
  }
}
