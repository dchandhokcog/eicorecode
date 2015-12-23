using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Manager.HelperClasses
{
    class QA_LDState : QAItem
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
                return "States in LDAR Database";
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

        public QA_LDState()
        {
            _projectData = MainForm.CurrentProjectData;
            _affectedComponents = new List<TaggedComponent>();
        }

        public override void doQA()
        {
            _affectedComponents.Clear();
            
            System.Diagnostics.Debug.WriteLine("Starting " + this.QACheck);
            
            List<String> states = new List<string>();
            bool foundEmpty = false;

            foreach (TaggedComponent tag in MainForm.CollectedTags)
            {
                if (!String.IsNullOrEmpty(tag.ChemicalState))
                {
                    if (!states.Contains(tag.ChemicalState)) states.Add(tag.ChemicalState);
                }
                else
                {
                    tag.ErrorMessage = addMessage("Empty State", tag.ErrorMessage);
                    _affectedComponents.Add(tag);
                    foundEmpty = true;
                }
            }

            //decide
            foreach (string state in states)
            {
                //long stateID = LeakDAS.GetLeakdasChemicalStateId(state);
                LDARChemicalState ldarState = _projectData.LDARData.ChemicalStates.Where(c => c.ChemicalState == state).FirstOrDefault();
                if (ldarState == null)
                {
                    _affectedComponents.AddRange(MainForm.CollectedTags.Where(c => c.ChemicalState == state));
                    _affectedComponents.ForEach(c => c.ErrorMessage = addMessage("Chemical state not In LDAR Database", c.ErrorMessage));
                }

            }

            if (_affectedComponents.Count > 0)
            {
                qaMessage = _affectedComponents.Count.ToString() + " Tags with no matching chemical state in LDAR Database";
                if (foundEmpty) qaMessage += " / Empty state";
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

