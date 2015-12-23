using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Manager.HelperClasses
{
    class QA_LDAccess : QAItem
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
                return "Accessibility codes in LDAR Database";
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

        public QA_LDAccess()
        {
            _projectData = MainForm.CurrentProjectData;
            _affectedComponents = new List<TaggedComponent>();
        }

        public override void doQA()
        {

            _affectedComponents.Clear();
            
            System.Diagnostics.Debug.WriteLine("Starting " + this.QACheck);
            
            List<string> access = new List<string>();
            List<string> reason = new List<string>();
            bool foundEmpty = false;

            foreach (TaggedComponent tag in MainForm.CollectedTags)
            {
                if (!String.IsNullOrEmpty(tag.Access))
                {
                    if (!access.Contains(tag.Access)) access.Add(tag.Access);
                }
                else
                {
                    tag.ErrorMessage = addMessage("Access Empty", tag.ErrorMessage);
                    _affectedComponents.Add(tag);
                    foundEmpty = true;
                }
            }

            //decide
            foreach (string acc in access)
            {
                string ldAccess = string.Empty;
                string ldReason = string.Empty;

                if (acc.Contains('-'))
                {
                    ldAccess = acc.Split('-')[0].Trim();
                    ldReason = acc.Split('-')[1].Trim();
                }
                else
                {
                    ldAccess = acc;
                    ldReason = "";
                }

                if (ldAccess.Length > 1) ldAccess = ldAccess.Substring(0, 1);

                //int accessId = LeakDAS.GetLeakdasCategoryCodeId(ldAccess);
                //int reasonId = LeakDAS.GetLeakdasCategoryReasonCodeId(ldReason);

                LDARCategory ldarCategory = _projectData.LDARData.ComponentCategories.Where(c => c.CategoryCode == ldAccess).FirstOrDefault();
                LDARReason ldarReason = _projectData.LDARData.ComponentReasons.Where(c => c.ReasonDescription == ldReason).FirstOrDefault();

                string qaMsg = string.Empty;

                if (ldarCategory == null)
                {
                    qaMsg = "Access code not in LDAR Database";
                    if (ldarReason == null && ldReason != "") qaMsg = "Reason/" + qaMsg;
                }


                if (qaMsg != string.Empty)
                {
                    _affectedComponents.AddRange(MainForm.CollectedTags.Where(c => c.Access == acc));
                    _affectedComponents.ForEach(c => c.ErrorMessage = addMessage(qaMsg, c.ErrorMessage));
                }

            }

            if (_affectedComponents.Count > 0)
            {
                qaMessage = _affectedComponents.Count.ToString() + " Tags with no matching accessibility or reason in LDAR Database";
                if (foundEmpty) qaMessage += " / Empty accessibility state";
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



