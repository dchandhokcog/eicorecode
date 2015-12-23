using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Manager.HelperClasses
{
    class QA_LDUnit : QAItem
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
                return "Units in LDAR Database";
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

        public QA_LDUnit()
        {
            _projectData = MainForm.CurrentProjectData;
            _affectedComponents = new List<TaggedComponent>();
        }

        public override void doQA()
        {

            _affectedComponents.Clear();
            
            System.Diagnostics.Debug.WriteLine("Starting " + this.QACheck);
            
            List<String> units = new List<string>();
            bool foundEmpty = false;

            foreach ( TaggedComponent tag in MainForm.CollectedTags)
            {
                if (!String.IsNullOrEmpty(tag.Unit))
                {
                    if (!units.Contains(tag.Unit)) units.Add(tag.Unit);
                }
                else
                {
                    tag.ErrorMessage = addMessage("Empty Unit", tag.ErrorMessage);
                    _affectedComponents.Add(tag);
                    foundEmpty = true;
                }
            }

            //decide
            foreach (string unit in units)
            {

                LDARProcessUnit ldu = _projectData.LDARData.ProcessUnits.Where(c => c.UnitDescription == unit).FirstOrDefault();
                
                if (ldu == null)
                {
                    _affectedComponents.AddRange(MainForm.CollectedTags.Where(c => c.Unit == unit));
                    _affectedComponents.ForEach(c => c.ErrorMessage = addMessage("Unit Not In LDAR Database",c.ErrorMessage));
                }

            }

            if (_affectedComponents.Count > 0)
            {
                qaMessage = _affectedComponents.Count.ToString() + " Tags with no matching unit in LDAR Database";
                if (foundEmpty) qaMessage += " / Empty unit";
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

