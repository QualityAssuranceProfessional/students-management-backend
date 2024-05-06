using System;
using System.Collections.Generic;

namespace API.Module
{
    public partial class AcademicYear
    {
        public AcademicYear()
        {
            AcadimicYearsLevels = new HashSet<AcadimicYearsLevel>();
        }

        public short AcademicYearId { get; set; }
        public string? AcademicYearName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public short? Status { get; set; }

        public virtual ICollection<AcadimicYearsLevel> AcadimicYearsLevels { get; set; }
    }
}
