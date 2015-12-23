using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Manager.HelperClasses
{
    class QA_OOS : QAItem
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
                return "OOS Reasons in LDAR Database";
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

        public QA_OOS()
        {
            _projectTags = MainForm.CurrentProjectTags;
            _projectData = MainForm.CurrentProjectData;
            _affectedComponents = new List<TaggedComponent>();
        }

        public override void doQA()
        {

            _affectedComponents.Clear();
            
            System.Diagnostics.Debug.WriteLine("Starting " + this.QACheck);
            
            List<String> oos = new List<string>();

            foreach ( TaggedComponent tag in MainForm.CollectedTags)
            {
                if (!String.IsNullOrEmpty(tag.TagOOSReason))
                {
                    if (!oos.Contains(tag.TagOOSReason)) oos.Add(tag.TagOOSReason);
                }
            }

            //decide
            foreach (string oosDesc in oos)
            {

                LDAROOSDescription oosRec = _projectData.LDARData.OOSDescriptions.Where(c => c.OOSDescription == oosDesc).FirstOrDefault();
                
                if (oosRec == null)
                {
                    _affectedComponents.AddRange(MainForm.CollectedTags.Where(c => c.TagOOSReason == oosDesc));
                    _affectedComponents.ForEach(c => c.ErrorMessage = addMessage("OOS Reason Not In LDAR Database",c.ErrorMessage));
                }

            }

            if (_affectedComponents.Count > 0)
            {
                qaMessage = _affectedComponents.Count.ToString() + " Tags with no matching OOS Reason in LDAR Database";
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

