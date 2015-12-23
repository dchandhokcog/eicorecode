using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Manager.HelperClasses
{
    class QA_GWEquipment : QAItem
    {
        
        private string qaMessage = string.Empty;
        private bool qaPassed = true;

        public override string Device
        {
            get
            {
                return "Staged Components";
            }

        }

        public override string QACheck
        {
            get
            {
                return "Equipment in LDAR Database";
            }
        }

        public override string Message
        {
            get
            {
                return qaMessage;
            }
        }

        public override bool QAPassed
        {
            get
            {
                return qaPassed;
            }

        }

        public QA_GWEquipment()
        {
            _projectData = MainForm.CurrentProjectData;
            _affectedComponents = new List<TaggedComponent>();
        }

        public override void doQA()
        {

            _affectedComponents.Clear();
            
            System.Diagnostics.Debug.WriteLine("Starting " + this.QACheck);
            
            List<String> equipment = new List<string>();
            bool foundEmpty = false;

            foreach (TaggedComponent tag in MainForm.CollectedTags)
            {
                if (!String.IsNullOrEmpty(tag.Equipment))
                {
                    if (!equipment.Contains(tag.Equipment + "|" + tag.Area + "|" + tag.Unit)) equipment.Add(tag.Equipment + "|" + tag.Area + "|" + tag.Unit);
                }
                else
                {
                    tag.ErrorMessage = addMessage("Empty Equipment", tag.ErrorMessage);
                    _affectedComponents.Add(tag);
                    foundEmpty = true;
                }
            }

            //decide
            foreach (string equip in equipment)
            {

                string partEquipment = equip.Split('|')[0];
                string partArea = equip.Split('|')[1];
                string partUnit = equip.Split('|')[2];

                LDARProcessUnit pu = MainForm.CurrentProjectData.LDARData.ProcessUnits.Where(a => a.UnitDescription == partUnit).FirstOrDefault();
                LDARArea ar = MainForm.CurrentProjectData.LDARData.Areas.Where(a => a.AreaDescription == partArea).FirstOrDefault();

                if (pu == null || ar == null)
                {
                    _affectedComponents.AddRange(MainForm.CollectedTags.Where(c => c.Equipment + "|" + c.Area + "|" + c.Unit == equip));
                    _affectedComponents.ForEach(c => c.ErrorMessage = addMessage("Equipment/Area/Unit Combination Not In LDAR Database", c.ErrorMessage));
                }
                else
                {
                    LDAREquipment lde = _projectData.LDARData.Equipment.Where(c => c.EquipmentDescription == partEquipment && c.UnitCode == pu.UnitCode && c.AreaCode == ar.AreaCode).FirstOrDefault();

                    if (lde == null)
                    {
                        _affectedComponents.AddRange(MainForm.CollectedTags.Where(c => c.Equipment + "|" + c.Area + "|" + c.Unit == equip));
                        _affectedComponents.ForEach(c => c.ErrorMessage = addMessage("Equipment/Area/Unit Combination Not In LDAR Database", c.ErrorMessage));
                    }
                }

            }

            if (_affectedComponents.Count > 0)
            {
                qaMessage = _affectedComponents.Count.ToString() + " Tags with no matching equipment/area/unit combination in LDAR Database";
                if (foundEmpty) qaMessage += " / Empty equipment";
                qaPassed = false;
            }
            else
            {
                qaMessage = passMessage;
                qaPassed = true;
            }
        }

    }
}

