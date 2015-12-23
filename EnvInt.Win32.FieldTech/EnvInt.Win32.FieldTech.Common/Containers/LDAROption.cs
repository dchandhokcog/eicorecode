using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvInt.Win32.FieldTech.Containers
{
    public class LDAROption : CSVable
    {
        public int OptionId { get; set; }
        public string OptionName { get; set; }
        public string OptionValue { get; set; }

        public override string GetHeader()
        {
            List<string> values = new List<string>();
            values.Add("\"OptionId\"");
            values.Add("\"OptionName\"");
            values.Add("\"OptionValue\"");
            return String.Join(",", values.ToArray());
        }

        public override string ToString()
        {
            List<string> values = new List<string>();
            values.Add("\"" + OptionId + "\"");
            values.Add("\"" + OptionName + "\"");
            values.Add("\"" + OptionValue + "\"");
            return String.Join(",", values.ToArray());
        }

        public LDAROption() { }

        public LDAROption(LDAROption refData)
        {
            OptionId = refData.OptionId;
            OptionName = refData.OptionName;
            OptionValue = refData.OptionValue; 
        }
    }
}
