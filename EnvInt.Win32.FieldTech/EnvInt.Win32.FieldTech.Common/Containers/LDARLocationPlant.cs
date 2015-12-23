using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvInt.Win32.FieldTech.Containers
{
    public class LDARLocationPlant : CSVable
    {
        public int PlantId { get; set; }
        public string PlantDescription { get; set; }
        public bool showInTablet { get; set; }

        public override string GetHeader()
        {
            List<string> values = new List<string>();
            values.Add("\"PlantId\"");
            values.Add("\"PlantDescription\"");
            values.Add("\"showInTablet\"");
            return String.Join(",", values.ToArray());
        }

        public override string ToString()
        {
            List<string> values = new List<string>();
            values.Add("\"" + PlantId + "\"");
            values.Add("\"" + PlantDescription + "\"");
            values.Add("\"" + showInTablet + "\"");
            return String.Join(",", values.ToArray());
        }

        public LDARLocationPlant() { }

        public LDARLocationPlant(LDARLocationPlant refData)
        {
            PlantId = refData.PlantId;
            PlantDescription = refData.PlantDescription;
            showInTablet = refData.showInTablet;
        }
    }
}
