using System;
using System.Collections.Generic;

namespace API.Module
{
    public partial class AcadimicLevel
    {
        public AcadimicLevel()
        {
            AcadimicYearsLevels = new HashSet<AcadimicYearsLevel>();
            Attendances = new HashSet<Attendance>();
            Libraries = new HashSet<Library>();
            Subjects = new HashSet<Subject>();
        }

        public short AcadimicLevelId { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public short? Status { get; set; }
        /// <summary>
        /// 1- ابتدائي
        /// 2- اعدادي
        /// 3- ثانوي
        /// </summary>
        public short? AcadmicLevelType { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public virtual ICollection<AcadimicYearsLevel> AcadimicYearsLevels { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Library> Libraries { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
