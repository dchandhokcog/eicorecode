using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvInt.Win32.FieldTech.Containers
{
    public class LDARReason : CSVable
    {
        public int ComponentReasonID { get; set; }
        public string ComponentReason { get; set; }
        public int? ComponentCategoryID { get; set; }
        public string ReasonDescription { get; set; }
        public bool showInTablet { get; set; }

        public override string GetHeader()
        {
            List<string> values = new List<string>();
            values.Add("\"ComponentReasonID\"");
            values.Add("\"ComponentReason\"");
            values.Add("\"ComponentCategoryID\"");
            values.Add("\"ReasonDescription\"");
            values.Add("\"showInTablet\"");
            return String.Join(",", values.ToArray());
        }

        public override string ToString()
        {
            List<string> values = new List<string>();
            values.Add("\"" + ComponentReasonID + "\"");
            values.Add("\"" + ComponentReason + "\"");
            values.Add("\"" + ComponentCategoryID + "\"");
            values.Add("\"" + ReasonDescription + "\"");
            values.Add("\"" + showInTablet + "\"");
            return String.Join(",", values.ToArray());
        }

        public LDARReason() { }

        public LDARReason(LDARReason refData)
        {
            ComponentReasonID = refData.ComponentReasonID;
            ComponentReason = refData.ComponentReason;
            ComponentCategoryID = refData.ComponentCategoryID;
            ReasonDescription = refData.ReasonDescription;
            showInTablet = refData.showInTablet;
        }
    }
}
