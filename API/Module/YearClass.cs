using System;
using System.Collections.Generic;

namespace API.Module
{
    public partial class YearClass
    {
        public YearClass()
        {
            Enrollments = new HashSet<Enrollment>();
            Students = new HashSet<Student>();
        }

        public short YearClassId { get; set; }
        public int? AcademicYearsLevelId { get; set; }
        public short? ClassId { get; set; }

        public virtual AcadimicYearsLevel? AcademicYearsLevel { get; set; }
        public virtual Class? Class { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
