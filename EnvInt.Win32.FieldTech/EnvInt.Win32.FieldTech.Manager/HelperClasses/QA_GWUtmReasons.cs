using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Manager.HelperClasses
{
    class QA_GWUtmReasons : QAItem
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
                return "UTM Reasons in LDAR Database";
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

        public QA_GWUtmReasons()
        {
            _projectData = MainForm.CurrentProjectData;
            _affectedComponents = new List<TaggedComponent>();
        }

        public override void doQA()
        {
            _affectedComponents.Clear();

            System.Diagnostics.Debug.WriteLine("Starting " + this.QACheck);

            List<String> UTMReasons = new List<string>();

            foreach (TaggedComponent tag in MainForm.CollectedTags)
            {
                if (!String.IsNullOrEmpty(tag.UTMReason))
                {
                    if (!UTMReasons.Contains(tag.UTMReason)) UTMReasons.Add(tag.UTMReason);
                }
            }

            //decide
            foreach (string reason in UTMReasons)
            {
                //long stateID = LeakDAS.GetLeakdasChemicalStateId(state);
                LDARReason ldarReason = _projectData.LDARData.ComponentReasons.Where(c => c.ReasonDescription == reason && c.ComponentCategoryID == 3).FirstOrDefault();
                if (ldarReason == null)
                {
                    _affectedComponents.AddRange(MainForm.CollectedTags.Where(c => c.UTMReason == reason));
                    _affectedComponents.ForEach(c => c.ErrorMessage = addMessage("UTM Reason not In LDAR Database", c.ErrorMessage));
                }

            }

            if (_affectedComponents.Count > 0)
            {
                qaMessage = _affectedComponents.Count.ToString() + " Tags with no matching UTM Reason in LDAR Database";
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


