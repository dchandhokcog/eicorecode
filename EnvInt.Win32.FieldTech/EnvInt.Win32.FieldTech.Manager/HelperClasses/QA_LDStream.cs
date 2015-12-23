using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnvInt.Win32.FieldTech.Containers;

namespace EnvInt.Win32.FieldTech.Manager.HelperClasses
{
    class QA_LDStream : QAItem
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
                return "Streams in LDAR Database";
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

        public QA_LDStream()
        {
            _projectData = MainForm.CurrentProjectData;
            _affectedComponents = new List<TaggedComponent>();
        }

        public override void doQA()
        {

            _affectedComponents.Clear();
            
            System.Diagnostics.Debug.WriteLine("Starting " + this.QACheck);
            
            List<String> streams = new List<string>();
            bool foundEmpty = false;

            foreach (TaggedComponent tag in MainForm.CollectedTags)
            {
                if (!String.IsNullOrEmpty(tag.Stream))
                {
                    if (!streams.Contains(tag.Stream)) streams.Add(tag.Stream);
                }
                else
                {
                    tag.ErrorMessage = addMessage("Empty Stream", tag.ErrorMessage);
                    _affectedComponents.Add(tag);
                    foundEmpty = true;
                }
            }

            //decide
            foreach (string stream in streams)
            {
                //long streamID = LeakDAS.GetLeakdasComponentStreamId(stream);
                LDARComponentStream ldarStream = _projectData.LDARData.ComponentStreams.Where(c => c.StreamDescription == stream).FirstOrDefault();
                if (ldarStream == null)
                {
                    _affectedComponents.AddRange(MainForm.CollectedTags.Where(c => c.Stream == stream));
                    _affectedComponents.ForEach(c => c.ErrorMessage = addMessage("Stream Not In LDAR Database", c.ErrorMessage));
                }

            }

            if (_affectedComponents.Count > 0)
            {
                qaMessage = _affectedComponents.Count.ToString() + " Tags with no matching stream in LDAR Database";
                if (foundEmpty) qaMessage += " / Empty stream";
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

