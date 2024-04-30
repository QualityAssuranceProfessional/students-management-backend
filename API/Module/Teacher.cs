using System;
using System.Collections.Generic;

namespace API.Module
{
    public partial class Teacher
    {
        public Teacher()
        {
            Attendances = new HashSet<Attendance>();
            Libraries = new HashSet<Library>();
            Subjects = new HashSet<Subject>();
        }

        public long TeacherId { get; set; }
        public string? FirstName { get; set; }
        public string? FatherName { get; set; }
        public string? GrandFatherName { get; set; }
        public string? SurName { get; set; }
       
        public short? Gender { get; set; }
        public string? NationalId { get; set; }
        public DateTime? JoinDate { get; set; }
        public string? Specialization { get; set; }
        public int? RegionId { get; set; }
        public string? Address { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public byte[]? Password { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public short? Status { get; set; }

        public virtual Region? Region { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Library> Libraries { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
