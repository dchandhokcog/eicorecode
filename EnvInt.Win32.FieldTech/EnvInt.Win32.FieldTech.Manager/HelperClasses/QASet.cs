using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

using EnvInt.Win32.FieldTech.Manager.HelperClasses;
using EnvInt.Win32.FieldTech.Common;
using EnvInt.Win32.FieldTech.Containers;


namespace EnvInt.Win32.FieldTech.Manager.HelperClasses
{
    public class QASet
    {
        public List<QAItem> qaItems { get; set; }
        public bool QADirty { get; set; }

        public QASet()
        {
            qaItems = new List<QAItem>();
        }
        
        public List<string> getQAHeaderList_LeakDAS()
        {
            List<string> tl = new List<string>();
            tl.Add("QADuplicates");
            tl.Add("QATagGaps");
            tl.Add("QA_LDUnit");
            tl.Add("QA_LDArea");
            tl.Add("QA_LDType");
            tl.Add("QA_LDAccess");
            tl.Add("QA_LDStream");
            tl.Add("QA_LDState");
            tl.Add("QADuplicate_OldTags");
            tl.Add("QADuplicate_Ids");
            tl.Add("QA_DTMPercent");
            tl.Add("QA_ClassMismatch");
            tl.Add("QA_UnmatchedOldTags");
            tl.Add("QADuplicate_Descriptions");
            //tl.Add("QAOrphans");
            tl.Add("QA_OOS");
            return tl;

        }


        public List<string> getQAHeaderList_Guideware()
        {
            List<string> tl = new List<string>();
            tl.Add("QA_GWDuplicates");
            tl.Add("QA_GWAccess");
            tl.Add("QA_GWUtmReasons");
            tl.Add("QATagGaps");
            tl.Add("QA_LDUnit");
            tl.Add("QA_GWArea");
            tl.Add("QA_LDEquipment");
            tl.Add("QA_LDType");
            tl.Add("QA_LDAccess");
            tl.Add("QA_LDStream");
            tl.Add("QA_LDState");
            tl.Add("QADuplicate_OldTags");
            tl.Add("QADuplicate_Ids");
            tl.Add("QA_DTMPercent");
            tl.Add("QA_ClassMismatch");
            tl.Add("QA_GWUnmatchedOldTags");
            tl.Add("QADuplicate_Descriptions");
            //tl.Add("QAOrphans");
            tl.Add("QA_OOS");
            tl.Add("QA_GWNewTagExists");

            return tl;

        }

        public void setQAItems_LeakDAS()
        {
            qaItems.Clear();
            qaItems.Add(new QADuplicates());
            qaItems.Add(new QATagGaps());
            qaItems.Add(new QA_LDUnit());
            qaItems.Add(new QA_LDArea());
            qaItems.Add(new QA_LDType());
            qaItems.Add(new QA_LDAccess());
            qaItems.Add(new QA_LDStream());
            qaItems.Add(new QA_LDState());
            qaItems.Add(new QADuplicate_OldTags());
            qaItems.Add(new QADuplicate_Ids());
            qaItems.Add(new QA_DTMPercent());
            qaItems.Add(new QA_ClassMismatch());
            qaItems.Add(new QA_UnmatchedOldTags());
            qaItems.Add(new QADuplicate_Descriptions());
            //qaItems.Add(new QAOrphans());
            qaItems.Add(new QA_OOS());
 
        }

        public void setQAItems_Guideware()
        {
            qaItems.Clear();
            qaItems.Add(new QA_GWDuplicates());
            qaItems.Add(new QA_LDUnit());
            qaItems.Add(new QA_GWArea());
            qaItems.Add(new QA_GWEquipment());
            qaItems.Add(new QA_GWAccess());
            qaItems.Add(new QA_GWUtmReasons());
            qaItems.Add(new QATagGaps());
            qaItems.Add(new QA_LDType());
            qaItems.Add(new QA_LDStream());
            qaItems.Add(new QA_LDState());
            qaItems.Add(new QADuplicate_OldTags());
            qaItems.Add(new QADuplicate_Ids());
            qaItems.Add(new QA_DTMPercent());
            qaItems.Add(new QA_ClassMismatch());
            qaItems.Add(new QA_GWUnmatchedOldTags());
            qaItems.Add(new QADuplicate_Descriptions());
            //qaItems.Add(new QAOrphans());
            qaItems.Add(new QA_OOS());
            qaItems.Add(new QA_GWNewTagExists());
        }

        public void processCurrentSet()
        {
            Parallel.ForEach(qaItems, qa_Check =>
            {
                qa_Check.doQA();
            });
        }
    
    }


}
