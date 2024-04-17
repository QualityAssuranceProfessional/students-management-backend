using System;
using System.Collections.Generic;

namespace API.Module
{
    public partial class Class
    {
        public Class()
        {
            Attendances = new HashSet<Attendance>();
            Libraries = new HashSet<Library>();
            Subjects = new HashSet<Subject>();
            YearClasses = new HashSet<YearClass>();
        }

        public short ClassId { get; set; }
        public string? ClassName { get; set; }
        public short? ClassNumber { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public short? Status { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Library> Libraries { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
        public virtual ICollection<YearClass> YearClasses { get; set; }
    }
}
