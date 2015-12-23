using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Manager.HelperClasses
{
    class QA_GWNewTagExists : QAItem
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
                return "New Tag Exists";
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

        public QA_GWNewTagExists()
        {
            _affectedComponents = new List<TaggedComponent>();
        }

        public override void doQA()
        {

            _affectedComponents.Clear();
            
            System.Diagnostics.Debug.WriteLine("Starting " + this.QACheck);

            foreach (TaggedComponent tc in MainForm.CollectedTags)
            {
                if (string.IsNullOrEmpty(tc.PreviousTag) || (tc.PreviousTag != tc.LDARTag || tc.Extension != tc.PreviousTagExtension))
                {
                    LDARComponent ldc = MainForm.CurrentProjectData.LDARData.getLDARComponentByTag(tc.LDARTag, tc.Extension);
                    if (ldc != null)
                    {
                        _affectedComponents.Add(tc);
                        tc.ErrorMessage = addMessage("New Tag Exists", tc.ErrorMessage);
                    }
                }
            }

            if (_affectedComponents.Count > 0)
            {
                qaMessage = _affectedComponents.Count.ToString() + " New tags already exist in target database";
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
