using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvInt.Win32.FieldTech.Containers
{
    public class LDAROOSDescription : CSVable
    {
        public int OOSId { get; set; }
        public string OOSDescription { get; set; }
        public bool Permanent { get; set; }
        public bool showInTablet { get; set; }

        public override string GetHeader()
        {
            List<string> values = new List<string>();
            values.Add("\"OOSId\"");
            values.Add("\"OOSDescription\"");
            values.Add("\"Permanent\"");
            values.Add("\"showInTablet\"");
            return String.Join(",", values.ToArray());
        }

        public override string ToString()
        {
            List<string> values = new List<string>();
            values.Add("\"" + OOSId + "\"");
            values.Add("\"" + OOSDescription + "\"");
            values.Add("\"" + Permanent.ToString() + "\"");
            values.Add("\"" + showInTablet.ToString() + "\"");
            return String.Join(",", values.ToArray());
        }

        public LDAROOSDescription() { }

        public LDAROOSDescription(LDAROOSDescription refData)
        {
            OOSId = refData.OOSId;
            OOSDescription = refData.OOSDescription;
            Permanent = refData.Permanent;
            showInTablet = refData.showInTablet;
        }
    }
}
