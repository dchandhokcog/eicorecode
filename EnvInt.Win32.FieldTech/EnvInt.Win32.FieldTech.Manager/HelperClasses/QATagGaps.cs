using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Manager.HelperClasses
{
    class QATagGaps : QAItem
    {
        private string qaMessage = string.Empty;
        private string qaWarning = string.Empty;
        
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
                return "Tag Gaps";
            }
        }

        public override string Message
        {
            get
            {
                return passMessage;
            }
        }

        public override bool QAPassed
        {
            get
            {
                return true;
            }

        }

        public QATagGaps()
        {
            _affectedComponents = new List<TaggedComponent>();
        }

        public override void doQA()
        {

            _affectedComponents.Clear();
            
            System.Diagnostics.Debug.WriteLine("Starting " + this.QACheck);
            
            SortedDictionary<double,TaggedComponent> LDARTags = new SortedDictionary<double,TaggedComponent>();
            TaggedComponent lastTag = new TaggedComponent();
            int gapGroup = 1;
            double lastNum = 0;
            double tagAsNum = 0;

            foreach (TaggedComponent tc in MainForm.CollectedTags)
            {
                if (double.TryParse(tc.LDARTag, out tagAsNum))
                {
                    if (!LDARTags.Keys.Contains(tagAsNum)) LDARTags.Add(tagAsNum, tc);
                }
            }

            foreach (KeyValuePair<double, TaggedComponent> kvp in LDARTags)
            {
                
                
                if (lastNum != 0 && (kvp.Key - lastNum) > 1)
                {
                    lastTag.WarningMessage = addMessage("Tag Gap Group " + gapGroup.ToString(), lastTag.WarningMessage);
                    kvp.Value.WarningMessage = addMessage("Tag Gap Group " + gapGroup.ToString(), kvp.Value.WarningMessage);
                    _affectedComponents.Add(lastTag);
                    _affectedComponents.Add(kvp.Value);
                    gapGroup += 1;
                }
                lastNum = kvp.Key;
                lastTag = kvp.Value;
            }


            if (_affectedComponents.Count > 0)
            {
                WarningMsg = ((_affectedComponents.Count) / 2).ToString() + " Tag Gaps Found";
                QAWarning = true;
            }
            else
            {
                WarningMsg = string.Empty;
                QAWarning = false;
            }

        }

    }
}
