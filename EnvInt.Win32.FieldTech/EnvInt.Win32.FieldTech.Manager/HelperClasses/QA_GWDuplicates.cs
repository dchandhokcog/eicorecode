using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Manager.HelperClasses
{
    class QA_GWDuplicates : QAItem
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
                return "Duplicate Components";
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

        public QA_GWDuplicates()
        {
            _affectedComponents = new List<TaggedComponent>();
        }

        public override void doQA()
        {

            _affectedComponents.Clear();
            
            System.Diagnostics.Debug.WriteLine("Starting " + this.QACheck);
            
            List<string> LDARTags = new List<string>();
            List<string> DupTags = new List<string>();

            foreach (TaggedComponent tc in MainForm.CollectedTags)
            {
                if (LDARTags.Contains(tc.LDARTag + "." + tc.Extension))
                {
                    if (!DupTags.Contains(tc.LDARTag + "." + tc.Extension)) DupTags.Add(tc.LDARTag + "." + tc.Extension);
                }
                else
                {
                    LDARTags.Add(tc.LDARTag + "." + tc.Extension);
                }
            }

            foreach (TaggedComponent tc in MainForm.CollectedTags)
            {
                if (DupTags.Contains(tc.LDARTag + "." + tc.Extension))
                {
                    tc.ErrorMessage = addMessage("Dup New Tag", tc.ErrorMessage);
                    _affectedComponents.Add(tc);
                }
            }

            if (_affectedComponents.Count > 0)
            {
                qaMessage = _affectedComponents.Count.ToString() + " Duplicates Found";
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
