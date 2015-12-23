using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvInt.Win32.FieldTech.Containers
{
    public class LDARComponentClassType : CSVable
    {
        public int ComponentClassId { get; set; }
        public string ComponentClass { get; set; }
        public string ClassDescription { get; set; }
        public int ComponentTypeId { get; set; }
        public string ComponentType { get; set; }
        public string TypeDescription { get; set; }
        public bool showInTablet { get; set; }
        public bool childType { get; set; }
        public bool parentType { get; set; }

        public override string GetHeader()
        {
            List<string> values = new List<string>();
            values.Add("\"ComponentClassId\"");
            values.Add("\"ComponentClass\"");
            values.Add("\"ClassDescription\"");
            values.Add("\"ComponentTypeId\"");
            values.Add("\"ComponentType\"");
            values.Add("\"TypeDescription\"");
            values.Add("\"showInTablet\"");
            values.Add("\"childType\"");
            values.Add("\"parentType\"");
            return String.Join(",", values.ToArray());
        }

        public override string ToString()
        {
            List<string> values = new List<string>();
            values.Add("\"" + ComponentClassId + "\"");
            values.Add("\"" + ComponentClass + "\"");
            values.Add("\"" + ClassDescription + "\"");
            values.Add("\"" + ComponentTypeId + "\"");
            values.Add("\"" + ComponentType + "\"");
            values.Add("\"" + TypeDescription + "\"");
            values.Add("\"" + showInTablet + "\"");
            values.Add("\"" + childType + "\"");
            values.Add("\"" + parentType + "\"");
            return String.Join(",", values.ToArray());
        }

        public LDARComponentClassType() { }

        public LDARComponentClassType(LDARComponentClassType refData)
        {
            ComponentClassId = refData.ComponentClassId;
            ComponentClass = refData.ComponentClass;
            ClassDescription = refData.ClassDescription;
            ComponentTypeId = refData.ComponentTypeId;
            ComponentType = refData.ComponentType;
            TypeDescription = refData.TypeDescription;
            showInTablet = refData.showInTablet;
            childType = refData.childType;
            parentType = refData.parentType;
        }
    }
}
