using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvInt.Win32.FieldTech.Containers
{
    public class LDARComplianceGroup : CSVable
    {
        public int ComplianceGroupId { get; set; }
        public string ComplianceGroupDescription { get; set; }

        public override string GetHeader()
        {
            List<string> values = new List<string>();
            values.Add("\"ComplianceGroupId\"");
            values.Add("\"ComplianceGroupDescription\"");
            return String.Join(",", values.ToArray());
        }

        public override string ToString()
        {
            List<string> values = new List<string>();
            values.Add("\"" + ComplianceGroupId + "\"");
            values.Add("\"" + ComplianceGroupDescription + "\"");
            return String.Join(",", values.ToArray());
        }

        public LDARComplianceGroup() { }

        public LDARComplianceGroup(LDARComplianceGroup refData)
        {
            ComplianceGroupId = refData.ComplianceGroupId;
            ComplianceGroupDescription = refData.ComplianceGroupDescription;
        }
    }
}
