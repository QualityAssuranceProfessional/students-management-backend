using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API.Module
{
    public partial class SchoolManagementSystemContext : DbContext
    {
        public SchoolManagementSystemContext()
        {
        }

        public SchoolManagementSystemContext(DbContextOptions<SchoolManagementSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AcademicYear> AcademicYears { get; set; } = null!;
        public virtual DbSet<AcadimicLevel> AcadimicLevels { get; set; } = null!;
        public virtual DbSet<AcadimicYearsLevel> AcadimicYearsLevels { get; set; } = null!;
        public virtual DbSet<AcadimicYearsLevelSubject> AcadimicYearsLevelSubjects { get; set; } = null!;
        public virtual DbSet<Attendance> Attendances { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Enrollment> Enrollments { get; set; } = null!;
        public virtual DbSet<Grade> Grades { get; set; } = null!;
        public virtual DbSet<Library> Libraries { get; set; } = null!;
        public virtual DbSet<Region> Regions { get; set; } = null!;
        public virtual DbSet<Semester> Semesters { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<YearClass> YearClasses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=SchoolManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcademicYear>(entity =>
            {
                entity.Property(e => e.AcademicYearId).HasColumnName("AcademicYearID");

                entity.Property(e => e.AcademicYearName).HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<AcadimicLevel>(entity =>
            {
                entity.Property(e => e.AcadimicLevelId).ValueGeneratedNever();

                entity.Property(e => e.AcadmicLevelType).HasComment("1- ابتدائي\r\n2- اعدادي\r\n3- ثانوي");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsFixedLength();
            });

            modelBuilder.Entity<AcadimicYearsLevel>(entity =>
            {
                entity.HasKey(e => e.AcademicYearsLevelId);

                entity.ToTable("AcadimicYearsLevel");

                entity.Property(e => e.AcademicYearsLevelId)
                    .ValueGeneratedNever()
                    .HasColumnName("AcademicYearsLevelID");

                entity.Property(e => e.AcademicYearId).HasColumnName("AcademicYearID");

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.AcadimicYearsLevels)
                    .HasForeignKey(d => d.AcademicYearId)
                    .HasConstraintName("FK_AcadimicYearsLevel_AcademicYears");

                entity.HasOne(d => d.AcadimicLevel)
                    .WithMany(p => p.AcadimicYearsLevels)
                    .HasForeignKey(d => d.AcadimicLevelId)
                    .HasConstraintName("FK_AcadimicYearsLevel_AcadimicLevels");
            });

            modelBuilder.Entity<AcadimicYearsLevelSubject>(entity =>
            {
                entity.ToTable("AcadimicYearsLevelSubject");

                entity.Property(e => e.AcademicYearsLevelId).HasColumnName("AcademicYearsLevelID");

                entity.HasOne(d => d.AcademicYearsLevel)
                    .WithMany(p => p.AcadimicYearsLevelSubjects)
                    .HasForeignKey(d => d.AcademicYearsLevelId)
                    .HasConstraintName("FK_AcadimicYearsLevelSubject_AcadimicYearsLevel");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.AcadimicYearsLevelSubjects)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_AcadimicYearsLevelSubject_Subjects");
            });

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasKey(e => e.AttendencId);

                entity.ToTable("Attendance");

                entity.Property(e => e.AttendencId).HasColumnName("AttendencID");

                entity.Property(e => e.AttendenceDate)
                    .HasColumnType("date")
                    .HasColumnName("Attendence_date");

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.HasOne(d => d.AcadimicLevel)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.AcadimicLevelId)
                    .HasConstraintName("FK_Attendance_AcadimicLevels");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_Attendance_Classes");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.SemesterId)
                    .HasConstraintName("FK_Attendance_Semesters");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_Attendance_Students");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK_Attendance_Teachers");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.Property(e => e.EnrollmentId).HasColumnName("EnrollmentID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EnrollmentDate).HasColumnType("datetime");

                entity.Property(e => e.YearClassId).HasColumnName("YearClassID");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_Enrollments_Students");

                entity.HasOne(d => d.YearClass)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.YearClassId)
                    .HasConstraintName("FK_Enrollments_YearClass");
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.Property(e => e.GradeId).HasColumnName("GradeID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EnrollmentId).HasColumnName("EnrollmentID");

                entity.Property(e => e.SemesterId).HasColumnName("SemesterID");

                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.HasOne(d => d.Enrollment)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.EnrollmentId)
                    .HasConstraintName("FK_Grades_Enrollments");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.SemesterId)
                    .HasConstraintName("FK_Grades_Semesters");
            });

            modelBuilder.Entity<Library>(entity =>
            {
                entity.ToTable("Library");

                entity.Property(e => e.LibraryId).HasColumnName("LibraryID");

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.AcadimicLevel)
                    .WithMany(p => p.Libraries)
                    .HasForeignKey(d => d.AcadimicLevelId)
                    .HasConstraintName("FK_Library_AcadimicLevels");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Libraries)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_Library_Classes");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Libraries)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK_Library_Teachers");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Regions)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Regions_Cities");
            });

            modelBuilder.Entity<Semester>(entity =>
            {
                entity.Property(e => e.SemesterId).HasColumnName("SemesterID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.SemesterName).HasMaxLength(50);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.BloodType)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("'A+','A-','B+', 'B-', 'AB+', 'AB-', 'O+', 'O-'");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FahterName).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gender).HasComment("1- Male\r\n2- Female");

                entity.Property(e => e.GrandFatherName).HasMaxLength(50);

                entity.Property(e => e.JoinDate).HasColumnType("date");

                entity.Property(e => e.NationalId)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.ParentEmail)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.ParentName).HasMaxLength(100);

                entity.Property(e => e.ParentPhone).HasMaxLength(20);

                entity.Property(e => e.Password).HasMaxLength(150);

                entity.Property(e => e.SurName).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(100);

                entity.Property(e => e.YearClassId).HasColumnName("YearClassID");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("FK_Students_Regions");

                entity.HasOne(d => d.YearClass)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.YearClassId)
                    .HasConstraintName("FK_Students_YearClass");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.SubjectName).HasMaxLength(150);

                entity.HasOne(d => d.AcadimicLevel)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.AcadimicLevelId)
                    .HasConstraintName("FK_Subjects_AcadimicLevels");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_Subjects_Classes");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK_Subjects_Teachers");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FatherName).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gender).HasComment("1- Male  2-Female ");

                entity.Property(e => e.GrandFatherName).HasMaxLength(50);

                entity.Property(e => e.JoinDate).HasColumnType("date");

                entity.Property(e => e.NationalId)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Password).HasMaxLength(150);

                entity.Property(e => e.Specialization).HasMaxLength(100);

                entity.Property(e => e.SurName).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(100);

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("FK_Teachers_Regions");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName).HasMaxLength(75);

                entity.Property(e => e.Password).HasMaxLength(150);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<YearClass>(entity =>
            {
                entity.ToTable("YearClass");

                entity.Property(e => e.YearClassId).HasColumnName("YearClassID");

                entity.Property(e => e.AcademicYearsLevelId).HasColumnName("AcademicYearsLevelID");

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.HasOne(d => d.AcademicYearsLevel)
                    .WithMany(p => p.YearClasses)
                    .HasForeignKey(d => d.AcademicYearsLevelId)
                    .HasConstraintName("FK_YearClass_AcadimicYearsLevel");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.YearClasses)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_YearClass_Classes");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
