using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvInt.Win32.FieldTech.Containers
{
    public class LDARComponent : CSVable
    {
        public int Id { get; set; }
        public string ComponentTag { get; set; }
        public string TagExtension { get; set; }
        public int PlantId { get; set; }
        public int ProcessUnitId { get; set; }
        public int? AreaID { get; set; }
        public int? ManufacturerID { get; set; }
        public string compProperty { get; set; }
        public string Drawing { get; set; }
        public string LocationDescription { get; set; }
        public string Size { get; set; }
        public int PressureServiceId { get; set; }
        public string Location1 { get; set; }
        public string Location2 { get; set; }
        public string Location3 { get; set; }
        public int? ComponentCategoryId { get; set; }
        public int? ComponentReasonId { get; set; }
        //added 4/1/15 DW
        public int? ComponentUTMReasonId { get; set; }
        public int? ChemicalStateId { get; set; }
        public int? ChemicalStreamId { get; set; }
        public int? ComponentClassId { get; set; }
        public int? ComponentTypeId { get; set; }
        //added 4/1/15 DW
        public bool POS { get; set; }
        //added 4/1/15 DW
        public bool TOS { get; set; }
        //added 4/15/15 DW
        public string POSReason { get; set; }
        //added 4/15/15 DW
        public string TOSReason { get; set; }
        //added 4/14/15 DW
        public double RouteSequence { get; set; }
        //added 8/26/15 DW
        public bool CVS { get; set; }
        public string CVSReason { get; set; }

        /// <summary>
        /// Get Header
        /// </summary>
        /// <returns></returns>
        public override string GetHeader()
        {
            List<string> values = new List<string>();
            values.Add("\"Id\"");
            values.Add("\"ComponentTag\"");
            values.Add("\"TagExtension\"");
            values.Add("\"PlantId\"");
            values.Add("\"ProcessUnitId\"");
            values.Add("\"AreaID\"");
            values.Add("\"ManufacturerID\"");
            values.Add("\"compProperty\"");
            values.Add("\"Drawing\"");
            values.Add("\"LocationDescription\"");
            values.Add("\"Size\"");
            values.Add("\"PressureServiceId\"");
            values.Add("\"Location1\"");
            values.Add("\"Location2\"");
            values.Add("\"Location3\"");
            values.Add("\"ComponentCategoryId\"");
            values.Add("\"ComponentReasonId\"");
            values.Add("\"ComponentUTMReasonId\"");
            values.Add("\"ChemicalStateId\"");
            values.Add("\"ChemicalStreamId\"");
            values.Add("\"ComponentClassId\"");
            values.Add("\"ComponentTypeId\"");
            values.Add("\"POS\"");
            values.Add("\"TOS\"");
            values.Add("\"POSReason\"");
            values.Add("\"TOSReason\"");
            values.Add("\"RouteSequence\"");
            values.Add("\"CVS\"");
            values.Add("\"CVSReason\"");
            return String.Join(",", values.ToArray());
        }

        /// <summary>
        /// Override String function
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            List<string> values = new List<string>();
            values.Add("\"" + Id + "\"");
            values.Add("\"" + ComponentTag + "\"");
            values.Add("\"" + TagExtension + "\"");
            values.Add("\"" + PlantId + "\"");
            values.Add("\"" + ProcessUnitId + "\"");
            values.Add("\"" + AreaID + "\"");
            values.Add("\"" + ManufacturerID + "\"");
            values.Add("\"" + compProperty + "\"");
            values.Add("\"" + Drawing + "\"");
            values.Add("\"" + LocationDescription + "\"");
            values.Add("\"" + Size + "\"");
            values.Add("\"" + PressureServiceId + "\"");
            values.Add("\"" + Location1 + "\"");
            values.Add("\"" + Location2 + "\"");
            values.Add("\"" + Location3 + "\"");
            values.Add("\"" + ComponentCategoryId + "\"");
            values.Add("\"" + ComponentReasonId + "\"");
            values.Add("\"" + ComponentUTMReasonId + "\"");
            values.Add("\"" + ChemicalStateId + "\"");
            values.Add("\"" + ChemicalStreamId + "\"");
            values.Add("\"" + ComponentClassId + "\"");
            values.Add("\"" + ComponentTypeId + "\"");
            values.Add("\"" + POS + "\"");
            values.Add("\"" + TOS + "\"");
            values.Add("\"" + POSReason + "\"");
            values.Add("\"" + TOSReason + "\"");
            values.Add("\"" + RouteSequence.ToString() + "\"");
            values.Add("\"" + CVS.ToString() + "\"");
            values.Add("\"" + CVSReason.ToString() + "\"");
            return String.Join(",", values.ToArray());
            return String.Join(",", values.ToArray());
        }

        /// <summary>
        /// LDAR component default Constructor
        /// </summary>
        public LDARComponent() { }

        /// <summary>
        /// LDAR component Constructor
        /// </summary>
        /// <param name="refData"></param>
        public LDARComponent(LDARComponent refData)
        {
            Id = refData.Id;
            ComponentTag = refData.ComponentTag;
            TagExtension = refData.TagExtension;
            PlantId = refData.PlantId;
            ProcessUnitId = refData.ProcessUnitId;
            AreaID = refData.AreaID;
            ManufacturerID = refData.ManufacturerID;
            compProperty = refData.compProperty;
            Drawing = refData.Drawing;
            LocationDescription = refData.LocationDescription;
            Size = refData.Size;
            PressureServiceId = refData.PressureServiceId;
            Location1 = refData.Location1;
            Location2 = refData.Location2;
            Location3 = refData.Location3;
            ComponentCategoryId = refData.ComponentCategoryId;
            ComponentReasonId = refData.ComponentReasonId;
            ComponentUTMReasonId = refData.ComponentUTMReasonId;
            ChemicalStateId = refData.ChemicalStateId;
            ChemicalStreamId = refData.ChemicalStreamId;
            ComponentClassId = refData.ComponentClassId;
            ComponentTypeId = refData.ComponentTypeId;
            POS = refData.POS;
            TOS = refData.TOS;
            POSReason = refData.POSReason;
            TOSReason = refData.TOSReason;
            RouteSequence = refData.RouteSequence;
            CVS = refData.CVS;
            CVSReason = refData.CVSReason;
        }

    }
}
