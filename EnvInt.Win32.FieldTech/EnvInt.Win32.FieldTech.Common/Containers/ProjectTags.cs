using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace EnvInt.Win32.FieldTech.Containers
{
    /// <summary>
    /// The ProjectTagData
    /// </summary>
    /// 
    public class ProjectTags
    {
        public string Id { get; set; }
        public string Device { get; set; }
        public DateTime CreateDate { get; set; }
        public string FileName { get; set; }
        public List<TaggedComponent> Tags { get; set; }
        public bool Exported { get; set; }
        public DateTime? ExportedOn { get; set; }
        public string ExportedBy { get; set; }
        public string WorkingFileName { get; set; }
        public DateTime? NeedsReloadedFromWorkingFile { get; set; }
        public bool SkipQC { get; set; }
        public DateTime? SentToCollected { get; set; }

        public string ExcelMessage
        {
            get { return "View in Excel"; }
        }

        public int TagCount()
        {
            int tc = 0;
            if (Tags != null)
            {
                foreach (TaggedComponent tag in Tags)
                {
                    tc = tc + 1 + tag.Children.Count();
                }
            }

            return tc;
        }

        public List<TaggedComponent> getAllAsTaggedComponent()
        {
            List<TaggedComponent> allComps = new List<TaggedComponent>();

            foreach( TaggedComponent parent in Tags )
            {
                if (!string.IsNullOrEmpty(parent.LDARTag))
                {
                    allComps.Add(parent);
                    if (parent.Children.Count > 0)
                    {
                        allComps.AddRange(parent.GetChildrenAsTaggedComponent());
                    }
                }
            }


            return allComps;
        }

        public void setDefaults()
        {
            Id = Guid.NewGuid().ToString();
            Device = Environment.MachineName;
            CreateDate = DateTime.Now;
            FileName = "";
            Tags = new List<TaggedComponent>();
            Exported = false;
            ExportedOn = DateTime.MinValue;
            ExportedBy = Environment.UserName;
            SkipQC = false;
            WorkingFileName = "";
        }

    }


}
