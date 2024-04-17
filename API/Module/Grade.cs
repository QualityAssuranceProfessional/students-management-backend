using System;
using System.Collections.Generic;

namespace API.Module
{
    public partial class Grade
    {
        public int GradeId { get; set; }
        public int? SubjectId { get; set; }
        public int? SemesterId { get; set; }
        public double? ExamGrade { get; set; }
        public double? HomeworkGrade { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public short? Status { get; set; }
        public int? EnrollmentId { get; set; }

        public virtual Enrollment? Enrollment { get; set; }
        public virtual Semester? Semester { get; set; }
    }
}
