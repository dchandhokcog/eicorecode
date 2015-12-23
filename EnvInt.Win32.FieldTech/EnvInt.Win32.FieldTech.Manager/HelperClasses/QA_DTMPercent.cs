using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Manager.HelperClasses
{
    class QA_DTMPercent : QAItem
    {
        private double _percentThreshold = 0;

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
                return "DTM Percentage";
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

        public QA_DTMPercent(double percentThreshold = 3)
        {
            _affectedComponents = new List<TaggedComponent>();
            _percentThreshold = percentThreshold;
        }

        public override void doQA()
        {

            _affectedComponents.Clear();
            
            System.Diagnostics.Debug.WriteLine("Starting " + this.QACheck);

            double AllCount = 0;
            double DTMCount = 0;

            foreach (TaggedComponent tc in MainForm.CollectedTags)
            {
                AllCount ++;
                if (MainForm.CurrentProjectData.LDARDatabaseType.Contains("LeakDAS"))
                {
                    if (tc.Access != null) //for leakdas, look for DTM in text
                    {
                        if (tc.Access.Contains("DTM"))
                        {
                            DTMCount++;
                            _affectedComponents.Add(tc);
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(tc.Access)) //for guideware look to see if Access is populated
                    {
                        DTMCount++;
                        _affectedComponents.Add(tc);
                    } 
                }
            }

            if (AllCount == 0)
            {
                WarningMsg = "";
                QAWarning = false;
                return;
            }

            double dtmPercent = (DTMCount / AllCount) * 100;

            if (dtmPercent > _percentThreshold)
            {
                WarningMsg = Math.Round(((DTMCount / AllCount) * 100),2).ToString() + "% of tagged components are DTMs";
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

