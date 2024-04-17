using System;
using System.Collections.Generic;

namespace API.Module
{
    public partial class Attendance
    {
        public long AttendencId { get; set; }
        public long? StudentId { get; set; }
        public short? AcadimicLevelId { get; set; }
        public short? ClassId { get; set; }
        public long? TeacherId { get; set; }
        public DateTime? AttendenceDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public short? Status { get; set; }
        public int? SemesterId { get; set; }

        public virtual AcadimicLevel? AcadimicLevel { get; set; }
        public virtual Class? Class { get; set; }
        public virtual Semester? Semester { get; set; }
        public virtual Student? Student { get; set; }
        public virtual Teacher? Teacher { get; set; }
    }
}
