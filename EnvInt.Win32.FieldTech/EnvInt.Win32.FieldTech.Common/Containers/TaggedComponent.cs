using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

//using System.Runtime.Serialization;
//using System.Runtime.Serialization.Json;

using EnvInt.Win32.FieldTech.Common;

namespace EnvInt.Win32.FieldTech.Containers
{
    public class TaggedComponent
    {
        public string Id { get; set; }
        public string EngineeringTag { get; set; }
        public string ComponentType { get; set; }
        public string Location { get; set; }
        public string LDARTag { get; set; }
        //added 3/31/15 DW
        public string Extension { get; set; }
        public string PreviousTag { get; set; }
        //added 6/5/15 DW
        public string PreviousTagExtension { get; set; }
        //added 3/9/15 DW
        public int PreviousTagId { get; set; }
        public string ReferenceTag { get; set; }
        //added 6/5/15 DW
        public string ReferenceTagExtension { get; set; }
        public string Access { get; set; }
        //added 3/9/15 DW
        public string UTMReason { get; set; }
        //added 3/9/15 DW
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Stream { get; set; }
        public string Drawing { get; set; }
        public string ChemicalState { get; set; }
        public double Size { get; set; }
        public string MOCNumber { get; set; }
        public string Unit { get; set; }
        public string Area { get; set; }
        //added 3/31/15 DW
        public string Equipment { get; set; }
        public string Batch { get; set; }
        public string Manufacturer { get; set; }
        public string Property { get; set; }
        public double RouteSequence { get; set; }
        public double InspectionBackground { get; set; }
        public double InspectionReading { get; set; }
        public string InspectionInstrument { get; set; }
        public DateTime InspectionDate { get; set; }
        public string InspectionInspector { get; set; }
        public bool Inspected { get; set; }
        public bool AttachDrawing { get; set; }
        //added 3/9/15 DW
        public bool RemoveInProject { get; set; }
        //added 3/31/15 DW
        public bool TagPOS { get; set; }
        public bool TagOOS { get; set; }
        //added 10/13/15 DW
        public bool GhostTag { get; set; }
        //added 4/13/15 DW
        public string TagPOSReason { get; set; }
        //added 4/13/15 DW
        public string TagOOSReason { get; set; }
        //added 4/16/15 DW
        public string InstalledResponse { get; set; }
        //added 4/15/15 DW
        public DateTime? InstalledDate { get; set; }
        //added 4/15/15 DW
        public DateTime? HCServiceDate { get; set; }
        public bool isDrawingTag { get; set; }
        //added 4/22/15 DW
        public bool isChild { get; set; }
        //added 5/5/15 DW
        public string pnpId { get; set; }
        public string ErrorMessage { get; set; }
        public string WarningMessage { get; set; }
        //added 6/5/15 DW
        public string DrawingObjectXY { get; set; }
        //added 8/26/15 DW
        public string CVSReason { get; set; }
        public List<ChildComponent> Children { get; set; }
        //public string ExtraFields { get; set; }

        public TaggedComponent()
        {
            Children = new List<ChildComponent>();
            Size = 0;
            RouteSequence = 0;
        }

        public override string ToString()
        {

            if (Id == null) return "";
            
            List<string> values = new List<string>();
            values.Add("\"" + Id + "\"");
            values.Add("\"" + textCleaner(EngineeringTag) + "\"");
            values.Add("\"" + textCleaner(ComponentType) + "\"");
            values.Add("\"" + Location.Replace('"', '‟').Replace('\\', '‖') + "\"");
            values.Add("\"" + textCleaner(LDARTag) + "\"");
            values.Add("\"" + textCleaner(Extension) + "\"");
            values.Add("\"" + textCleaner(PreviousTag) + "\"");
            values.Add("\"" + textCleaner(PreviousTagExtension) + "\"");
            values.Add("\"" + PreviousTagId.ToString() + "\"");
            values.Add("\"" + textCleaner(ReferenceTag) + "\"");
            values.Add("\"" + textCleaner(Access) + "\"");
            values.Add("\"" + textCleaner(UTMReason) + "\"");
            values.Add("\"" + CreatedDate + "\"");
            values.Add("\"" + ModifiedDate + "\"");
            values.Add("\"" + ModifiedBy + "\"");
            values.Add("\"" + textCleaner(Stream) + "\"");
            values.Add("\"" + textCleaner(Drawing) + "\"");
            values.Add("\"" + textCleaner(ChemicalState) + "\"");
            values.Add("\"" + Size + "\"");
            values.Add("\"" + textCleaner(MOCNumber) + "\"");
            values.Add("\"" + textCleaner(Unit) + "\"");
            values.Add("\"" + textCleaner(Area) + "\"");
            values.Add("\"" + textCleaner(Equipment) + "\"");
            values.Add("\"" + textCleaner(Batch) + "\"");
            values.Add("\"" + textCleaner(Manufacturer) + "\"");
            values.Add("\"" + textCleaner(Property) + "\"");
            values.Add("\"" + RouteSequence.ToString() + "\"");
            values.Add("\"" + InspectionBackground + "\"");
            values.Add("\"" + InspectionReading + "\"");
            values.Add("\"" + InspectionInstrument + "\"");
            values.Add("\"" + InspectionDate + "\"");
            values.Add("\"" + textCleaner(InspectionInspector) + "\"");
            values.Add("\"" + Inspected + "\"");
            values.Add("\"" + AttachDrawing + "\"");
            values.Add("\"" + RemoveInProject + "\"");
            values.Add("\"" + TagPOS + "\"");
            values.Add("\"" + TagOOS + "\"");
            values.Add("\"" + GhostTag + "\"");
            values.Add("\"" + TagPOSReason + "\"");
            values.Add("\"" + TagOOSReason + "\"");
            values.Add("\"" + InstalledResponse + "\"");
            values.Add("\"" + InstalledDate + "\"");
            values.Add("\"" + HCServiceDate + "\"");
            values.Add("\"" + isDrawingTag + "\"");
            values.Add("\"" + isChild + "\"");
            values.Add("\"" + pnpId + "\"");
            values.Add("\"" + ErrorMessage + "\"");
            values.Add("\"" + WarningMessage + "\"");
            values.Add("\"" + DrawingObjectXY + "\"");
            values.Add("\"" + CVSReason + "\"");
            values.Add("\"\"");
            //if (ExtraFields != null) values.Add("\"" + ExtraFields.ToString() + "\"");
            return String.Join(",", values.ToArray());
        }

        public List<TaggedComponent> GetChildrenAsTaggedComponent()
        {
            List<TaggedComponent> children = new List<TaggedComponent>();

            if (this.Children.Count > 0)
            {
                foreach (ChildComponent child in this.Children)
                {
                    TaggedComponent kid = new TaggedComponent();
                    kid.Access = Access;
                    kid.UTMReason = UTMReason;
                    kid.ChemicalState = ChemicalState;
                    kid.ComponentType = child.ComponentType;
                    kid.Drawing = Drawing;
                    kid.EngineeringTag = "";
                    kid.Id = Id + "|" + child.LDARTag + child.Extension;
                    kid.Inspected = child.Inspected;
                    kid.InspectionBackground = child.InspectionBackground;
                    kid.InspectionDate = child.InspectionDate;
                    kid.InspectionInspector = child.InspectionInspector;
                    kid.InspectionInstrument = child.InspectionInstrument;
                    kid.InspectionReading = child.InspectionReading;
                    kid.LDARTag = child.LDARTag;
                    kid.Extension = child.Extension;
                    kid.Location = child.Location;
                    kid.MOCNumber = MOCNumber;
                    kid.ModifiedBy = ModifiedBy;
                    kid.ModifiedDate = ModifiedDate;
                    kid.CreatedDate = CreatedDate;
                    kid.PreviousTag = child.PreviousTag;
                    kid.PreviousTagId = child.PreviousTagId;
                    kid.PreviousTagExtension = child.PreviousTagExtension;
                    kid.ReferenceTag = ReferenceTag;
                    kid.Size = child.Size;
                    kid.Stream = Stream;
                    kid.Unit = Unit;
                    kid.Area = Area;
                    kid.Equipment = Equipment;
                    kid.Manufacturer = Manufacturer;
                    kid.Property = Property;
                    kid.RouteSequence = RouteSequence;
                    kid.Batch = Batch;
                    kid.AttachDrawing = AttachDrawing;
                    kid.RemoveInProject = RemoveInProject;
                    kid.TagPOS = TagPOS;
                    kid.TagOOS = TagOOS;
                    kid.GhostTag = GhostTag;
                    kid.TagOOSReason = TagOOSReason;
                    kid.TagPOSReason = TagPOSReason;
                    kid.InstalledResponse = InstalledResponse;
                    kid.InstalledDate = InstalledDate;
                    kid.HCServiceDate = HCServiceDate;
                    kid.isDrawingTag = isDrawingTag;
                    kid.isChild = true;
                    kid.pnpId = string.Empty;
                    kid.ErrorMessage = ErrorMessage;
                    kid.WarningMessage = WarningMessage;
                    kid.DrawingObjectXY = DrawingObjectXY;
                    kid.CVSReason = CVSReason;
                    kid.Children = new List<ChildComponent>();
                    //kid.ExtraFields = ExtraFields;
                    children.Add(kid);
                }
            }

            return children;

        }

        public List<TaggedComponent> CopyComponentAndChildren()
        {
            List<TaggedComponent> children = new List<TaggedComponent>();
            TaggedComponent parent = new TaggedComponent();

            parent.Access = Access;
            parent.UTMReason = UTMReason;
            parent.ChemicalState = ChemicalState;
            parent.ComponentType = ComponentType;
            parent.Drawing = Drawing;
            parent.EngineeringTag = EngineeringTag;
            parent.Id = Id;
            parent.Inspected = Inspected;
            parent.InspectionBackground = InspectionBackground;
            parent.InspectionDate = InspectionDate;
            parent.InspectionInspector = InspectionInspector;
            parent.InspectionInstrument = InspectionInstrument;
            parent.InspectionReading = InspectionReading;
            parent.LDARTag = LDARTag;
            parent.Extension = Extension;
            parent.Location = Location;
            parent.MOCNumber = MOCNumber;
            parent.ModifiedBy = ModifiedBy;
            parent.ModifiedDate = ModifiedDate;
            parent.CreatedDate = CreatedDate;
            parent.PreviousTag = PreviousTag;
            parent.PreviousTagId = PreviousTagId;
            parent.PreviousTagExtension = PreviousTagExtension;
            parent.ReferenceTag = ReferenceTag;
            parent.Size = Size;
            parent.Stream = Stream;
            parent.Unit = Unit;
            parent.Area = Area;
            parent.Equipment = Equipment;
            parent.Manufacturer = Manufacturer;
            parent.Property = Property;
            parent.RouteSequence = RouteSequence;
            parent.Batch = Batch;
            parent.AttachDrawing = AttachDrawing;
            parent.RemoveInProject = RemoveInProject;
            parent.TagPOS = TagPOS;
            parent.TagOOS = TagOOS;
            parent.GhostTag = GhostTag;
            parent.TagOOSReason = TagOOSReason;
            parent.TagPOSReason = TagPOSReason;
            parent.InstalledResponse = InstalledResponse;
            parent.InstalledDate = InstalledDate;
            parent.HCServiceDate = HCServiceDate;
            parent.isDrawingTag = isDrawingTag;
            parent.isChild = false;
            parent.pnpId = string.Empty;
            parent.ErrorMessage = ErrorMessage;
            parent.WarningMessage = WarningMessage;
            parent.DrawingObjectXY = DrawingObjectXY;
            parent.CVSReason = CVSReason;
            parent.Children = new List<ChildComponent>();
            //parent.ExtraFields = ExtraFields;
            children.Add(parent);



            if (this.Children.Count > 0)
            {
                foreach (ChildComponent child in this.Children)
                {
                    TaggedComponent kid = new TaggedComponent();
                    kid.Access = Access;
                    kid.UTMReason = UTMReason;
                    kid.ChemicalState = ChemicalState;
                    kid.ComponentType = child.ComponentType;
                    kid.Drawing = Drawing;
                    kid.EngineeringTag = "";
                    kid.Id = Id + "|" + child.LDARTag + child.Extension;
                    kid.Inspected = child.Inspected;
                    kid.InspectionBackground = child.InspectionBackground;
                    kid.InspectionDate = child.InspectionDate;
                    kid.InspectionInspector = child.InspectionInspector;
                    kid.InspectionInstrument = child.InspectionInstrument;
                    kid.InspectionReading = child.InspectionReading;
                    kid.LDARTag = child.LDARTag;
                    kid.Extension = child.Extension;
                    kid.Location = child.Location;
                    kid.MOCNumber = MOCNumber;
                    kid.ModifiedBy = ModifiedBy;
                    kid.ModifiedDate = ModifiedDate;
                    kid.CreatedDate = CreatedDate;
                    kid.PreviousTag = child.PreviousTag;
                    kid.PreviousTagId = child.PreviousTagId;
                    kid.PreviousTagExtension = child.PreviousTagExtension;
                    kid.ReferenceTag = ReferenceTag;
                    kid.Size = child.Size;
                    kid.Stream = Stream;
                    kid.Unit = Unit;
                    kid.Area = Area;
                    kid.Equipment = Equipment;
                    kid.Manufacturer = Manufacturer;
                    kid.Property = Property;
                    kid.RouteSequence = RouteSequence;
                    kid.Batch = Batch;
                    kid.AttachDrawing = AttachDrawing;
                    kid.RemoveInProject = RemoveInProject;
                    kid.TagPOS = TagPOS;
                    kid.TagOOS = TagOOS;
                    kid.GhostTag = GhostTag;
                    kid.TagOOSReason = TagOOSReason;
                    kid.TagPOSReason = TagPOSReason;
                    kid.InstalledResponse = InstalledResponse;
                    kid.InstalledDate = InstalledDate;
                    kid.HCServiceDate = HCServiceDate;
                    kid.isDrawingTag = isDrawingTag;
                    kid.isChild = true;
                    kid.pnpId = string.Empty;
                    kid.ErrorMessage = ErrorMessage;
                    kid.WarningMessage = WarningMessage;
                    kid.DrawingObjectXY = DrawingObjectXY;
                    kid.CVSReason = CVSReason;
                    kid.Children = new List<ChildComponent>();
                    //kid.ExtraFields = ExtraFields;
                    children.Add(kid);
                }
            }

            return children;

        }

        public override int GetHashCode()
        {
            string tagString = string.Empty;

            if (this.Id != null) tagString += this.Id.ToString();
            if (this.Access != null) tagString += this.Access.ToString();
            if (this.UTMReason != null) tagString += this.UTMReason;
            if (this.Area != null) tagString += this.Area.ToString();
            tagString += this.AttachDrawing.ToString();
            if (this.Batch != null) tagString += this.Batch.ToString();
            if (this.ChemicalState != null) tagString += this.ChemicalState.ToString();
            if (this.Children != null) tagString += this.Children.ToString();
            if (this.ComponentType != null) tagString += this.ComponentType.ToString();
            if (this.Drawing != null) tagString += this.Drawing.ToString();
            if (this.EngineeringTag != null) tagString += this.EngineeringTag.ToString();
            tagString += this.Inspected.ToString();
            tagString += this.InspectionBackground.ToString();
            if (this.InspectionDate != null) tagString += this.InspectionDate.ToString();
            if (this.InspectionInspector != null) tagString += this.InspectionInspector.ToString();
            tagString += this.InspectionReading.ToString();
            tagString += this.isDrawingTag.ToString();
            if (this.LDARTag != null) tagString += this.LDARTag.ToString();
            if (this.Location != null) tagString += this.Location.ToString();
            if (this.Manufacturer != null) tagString += this.Manufacturer.ToString();
            if (this.MOCNumber != null) tagString += this.MOCNumber.ToString();
            if (this.PreviousTag != null) tagString += this.PreviousTag.ToString();
            if (this.PreviousTagExtension != null) tagString += this.PreviousTagExtension.ToString();
            if (this.Property != null) tagString += this.Property.ToString();
            if (this.ReferenceTag != null) tagString += this.ReferenceTag.ToString();
            tagString += this.RouteSequence.ToString();
            tagString += this.Size.ToString();
            if (this.Stream != null) tagString += this.Stream.ToString();
            if (this.Unit != null) tagString += this.Unit.ToString();
            tagString += this.RemoveInProject.ToString();
            if (this.Extension != null) tagString += this.Extension.ToString();
            if (this.Equipment != null) tagString += this.Equipment.ToString();
            tagString += this.TagPOS.ToString();
            tagString += this.TagOOS.ToString();
            tagString += this.GhostTag.ToString();
            if (this.TagPOSReason != null) tagString += this.TagPOSReason;
            if (this.TagOOSReason != null) tagString += this.TagOOSReason;
            if (this.InstalledResponse != null) tagString += this.InstalledResponse;
            tagString += this.InstalledDate.ToString();
            tagString += this.HCServiceDate.ToString();
            tagString += this.pnpId;
            tagString += this.DrawingObjectXY;
            tagString += this.CVSReason;

            return tagString.GetHashCode();
        }

        public override bool Equals(object obj)
        {

            bool foundMatch = true;

            if (obj.GetType() != typeof(TaggedComponent) || obj == null) return false;

            TaggedComponent otherTag = (TaggedComponent)obj;

            //if (this.Id == "0336ca58-7718-46cc-8093-d63a8206547d" && otherTag.Id == "0336ca58-7718-46cc-8093-d63a8206547d")
            //{
            //    Console.WriteLine("Stop Here");
            //}

            foundMatch = foundMatch && foundMatch && this.Id == otherTag.Id;
            foundMatch = foundMatch && this.Access == otherTag.Access;
            foundMatch = foundMatch && this.Area == otherTag.Area;
            foundMatch = foundMatch && this.AttachDrawing == otherTag.AttachDrawing;
            foundMatch = foundMatch && this.Batch == otherTag.Batch;
            foundMatch = foundMatch && this.ChemicalState == otherTag.ChemicalState;
            foundMatch = foundMatch && this.Children.ToString() == otherTag.Children.ToString();
            foundMatch = foundMatch && this.ComponentType == otherTag.ComponentType;
            foundMatch = foundMatch && this.Drawing == otherTag.Drawing;
            foundMatch = foundMatch && this.EngineeringTag == otherTag.EngineeringTag;
            foundMatch = foundMatch && this.Inspected == otherTag.Inspected;
            foundMatch = foundMatch && this.InspectionBackground == otherTag.InspectionBackground;
            foundMatch = foundMatch && this.InspectionDate == otherTag.InspectionDate;
            foundMatch = foundMatch && this.InspectionInspector == otherTag.InspectionInspector;
            foundMatch = foundMatch && this.InspectionReading == otherTag.InspectionReading;
            foundMatch = foundMatch && this.isDrawingTag == otherTag.isDrawingTag;
            foundMatch = foundMatch && this.LDARTag == otherTag.LDARTag;
            foundMatch = foundMatch && this.Location == otherTag.Location;
            foundMatch = foundMatch && this.Manufacturer == otherTag.Manufacturer;
            foundMatch = foundMatch && this.MOCNumber == otherTag.MOCNumber;
            foundMatch = foundMatch && this.PreviousTag == otherTag.PreviousTag;
            foundMatch = foundMatch && this.PreviousTagExtension == otherTag.PreviousTagExtension;
            foundMatch = foundMatch && this.Property == otherTag.Property;
            foundMatch = foundMatch && this.ReferenceTag == otherTag.ReferenceTag;
            foundMatch = foundMatch && this.RouteSequence == otherTag.RouteSequence;
            foundMatch = foundMatch && this.Size == otherTag.Size;
            foundMatch = foundMatch && this.Stream == otherTag.Stream;
            foundMatch = foundMatch && this.Unit == otherTag.Unit;
            foundMatch = foundMatch && this.RemoveInProject == otherTag.RemoveInProject;
            foundMatch = foundMatch && this.Extension == otherTag.Extension;
            foundMatch = foundMatch && this.Equipment == otherTag.Equipment;
            foundMatch = foundMatch && this.TagPOS == otherTag.TagPOS;
            foundMatch = foundMatch && this.TagOOS == otherTag.TagOOS;
            foundMatch = foundMatch && this.GhostTag == otherTag.GhostTag;
            foundMatch = foundMatch && this.TagPOSReason == otherTag.TagPOSReason;
            foundMatch = foundMatch && this.TagOOSReason == otherTag.TagOOSReason;
            foundMatch = foundMatch && this.InstalledResponse == otherTag.InstalledResponse;
            foundMatch = foundMatch && this.InstalledDate == otherTag.InstalledDate;
            foundMatch = foundMatch && this.HCServiceDate == otherTag.HCServiceDate;
            foundMatch = foundMatch && this.pnpId == otherTag.pnpId;
            foundMatch = foundMatch && this.DrawingObjectXY == otherTag.DrawingObjectXY;
            foundMatch = foundMatch && this.CVSReason == otherTag.CVSReason;

            return foundMatch;
        }

        public string GetHeader()
        {
            List<string> values = new List<string>();
            values.Add("\"Id\"");
            values.Add("\"EngineeringTag\"");
            values.Add("\"ComponentType\"");
            values.Add("\"Location\"");
            values.Add("\"LDARTag\"");
            values.Add("\"Extension\"");
            values.Add("\"PreviousTag\"");
            values.Add("\"PreviousTagExtension\"");
            values.Add("\"PreviousTagId\"");
            values.Add("\"ReferenceTag\"");
            values.Add("\"Access\"");
            values.Add("\"UTMReason\"");
            values.Add("\"CreatedDate\"");
            values.Add("\"ModifiedDate\"");
            values.Add("\"ModifiedBy\"");
            values.Add("\"Stream\"");
            values.Add("\"Drawing\"");
            values.Add("\"ChemicalState\"");
            values.Add("\"Size\"");
            values.Add("\"MOCNumber\"");
            values.Add("\"Unit\"");
            values.Add("\"Area\"");
            values.Add("\"Equipment\"");
            values.Add("\"Batch\"");
            values.Add("\"Manufacturer\"");
            values.Add("\"Property\"");
            values.Add("\"RouteSequence\"");
            values.Add("\"InspectionBackground\"");
            values.Add("\"InspectionReading\"");
            values.Add("\"InspectionInstrument\"");
            values.Add("\"InspectionDate\"");
            values.Add("\"InspectionInspector\"");
            values.Add("\"Inspected\"");
            values.Add("\"AttachDrawing\"");
            values.Add("\"RemoveInProject\"");
            values.Add("\"TagPOS\"");
            values.Add("\"TagOOS\"");
            values.Add("\"GhostTag\"");
            values.Add("\"TagPOSReason\"");
            values.Add("\"TagOOSReason\"");
            values.Add("\"InstalledResponse\"");
            values.Add("\"InstalledDate\"");
            values.Add("\"HCServiceDate\"");
            values.Add("\"isDrawingTag\"");
            values.Add("\"isChild\"");
            values.Add("\"pnpId\"");
            values.Add("\"ErrorMessage\"");
            values.Add("\"WarningMessage\"");
            values.Add("\"DrawingObjectXY\"");
            values.Add("\"CVSReason\"");
            values.Add("\"Relation\"");
            //values.Add("\"ExtraFields\"");
            return String.Join(",", values.ToArray());
        }

        public List<string> GetChildrenAsComponents()
        {
            List<string> children = new List<string>();
            foreach (ChildComponent child in Children)
            {
                List<string> values = new List<string>();
                values.Add("\"" + Id + "|" + child.LDARTag + "\"");
                values.Add("\"" + string.Empty + "\"");
                values.Add("\"" + TaggedComponent.textCleaner(child.ComponentType) + "\"");
                values.Add("\"" + child.Location.Replace('"', '‟').Replace('\\', '‖') + "\"");
                values.Add("\"" + TaggedComponent.textCleaner(child.LDARTag) + "\"");
                values.Add("\"" + TaggedComponent.textCleaner(child.Extension) + "\"");
                values.Add("\"" + TaggedComponent.textCleaner(child.PreviousTag) + "\"");
                values.Add("\"" + TaggedComponent.textCleaner(child.PreviousTagExtension) + "\"");
                values.Add("\"" + child.PreviousTagId.ToString() + "\"");
                values.Add("\"" + TaggedComponent.textCleaner(ReferenceTag) + "\"");
                values.Add("\"" + TaggedComponent.textCleaner(Access) + "\"");
                values.Add("\"" + TaggedComponent.textCleaner(UTMReason) + "\"");
                values.Add("\"" + CreatedDate + "\"");
                values.Add("\"" + ModifiedDate + "\"");
                values.Add("\"" + ModifiedBy + "\"");
                values.Add("\"" + TaggedComponent.textCleaner(Stream) + "\"");
                values.Add("\"" + TaggedComponent.textCleaner(Drawing) + "\"");
                values.Add("\"" + TaggedComponent.textCleaner(ChemicalState) + "\"");
                values.Add("\"" + child.Size.ToString() + "\"");
                values.Add("\"" + TaggedComponent.textCleaner(MOCNumber) + "\"");
                values.Add("\"" + TaggedComponent.textCleaner(Unit) + "\"");
                values.Add("\"" + TaggedComponent.textCleaner(Area) + "\"");
                values.Add("\"" + TaggedComponent.textCleaner(Equipment) + "\"");
                values.Add("\"" + TaggedComponent.textCleaner(Batch) + "\"");
                values.Add("\"" + TaggedComponent.textCleaner(Manufacturer) + "\"");
                values.Add("\"" + TaggedComponent.textCleaner(Property) + "\"");
                values.Add("\"" + RouteSequence.ToString() + "\"");
                values.Add("\"" + child.InspectionBackground + "\"");
                values.Add("\"" + child.InspectionReading + "\"");
                values.Add("\"" + TaggedComponent.textCleaner(child.InspectionInstrument) + "\"");
                values.Add("\"" + child.InspectionDate + "\"");
                values.Add("\"" + TaggedComponent.textCleaner(child.InspectionInspector) + "\"");
                values.Add("\"" + child.Inspected + "\"");
                values.Add("\"" + AttachDrawing + "\"");
                values.Add("\"" + RemoveInProject + "\"");
                values.Add("\"" + TagPOS + "\"");
                values.Add("\"" + TagOOS + "\"");
                values.Add("\"" + GhostTag + "\"");
                values.Add("\"" + TaggedComponent.textCleaner(TagPOSReason) + "\"");
                values.Add("\"" + TaggedComponent.textCleaner(TagOOSReason) + "\"");
                values.Add("\"" + TaggedComponent.textCleaner(InstalledResponse) + "\"");
                values.Add("\"" + InstalledDate.ToString() + "\"");
                values.Add("\"" + HCServiceDate.ToString() + "\"");
                values.Add("\"" + isDrawingTag + "\"");
                values.Add("\"" + pnpId + "\"");
                values.Add("\"" + true + "\"");
                values.Add("\"" + ErrorMessage + "\"");
                values.Add("\"" + WarningMessage + "\"");
                values.Add("\"" + DrawingObjectXY + "\"");
                values.Add("\"" + CVSReason + "\"");
                values.Add("\"" + Id + "\"");
                //values.Add("\"" + ExtraFields.ToString() + "\"");
                children.Add(String.Join(",", values.ToArray()));
            }
            return children;
        }

        public List<string> getHeaderAsList()
        {
            List<string> values = new List<string>();
            values.Add("Id");
            values.Add("EngineeringTag");
            values.Add("ComponentType");
            values.Add("Location");
            values.Add("LDARTag");
            values.Add("Extension");
            values.Add("PreviousTag");
            values.Add("PreviousTagExtension");
            values.Add("PreviousTagId");
            values.Add("ReferenceTag");
            values.Add("Access");
            values.Add("UTMReason");
            values.Add("CreatedDate");
            values.Add("ModifiedDate");
            values.Add("ModifiedBy");
            values.Add("Stream");
            values.Add("Drawing");
            values.Add("ChemicalState");
            values.Add("Size");
            values.Add("MOCNumber");
            values.Add("Unit");
            values.Add("Area");
            values.Add("Equipment");
            values.Add("Batch");
            values.Add("Manufacturer");
            values.Add("Property");
            values.Add("RouteSequence");
            values.Add("InspectionBackground");
            values.Add("InspectionReading");
            values.Add("InspectionInstrument");
            values.Add("InspectionDate");
            values.Add("InspectionInspector");
            values.Add("Inspected");
            values.Add("AttachDrawing");
            values.Add("RemoveInProject");
            values.Add("TagPOS");
            values.Add("TagOOS");
            values.Add("GhostTag");
            values.Add("TagPOSReason");
            values.Add("TagOOSReason");
            values.Add("InstalledResponse");
            values.Add("InstalledDate");
            values.Add("HCServiceDate");
            values.Add("isDrawingTag");
            values.Add("isChild");
            values.Add("pnpId");
            values.Add("ErrorMessage");
            values.Add("WarningMessage");
            values.Add("DrawingObjectXY");
            values.Add("CVSReason");
            values.Add("Relation");
            values.Add("ExtraFields");

            return values;
        }

        public static string textCleaner(string str)
        {
            //this function makes sure certain characters aren't fouling up our csv
            string tmpString = string.Empty;

            if (string.IsNullOrEmpty(str)) return "";

            foreach (char c in str)
            {
                if (c != '"' && c != '\\')
                {
                    tmpString += c;
                }
            }

            return tmpString;
        }

        public void Clear()
        {
            this.Id = string.Empty;
            EngineeringTag = string.Empty;
            ComponentType = string.Empty;
            Location = string.Empty;
            LDARTag = string.Empty;
            Extension = string.Empty;
            PreviousTag  = string.Empty;
            PreviousTagExtension = string.Empty;
            ReferenceTag = string.Empty;
            Access  = string.Empty;
            UTMReason = string.Empty;
            CreatedDate = string.Empty;
            ModifiedDate = string.Empty;
            ModifiedBy = string.Empty;
            Stream = string.Empty;
            Drawing = string.Empty;
            ChemicalState = string.Empty;
            Size = 0;
            MOCNumber = string.Empty;
            Unit = string.Empty;
            Area = string.Empty;
            Equipment = string.Empty;
            Batch = string.Empty;
            Manufacturer = string.Empty;
            Property = string.Empty;
            RouteSequence = 0;
            InspectionBackground = 0;
            InspectionReading = 0;
            InspectionInstrument = string.Empty;
            InspectionDate = DateTime.Now;
            InspectionInspector = string.Empty;
            Inspected = false;
            AttachDrawing = false;
            RemoveInProject = false;
            TagOOS = false;
            TagPOS = false;
            GhostTag = false;
            TagOOSReason = string.Empty;
            TagPOSReason = string.Empty;
            InstalledResponse = string.Empty;
            isDrawingTag = false;
            pnpId = string.Empty;
            ErrorMessage = string.Empty;
            WarningMessage = string.Empty;
            DrawingObjectXY = string.Empty;
            Children = new List<ChildComponent>();
        }

        public string setValueByName(string fieldToChange, string changeValue)
        {
            string errorMessage = string.Empty;
            
            PropertyInfo prop = this.GetType().GetProperty(fieldToChange, BindingFlags.Public | BindingFlags.Instance);
            if (null != prop && prop.CanWrite)
            {
                try
                {
                    string propFullName = prop.PropertyType.FullName;
                    if (propFullName.Contains("Nullable") && propFullName.Contains("DateTime")) propFullName = "Nullable.DateTime";
                    switch (propFullName)
                    {
                        case "System.Double":
                            if (!String.IsNullOrEmpty(changeValue))
                            {
                                prop.SetValue(this, Double.Parse(changeValue), null);
                            }
                            else
                            {
                                prop.SetValue(this, 0, null);
                            }
                            break;
                        case "System.Int32":
                            if (!String.IsNullOrEmpty(changeValue))
                            {
                                prop.SetValue(this, Int32.Parse(changeValue), null);
                            }
                            break;
                        case "System.DateTime":
                            if (!String.IsNullOrEmpty(changeValue))
                            {
                                prop.SetValue(this, DateTime.Parse(changeValue), null);
                            }
                            break;
                        case "Nullable.DateTime":
                            if (!String.IsNullOrEmpty(changeValue))
                            {
                                prop.SetValue(this, DateTime.Parse(changeValue), null);
                            }
                            else
                            {
                                prop.SetValue(this, null, null);
                            }
                            break;
                        case "System.Boolean":
                            if (!String.IsNullOrEmpty(changeValue))
                            {
                                if (changeValue == "0" || changeValue == "1")
                                {
                                    if (changeValue == "0")
                                        prop.SetValue(this, false, null);
                                    else
                                        prop.SetValue(this, true, null);
                                }
                                else
                                {
                                    prop.SetValue(this, bool.Parse(changeValue), null);
                                }
                            }
                            break;
                        case "System.String":
                            if (fieldToChange == "Location")
                            {
                                prop.SetValue(this, changeValue.Replace("\\\\", "\\").Replace("\"", "\\\""), null);
                            }
                            else
                            {
                                prop.SetValue(this, changeValue, null);
                            }
                            break;
                        default:
                            errorMessage = "Cannot load value of '" + changeValue + "' into Property for '" + prop.ReflectedType.ToString() + "'. The Type of '" + prop.PropertyType.FullName + "' is not supported.";
                            break;
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = "An error occurred while updating - " + ex.Message;
                    return errorMessage;
                }
            }
            return errorMessage;
        }

        public string getValueByName(string PropertyName)
        {

            string errorMessage = string.Empty;

            PropertyInfo prop = this.GetType().GetProperty(PropertyName, BindingFlags.Public | BindingFlags.Instance);

            string retValue = prop.GetValue(this, null).ToString();

            return retValue;
 
        }

        public string getHighestTagInSeries()
        {
            List<TaggedComponent> tList = new List<TaggedComponent>();

            tList.Add(this);
            if (this.Children.Count > 0)
            {
                tList.AddRange(this.GetChildrenAsTaggedComponent());
            }

            return tList.Max(c => c.LDARTag);            
        }

        public string getHighestExtensionInSeries()
        {
            List<TaggedComponent> tList = new List<TaggedComponent>();

            tList.Add(this);
            if (this.Children.Count > 0)
            {
                tList.AddRange(this.GetChildrenAsTaggedComponent());
            }

            return tList.Max(c => c.Extension); 
        }

        public object getPropertyValueByName(string PropertyName)
        {

            PropertyInfo prop = this.GetType().GetProperty(PropertyName, BindingFlags.Public | BindingFlags.Instance);

            return prop.GetValue(this, null);

        }

        public static List<TaggedComponent> rejoinTagFamilies(List<TaggedComponent> tagLoaners, bool useExtension)
        {
            List<TaggedComponent> rejoinedTags = new List<TaggedComponent>();
            List<string> usedTags = new List<string>();

            Regex TagSearch = new Regex(@"\w*\.\w*");

            if (useExtension)
            {
                foreach (TaggedComponent tc in tagLoaners)
                {
                    if (!usedTags.Contains(tc.LDARTag + tc.Extension))
                    {
                        string inLDARTag = tc.LDARTag;
                        string inExtension = tc.Extension;

                        List<TaggedComponent> tagFamily = null;
                        List<TaggedComponent> tempFamily = null;

                        tagFamily = tagLoaners.Where(c => c.LDARTag == tc.LDARTag).ToList();
                        tempFamily = tagFamily.OrderBy(d => d.Extension).ToList();

                        TaggedComponent firstTag = null;
                        foreach (TaggedComponent memberTag in tempFamily)
                        {
                            if (firstTag == null)
                            {
                                firstTag = memberTag.CopyComponentAndChildren().FirstOrDefault();
                                firstTag.isChild = false;
                            }
                            else
                            {
                                ChildComponent child = new ChildComponent(memberTag);
                                firstTag.Children.Add(child);
                                usedTags.Add(child.LDARTag + child.Extension);
                            }
                        }
                        rejoinedTags.Add(firstTag);
                        usedTags.Add(firstTag.LDARTag + firstTag.Extension);
                    }
                }
            }
            else
            {
                foreach (TaggedComponent tc in tagLoaners)
                {
                    if (!usedTags.Contains(tc.LDARTag))
                    {
                        string inLDARTag = tc.LDARTag;

                        List<TaggedComponent> tagFamily = null;
                        List<TaggedComponent> tempFamily = null;

                        string baseTag = string.Empty;
                        if (tc.LDARTag.Contains('.'))
                        {
                            baseTag = tc.LDARTag.Split('.')[0];
                        }
                        else
                        {
                            baseTag = tc.LDARTag;
                        }
                        tagFamily = tagLoaners.Where(c => c.LDARTag.StartsWith(baseTag)).ToList();
                        tempFamily = tagFamily.OrderBy(d => d.LDARTag).ToList();

                        TaggedComponent firstTag = null;
                        foreach (TaggedComponent memberTag in tempFamily)
                        {
                            if (firstTag == null)
                            {
                                firstTag = memberTag.CopyComponentAndChildren().FirstOrDefault();
                                firstTag.isChild = false;
                            }
                            else
                            {
                                ChildComponent child = new ChildComponent(memberTag);
                                firstTag.Children.Add(child);
                                usedTags.Add(child.LDARTag + child.Extension);
                            }
                        }
                        rejoinedTags.Add(firstTag);
                        usedTags.Add(firstTag.LDARTag + firstTag.Extension);
                    }
                }
            }
            
            return rejoinedTags;
        }
    }

    public class ChildComponent
    {
        public string ComponentType { get; set; }
        public string LDARTag { get; set; }
        public string Extension { get; set; }
        public string Location { get; set; }
        public string PreviousTag { get; set; }
        public int PreviousTagId { get; set; }
        public string PreviousTagExtension { get; set; }
        public double RouteSequence { get; set; }
        public double InspectionBackground { get; set; }
        public double InspectionReading { get; set; }
        public string InspectionInstrument { get; set; }
        public string InspectionInspector { get; set; }
        public DateTime InspectionDate { get; set; }
        public bool Inspected { get; set; }
        public double Size { get; set; }

        public ChildComponent()
        {

        }

        public ChildComponent(TaggedComponent fullComponent)
        {
            ComponentType = fullComponent.ComponentType;
            LDARTag = fullComponent.LDARTag;
            Extension = fullComponent.Extension;
            Location = fullComponent.Location;
            PreviousTag = fullComponent.PreviousTag;
            PreviousTagExtension = fullComponent.PreviousTagExtension;
            PreviousTagId = fullComponent.PreviousTagId;
            RouteSequence = fullComponent.RouteSequence;
            InspectionBackground = fullComponent.InspectionBackground;
            InspectionReading = fullComponent.InspectionReading;
            InspectionInstrument = fullComponent.InspectionInstrument;
            InspectionInspector = fullComponent.InspectionInspector;
            InspectionDate = fullComponent.InspectionDate;
            Inspected = fullComponent.Inspected;
            Size = fullComponent.Size;
        }

        //DONT UNCOMMENT
        //public override string ToString()
        //{
        //    List<string> values = new List<string>();
        //    values.Add("\"" + ComponentType + "\"");
        //    values.Add("\"" + Location + "\"");
        //    values.Add("\"" + LDARTag + "\"");
        //    values.Add("\"" + PreviousTag + "\"");
        //    values.Add("\"" + InspectionBackground + "\"");
        //    values.Add("\"" + InspectionReading + "\"");
        //    values.Add("\"" + InspectionInstrument + "\"");
        //    values.Add("\"" + InspectionDate + "\"");
        //    values.Add("\"" + InspectionInspector + "\"");
        //    values.Add("\"" + Inspected + "\"");
        //    values.Add("\"Child\"");  //THIS WONT WORK IF THIS METHOD COMES BACK
        //    return String.Join(",", values.ToArray());
        //}
    }

    //public class ExtraTagFields
    //{
    //    public string FieldName { get; set; }
    //    public string FieldValue { get; set; }

    //    public override string ToString()
    //    {
    //        return FileUtilities.SerializeObject<ExtraTagFields>(this);
    //    }
    //}

}
