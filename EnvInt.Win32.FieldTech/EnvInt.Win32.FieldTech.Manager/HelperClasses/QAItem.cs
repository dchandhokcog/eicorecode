using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Manager.HelperClasses
{
    public class QAItem : CSVable
    {

        public virtual string Device { get; set; }
        public virtual string QACheck { get; set; }
        public virtual string Message { get; set; }
        public virtual bool QAPassed { get; set; }
        public virtual string WarningMsg { get; set; }
        public virtual bool QAWarning { get; set; }
        public virtual bool Enabled { get; set; }
        public virtual DateTime LastRan { get; set; }
        public List<TaggedComponent> _affectedComponents { get; set; }
        public List<ProjectTags> _projectTags { get; set; }
        public ProjectData _projectData { get; set; }
        public const string passMessage = "Passed/OK";

        public int affectedComponentCount()
        {
            return _affectedComponents.Count;
        }

        public List<TaggedComponent> getAffectedComponents()
        {
            return _affectedComponents;
        }

        public virtual void doQA()
        { }

        public string addMessage(string newMsg, string originalMsg)
        {
            List<string> messages = new List<string>();
            List<string> fixedMessages = new List<string>();
            string fullMessage = string.Empty;

            if (originalMsg != null)
            {
                if (originalMsg.Contains(")"))
                {
                    messages.AddRange(originalMsg.Split(')'));
                }
                else
                {
                    messages.Add(originalMsg);
                }
            }
            
            foreach (string msgElement in messages)
            {
                if (msgElement != null)
                {
                    string tmpMsg = msgElement.Replace(")","").Replace("(", "").Trim();
                    if (!fixedMessages.Contains("(" + tmpMsg + ")")) fixedMessages.Add("(" + tmpMsg + ")");
                }
            }

            if (!fixedMessages.Contains("(" + newMsg + ")")) fixedMessages.Add("(" + newMsg + ")");
            
            foreach (string fixedMsg in fixedMessages)
            {
                if (fixedMsg != "()") fullMessage = fullMessage + fixedMsg;
            }

            return fullMessage;
        }

    }
}
