using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvInt.Win32.FieldTech.Containers
{
    public class LDARPressureService : CSVable
    {
        public int PressureServiceId { get; set; }
        public string PressureService { get; set; }
        public string ServiceDescription { get; set; }
        public bool showInTablet { get; set; }

        public override string GetHeader()
        {
            List<string> values = new List<string>();
            values.Add("\"PressureServiceId\"");
            values.Add("\"PressureService\"");
            values.Add("\"ServiceDescription\"");
            values.Add("\"showInTablet\"");
            return String.Join(",", values.ToArray());
        }

        public override string ToString()
        {
            List<string> values = new List<string>();
            values.Add("\"" + PressureServiceId + "\"");
            values.Add("\"" + PressureService + "\"");
            values.Add("\"" + ServiceDescription + "\"");
            values.Add("\"" + showInTablet + "\"");
            return String.Join(",", values.ToArray());
        }

        public LDARPressureService() { }

        public LDARPressureService(LDARPressureService refData)
        {
            PressureServiceId = refData.PressureServiceId;
            PressureService = refData.PressureService;
            ServiceDescription = refData.ServiceDescription;
            showInTablet = refData.showInTablet;
        }
    }
}
