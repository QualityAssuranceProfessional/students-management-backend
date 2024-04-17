using System;
using System.Collections.Generic;

namespace API.Module
{
    public partial class AcadimicYearsLevelSubject
    {
        public int AcadimicYearsLevelSubjectId { get; set; }
        public int? SubjectId { get; set; }
        public int? AcademicYearsLevelId { get; set; }

        public virtual AcadimicYearsLevel? AcademicYearsLevel { get; set; }
        public virtual Subject? Subject { get; set; }
    }
}
