using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Manager.HelperClasses
{
    class QA_GWAccess : QAItem
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
                return "DTM Reasons in LDAR Database";
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

        public QA_GWAccess()
        {
            _projectData = MainForm.CurrentProjectData;
            _affectedComponents = new List<TaggedComponent>();
        }

        public override void doQA()
        {
            _affectedComponents.Clear();

            System.Diagnostics.Debug.WriteLine("Starting " + this.QACheck);

            List<String> DTMReasons = new List<string>();

            foreach (TaggedComponent tag in MainForm.CollectedTags)
            {
                if (!String.IsNullOrEmpty(tag.Access))
                {
                    if (!DTMReasons.Contains(tag.Access)) DTMReasons.Add(tag.Access);
                }
            }

            //decide
            foreach (string reason in DTMReasons)
            {
                //long stateID = LeakDAS.GetLeakdasChemicalStateId(state);
                LDARReason ldarReason = _projectData.LDARData.ComponentReasons.Where(c => c.ReasonDescription == reason && c.ComponentCategoryID == 2).FirstOrDefault();
                if (ldarReason == null)
                {
                    _affectedComponents.AddRange(MainForm.CollectedTags.Where(c => c.Access == reason));
                    _affectedComponents.ForEach(c => c.ErrorMessage = addMessage("DTM Reason not In LDAR Database", c.ErrorMessage));
                }

            }

            if (_affectedComponents.Count > 0)
            {
                qaMessage = _affectedComponents.Count.ToString() + " Tags with no matching DTM Reason in LDAR Database";
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


