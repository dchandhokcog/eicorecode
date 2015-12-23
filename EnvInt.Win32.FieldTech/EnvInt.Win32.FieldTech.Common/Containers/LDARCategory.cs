using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvInt.Win32.FieldTech.Containers
{

    public class LDARCategory : CSVable
    {
        public int ComponentCategoryID { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryDescription { get; set; }
        public bool showInTablet { get; set; }

        public override string GetHeader()
        {
            List<string> values = new List<string>();
            values.Add("\"ComponentCategoryID\"");
            values.Add("\"CategoryCode\"");
            values.Add("\"CategoryDescription\"");
            values.Add("\"showInTablet\"");
            return String.Join(",", values.ToArray());
        }

        public override string ToString()
        {
            List<string> values = new List<string>();
            values.Add("\"" + ComponentCategoryID + "\"");
            values.Add("\"" + CategoryCode + "\"");
            values.Add("\"" + CategoryDescription + "\"");
            values.Add("\"" + showInTablet + "\"");
            return String.Join(",", values.ToArray());
        }

        public LDARCategory() { }

        public LDARCategory(LDARCategory refData)
        {
            ComponentCategoryID = refData.ComponentCategoryID;
            CategoryCode = refData.CategoryCode;
            CategoryDescription = refData.CategoryDescription;
            showInTablet = refData.showInTablet;
        }
    }
}
