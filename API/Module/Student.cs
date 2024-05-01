using System;
using System.Collections.Generic;

namespace API.Module
{
    public partial class Student
    {
        public Student()
        {
            Attendances = new HashSet<Attendance>();
            Enrollments = new HashSet<Enrollment>();
        }

        public long StudentId { get; set; }
        public byte[]? Photo { get; set; }
        public string? FirstName { get; set; }
        public string? FahterName { get; set; }
        public string? GrandFatherName { get; set; }
        public string? SurName { get; set; }
        /// <summary>
        /// 1- Male
        /// 2- Female
        /// </summary>
        public short? Gender { get; set; }
        public string? NationalId { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? JoinDate { get; set; }
        /// <summary>
        /// &apos;A+&apos;,&apos;A-&apos;,&apos;B+&apos;, &apos;B-&apos;, &apos;AB+&apos;, &apos;AB-&apos;, &apos;O+&apos;, &apos;O-&apos;
        /// </summary>
        public string? BloodType { get; set; }
        public short? YearClassId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public byte[]? Password { get; set; }
        public int? RegionId { get; set; }
        public string? Address { get; set; }
        public string? ParentName { get; set; }
        public string ParentPhone { get; set; }
        public string? ParentEmail { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; internal set; }
        public int? UpdatedBy { get; internal set; }
        public short? Status { get; set; }

        public virtual Region? Region { get; set; }
        public virtual YearClass? YearClass { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
 
    }
}
