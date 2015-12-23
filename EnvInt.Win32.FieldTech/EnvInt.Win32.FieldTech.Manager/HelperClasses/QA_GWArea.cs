using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Manager.HelperClasses
{
    class QA_GWArea : QAItem
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
                return "Areas in LDAR Database";
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

        public QA_GWArea()
        {
            _projectData = MainForm.CurrentProjectData;
            _affectedComponents = new List<TaggedComponent>();
        }

        public override void doQA()
        {

            _affectedComponents.Clear();
            
            System.Diagnostics.Debug.WriteLine("Starting " + this.QACheck);
            
            List<String> areas = new List<string>();
            bool foundEmpty = false;

            foreach (TaggedComponent tag in MainForm.CollectedTags)
            {
                if (!String.IsNullOrEmpty(tag.Area))
                {
                    if (!areas.Contains(tag.Area + "|" + tag.Unit)) areas.Add(tag.Area + "|" + tag.Unit);
                }
                else
                {
                    //area isn't required in Leakdas, so only flag empty in Guideware
                    if (MainForm.CurrentProjectData.LDARDatabaseType == "Guideware")
                    {
                        tag.ErrorMessage = addMessage("Empty Area", tag.ErrorMessage);
                        _affectedComponents.Add(tag);
                        foundEmpty = true;
                    }
                }
            }

            //decide
            foreach (string area in areas)
            {

                string areaPart = area.Split('|')[0];
                string unitPart = area.Split('|')[1];
                LDARProcessUnit pu = MainForm.CurrentProjectData.LDARData.ProcessUnits.Where(a => a.UnitDescription == unitPart).FirstOrDefault();

                if (pu == null)
                {
                    _affectedComponents.AddRange(MainForm.CollectedTags.Where(c => c.Area + "|" + c.Unit == area));
                    _affectedComponents.ForEach(c => c.ErrorMessage = addMessage("Area/Unit Combination Not In LDAR Database",c.ErrorMessage));
                }
                else
                {
                    LDARArea lda =  _projectData.LDARData.Areas.Where(c => c.AreaDescription == areaPart && c.UnitCode == pu.UnitCode ).FirstOrDefault();
                
                    if (lda == null)
                    {
                        _affectedComponents.AddRange(MainForm.CollectedTags.Where(c => c.Area + "|" + c.Unit == area));
                        _affectedComponents.ForEach(c => c.ErrorMessage = addMessage("Area/Unit Combination Not In LDAR Database",c.ErrorMessage));
                    }
                }

            }

            if (_affectedComponents.Count > 0)
            {
                qaMessage = _affectedComponents.Count.ToString() + " Tags with no matching area/unit combination in LDAR Database";
                if (foundEmpty) qaMessage += " / Empty area";
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

