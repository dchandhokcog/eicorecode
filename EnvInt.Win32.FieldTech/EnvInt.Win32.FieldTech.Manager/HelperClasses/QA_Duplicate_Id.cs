using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Manager.HelperClasses
{
    class QADuplicate_Ids : QAItem
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
                return "Duplicate ID";
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

        public QADuplicate_Ids()
        {
            _affectedComponents = new List<TaggedComponent>();
        }

        public override void doQA()
        {
            _affectedComponents.Clear();
            
            System.Diagnostics.Debug.WriteLine("Starting " + this.QACheck);

            List<string> ids = new List<string>();
            List<string> DupIds = new List<string>();

            foreach (TaggedComponent tc in MainForm.CollectedTags)
            {
                if (ids.Contains(tc.Id) && !string.IsNullOrEmpty(tc.Id))
                {
                    if (!DupIds.Contains(tc.Id)) DupIds.Add(tc.Id);
                }
                else
                {
                    ids.Add(tc.Id);
                }
            }

            foreach (TaggedComponent tc in MainForm.CollectedTags)
            {
                if (DupIds.Contains(tc.Id))
                {
                    tc.WarningMessage = addMessage("Dup Id", tc.WarningMessage);
                    _affectedComponents.Add(tc);
                }
            }


            if (_affectedComponents.Count > 0)
            {
                QAWarning = true;
                qaMessage = passMessage;
                WarningMsg = _affectedComponents.Count.ToString() + " Duplicate IDs Found";
                qaPassed = true;
            }
            else
            {
                qaMessage = passMessage;
                qaPassed = true;
                QAWarning = false;
                WarningMsg = "";
            }
        }

    }
}


