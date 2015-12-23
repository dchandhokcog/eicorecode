using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvInt.Win32.FieldTech.Containers
{
    public class LDARManufacturer : CSVable
    {
        public int ManufacturerId { get; set; }
        public string ProductDescription { get; set; }
        public int? ComponentClassId { get; set; }
        public int? ComponentTypeId { get; set; }
        public string ManufacturerCode { get; set; }
        public bool showInTablet { get; set; }

        public override string GetHeader()
        {
            List<string> values = new List<string>();
            values.Add("\"ManufacturerId\"");
            values.Add("\"ProductDescription\"");
            values.Add("\"ComponentClassId\"");
            values.Add("\"ComponentTypeId\"");
            values.Add("\"ManufacturerCode\"");
            values.Add("\"showInTablet\"");
            return String.Join(",", values.ToArray());
        }

        public override string ToString()
        {
            List<string> values = new List<string>();
            values.Add("\"" + ManufacturerId + "\"");
            values.Add("\"" + ProductDescription + "\"");
            values.Add("\"" + ComponentClassId + "\"");
            values.Add("\"" + ComponentTypeId + "\"");
            values.Add("\"" + ManufacturerCode + "\"");
            values.Add("\"" + showInTablet + "\"");
            return String.Join(",", values.ToArray());
        }

        public LDARManufacturer() { }

        public LDARManufacturer(LDARManufacturer refData)
        {
            ManufacturerId = refData.ManufacturerId;
            ProductDescription = refData.ProductDescription;
            ComponentClassId = refData.ComponentClassId;
            ComponentTypeId = refData.ComponentTypeId;
            ManufacturerCode = refData.ManufacturerCode;
            showInTablet = refData.showInTablet;
        }
    }
}
