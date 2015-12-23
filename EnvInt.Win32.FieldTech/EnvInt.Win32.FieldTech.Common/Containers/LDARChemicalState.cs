using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnvInt.Win32.FieldTech.Containers
{
    public class LDARChemicalState : CSVable
    {
        public int ChemicalStateId { get; set; }
        public string ChemicalState { get; set; }
        public string StateDescription { get; set; }
        public bool showInTablet { get; set; }

        public override string GetHeader()
        {
            List<string> values = new List<string>();
            values.Add("\"ChemicalStateId\"");
            values.Add("\"ChemicalState\"");
            values.Add("\"StateDescription\"");
            values.Add("\"showInTablet\"");
            return String.Join(",", values.ToArray());
        }

        public override string ToString()
        {
            List<string> values = new List<string>();
            values.Add("\"" + ChemicalStateId + "\"");
            values.Add("\"" + ChemicalState + "\"");
            values.Add("\"" + StateDescription + "\"");
            values.Add("\"" + showInTablet + "\"");
            return String.Join(",", values.ToArray());
        }

        public LDARChemicalState() { }

        public LDARChemicalState(LDARChemicalState refData)
        {
            ChemicalStateId = refData.ChemicalStateId;
            ChemicalState = refData.ChemicalState;
            StateDescription = refData.StateDescription;
            showInTablet = refData.showInTablet;
        }
    }
}
