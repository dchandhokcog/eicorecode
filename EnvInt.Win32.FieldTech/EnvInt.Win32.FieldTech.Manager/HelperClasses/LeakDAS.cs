using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using EnvInt.Data.DAL.LeakDAS4.DatabaseSpecific;
//using EnvInt.Data.DAL.LeakDAS4.EntityClasses;
//using EnvInt.Data.DAL.LeakDAS4.FactoryClasses;
//using EnvInt.Data.DAL.LeakDAS4.HelperClasses;
//using EnvInt.Data.DAL.LeakDAS4.Linq;
using EnvInt.Win32.FieldTech.Containers;

//using SD.LLBLGen.Pro.ORMSupportClasses;
//using SD.LLBLGen.Pro.LinqSupportClasses;

using EnvInt.Data.LDAR.LeakDAS;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace EnvInt.Win32.FieldTech.Manager.HelperClasses
{
    public static class LeakDAS
    {

        public static LeakDASv4 md;

        #region LeakDAS Utilities

        public static void initializeDatabase(string ConnectionString)
        {
            md = new LeakDASv4(ConnectionString);
        }

        public static List<LDARLocationPlant> GetLocationPlants()
        {
            //DataAccessAdapter adapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(adapter);
            List<LDARLocationPlant> plants = md.LocationPlants.Select(p => new LDARLocationPlant() { PlantId = p.PlantID, PlantDescription = p.PlantDescription, showInTablet = true }).ToList();
            return plants;
        }

        public static List<LDARProcessUnit> GetProcessUnits()
        {
            //DataAccessAdapter adapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(adapter);
            List<LDARProcessUnit> processUnits = md.ProcessUnits.Select(p => new LDARProcessUnit() { ProcessUnitId = p.ProcessUnitID, UnitDescription = p.UnitDescription, UnitCode = "", showInTablet = true }).ToList();
            return processUnits;
        }

        public static List<LDARArea> GetAreas()
        {
            //DataAccessAdapter adapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(adapter);
            List<LDARArea> areas = md.LocationAreas.Select(p => new LDARArea() { AreaId = p.AreaID, AreaDescription = p.AreaDescription, AreaCode = "", showInTablet = true }).ToList();
            return areas;
        }

        public static List<LDARManufacturer> GetManufacturers()
        {
           // DataAccessAdapter adapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(adapter);
            List<LDARManufacturer> manufacturers = md.Manufacturers.Select(p => new LDARManufacturer() { ManufacturerId = p.ManufacturerID, ComponentClassId = p.ComponentClassID, ComponentTypeId = p.ComponentTypeID, ManufacturerCode = p.ManufacturerCode, ProductDescription = p.ProductDescription, showInTablet = true }).ToList();
            return manufacturers;
        }

        public static List<LDARChemicalState> GetChemicalStates()
        {
            //DataAccessAdapter adapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(adapter);
            List<LDARChemicalState> chemicalStates = md.ChemicalStates.Select(c => new LDARChemicalState() { ChemicalStateId = c.ChemicalStateID, ChemicalState = c.ChemicalState1, showInTablet = true }).ToList();
            return chemicalStates;
        }

        public static List<LDARComponentClassType> GetComponentClassTypes()
        {
            //DataAccessAdapter adapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(adapter);

            List<LDARComponentClassType> componentClassTypes = (from cc in md.ComponentClasses
                                                                join ct in md.ComponentTypes
                                                                on cc.ComponentClassID equals ct.ComponentClassID
                                                                select new LDARComponentClassType { ComponentClassId = cc.ComponentClassID, ComponentClass = cc.ComponentClass1, ClassDescription = cc.ClassDescription, ComponentTypeId = ct.ComponentTypeID, ComponentType = ct.ComponentType1, showInTablet = true, childType = true, parentType = true }).ToList();
            List<LDARComponentClassType> componentClassOnly = (from cc in md.ComponentClasses
                                                               select new LDARComponentClassType { ComponentClassId = cc.ComponentClassID, ComponentClass = cc.ComponentClass1, ClassDescription = cc.ClassDescription, ComponentTypeId = 0, ComponentType = "", showInTablet = true, childType = true, parentType = true }).ToList();

            if (componentClassOnly != null)
            {
                componentClassTypes.AddRange(componentClassOnly);
            }

            return componentClassTypes;
        }

        public static List<LDARComponentStream> GetComponentStreams()
        {
           // DataAccessAdapter adapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(adapter);
            List<LDARComponentStream> componentStreams = md.ComponentStreams.Select(s => new LDARComponentStream() { ComponentStreamId = s.ComponentStreamID, ComponentStream = s.ComponentStream1, StreamDescription = s.StreamDescription, showInTablet = true }).ToList();
            return componentStreams;
        }

        public static List<LDARComponent> GetExistingComponents()
        {
            //DataAccessAdapter adapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(adapter);
            List<LDARComponent> components = md.Components.Select(c => new LDARComponent() { Id = c.ComponentID, ComponentTag = c.ComponentTag, LocationDescription = c.LocationDescription,
                Drawing = c.Drawing, Size = c.Size.ToString(), ChemicalStateId = c.ChemicalStateID, ChemicalStreamId = c.ComponentStreamID, ComponentClassId = c.ComponentClassID,
                ComponentTypeId = c.ComponentTypeID, PlantId = c.PlantID, PressureServiceId = c.PressureServiceID,
                ProcessUnitId = c.ProcessUnitID, ComponentCategoryId = c.ComponentCategoryID, ComponentReasonId = c.ComponentReasonID, AreaID = c.AreaID, compProperty = c.Property, ManufacturerID = c.ManufacturerID, RouteSequence = (c.FITRouteSequence == null ? 0 : (int)c.FITRouteSequence), POS = c.Status != 1, POSReason = c.ReasonforStatus }).ToList();
            return components;
        }

        public static List<LDARPressureService> GetPressureServices()
        {
            //DataAccessAdapter adapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(adapter);
            List<LDARPressureService> pressureServices = md.ComponentPressureServices.Select(p => new LDARPressureService() { PressureServiceId = p.PressureServiceID, PressureService = p.PressureService, ServiceDescription = p.ServiceDescription, showInTablet = true }).ToList();
            return pressureServices;
        }

        public static List<LDARTechnician> GetTechnicians()
        {
            //DataAccessAdapter adapter = GetLeakdasAdapter();
           // LinqMetaData md = new LinqMetaData(adapter);
            List<LDARTechnician> technicians = md.LDARUsers.Select(t => new LDARTechnician() { Id = t.LDARUserID, Name = t.LDARUserName, showInTablet = true }).ToList();
            return technicians;
        }

        public static List<LDARCategory> GetCategories()
        {
            //DataAccessAdapter adapter = GetLeakdasAdapter();
           // LinqMetaData md = new LinqMetaData(adapter);
            List<LDARCategory> components = md.ComponentCategories.Select(c => new LDARCategory() { ComponentCategoryID = c.ComponentCategoryID, CategoryDescription = c.CategoryDescription, CategoryCode = c.CategoryCode, showInTablet = true }).ToList();
            return components;
        }

        public static List<LDARReason> GetReasons()
        {
            //DataAccessAdapter adapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(adapter);
            List<LDARReason> components = md.ComponentReasons.Select(c => new LDARReason() { ComponentReasonID = c.ComponentReasonID, ComponentReason = c.ComponentReason1, ComponentCategoryID = c.ComponentCategoryID, ReasonDescription = c.ReasonDescription, showInTablet = true }).ToList();
            return components;
        }

        public static List<LDAROOSDescription> GetOOSCodes()
        {
            //DataAccessAdapter adapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(adapter);
            List<LDAROOSDescription> oosCodes = md.OutofServiceCodes.Select(c => new LDAROOSDescription() { OOSId = c.OOSCodeID, OOSDescription = c.OOSDescription, Permanent = true, showInTablet = true }).ToList();
            return oosCodes;
        }

        public static List<LDARRegulation> GetRegulations()
        {
            //DataAccessAdapter adapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(adapter);
            List<LDARRegulation> regs = md.Regulations.Select(p => new LDARRegulation() { RegulationId = p.RegulationID, RegulationDescription = p.RegulationDescription, LicenseKey = p.LicenseKey }).ToList();
            return regs;
        }

        public static List<LDARComplianceGroup> GetComplianceGroups()
        {
            //DataAccessAdapter adapter = GetLeakdasAdapter();
            //inqMetaData md = new LinqMetaData(adapter);
            List<LDARComplianceGroup> compGroup = md.ComplianceGroups.Select(p => new LDARComplianceGroup() { ComplianceGroupId = p.ComplianceGroupID, ComplianceGroupDescription = p.ComplianceGroupDescription }).ToList();
            return compGroup;
        }

        public static int GetLeakdasComponentStreamId(string stream)
        {
            int id = 0;
            try
            {
                //DataAccessAdapter ldAdapter = GetLeakdasAdapter();
                //LinqMetaData md = new LinqMetaData(ldAdapter);

                ComponentStream cs = md.ComponentStreams.Where(c => c.StreamDescription.ToLower().Trim() == stream.ToLower().Trim()).FirstOrDefault();
                if (cs != null)
                {
                    id = cs.ComponentStreamID;
                }
                else
                {
                    cs = md.ComponentStreams.Where(c => c.ComponentStream1.ToLower().Trim() == stream.ToLower().Trim()).FirstOrDefault();
                    if (cs != null)
                    {
                        id = cs.ComponentStreamID;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return id;
        }

        public static int GetLeakdasChemicalStateId(string chemicalState)
        {
            int id = 0;
            //DataAccessAdapter ldAdapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(ldAdapter);

            ChemicalState cs = md.ChemicalStates.Where(c => c.ChemicalState1.ToLower().Trim() == chemicalState.ToLower().Trim()).FirstOrDefault();
            if (cs != null)
            {
                id = cs.ChemicalStateID;
            }
            else
            {
                cs = md.ChemicalStates.Where(c => c.StateDescription.ToLower().Trim() == chemicalState.ToLower().Trim()).FirstOrDefault();
                if (cs != null)
                {
                    id = cs.ChemicalStateID;
                }
            }
            return id;
        }


        public static int GetLeakdasComponentClassId(string componentClass)
        {
            int id = 0;
           // DataAccessAdapter ldAdapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(ldAdapter);

            ComponentClass cc = md.ComponentClasses.Where(c => c.ComponentClass1.ToLower().Trim() == componentClass.ToLower().Trim()).FirstOrDefault();
            if (cc != null)
            {
                id = cc.ComponentClassID;
            }
            return id;
        }

        public static int GetLeakdasComponentTypeId(string componentType, int componentClassId)
        {
            int id = 0;
            //DataAccessAdapter ldAdapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(ldAdapter);

            ComponentType ct = md.ComponentTypes.Where(c => c.ComponentType1.ToLower().Trim() == componentType.ToLower().Trim() && c.ComponentClassID == componentClassId).FirstOrDefault();
            if (ct != null)
            {
                id = ct.ComponentTypeID;
            }
            return id;
        }

        public static int GetLeakdasServiceTypeId(string serviceType)
        {
            int id = 0;
            //DataAccessAdapter leakdasAdapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(leakdasAdapter);

            ChemicalState cs = md.ChemicalStates.Where(s => s.ChemicalState1 == serviceType).FirstOrDefault();
            if (cs != null)
            {
                id = cs.ChemicalStateID;
            }
            return id;
        }

        public static int GetLeakdasComponentBatchId(string componentBatch)
        {
            int id = 0;
            //DataAccessAdapter leakdasAdapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(leakdasAdapter);

            ComponentBatch cb = md.ComponentBatches.Where(c => c.ComponentBatch1.ToLower().Trim() == componentBatch.ToLower().Trim()).FirstOrDefault();
            if (cb != null)
            {
                id = cb.ComponentBatchID;
            }
            return id;
        }

        public static int GetLeakdasProcessUnitId(string processUnit)
        {
            int id = 0;
            //DataAccessAdapter leakdasAdapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(leakdasAdapter);

            ProcessUnit pu = md.ProcessUnits.Where(c => c.UnitDescription.ToLower().Trim() == processUnit.ToLower().Trim()).FirstOrDefault();
            if (pu != null)
            {
                id = pu.ProcessUnitID;
            }
            return id;
        }

        public static int GetLeakdasCategoryCodeId(string catCode)
        {
            int id = 0;
           // DataAccessAdapter leakdasAdapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(leakdasAdapter);

            ComponentCategory cc = md.ComponentCategories.Where(c => c.CategoryCode.Equals(catCode)).FirstOrDefault();
            if (cc != null)
            {
                id = cc.ComponentCategoryID;
            }
            return id;
        }

        public static int GetLeakdasCategoryReasonCodeId(string reasonCode)
        {
            int id = 0;
            //DataAccessAdapter leakdasAdapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(leakdasAdapter);

            ComponentReason cc = md.ComponentReasons.Where(c => c.ReasonDescription.Equals(reasonCode)).FirstOrDefault();
            if (cc != null)
            {
                id = cc.ComponentReasonID;
            }
            return id;
        }

        public static int GetLeakdasOOSCodeId(string reasonDescription)
        {
            int id = 0;
            //DataAccessAdapter leakdasAdapter = GetLeakdasAdapter();
            //LinqMetaData md = new LinqMetaData(leakdasAdapter);

            OutofServiceCode cc = md.OutofServiceCodes.Where(c => c.OOSDescription.Equals(reasonDescription)).FirstOrDefault();
            if (cc != null)
            {
                id = cc.OOSCodeID;
            }
            return id;
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
