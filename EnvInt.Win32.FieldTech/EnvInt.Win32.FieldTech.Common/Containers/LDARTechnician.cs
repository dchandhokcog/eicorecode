using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvInt.Win32.FieldTech.Containers
{
    public class LDARTechnician : CSVable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool showInTablet { get; set; }

        public override string GetHeader()
        {
            List<string> values = new List<string>();
            values.Add("\"Id\"");
            values.Add("\"Name\"");
            values.Add("\"showInTablet\"");
            return String.Join(",", values.ToArray());
        }

        public override string ToString()
        {
            List<string> values = new List<string>();
            values.Add("\"" + Id + "\"");
            values.Add("\"" + Name + "\"");
            values.Add("\"" + showInTablet + "\"");
            return String.Join(",", values.ToArray());
        }

        public LDARTechnician() { }

        public LDARTechnician(LDARTechnician refData)
        {
            Id = refData.Id;
            Name = refData.Name;
            showInTablet = refData.showInTablet;
        }
    }
}
