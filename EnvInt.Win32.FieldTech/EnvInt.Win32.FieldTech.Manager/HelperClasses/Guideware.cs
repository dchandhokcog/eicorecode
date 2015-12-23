using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using EnvInt.Data.DAL.Guideware2.DatabaseSpecific;
//using EnvInt.Data.DAL.Guideware2.EntityClasses;
//using EnvInt.Data.DAL.Guideware2.FactoryClasses;
//using EnvInt.Data.DAL.Guideware2.HelperClasses;
//using EnvInt.Data.DAL.Guideware2.Linq;
using EnvInt.Win32.FieldTech.Containers;

//using SD.LLBLGen.Pro.ORMSupportClasses;
//using SD.LLBLGen.Pro.LinqSupportClasses;

using EnvInt.Data.LDAR.Guideware;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace EnvInt.Win32.FieldTech.Manager.HelperClasses
{
    public static class Guideware
    {

        public static Guidewarev2 gwEntities;

        #region Guideware Utilities

        public static void initializeDatabase(string ConnectionString)
        {
            gwEntities = new Guidewarev2(ConnectionString);
        }

        //public static DataAccessAdapter GetGuideware2Adapter()
        //{
        //    DataAccessAdapter guideware2Adapter = new DataAccessAdapter();
        //    if (string.IsNullOrEmpty(MainForm.CurrentProjectData.LDARDatabaseConnectionString))
        //    {
        //        MainForm.CurrentProjectData.LDARDatabaseConnectionString = getConnectionString();
        //    }
        //    guideware2Adapter.ConnectionString = MainForm.CurrentProjectData.LDARDatabaseConnectionString;
        //    guideware2Adapter.CatalogNameUsageSetting = CatalogNameUsage.Clear;
        //    return guideware2Adapter;
        //}

        public static string getConnectionString()
        {
            string ConnectionString = string.Empty;
            if (MainForm.CurrentProjectData.LDARDatabaseAuthentication.Contains("Windows"))
            {
                ConnectionString = "Data Source=" + MainForm.CurrentProjectData.LDARDatabaseServer + ";Initial Catalog=" + MainForm.CurrentProjectData.LDARDatabaseName + ";Trusted_Connection=true";
            }
            else
            {
                ConnectionString = "Data Source=" + MainForm.CurrentProjectData.LDARDatabaseServer + "; Initial Catalog=" + MainForm.CurrentProjectData.LDARDatabaseName + "; User Id=" + MainForm.CurrentProjectData.LDARDatabaseUsername + "; Password=" + MainForm.CurrentProjectData.LDARDatabasePassword + ";";
            }
            return ConnectionString;
        }

        public static List<LDARLocationPlant> GetLocationPlants()
        {
            //DataAccessAdapter adapter = GetGuideware2Adapter();
            //LinqMetaData md = new LinqMetaData(adapter);
            Option optionTable = gwEntities.Options.Where(p => p.Name == "CompanyName").FirstOrDefault();
            LDARLocationPlant ldp = new LDARLocationPlant();
            if (optionTable != null)
            {
                ldp.PlantId = 0;
                ldp.PlantDescription = optionTable.Value;
                ldp.showInTablet = true;
            }

            List<LDARLocationPlant> ldpList = new List<LDARLocationPlant>();
            ldpList.Add(ldp);

            return ldpList;
        }

        public static List<LDARProcessUnit> GetProcessUnits()
        {
            //DataAccessAdapter adapter = GetGuideware2Adapter();
           // LinqMetaData md = new LinqMetaData(adapter);
            //find which location contains the process unit, default to "location1" if we can't figure it out
            Option optionTable = gwEntities.Options.Where(p => p.Name.Contains("Location") && p.Value.ToUpper().Contains("UNIT") ).FirstOrDefault();
            List<LDARProcessUnit> ldu = new List<LDARProcessUnit>();
            //if (optionTable != null)
            //{
            //    switch (optionTable.Name)
            //    {
            //        case "Location1Name":
            //            ldu = gwEntities.Location1.Select(p => new LDARProcessUnit() { ProcessUnitId = p.Location1Id, UnitDescription = p.Description, UnitCode = p.Location1Code, showInTablet = true }).ToList();
            //            break;
            //        case "Location2Name":
            //            ldu = gwEntities.Location2.Select(p => new LDARProcessUnit() { ProcessUnitId = p.Location2Id, UnitDescription = p.Description, UnitCode = p.Location2Code, showInTablet = true }).ToList();
            //            break;
            //        case "Location3Name":
            //            ldu = gwEntities.Location3.Select(p => new LDARProcessUnit() { ProcessUnitId = p.Location3Id, UnitDescription = p.Description, UnitCode = p.Location3Code, showInTablet = true }).ToList();
            //            break;
            //    }
            //}
            //else
            //{
                ldu = gwEntities.Location1.Select(p => new LDARProcessUnit() { ProcessUnitId = p.Location1_ID, UnitDescription = p.Description, UnitCode = p.Location1_Code, showInTablet = true }).ToList();
            //}

            return ldu;
        }

        public static List<LDARArea> GetAreas()
        {
            //DataAccessAdapter adapter = GetGuideware2Adapter();
            //LinqMetaData md = new LinqMetaData(adapter);
            //find which location contains the areas, default to "location2" if we can't figure it out
            Option optionTable = gwEntities.Options.Where(p => p.Name.Contains("Location") && p.Value.ToUpper().Contains("AREA")).FirstOrDefault();
            List<LDARArea> lda = new List<LDARArea>();
            //if (optionTable != null)
            //{
            //    switch (optionTable.Name)
            //    {
            //        case "Location1Name":
            //            lda = gwEntities.Location1.Select(p => new LDARArea() { AreaId = p.Location1Id, AreaDescription = p.Description, AreaCode = p.Location1Code, showInTablet = true }).ToList();
            //            break;
            //        case "Location2Name":
            //            lda = gwEntities.Location2.Select(p => new LDARArea() { AreaId = p.Location2Id, AreaDescription = p.Description, AreaCode = p.Location2Code, showInTablet = true }).ToList();
            //            break;
            //        case "Location3Name":
            //            lda = gwEntities.Location3.Select(p => new LDARArea() { AreaId = p.Location3Id, AreaDescription = p.Description, AreaCode = p.Location3Code, showInTablet = true }).ToList();
            //            break;
            //    }
            //}
            //else
            //{
                lda = gwEntities.Location2.Select(p => new LDARArea() { AreaId = p.Location2_ID, AreaDescription = p.Description, AreaCode = p.Location2_Code, UnitCode = p.Location1_Code, showInTablet = true }).ToList();
            //}

            return lda;
        }

        public static List<LDAREquipment> GetEquipment()
        {
            //DataAccessAdapter adapter = GetGuideware2Adapter();
            //LinqMetaData md = new LinqMetaData(adapter);
            //find which location contains the equipment, default to "location3" if we can't figure it out
            Option optionTable = gwEntities.Options.Where(p => p.Name.Contains("Location") && p.Value.ToUpper().Contains("EQUIPMENT")).FirstOrDefault();
            List<LDAREquipment> lde = new List<LDAREquipment>();
            //if (optionTable != null)
            //{
            //    switch (optionTable.Name)
            //    {
            //        case "Location1Name":
            //            lde = gwEntities.Location1.Select(p => new LDAREquipment() { EquipmentId = p.Location1Id, EquipmentDescription = p.Description, EquipmentCode = p.Location1Code, showInTablet = true }).ToList();
            //            break;
            //        case "Location2Name":
            //            lde = gwEntities.Location2.Select(p => new LDAREquipment() { EquipmentId = p.Location2Id, EquipmentDescription = p.Description, EquipmentCode = p.Location2Code, showInTablet = true }).ToList();
            //            break;
            //        case "Location3Name":
            //            lde = gwEntities.Location3.Select(p => new LDAREquipment() { EquipmentId = p.Location3Id, EquipmentDescription = p.Description, EquipmentCode = p.Location3Code, showInTablet = true }).ToList();
            //            break;
            //        default:
                        lde = gwEntities.Location3.Select(p => new LDAREquipment() { EquipmentId = p.Location3_ID, EquipmentDescription = p.Description, EquipmentCode = p.Location3_Code, AreaCode = p.Location2_Code, UnitCode = p.Location1_Code, showInTablet = true }).ToList();
            //            break;
            //    }
            //}

            return lde;
        }

        public static List<LDARManufacturer> GetManufacturers()
        {
            //DataAccessAdapter adapter = GetGuideware2Adapter();
            //LinqMetaData md = new LinqMetaData(adapter);
            //TODO: Make table not in guideware2 DAL, need to add
            //List<LDARManufacturer> manufacturers = gwEntities.Make.Select(p => new LDARManufacturer() { ManufacturerId = p.ManufacturerId, ComponentClassId = p.ComponentClassId, ComponentTypeId = p.ComponentTypeId, ManufacturerCode = p.ManufacturerCode, ProductDescription = p.ProductDescription, showInTablet = true }).ToList();
            return new List<LDARManufacturer>();
        }

        public static List<LDARChemicalState> GetChemicalStates()
        {
            //DataAccessAdapter adapter = GetGuideware2Adapter();
            //LinqMetaData md = new LinqMetaData(adapter);
            List<LDARChemicalState> chemicalStates = gwEntities.ServiceTypes.Select(c => new LDARChemicalState() { ChemicalStateId = c.ServiceType_ID, ChemicalState = c.Description, showInTablet = true }).ToList();
            return chemicalStates;
        }

        public static List<LDAROption> GetLDAROptions()
        {
            //DataAccessAdapter adapter = GetGuideware2Adapter();
            //LinqMetaData md = new LinqMetaData(adapter);
            List<LDAROption> ldarOptions = gwEntities.Options.Select(c => new LDAROption() { OptionId = c.Option_ID, OptionName = c.Name, OptionValue = c.Value }).ToList();
            return ldarOptions;
        }

        public static List<LDARComponentClassType> GetComponentClassTypes()
        {
            //DataAccessAdapter adapter = GetGuideware2Adapter();
            //LinqMetaData md = new LinqMetaData(adapter);

            List<LDARComponentClassType> componentClassTypes = (from cc in gwEntities.ComponentTypes
                                                                from ct in gwEntities.SubTypes
                                                                    .Where(o => cc.ComponentType_ID == o.ComponentType_ID) //|| cc.ComponentTypeId != null)
                                                                select new LDARComponentClassType { ComponentClassId = cc.ComponentType_ID, ComponentClass = cc.Description, ClassDescription = cc.Description, ComponentTypeId = ct.SubType_ID, ComponentType = ct.Description, showInTablet = true, childType = true, parentType = true }).ToList();

            return componentClassTypes;
        }

        public static List<LDARComponentStream> GetComponentStreams()
        {
           // DataAccessAdapter adapter = GetGuideware2Adapter();
           // LinqMetaData md = new LinqMetaData(adapter);
            List<LDARComponentStream> componentStreams = gwEntities.ProductStreams.Select(s => new LDARComponentStream() { ComponentStreamId = s.Product_ID, ComponentStream = s.Description, StreamDescription = s.Description, showInTablet = true }).ToList();
            return componentStreams;
        }

        public static List<LDARComponent> GetExistingComponents()
        {
           // DataAccessAdapter adapter = GetGuideware2Adapter();
            //LinqMetaData md = new LinqMetaData(adapter);
            List<LDARComponent> components = new List<LDARComponent>();
            List<Data.LDAR.Guideware.Component> gwComponents = gwEntities.Components.Select(c => c).ToList();

            //DataAccessAdapter adapter2 = GetGuideware2Adapter();
           // LinqMetaData md2 = new LinqMetaData(adapter);
            List<LDAROOSDescription> oosReasons = gwEntities.POOS.Select(t => new LDAROOSDescription() { OOSId = t.POOS_ID, Permanent = true, showInTablet = true }).ToList();

            foreach (Data.LDAR.Guideware.Component gwComp in gwComponents)
            {
                LDARComponent newComp = new LDARComponent();

                newComp.AreaID = 0;
                newComp.ChemicalStateId = gwComp.ServiceType_ID;
                newComp.ChemicalStreamId = gwComp.Product_ID;
                int accessCode = 1;
                if (gwComp.DTM)
                {
                    if (gwComp.UTM)
                    {
                        accessCode = 4;
                    }
                    else
                    {
                        accessCode = 2;
                    }
                }
                else
                {
                    if (gwComp.UTM)
                    {
                        accessCode = 3;
                    }
                    else
                    {
                        accessCode = 1;
                    }
                }

                newComp.ComponentCategoryId = accessCode;
                newComp.ComponentClassId = gwComp.ComponentType_ID;
                newComp.ComponentReasonId = gwComp.DTM_ID;
                newComp.ComponentUTMReasonId = gwComp.UTM_ID;
                newComp.ComponentTag = gwComp.Tag;
                newComp.ComponentTypeId = gwComp.SubType_ID;
                newComp.compProperty = "";
                newComp.Drawing = gwComp.Drawing;
                newComp.Id = gwComp.Component_ID;
                newComp.Location1 = gwComp.Location1_Code;
                newComp.Location2 = gwComp.Location2_Code;
                newComp.Location3 = gwComp.Location3_Code;
                newComp.LocationDescription = gwComp.LocationDescription;
                newComp.ManufacturerID = 0;
                newComp.PlantId = 0;
                newComp.PressureServiceId = Convert.ToInt32(gwComp.VacuumService);
                newComp.ProcessUnitId = 0;
                newComp.Size = gwComp.Size.ToString();
                newComp.TagExtension = gwComp.Extension;
                newComp.RouteSequence = gwComp.Route;
                newComp.POS = gwComp.POOS;
                int poosId = 0;
                if (gwComp.POOS_ID != null)
                {
                    if (gwComp.POOS_ID != 0)
                    {
                        poosId = (int)gwComp.POOS_ID;
                        LDAROOSDescription pos = oosReasons.Where(c => c.OOSId == poosId).FirstOrDefault();
                        if (pos != null) newComp.POSReason = pos.OOSDescription;
                    }
                }
                newComp.TOS = gwComp.OnRemoval;
                components.Add(newComp);

            }

            //= gwEntities.Component.Select(c => new LDARComponent()
            //{
            //    Id = c.ComponentId,
            //    ComponentTag = c.Tag,
            //    TagExtension = c.Extension,
            //    LocationDescription = c.LocationDescription,
            //    Drawing = c.Drawing,
            //    Size = c.Size.ToString(),
            //    ChemicalStateId = c.ServiceTypeId,
            //    ChemicalStreamId = c.ProductId,
            //    ComponentClassId = c.ComponentTypeId,
            //    ComponentTypeId = c.ComponentTypeId,
            //    PlantId = 0,
            //    PressureServiceId = Convert.ToInt32(c.VacuumService),
            //    //todo: defaulting several values here.  need additional handling in TaggedComponent
            //    ProcessUnitId = 0,
            //    ComponentCategoryId = 0,
            //    ComponentReasonId = c.DtmId,
            //    AreaID = 0,
            //    compProperty = "",
            //    ManufacturerID = 0
            //}).ToList();
            
            return components;
        }

        public static List<LDARPressureService> GetPressureServices()
        {
            return new List<LDARPressureService>();
        }

        public static List<LDARTechnician> GetTechnicians()
        {
            //DataAccessAdapter adapter = GetGuideware2Adapter();
            //LinqMetaData md = new LinqMetaData(adapter);
            List<LDARTechnician> technicians = gwEntities.Inspectors.Select(t => new LDARTechnician() { Id = t.Inspector_ID, Name = t.Description, showInTablet = true }).ToList();
            return technicians;
        }

        public static List<LDARCategory> GetCategories()
        {
            //DataAccessAdapter adapter = GetGuideware2Adapter();
            //LinqMetaData md = new LinqMetaData(adapter);
            List<LDARCategory> categories = new List<LDARCategory>();
            categories.Add(new LDARCategory() { CategoryCode = "N", ComponentCategoryID = 1, showInTablet = true });
            categories.Add(new LDARCategory() { CategoryCode = "D", ComponentCategoryID = 2, showInTablet = true });
            categories.Add(new LDARCategory() { CategoryCode = "U", ComponentCategoryID = 3, showInTablet = true });
            categories.Add(new LDARCategory() { CategoryCode = "D/U", ComponentCategoryID = 4, showInTablet = true });
            return categories;
        }

        public static List<LDAROOSDescription> GetOOSReasons()
        {
            //DataAccessAdapter adapter = GetGuideware2Adapter();
            //LinqMetaData md = new LinqMetaData(adapter);
            List<LDAROOSDescription> oosReasons = gwEntities.POOS.Select(t => new LDAROOSDescription() { OOSId = t.POOS_ID, OOSDescription = t.Description, Permanent = true, showInTablet = true }).ToList();
            List<LDAROOSDescription> toosReasons = gwEntities.RemovalReasons.Select(t => new LDAROOSDescription() { OOSId = t.RemovalReason_ID, OOSDescription = t.Description, Permanent = false, showInTablet = true }).ToList();
            oosReasons.AddRange(toosReasons);
            return oosReasons;
        }


        public static List<LDARReason> GetReasons()
        {
            //DataAccessAdapter adapter = GetGuideware2Adapter();
            //LinqMetaData md = new LinqMetaData(adapter);
            List<LDARReason> reasons = gwEntities.UTMs.Select(c => new LDARReason() { ComponentReasonID = c.UTM_ID, ComponentReason = c.Description, ComponentCategoryID = 3, ReasonDescription = c.Description, showInTablet = true }).ToList();
            reasons.AddRange( gwEntities.DTMs.Select(c => new LDARReason() { ComponentReasonID = c.DTM_ID, ComponentReason = c.Description, ComponentCategoryID = 2, ReasonDescription = c.Description, showInTablet = true }).ToList());
            List<LDARReason> combinedReasons = new List<LDARReason>();
            //foreach (LDARReason reason in reasons.Where(c => c.ComponentCategoryID == 3))
            //{
            //    foreach (LDARReason utmReason in reasons.Where(c => c.ComponentCategoryID == 2))
            //    {
            //        LDARReason newReason = new LDARReason();
            //        newReason.ComponentCategoryID = 4;
            //        newReason.ComponentReason = reason.ComponentReason + " / " + utmReason.ComponentReason;
            //        newReason.ComponentReasonID = 0;
            //        newReason.ReasonDescription = reason.ComponentReason + " / " + utmReason.ComponentReason;
            //        newReason.showInTablet = true;
            //        combinedReasons.Add(newReason);
            //    }
            //}

            reasons.AddRange(combinedReasons);

            return reasons;
        }


        public static List<LDARRegulation> GetRegulations()
        {
            //DataAccessAdapter adapter = GetGuideware2Adapter();
           // LinqMetaData md = new LinqMetaData(adapter);
            List<LDARRegulation> regs = gwEntities.Rules.Select(p => new LDARRegulation() { RegulationId = p.Rule_ID, RegulationDescription = p.Description, LicenseKey = "" }).ToList();
            return regs;
        }

        public static List<LDARComplianceGroup> GetComplianceGroups()
        {
            return new List<LDARComplianceGroup>();
        }

        public static int GetGuidewareComponentStreamId(string stream)
        {
            int id = 0;
            try
            {
                //DataAccessAdapter ldAdapter = GetGuideware2Adapter();
                //LinqMetaData md = new LinqMetaData(ldAdapter);

                ProductStream cs = gwEntities.ProductStreams.Where(c => c.Description.ToLower().Trim() == stream.ToLower().Trim()).FirstOrDefault();
                if (cs != null)
                {
                    id = cs.Product_ID;
                }
                else
                {
                    cs = gwEntities.ProductStreams.Where(c => c.Description.ToLower().Trim() == stream.ToLower().Trim()).FirstOrDefault();
                    if (cs != null)
                    {
                        id = cs.Product_ID;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return id;
        }

        public static int GetGuidewareChemicalStateId(string chemicalState)
        {
            int id = 0;
            //DataAccessAdapter ldAdapter = GetGuideware2Adapter();
            //LinqMetaData md = new LinqMetaData(ldAdapter);

            ServiceType cs = gwEntities.ServiceTypes.Where(c => c.Description.ToLower().Trim() == chemicalState.ToLower().Trim()).FirstOrDefault();
            if (cs != null)
            {
                id = cs.ServiceType_ID;
            }
            else
            {
                cs = gwEntities.ServiceTypes.Where(c => c.Description.ToLower().Trim() == chemicalState.ToLower().Trim()).FirstOrDefault();
                if (cs != null)
                {
                    id = cs.ServiceType_ID;
                }
            }
            return id;
        }


        public static int GetGuidewareComponentClassId(string componentClass)
        {
            int id = 0;
            string className = string.Empty;
            Char[] charArray = " - ".ToCharArray();


            //DataAccessAdapter ldAdapter = GetGuideware2Adapter();
           // LinqMetaData md = new LinqMetaData(ldAdapter);

            if (componentClass.Contains(" - "))
            {
                className = componentClass.Split(charArray, 3)[0].ToString().ToLower().Trim();
            }
            else
            {
                className = componentClass;
            }
            
            
            ComponentType cc = gwEntities.ComponentTypes.Where(c => c.Description.ToLower().Trim() == className).FirstOrDefault();
            if (cc != null)
            {
                id = cc.ComponentType_ID;
            }
            return id;
        }

        public static int GetGuidewareComponentTypeId(string componentClass)
        {
            int id = 0;
            string className = string.Empty;
            string typeName = string.Empty;
            Char[] charArray = " - ".ToCharArray();
            //DataAccessAdapter ldAdapter = GetGuideware2Adapter();
           // LinqMetaData md = new LinqMetaData(ldAdapter);

            if (componentClass.Contains(" - "))
            {
                className = componentClass.Split(charArray, 3)[0].ToString().ToLower().Trim();
                typeName = componentClass.Split('-')[1].ToString().ToLower().Trim();
            }
            else
            {
                return 0;
            }

            ComponentType cc = gwEntities.ComponentTypes.Where(c => c.Description.ToLower().Trim() == className).FirstOrDefault();
            if (cc == null) return 0;

            SubType ct = gwEntities.SubTypes.Where(c => c.Description.ToLower().Trim() == typeName && c.ComponentType_ID == cc.ComponentType_ID).FirstOrDefault();

            if (ct != null)
            {
                id = ct.SubType_ID;
            }
            return id;
        }

        public static int GetGuidewareServiceTypeId(string serviceType)
        {
            int id = 0;
            //DataAccessAdapter leakdasAdapter = GetGuideware2Adapter();
            //LinqMetaData md = new LinqMetaData(leakdasAdapter);

            ServiceType cs = gwEntities.ServiceTypes.Where(s => s.Description == serviceType).FirstOrDefault();
            if (cs != null)
            {
                id = cs.ServiceType_ID;
            }
            return id;
        }

        public static int GetGuidewareComponentBatchId(string componentBatch)
        {
            return 0;
        }

        public static int GetGuidewareLocation1Id(string LocationDescription)
        {
            int id = 0;
            //DataAccessAdapter leakdasAdapter = GetGuideware2Adapter();
            //LinqMetaData md = new LinqMetaData(leakdasAdapter);

            Location1 pu = gwEntities.Location1.Where(c => c.Description.ToLower().Trim() == LocationDescription.ToLower().Trim()).FirstOrDefault();
            if (pu != null)
            {
                id = pu.Location1_ID;
            }
            return id;
        }

        public static int GetGuidewareLocation2Id(string LocationDescription)
        {
            int id = 0;
            //DataAccessAdapter leakdasAdapter = GetGuideware2Adapter();
            //LinqMetaData md = new LinqMetaData(leakdasAdapter);

            Location2 pu = gwEntities.Location2.Where(c => c.Description.ToLower().Trim() == LocationDescription.ToLower().Trim()).FirstOrDefault();
            if (pu != null)
            {
                id = pu.Location2_ID;
            }
            return id;
        }

        public static int GetGuidewareLocation3Id(string LocationDescription)
        {
            int id = 0;
           // DataAccessAdapter leakdasAdapter = GetGuideware2Adapter();
           // LinqMetaData md = new LinqMetaData(leakdasAdapter);

            Location3 pu = gwEntities.Location3.Where(c => c.Description.ToLower().Trim() == LocationDescription.ToLower().Trim()).FirstOrDefault();
            if (pu != null)
            {
                id = pu.Location3_ID;
            }
            return id;
        }

        public static string GetGuidewareLocation1Code(string Description)
        {
            string id = string.Empty;
           // DataAccessAdapter leakdasAdapter = GetGuideware2Adapter();
           // LinqMetaData md = new LinqMetaData(leakdasAdapter);

            Location1 pu = gwEntities.Location1.Where(c => c.Description.ToLower().Trim() == Description.ToLower().Trim()).FirstOrDefault();
            if (pu != null)
            {
                id = pu.Location1_Code;
            }
            return id;
        }

        public static string GetGuidewareLocation2Code(string Description)
        {
            string id = string.Empty;
            //DataAccessAdapter leakdasAdapter = GetGuideware2Adapter();
            //LinqMetaData md = new LinqMetaData(leakdasAdapter);

            Location2 pu = gwEntities.Location2.Where(c => c.Description.ToLower().Trim() == Description.ToLower().Trim()).FirstOrDefault();
            if (pu != null)
            {
                id = pu.Location2_Code;
            }
            return id;
        }

        public static string GetGuidewareLocation3Code(string Description)
        {
            string id = string.Empty;
            //DataAccessAdapter leakdasAdapter = GetGuideware2Adapter();
            //LinqMetaData md = new LinqMetaData(leakdasAdapter);

            Location3 pu = gwEntities.Location3.Where(c => c.Description.ToLower().Trim() == Description.ToLower().Trim()).FirstOrDefault();
            if (pu != null)
            {
                id = pu.Location3_Code;
            }
            return id;
        }

        public static int GetOOSCode(string OOSDescription)
        {
            int id = 0;
            //DataAccessAdapter leakdasAdapter = GetGuideware2Adapter();
            //LinqMetaData md = new LinqMetaData(leakdasAdapter);

            POO pu = gwEntities.POOS.Where(c => c.Description.ToLower().Trim() == OOSDescription.ToLower().Trim()).FirstOrDefault();
            if (pu != null)
            {
                id = pu.POOS_ID;
            }
            return id;
        }

        public static int GetRemovalCode(string OOSDescription)
        {
            int id = 0;
            //DataAccessAdapter leakdasAdapter = GetGuideware2Adapter();
            //LinqMetaData md = new LinqMetaData(leakdasAdapter);

            RemovalReason pu = gwEntities.RemovalReasons.Where(c => c.Description.ToLower().Trim() == OOSDescription.ToLower().Trim()).FirstOrDefault();
            if (pu != null)
            {
                id = pu.RemovalReason_ID;
            }
            return id;
        }

        public static int GetGuidewareCategoryCodeId(string catCode)
        {

            switch (catCode)
            {
                case "N":
                    return 1;
                case "D":
                    return 2;
                case "U":
                    return 3;
                case "D/U":
                    return 4;
                default:
                    return 1;
            }

        }

        public static int GetGuidewareDTMReasonCodeId(string reasonCode)
        {
            int id = 0;
            //DataAccessAdapter leakdasAdapter = GetGuideware2Adapter();
           // LinqMetaData md = new LinqMetaData(leakdasAdapter);

            DTM cc = gwEntities.DTMs.Where(c => c.Description.Equals(reasonCode)).FirstOrDefault();
            if (cc != null)
            {
                id = cc.DTM_ID;
            }
            else
            {
                DTM cd = gwEntities.DTMs.Where(c => c.Description.Equals(reasonCode)).FirstOrDefault();
                if (cd != null)
                {
                    id = cd.DTM_ID;
                }
            }

            return id;
        }

        public static int GetGuidewareUTMReasonCodeId(string reasonCode)
        {
            int id = 0;
            //DataAccessAdapter leakdasAdapter = GetGuideware2Adapter();
           // LinqMetaData md = new LinqMetaData(leakdasAdapter);

            UTM cc = gwEntities.UTMs.Where(c => c.Description.Equals(reasonCode)).FirstOrDefault();
            if (cc != null)
            {
                id = cc.UTM_ID;
            }
            else
            {
                DTM cd = gwEntities.DTMs.Where(c => c.Description.Equals(reasonCode)).FirstOrDefault();
                if (cd != null)
                {
                    id = cd.DTM_ID;
                }
            }

            return id;
        }

        public static string getGuidewareUnitLocation()
        {
           // MainForm.CurrentProjectData.LDARData.LDAROptions.Where(c => c.   
            return string.Empty;
        }

        public static string GetLeakDASCategoryCodeFromSheetValues(string DTM, string UTM)
        {

            string returnVal = string.Empty;

            if (UTM.ToLower().StartsWith("not"))
            {
                if (DTM.ToLower().StartsWith("not"))
                {
                    returnVal = "N";
                }
                else
                {
                    returnVal = "D";
                }
            }
            else
            {
                returnVal = "U";
            }

            return returnVal;
        }

        public static string GetLeakDASReasonCodeFromSheetValues(string DTM, string UTM)
        {

            string returnVal = string.Empty;

            if (UTM.ToLower().StartsWith("not"))
            {
                if (!DTM.ToLower().StartsWith("not") && DTM.Contains("-"))
                {
                    returnVal = DTM.Split('-')[1].Trim();
                }
            }
            else
            {
                if (UTM.Contains("-"))
                    returnVal = UTM.Split('-')[1].Trim();
            }

            return returnVal;

        }

        #endregion


    }
}

