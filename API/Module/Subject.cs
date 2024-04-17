using System;
using System.Collections.Generic;

namespace API.Module
{
    public partial class Subject
    {
        public Subject()
        {
            AcadimicYearsLevelSubjects = new HashSet<AcadimicYearsLevelSubject>();
        }

        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public short? ClassId { get; set; }
        public long? TeacherId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public short? Status { get; set; }
        public short? AcadimicLevelId { get; set; }

        public virtual AcadimicLevel? AcadimicLevel { get; set; }
        public virtual Class? Class { get; set; }
        public virtual Teacher? Teacher { get; set; }
        public virtual ICollection<AcadimicYearsLevelSubject> AcadimicYearsLevelSubjects { get; set; }
    }
}
