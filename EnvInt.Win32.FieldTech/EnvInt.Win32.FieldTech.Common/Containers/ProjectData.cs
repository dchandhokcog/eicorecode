using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvInt.Win32.FieldTech.Containers
{
    /// <summary>
    /// The ProjectData class is the main container for all the whole project parts.
    ///  1. Project Information
    ///  2. LDAR Database Connection information
    ///  3. LDAR Database Records
    ///  4. CAD Package Information
    ///  
    /// Important Note: The TaggedComponent information is not stored in this object and is stored in a seperate ProjectTagData object so it does
    /// not need to be automatically exported with this information.
    /// </summary>
    public class ProjectData
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectPath { get; set; }
        public Version ProjectVersion { get; set; }
        public LDARProjectType ProjectType { get; set; }

        public string LDARDatabaseType { get; set; }
        public string LDARDatabaseVersion { get; set; }
        public string LDARDatabaseServer { get; set; }
        public string LDARDatabaseName { get; set; }
        public string LDARDatabaseAuthentication { get; set; }
        public string LDARDatabaseUsername { get; set; }
        public string LDARDatabasePassword { get; set; }
        public string LDARDatabaseConnectionString { get; set; }
        public int LDARTagPaddedZeros { get; set; }

        // ROY: Variable for New Tag Format 
        public string LDARTAGStartsFrom { get; set; }
        public string LDARTAGStartsTo { get; set; }
        public string LDARTAGFormat { get; set; }

        public int LDARTagStartChildrenNumber { get; set; }
        public int LDARRoutePaddedZeros { get; set; }
        public bool ZeroStatusSet { get; set; }

        public DateTime? LDARDatabaseLastRefreshed { get; set; }

        public string CADProjectPath { get; set; }
        public string PDFDrawingPath { get; set; }        
        public List<ProjectConfiguration> Configurations = new List<ProjectConfiguration>();
        public LDARData LDARData = new LDARData();

        //JMA: This has been removed and the DrawingPath is now in the CADPackage.FileName property
        //public string CADDrawingPackage { get; set; }
        //JMA: This has been changed to a List<> to support multiple CAD packages in a project
        public List<CADPackage> CADPackages = new List<CADPackage>();
        public List<CADPackage> MarkedDrawings = new List<CADPackage>();
        public DateTime? CADDrawingPackageLastRefreshed { get; set; }
        public string DefaultDrawing { get; set; }

        public ProjectData()
        { }

        //this constructor takes a separate ProjectData object and creates a copy of it
        public ProjectData(ref ProjectData refData, List<string> unitFilter, List<string> drawingFilter)
        {
            this.ProjectId = refData.ProjectId;
            this.ProjectName = refData.ProjectName;
            this.ProjectPath = refData.ProjectPath;
            this.ProjectVersion = refData.ProjectVersion;
            this.ProjectType = refData.ProjectType;

            this.LDARDatabaseType = refData.LDARDatabaseType;
            this.LDARDatabaseVersion = refData.LDARDatabaseVersion;
            this.LDARDatabaseServer = refData.LDARDatabaseServer;
            this.LDARDatabaseName = refData.LDARDatabaseName;
            this.LDARDatabaseAuthentication = refData.LDARDatabaseAuthentication;
            this.LDARDatabaseUsername = refData.LDARDatabaseUsername;
            this.LDARDatabasePassword = refData.LDARDatabasePassword;
            this.LDARDatabaseConnectionString = refData.LDARDatabaseConnectionString;
            this.LDARTagPaddedZeros = refData.LDARTagPaddedZeros;

            this.LDARTAGStartsFrom = refData.LDARTAGStartsFrom;
            this.LDARTAGStartsTo = refData.LDARTAGStartsTo;
            this.LDARTAGFormat = refData.LDARTAGFormat;

            this.LDARTagStartChildrenNumber = refData.LDARTagStartChildrenNumber;
            this.LDARRoutePaddedZeros = refData.LDARRoutePaddedZeros;
            this.ZeroStatusSet = refData.ZeroStatusSet;

            this.LDARDatabaseLastRefreshed = refData.LDARDatabaseLastRefreshed;

            this.CADProjectPath = refData.CADProjectPath;
            this.PDFDrawingPath = refData.PDFDrawingPath;
            
            //this doesn't do anything so empty is fine
            Configurations = new List<ProjectConfiguration>();

            LDARData = new LDARData(refData.LDARData);
            CADPackages = new List<CADPackage>();
            MarkedDrawings = new List<CADPackage>();

            if (drawingFilter == null)
            {
                foreach (CADPackage cp in refData.CADPackages)
                {
                    CADPackage newcp = new CADPackage(cp);
                    CADPackages.Add(newcp);
                }
            }
            else
            {
                foreach (CADPackage cp in refData.CADPackages.Where(c => drawingFilter.Contains(System.IO.Path.GetFileName(c.FileName))))
                {
                    CADPackage newcp = new CADPackage(cp);
                    CADPackages.Add(newcp);
                }
            }

            foreach (CADPackage cp in refData.MarkedDrawings)
            {
                CADPackage newcp = new CADPackage(cp);
                MarkedDrawings.Add(newcp);
            }

            this.CADDrawingPackageLastRefreshed = refData.CADDrawingPackageLastRefreshed;
            this.DefaultDrawing = refData.DefaultDrawing;
    }

    }

    public enum LDARProjectType
    {
        FieldTechToolbox = 0,
        EiMOC = 1
    }

    public enum LDARDatabaseType
    {
        LeakDAS = 0,
        Guideware = 1
    }

    public class LDARData
    {
        public List<LDARComponent> ExistingComponents = new List<LDARComponent>();
        public List<LDARComponentClassType> ComponentClassTypes = new List<LDARComponentClassType>();
        public List<LDARChemicalState> ChemicalStates = new List<LDARChemicalState>();
        public List<LDARComponentStream> ComponentStreams = new List<LDARComponentStream>();
        public List<LDARLocationPlant> LocationPlants = new List<LDARLocationPlant>();
        public List<LDARPressureService> PressureServices = new List<LDARPressureService>();
        public List<LDARProcessUnit> ProcessUnits = new List<LDARProcessUnit>();
        public List<LDARTechnician> Technicians = new List<LDARTechnician>();
        public List<LDARCategory> ComponentCategories = new List<LDARCategory>();
        public List<LDARReason> ComponentReasons = new List<LDARReason>();
        public List<LDARArea> Areas = new List<LDARArea>();
        public List<LDARManufacturer> Manufacturers = new List<LDARManufacturer>();
        public List<LDARRegulation> Regulations = new List<LDARRegulation>();
        public List<LDARComplianceGroup> ComplianceGroups = new List<LDARComplianceGroup>();
        public List<LDAREquipment> Equipment = new List<LDAREquipment>();
        public List<LDAROOSDescription> OOSDescriptions = new List<LDAROOSDescription>();
        public List<LDAROption> LDAROptions = new List<LDAROption>();
        public List<LDARCVSReason> LDARCVSReasons = new List<LDARCVSReason>();

        public LDARData()
        {
            LDARCVSReason newReason1 = new LDARCVSReason();
            LDARCVSReason newReason2 = new LDARCVSReason();
            LDARCVSReason newReason3 = new LDARCVSReason();

            newReason1.CVSId = 1;
            newReason1.CVSDescription = "Unknown";

            newReason2.CVSId = 2;
            newReason2.CVSDescription = "Ductwork";

            newReason3.CVSId = 3;
            newReason3.CVSDescription = "Hard Pipe";

            LDARCVSReasons.Add(newReason1);
            LDARCVSReasons.Add(newReason2);
            LDARCVSReasons.Add(newReason3);
            
        }

        public LDARData(LDARData refData)
        {

            foreach (LDARComponent c in refData.ExistingComponents)
            {
                ExistingComponents.Add(new LDARComponent(c));
            }

            foreach (LDARComponentClassType c in refData.ComponentClassTypes)
            {
                ComponentClassTypes.Add(new LDARComponentClassType(c));
            }

            foreach (LDARChemicalState c in refData.ChemicalStates)
            {
                ChemicalStates.Add(new LDARChemicalState(c));
            }

            foreach (LDARComponentStream c in refData.ComponentStreams)
            {
                ComponentStreams.Add(new LDARComponentStream(c));
            }

            foreach (LDARLocationPlant c in refData.LocationPlants)
            {
                LocationPlants.Add(new LDARLocationPlant(c));
            }

            foreach (LDARPressureService c in refData.PressureServices)
            {
                PressureServices.Add(new LDARPressureService(c));
            }

            foreach (LDARProcessUnit c in refData.ProcessUnits)
            {
                ProcessUnits.Add(new LDARProcessUnit(c));
            }

            foreach (LDARTechnician c in refData.Technicians)
            {
                Technicians.Add(new LDARTechnician(c));
            }

            foreach (LDARCategory c in refData.ComponentCategories)
            {
                ComponentCategories.Add(new LDARCategory(c));
            }

            foreach (LDARReason c in refData.ComponentReasons)
            {
                ComponentReasons.Add(new LDARReason(c));
            }

            foreach (LDARArea c in refData.Areas)
            {
                Areas.Add(new LDARArea(c));
            }

            foreach (LDARManufacturer c in refData.Manufacturers)
            {
                Manufacturers.Add(new LDARManufacturer(c));
            }

            foreach (LDARRegulation c in refData.Regulations)
            {
                Regulations.Add(new LDARRegulation(c));
            }

            foreach (LDARComplianceGroup c in refData.ComplianceGroups)
            {
                ComplianceGroups.Add(new LDARComplianceGroup(c));
            }

            foreach (LDAREquipment c in refData.Equipment)
            {
                Equipment.Add(new LDAREquipment(c));
            }

            foreach (LDAROOSDescription c in refData.OOSDescriptions)
            {
                OOSDescriptions.Add(new LDAROOSDescription(c));
            }

            foreach (LDAROption c in refData.LDAROptions)
            {
                LDAROptions.Add(new LDAROption(c));
            }

            foreach (LDARCVSReason c in refData.LDARCVSReasons)
            {
                LDARCVSReasons.Add(new LDARCVSReason(c));
            }
        }

        public LDARComponent getLDARComponentByTag(string LDARTag, string Extension = "")
        {
            if (ExistingComponents == null) return null;
            else
            {
                if (Extension == "")
                {
                    return ExistingComponents.Where(c => c.ComponentTag == LDARTag).FirstOrDefault();
                }
                else
                {
                    return ExistingComponents.Where(c => c.ComponentTag == LDARTag && c.TagExtension == Extension).FirstOrDefault();
                }
            }
        }
    }

}
