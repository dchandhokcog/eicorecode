using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Manager.HelperClasses
{
    class QADuplicate_Descriptions : QAItem
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
                return "Duplicate Location Descriptions";
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

        public QADuplicate_Descriptions()
        {
            _affectedComponents = new List<TaggedComponent>();
        }

        public override void doQA()
        {

            _affectedComponents.Clear();
            
            System.Diagnostics.Debug.WriteLine("Starting " + this.QACheck);

            List<string> Descriptions = new List<string>();
            List<string> DupDescriptions = new List<string>();

            foreach (TaggedComponent tc in MainForm.CollectedTags)
            {
                if (Descriptions.Contains(tc.Location) && !string.IsNullOrEmpty(tc.Location))
                {
                    if (!DupDescriptions.Contains(tc.Location)) DupDescriptions.Add(tc.Location);
                }
                else
                {
                    Descriptions.Add(tc.Location);
                }
            }

            foreach (TaggedComponent tc in MainForm.CollectedTags)
            {
                if (DupDescriptions.Contains(tc.Location))
                {
                    tc.WarningMessage = addMessage("Dup Location Description", tc.WarningMessage);
                    _affectedComponents.Add(tc);
                }
            }


            if (_affectedComponents.Count > 0)
            {
                QAWarning = true;
                qaMessage = passMessage;
                WarningMsg = _affectedComponents.Count.ToString() + " Duplicate Location Descriptions Found";
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