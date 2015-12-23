using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvInt.Win32.FieldTech.Containers
{
    public class LDARComponentStream : CSVable
    {
        public int ComponentStreamId { get; set; }
        public string ComponentStream { get; set; }
        public string StreamDescription { get; set; }
        public bool showInTablet { get; set; }

        public override string GetHeader()
        {
            List<string> values = new List<string>();
            values.Add("\"ComponentStreamId\"");
            values.Add("\"ComponentStream\"");
            values.Add("\"StreamDescription\"");
            values.Add("\"showInTablet\"");
            return String.Join(",", values.ToArray());
        }

        public override string ToString()
        {
            List<string> values = new List<string>();
            values.Add("\"" + ComponentStreamId + "\"");
            values.Add("\"" + ComponentStream + "\"");
            values.Add("\"" + StreamDescription + "\"");
            values.Add("\"" + showInTablet + "\"");
            return String.Join(",", values.ToArray());
        }

        public LDARComponentStream() { }

        public LDARComponentStream(LDARComponentStream refData)
        {
            ComponentStreamId = refData.ComponentStreamId;
            ComponentStream = refData.ComponentStream;
            StreamDescription = refData.StreamDescription;
            showInTablet = refData.showInTablet;
        }
    }
}
