using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Manager.HelperClasses
{
    class QADuplicate_OldTags : QAItem
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
                return "Duplicate Old Tags";
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

        public QADuplicate_OldTags()
        {
            _affectedComponents = new List<TaggedComponent>();
        }

        public override void doQA()
        {
            _affectedComponents.Clear();
            
            System.Diagnostics.Debug.WriteLine("Starting " + this.QACheck);

            List<string> LDARTags = new List<string>();
            List<string> DupTags = new List<string>();

            if (MainForm.CurrentProjectData.LDARDatabaseType.Contains("LeakDAS"))
            {
                foreach (TaggedComponent tc in MainForm.CollectedTags)
                {
                    if (LDARTags.Contains(tc.PreviousTag) && !string.IsNullOrEmpty(tc.PreviousTag))
                    {
                        if (!DupTags.Contains(tc.PreviousTag)) DupTags.Add(tc.PreviousTag);
                    }
                    else
                    {
                        LDARTags.Add(tc.PreviousTag);
                    }
                }
            }
            else
            {
                foreach (TaggedComponent tc in MainForm.CollectedTags)
                {
                    string prevTag = tc.PreviousTag + "." + tc.PreviousTagExtension;

                    if (LDARTags.Contains(prevTag) && !string.IsNullOrEmpty(prevTag) && prevTag.Trim()!=".")
                    {
                        if (!DupTags.Contains(prevTag)) DupTags.Add(prevTag);
                    }
                    else
                    {
                        LDARTags.Add(prevTag);
                    }
                }
            }

            foreach (TaggedComponent tc in MainForm.CollectedTags)
            {
                if (MainForm.CurrentProjectData.LDARDatabaseType.Contains("LeakDAS"))
                {
                    if (DupTags.Contains(tc.PreviousTag))
                    {
                        tc.ErrorMessage = addMessage("Dup Previous Tag", tc.ErrorMessage);
                        _affectedComponents.Add(tc);
                    }
                }
                else
                {
                    if (DupTags.Contains(tc.PreviousTag + "." + tc.PreviousTagExtension))
                    {
                        tc.ErrorMessage = addMessage("Dup Previous Tag", tc.ErrorMessage);
                        _affectedComponents.Add(tc);
                    }                     
                }
            }


            if (_affectedComponents.Count > 0)
            {
                qaMessage = _affectedComponents.Count.ToString() + " Duplicate Old Tags Found";
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

