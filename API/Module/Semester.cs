using System;
using System.Collections.Generic;

namespace API.Module
{
    public partial class Semester
    {
        public Semester()
        {
            Attendances = new HashSet<Attendance>();
            Grades = new HashSet<Grade>();
        }

        public int SemesterId { get; set; }
        public string? SemesterName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public short? Status { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
    }
}
