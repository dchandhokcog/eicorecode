using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvInt.Win32.FieldTech.Containers
{
    public class LDAREquipment : CSVable
    {
        public int EquipmentId { get; set; }
        public string EquipmentDescription { get; set; }
        public string EquipmentCode { get; set; }
        public string AreaCode { get; set; }
        public string UnitCode { get; set; }
        public bool showInTablet { get; set; }

        public override string GetHeader()
        {
            List<string> values = new List<string>();
            values.Add("\"EquipmentId\"");
            values.Add("\"EquipmentDescription\"");
            values.Add("\"EquipmentCode\"");
            values.Add("\"AreatCode\"");
            values.Add("\"UnitCode\"");
            values.Add("\"showInTablet\"");
            return String.Join(",", values.ToArray());
        }

        public override string ToString()
        {
            List<string> values = new List<string>();
            values.Add("\"" + EquipmentId + "\"");
            values.Add("\"" + EquipmentDescription + "\"");
            values.Add("\"" + EquipmentCode + "\"");
            values.Add("\"" + AreaCode + "\"");
            values.Add("\"" + UnitCode + "\"");
            values.Add("\"" + showInTablet + "\"");
            return String.Join(",", values.ToArray());
        }

        public LDAREquipment() { }

        public LDAREquipment(LDAREquipment refData)
        {
            EquipmentId = refData.EquipmentId;
            EquipmentDescription = refData.EquipmentDescription;
            EquipmentCode = refData.EquipmentCode;
            AreaCode = refData.AreaCode;
            UnitCode = refData.UnitCode;
            showInTablet = refData.showInTablet;
        }
    }
}
