using System;
using System.Collections.Generic;

namespace API.Module
{
    public partial class Enrollment
    {
        public Enrollment()
        {
            Grades = new HashSet<Grade>();
        }

        public int EnrollmentId { get; set; }
        public long? StudentId { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public short? YearClassId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public short? Status { get; set; }

        public virtual Student? Student { get; set; }
        public virtual YearClass? YearClass { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
    }
}
