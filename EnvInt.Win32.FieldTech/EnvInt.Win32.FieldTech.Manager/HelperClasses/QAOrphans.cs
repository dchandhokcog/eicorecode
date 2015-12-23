using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Manager.HelperClasses
{
    class QAOrphans : QAItem
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
                return "Orphaned Children";
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

        public QAOrphans()
        {
            _projectTags = MainForm.CurrentProjectTags;
            _affectedComponents = new List<TaggedComponent>();
        }

        public override void doQA()
        {

            _affectedComponents.Clear();
            
            System.Diagnostics.Debug.WriteLine("Starting " + this.QACheck);
            
            List<string> LDARTags = new List<string>();
            List<string> NonOrphaned = new List<string>();

            foreach (ProjectTags pt in _projectTags.Where(c => c.Exported == false))
            {
                foreach (TaggedComponent tc in pt.Tags.Where(c => !c.isChild))
                {
                    if (!LDARTags.Contains(tc.LDARTag))
                    {
                        NonOrphaned.Add(tc.LDARTag);
                        if (tc.Children.Count > 0)
                        {
                            foreach (ChildComponent child in tc.Children)
                            {
                                if (!NonOrphaned.Contains(child.LDARTag)) NonOrphaned.Add(child.LDARTag);
                            }
                        }
                    }

                }
            }

            foreach (ProjectTags pt in _projectTags)
            {
                foreach (TaggedComponent tc in pt.Tags.Where(c => c.isChild))
                {
                    if (!NonOrphaned.Contains(tc.LDARTag))
                    {
                        tc.ErrorMessage = addMessage("Orphaned Child", tc.ErrorMessage);
                        _affectedComponents.Add(tc);
                    }
                }
            }


            if (_affectedComponents.Count > 0)
            {
                qaMessage = _affectedComponents.Count.ToString() + " Orphans Found";
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
