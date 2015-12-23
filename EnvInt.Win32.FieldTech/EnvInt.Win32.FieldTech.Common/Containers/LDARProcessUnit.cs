using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvInt.Win32.FieldTech.Containers
{
    public class LDARProcessUnit : CSVable
    {
        public int ProcessUnitId { get; set; }
        public string UnitDescription { get; set; }
        public string UnitCode { get; set; }
        public bool showInTablet { get; set; }

        public override string GetHeader()
        {
            List<string> values = new List<string>();
            values.Add("\"ProcessUnitId\"");
            values.Add("\"UnitDescription\"");
            values.Add("\"UnitCode\"");
            values.Add("\"showInTablet\"");
            return String.Join(",", values.ToArray());
        }

        public override string ToString()
        {
            List<string> values = new List<string>();
            values.Add("\"" + ProcessUnitId + "\"");
            values.Add("\"" + UnitDescription + "\"");
            values.Add("\"" + UnitCode + "\"");
            values.Add("\"" + showInTablet + "\"");
            return String.Join(",", values.ToArray());
        }

        public LDARProcessUnit() { }

        public LDARProcessUnit(LDARProcessUnit refData)
        {
            ProcessUnitId = refData.ProcessUnitId;
            UnitDescription = refData.UnitDescription;
            UnitCode = refData.UnitCode;
            showInTablet = refData.showInTablet;
        }
    }
}
