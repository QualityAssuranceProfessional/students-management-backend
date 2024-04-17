using System;
using System.Collections.Generic;

namespace API.Module
{
    public partial class Library
    {
        public int LibraryId { get; set; }
        public string? Title { get; set; }
        public string? FileName { get; set; }
        public short? AcadimicLevelId { get; set; }
        public long? TeacherId { get; set; }
        public short? ClassId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public short? Status { get; set; }

        public virtual AcadimicLevel? AcadimicLevel { get; set; }
        public virtual Class? Class { get; set; }
        public virtual Teacher? Teacher { get; set; }
    }
}
