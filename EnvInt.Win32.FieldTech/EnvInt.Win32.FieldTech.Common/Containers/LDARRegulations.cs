using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvInt.Win32.FieldTech.Containers
{
    public class LDARRegulation : CSVable
    {
        public int RegulationId { get; set; }
        public string RegulationDescription { get; set; }
        public string LicenseKey { get; set; }

        public override string GetHeader()
        {
            List<string> values = new List<string>();
            values.Add("\"RegulationId\"");
            values.Add("\"RegulationDescription\"");
            values.Add("\"LicenseKey\"");
            return String.Join(",", values.ToArray());
        }

        public override string ToString()
        {
            List<string> values = new List<string>();
            values.Add("\"" + RegulationId + "\"");
            values.Add("\"" + RegulationDescription + "\"");
            values.Add("\"" + LicenseKey + "\"");
            return String.Join(",", values.ToArray());
        }

        public LDARRegulation() { }

        public LDARRegulation(LDARRegulation refData)
        {
            RegulationId = refData.RegulationId;
            RegulationDescription = refData.RegulationDescription;
            LicenseKey = refData.LicenseKey;
        }
    }
}
