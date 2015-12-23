using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvInt.Win32.FieldTech.Containers
{
    public class LDARCVSReason : CSVable
    {
        public int CVSId { get; set; }
        public string CVSDescription { get; set; }

        public override string GetHeader()
        {
            List<string> values = new List<string>();
            values.Add("\"CVSId\"");
            values.Add("\"CVSDescription\"");
            return String.Join(",", values.ToArray());
        }

        public override string ToString()
        {
            List<string> values = new List<string>();
            values.Add("\"" + CVSId + "\"");
            values.Add("\"" + CVSDescription + "\"");
            return String.Join(",", values.ToArray());
        }

        public LDARCVSReason() { }

        public LDARCVSReason(LDARCVSReason refData)
        {
            CVSId = refData.CVSId;
            CVSDescription = refData.CVSDescription;
        }
    }
}
