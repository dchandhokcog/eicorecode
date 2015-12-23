using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using EnvInt.Win32.FieldTech.Containers;
using EnvInt.Win32.FieldTech.Common;
using Telerik.WinControls.UI;

namespace EnvInt.Win32.FieldTech.Manager.Controls
{
    public partial class LDARDataControl : UserControl
    {
        private MainForm _mainForm = null;
        private MenuItem invertAllShown = new MenuItem();
        private MenuItem invertAllChildren = new MenuItem();
        RadDropDownMenu contextMenuClassType = new RadDropDownMenu();
        RadDropDownMenu contextMenuUnit = new RadDropDownMenu();
        RadDropDownMenu contextMenuStream = new RadDropDownMenu();
        RadDropDownMenu contextMenuArea = new RadDropDownMenu();

        public LDARDataControl()
        {
            InitializeComponent();
            setCustomContextMenus();
        }

        private void setCustomContextMenus()
        {
            
            //class type specific menus
            RadMenuItem rmi1 = new RadMenuItem("Invert showInTablet Selection");
            rmi1.Click += new EventHandler(invertShownSelection_Click);
            RadMenuItem rmi2 = new RadMenuItem("Invert Child Selection");
            rmi2.Click += new EventHandler(invertChildSelection_Click);
            contextMenuClassType.Items.Add(rmi1);
            contextMenuClassType.Items.Add(rmi2);
            radGridViewClassType.ContextMenuOpening += new ContextMenuOpeningEventHandler(radGridViewClassType_ContextMenuOpening);

            //unit specific context menus
            RadMenuItem rmi3 = new RadMenuItem("Invert showInTablet Selection");
            rmi3.Click += new EventHandler(invertShownSelectionUnit_Click);
            contextMenuUnit.Items.Add(rmi3);
            radGridViewUnit.ContextMenuOpening += new ContextMenuOpeningEventHandler(radGridViewUnit_ContextMenuOpening);

            //stream specific context menus
            RadMenuItem rmi4 = new RadMenuItem("Invert showInTablet Selection");
            rmi4.Click += new EventHandler(invertShownSelectionStream_Click);
            contextMenuStream.Items.Add(rmi4);
            radGridViewStream.ContextMenuOpening += new ContextMenuOpeningEventHandler(radGridViewStream_ContextMenuOpening);

            //area specific context menus
            RadMenuItem rmi5 = new RadMenuItem("Invert showInTablet Selection");
            rmi5.Click += new EventHandler(invertShownSelectionArea_Click);
            contextMenuArea.Items.Add(rmi5);
            radGridViewAreas.ContextMenuOpening += new ContextMenuOpeningEventHandler(radGridViewArea_ContextMenuOpening);
        }

        private void radGridViewArea_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            e.ContextMenu = contextMenuArea;
        }

        private void radGridViewClassType_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            e.ContextMenu = contextMenuClassType;
        }

        private void radGridViewUnit_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            e.ContextMenu = contextMenuUnit;
        }

        private void radGridViewStream_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            e.ContextMenu = contextMenuStream;
        } 

        private void invertChildSelection_Click(object sender, EventArgs e)
        {
            foreach (LDARComponentClassType t in MainForm.CurrentProjectData.LDARData.ComponentClassTypes)
            {
                t.childType = !t.childType;
                MainForm.CurrentProjectDirty = true;
                radGridViewClassType.MasterTemplate.Refresh(null);
            }
        }

        private void invertShownSelection_Click(object sender, EventArgs e)
        {
            foreach (LDARComponentClassType t in MainForm.CurrentProjectData.LDARData.ComponentClassTypes)
            {
                t.showInTablet = !t.showInTablet;
                MainForm.CurrentProjectDirty = true;
                radGridViewClassType.MasterTemplate.Refresh(null); 
            }

        }

        private void invertShownSelectionUnit_Click(object sender, EventArgs e)
        {
            foreach (LDARProcessUnit t in MainForm.CurrentProjectData.LDARData.ProcessUnits)
            {
                t.showInTablet = !t.showInTablet;
                MainForm.CurrentProjectDirty = true;
                this.radGridViewUnit.MasterTemplate.Refresh(null);
            }
        }

        private void invertShownSelectionStream_Click(object sender, EventArgs e)
        {
            foreach (LDARComponentStream t in MainForm.CurrentProjectData.LDARData.ComponentStreams)
            {
                t.showInTablet = !t.showInTablet;
                MainForm.CurrentProjectDirty = true;
                this.radGridViewStream.MasterTemplate.Refresh(null);
            }
        }

        private void invertShownSelectionArea_Click(object sender, EventArgs e)
        {
            foreach (LDARArea t in MainForm.CurrentProjectData.LDARData.Areas)
            {
                t.showInTablet = !t.showInTablet;
                MainForm.CurrentProjectDirty = true;
                this.radGridViewAreas.MasterTemplate.Refresh(null);
            }
        }

        public bool LoadProject(MainForm mainform)
        {
            _mainForm = mainform;
            UpdateUI(MainForm.CurrentProjectData);
            return true;
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            _mainForm.RefreshProject(true, false);
            UpdateUI(MainForm.CurrentProjectData);
        }

        private void UpdateUI(ProjectData projectData)
        {
            radGridViewComponents.DataSource = projectData.LDARData.ExistingComponents;
            radPageViewPageComponents.Text = radPageViewPageComponents.Title + "(" + projectData.LDARData.ExistingComponents.Count() + ")";

            radGridViewClassType.DataSource = projectData.LDARData.ComponentClassTypes;
            radPageViewPageComponentClassTypes.Text = radPageViewPageComponentClassTypes.Title + "(" + projectData.LDARData.ComponentClassTypes.Count() + ")";

            radGridViewStream.DataSource = projectData.LDARData.ComponentStreams;
            radPageViewPageStreams.Text = radPageViewPageStreams.Title + "(" + projectData.LDARData.ComponentStreams.Count() + ")";

            radGridViewState.DataSource =  projectData.LDARData.ChemicalStates;
            radPageViewPageChemicalStates.Text = radPageViewPageChemicalStates.Title + "(" + projectData.LDARData.ChemicalStates.Count() + ")";

            radGridViewPlant.DataSource = projectData.LDARData.LocationPlants;
            radPageViewPagePlants.Text = radPageViewPagePlants.Title + "(" + projectData.LDARData.LocationPlants.Count() + ")";

            radGridViewUnit.DataSource = projectData.LDARData.ProcessUnits;
            radPageViewPageProcessUnits.Text = radPageViewPageProcessUnits.Title + "(" + projectData.LDARData.ProcessUnits.Count() + ")";

            radGridViewPressure.DataSource = projectData.LDARData.PressureServices;
            radPageViewPagePressureServices.Text = radPageViewPagePressureServices.Title + "(" + projectData.LDARData.PressureServices.Count() + ")";

            radGridViewTechnicians.DataSource =  projectData.LDARData.Technicians;
            radPageViewPageTechnicians.Text = radPageViewPageTechnicians.Title + "(" + projectData.LDARData.Technicians.Count() + ")";

            radGridViewCategories.DataSource = projectData.LDARData.ComponentCategories;
            radPageViewPageCategories.Text = radPageViewPageCategories.Title + "(" + projectData.LDARData.ComponentCategories.Count() + ")";

            radGridViewAreas.DataSource = projectData.LDARData.Areas;
            radPageViewPageArea.Text = radPageViewPageArea.Title + "(" + projectData.LDARData.Areas.Count() + ")";

            radGridViewManufacturers.DataSource = projectData.LDARData.Manufacturers;
            radPageViewPageManufacturers.Text = radPageViewPageManufacturers.Title + "(" + projectData.LDARData.Manufacturers.Count() + ")";

            radGridViewReasons.DataSource = projectData.LDARData.ComponentReasons;
            radPageViewPageReasons.Text = radPageViewPageReasons.Title + "(" + projectData.LDARData.ComponentReasons.Count() + ")";

            radGridViewEquipment.DataSource = projectData.LDARData.Equipment;
            radPageViewPageEquipment.Text = radPageViewPageEquipment.Title + "(" + projectData.LDARData.Equipment.Count() + ")";

            radGridViewOOSReasons.DataSource = projectData.LDARData.OOSDescriptions;
            radPageViewPageOOS.Text = radPageViewPageOOS.Title + "(" + projectData.LDARData.OOSDescriptions.Count() + ")";
            

            try
            {
                this.radGridViewClassType.Columns["ComponentClassId"].ReadOnly = true;
                this.radGridViewClassType.Columns["ComponentTypeId"].ReadOnly = true;
                this.radGridViewStream.Columns["ComponentStreamId"].ReadOnly = true;
                this.radGridViewState.Columns["ChemicalStateId"].ReadOnly = true;
                this.radGridViewPressure.Columns["PressureServiceId"].ReadOnly = true;
                this.radGridViewPlant.Columns["PlantId"].ReadOnly = true;
                this.radGridViewUnit.Columns["ProcessUnitId"].ReadOnly = true;
                this.radGridViewTechnicians.Columns["Id"].ReadOnly = true;
                this.radGridViewCategories.Columns["ComponentCategoryId"].ReadOnly = true;
                this.radGridViewAreas.Columns["AreaId"].ReadOnly = true;
                this.radGridViewManufacturers.Columns["ManufacturerId"].ReadOnly = true;
                this.radGridViewReasons.Columns["ComponentReasonId"].ReadOnly = true;
                this.radGridViewOOSReasons.Columns["OOSId"].ReadOnly = true;
            }
            catch { }

            try
            {
                if (MainForm.CurrentProjectData.LDARDatabaseType == "Guideware")
                {
                    radGridViewComponents.Columns["ProcessUnitID"].IsVisible = false;
                    radGridViewComponents.Columns["AreaID"].IsVisible = false;
                    radGridViewComponents.Columns["ComponentCategoryID"].IsVisible = false;
                    radGridViewComponents.Columns["TagExtension"].IsVisible = true;
                    radGridViewComponents.Columns["Location1"].IsVisible = true;
                    radGridViewComponents.Columns["Location2"].IsVisible = true;
                    radGridViewComponents.Columns["Location3"].IsVisible = true;
                    radGridViewComponents.Columns["ComponentUTMReason"].IsVisible = true; 
                }
                else
                {
                    radGridViewComponents.Columns["ProcessUnitID"].IsVisible = true;
                    radGridViewComponents.Columns["AreaID"].IsVisible = true;
                    radGridViewComponents.Columns["ComponentCategoryID"].IsVisible = true;
                    radGridViewComponents.Columns["TagExtension"].IsVisible = false;
                    radGridViewComponents.Columns["Location1"].IsVisible = false;
                    radGridViewComponents.Columns["Location2"].IsVisible = false;
                    radGridViewComponents.Columns["Location3"].IsVisible = false;
                    radGridViewComponents.Columns["ComponentUTMReason"].IsVisible = false; 
                }
            }
            catch { }

        }

        private void radGridViewComponents_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            MainForm.CurrentProjectDirty = true;
        }

        private void radGridViewClassType_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            MainForm.CurrentProjectDirty = true;
        }

        private void radGridViewStream_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            MainForm.CurrentProjectDirty = true;
        }

        private void radGridViewState_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            MainForm.CurrentProjectDirty = true;
        }

        private void radGridViewPressure_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            MainForm.CurrentProjectDirty = true;
        }

        private void radGridViewPlant_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            MainForm.CurrentProjectDirty = true;
        }

        private void radGridViewUnit_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            MainForm.CurrentProjectDirty = true;
        }

        private void radGridViewTechnicians_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            MainForm.CurrentProjectDirty = true;
        }

        private void radGridViewCategories_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            MainForm.CurrentProjectDirty = true;
        }

        private void radGridViewAreas_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            MainForm.CurrentProjectDirty = true;
        }

        private void radGridViewManufacturers_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            MainForm.CurrentProjectDirty = true;
        }

        private void toolStripButtonExportLDARData_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            if (fb.ShowDialog() == DialogResult.OK)
            {
                FileUtilities.writeCSVableToFile(MainForm.CurrentProjectData.LDARData.Areas.ToArray<object>() , fb.SelectedPath + "\\LDARAreas.csv");
                FileUtilities.writeCSVableToFile(MainForm.CurrentProjectData.LDARData.ChemicalStates.ToArray<object>(), fb.SelectedPath + "\\LDARChemicalState.csv");
                FileUtilities.writeCSVableToFile(MainForm.CurrentProjectData.LDARData.ComponentCategories.ToArray<object>(), fb.SelectedPath + "\\LDARComponentCategoy.csv");
                FileUtilities.writeCSVableToFile(MainForm.CurrentProjectData.LDARData.ComponentClassTypes.ToArray<object>(), fb.SelectedPath + "\\LDARClassType.csv");
                FileUtilities.writeCSVableToFile(MainForm.CurrentProjectData.LDARData.ComponentReasons.ToArray<object>(), fb.SelectedPath + "\\LDARComponentReason.csv");
                FileUtilities.writeCSVableToFile(MainForm.CurrentProjectData.LDARData.ComponentStreams.ToArray<object>(), fb.SelectedPath + "\\LDARComponentStream.csv");
                FileUtilities.writeCSVableToFile(MainForm.CurrentProjectData.LDARData.ExistingComponents.ToArray<object>(), fb.SelectedPath + "\\LDARExistingComponents.csv");
                FileUtilities.writeCSVableToFile(MainForm.CurrentProjectData.LDARData.LocationPlants.ToArray<object>(), fb.SelectedPath + "\\LDARLocationPlant.csv");
                FileUtilities.writeCSVableToFile(MainForm.CurrentProjectData.LDARData.Manufacturers.ToArray<object>(), fb.SelectedPath + "\\LDARManufacturer.csv");
                FileUtilities.writeCSVableToFile(MainForm.CurrentProjectData.LDARData.PressureServices.ToArray<object>(), fb.SelectedPath + "\\LDARPressureService.csv");
                FileUtilities.writeCSVableToFile(MainForm.CurrentProjectData.LDARData.ProcessUnits.ToArray<object>(), fb.SelectedPath + "\\LDARProcessUnit.csv");
                FileUtilities.writeCSVableToFile(MainForm.CurrentProjectData.LDARData.Technicians.ToArray<object>(), fb.SelectedPath + "\\LDARTechnician.csv");
                FileUtilities.writeCSVableToFile(MainForm.CurrentProjectData.LDARData.Equipment.ToArray<object>(), fb.SelectedPath + "\\LDAREquipment.csv");
                FileUtilities.writeCSVableToFile(MainForm.CurrentProjectData.LDARData.OOSDescriptions.ToArray<object>(), fb.SelectedPath + "\\LDAROOSDescriptions.csv");
            }
        }

        private void radGridViewReasons_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            MainForm.CurrentProjectDirty = true;
        }
    }
}
