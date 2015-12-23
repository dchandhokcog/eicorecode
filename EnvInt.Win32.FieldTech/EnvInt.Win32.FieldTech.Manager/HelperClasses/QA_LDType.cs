using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Manager.HelperClasses
{
    class QA_LDType : QAItem
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
                return "Types in LDAR Database";
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

        public QA_LDType()
        {
            _affectedComponents = new List<TaggedComponent>();
        }

        public override void doQA()
        {

            _affectedComponents.Clear();
            
            System.Diagnostics.Debug.WriteLine("Starting " + this.QACheck);

            List<string> types = new List<string>();
            List<string> classes = new List<string>();
            bool foundEmpty = false;

            foreach (TaggedComponent tag in MainForm.CollectedTags)
            {
                if (!String.IsNullOrEmpty(tag.ComponentType))
                {
                    if (!types.Contains(tag.ComponentType)) types.Add(tag.ComponentType);
                }
                else
                {
                    tag.ErrorMessage = addMessage("Empty Type", tag.ErrorMessage);
                    _affectedComponents.Add(tag);
                    foundEmpty = true;
                }
            }

            //decide
            foreach (string type in types)
            {
                string ldType = string.Empty;
                string ldClass = string.Empty;

                if (type.Contains('-'))
                {   //changed by SVS for type like XXXX-XXX-XX (multiple '-')
                    ldClass = type.Substring(0, type.IndexOf('-')).Trim().ToUpper(); //type.Split('-')[0].ToUpper().Trim();
                    ldType = type.Substring((type.Substring(0, type.IndexOf('-'))).Length + 1).Trim().ToUpper(); //type.Split('-')[1].ToUpper().Trim();
                }
                else
                {
                    ldClass = type;
                    //ldType = "";
                }

                LDARComponentClassType ldarClassType = MainForm.CurrentProjectData.LDARData.ComponentClassTypes.Where(c => c.ComponentClass.ToUpper().Trim() == ldClass && c.ComponentType.ToUpper().Trim() == ldType).FirstOrDefault();
                if (ldarClassType == null)
                {
                    //try to grab by class only if our database is LeakDAS
                    if (MainForm.CurrentProjectData.LDARDatabaseType == "LeakDAS")
                    {
                        ldarClassType = MainForm.CurrentProjectData.LDARData.ComponentClassTypes.Where(c => c.ComponentClass == ldClass).FirstOrDefault();
                    }
                }

                string qaMsg = string.Empty;

                if (ldarClassType == null)
                {
                    qaMsg = "Class/Type not in LDAR Database";
                }


                if (qaMsg != string.Empty)
                {

                    _affectedComponents.AddRange(MainForm.CollectedTags.Where(c => c.ComponentType == type));
                    _affectedComponents.ForEach(c => c.ErrorMessage = addMessage(qaMsg, c.ErrorMessage));
                }

            }

            if (_affectedComponents.Count > 0)
            {
                qaMessage = _affectedComponents.Count.ToString() + " Tags with no matching class or type in LDAR Database";
                if (foundEmpty) qaMessage += " / Empty type";
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


