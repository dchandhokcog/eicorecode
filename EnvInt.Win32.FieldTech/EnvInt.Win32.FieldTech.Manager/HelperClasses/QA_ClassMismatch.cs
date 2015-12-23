using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Manager.HelperClasses
{
    class QA_ClassMismatch : QAItem
    {

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
                return "Class Mismatches";
            }
        }

        public override string Message
        {
            get
            {
                return passMessage;
            }
        }

        public override bool QAPassed
        {
            get
            {
                return true;
            }

        }

        public QA_ClassMismatch()
        {
            _affectedComponents = new List<TaggedComponent>();
        }

        public override void doQA()
        {

            _affectedComponents.Clear();
            
            System.Diagnostics.Debug.WriteLine("Starting " + this.QACheck);

            foreach (TaggedComponent tc in MainForm.CollectedTags)
            {
                //try to find matching existing component by unit unless strict unit checking is disabled
                LDARComponent otMatch = new LDARComponent();
                string otClass = string.Empty;
                string currentClass = string.Empty;
                LDARProcessUnit pu;
                if (Properties.Settings.Default.StrictUnitChecking)
                {
                    pu = MainForm.CurrentProjectData.LDARData.ProcessUnits.Where(c => c.UnitDescription == tc.Unit).FirstOrDefault();
                    if (pu != null)
                    {
                        int unitId = pu.ProcessUnitId;
                        otMatch = MainForm.CurrentProjectData.LDARData.ExistingComponents.Where(e => e.ComponentTag == tc.PreviousTag && e.ProcessUnitId == unitId).FirstOrDefault();
                    }
                }
                else
                {
                    //let's still try to find a match if StrictUnitChecking is disabled
                    otMatch = MainForm.CurrentProjectData.LDARData.ExistingComponents.Where(e => e.ComponentTag == tc.PreviousTag).FirstOrDefault();
                }

                if (otMatch != null)
                {
                    if (tc.ComponentType.Contains("-"))
                    {
                        currentClass = tc.ComponentType.Split('-')[0].Trim().ToLower();
                    }
                    else
                    {
                        currentClass = tc.ComponentType;
                    }
                    LDARComponentClassType ctMatch = MainForm.CurrentProjectData.LDARData.ComponentClassTypes.Where(d => d.ComponentClassId == otMatch.ComponentClassId).FirstOrDefault();
                    if (ctMatch != null) otClass = ctMatch.ComponentClass;
                }
                if (!otClass.Equals(currentClass))
                {
                    tc.WarningMessage = addMessage("Class Mismatch - " + otClass, tc.WarningMessage);
                    _affectedComponents.Add(tc);
                }
            }

            if (_affectedComponents.Count > 0)
            {
                WarningMsg = _affectedComponents.Count.ToString() + "class mismatches found";
                QAWarning = true;
            }
            else
            {
                WarningMsg = "";
                QAWarning = false;
            }
        }

    }
}

