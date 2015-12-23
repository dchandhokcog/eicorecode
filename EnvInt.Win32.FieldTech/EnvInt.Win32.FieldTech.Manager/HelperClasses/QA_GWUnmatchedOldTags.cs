using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Manager.HelperClasses
{
    class QA_GWUnmatchedOldTags : QAItem
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
                return "Unmatched Old Tags";
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

        public QA_GWUnmatchedOldTags()
        {
            _affectedComponents = new List<TaggedComponent>();
        }

        public override void doQA()
        {

            _affectedComponents.Clear();
            
            System.Diagnostics.Debug.WriteLine("Starting " + this.QACheck);

            foreach (TaggedComponent tc in MainForm.CollectedTags)
            {
                //try to find matching existing component by unit
                LDARComponent otMatch = new LDARComponent();

                LDARProcessUnit ldu;
                //can't make a match if we don't have a process unit
                if (Properties.Settings.Default.StrictUnitChecking)
                {
                    ldu = MainForm.CurrentProjectData.LDARData.ProcessUnits.Where(c => c.UnitDescription.ToUpper().Trim() == tc.Unit.ToUpper().Trim()).FirstOrDefault();
                    if (ldu != null)
                    {
                        otMatch = MainForm.CurrentProjectData.LDARData.ExistingComponents.Where(e => e.ComponentTag == tc.PreviousTag && e.TagExtension == tc.PreviousTagExtension && e.Location1 == ldu.UnitCode).FirstOrDefault();
                        if (otMatch == null && !string.IsNullOrEmpty(tc.PreviousTag))
                        {
                            tc.WarningMessage = addMessage("Old Tag Not Found", tc.WarningMessage);
                            _affectedComponents.Add(tc);
                        }
                    }
                    else
                    {
                        tc.WarningMessage = addMessage("Old Tag Not Found", tc.WarningMessage);
                        _affectedComponents.Add(tc);
                    }
                }
                else
                {
                    //if strict unit checking is disabled, try to find a match regardless of unit
                    otMatch = MainForm.CurrentProjectData.LDARData.ExistingComponents.Where(e => e.ComponentTag == tc.PreviousTag && e.TagExtension == tc.PreviousTagExtension).FirstOrDefault();
                    if (otMatch == null && !string.IsNullOrEmpty(tc.PreviousTag))
                    {
                        tc.WarningMessage = addMessage("Old Tag Not Found", tc.WarningMessage);
                        _affectedComponents.Add(tc);
                    }
                }
            }

            if (_affectedComponents.Count > 0)
            {
                WarningMsg = _affectedComponents.Count.ToString() + "old tags not found";
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

