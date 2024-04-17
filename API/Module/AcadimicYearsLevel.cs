using System;
using System.Collections.Generic;

namespace API.Module
{
    public partial class AcadimicYearsLevel
    {
        public AcadimicYearsLevel()
        {
            AcadimicYearsLevelSubjects = new HashSet<AcadimicYearsLevelSubject>();
            YearClasses = new HashSet<YearClass>();
        }

        public int AcademicYearsLevelId { get; set; }
        public short? AcademicYearId { get; set; }
        public short? AcadimicLevelId { get; set; }

        public virtual AcademicYear? AcademicYear { get; set; }
        public virtual AcadimicLevel? AcadimicLevel { get; set; }
        public virtual ICollection<AcadimicYearsLevelSubject> AcadimicYearsLevelSubjects { get; set; }
        public virtual ICollection<YearClass> YearClasses { get; set; }
    }
}
