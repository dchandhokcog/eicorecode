using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvInt.Win32.FieldTech.Containers
{
    public class LDARArea : CSVable
    {
        public int AreaId { get; set; }
        public string AreaDescription { get; set; }
        public string AreaCode { get; set; }
        public string UnitCode { get; set; }
        public bool showInTablet { get; set; }

        public override string GetHeader()
        {
            List<string> values = new List<string>();
            values.Add("\"AreaId\"");
            values.Add("\"AreaDescription\"");
            values.Add("\"AreaCode\"");
            values.Add("\"UnitCode\"");
            values.Add("\"showInTablet\"");
            return String.Join(",", values.ToArray());
        }

        public override string ToString()
        {
            List<string> values = new List<string>();
            values.Add("\"" + AreaId + "\"");
            values.Add("\"" + AreaDescription + "\"");
            values.Add("\"" + AreaCode + "\"");
            values.Add("\"" + UnitCode + "\"");
            values.Add("\"" + showInTablet + "\"");
            return String.Join(",", values.ToArray());
        }

        public LDARArea() { }

        public LDARArea(LDARArea refData)
        {
            AreaId = refData.AreaId;
            AreaDescription = refData.AreaDescription;
            AreaCode = refData.AreaCode;
            UnitCode = refData.UnitCode;
            showInTablet = refData.showInTablet;
        }
    }
}
