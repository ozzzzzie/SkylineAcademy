
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SkylineAcademy.Models
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrator> Administrators { get; set; } = null!;
        public virtual DbSet<ClassSchedule> ClassSchedules { get; set; } = null!;
        public virtual DbSet<Classroom> Classrooms { get; set; } = null!;
        public virtual DbSet<ClassroomAvailability> ClassroomAvailabilities { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Enrollement> Enrollements { get; set; } = null!;
        public virtual DbSet<Faculty> Faculties { get; set; } = null!;
        public virtual DbSet<Grade> Grades { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<Major> Majors { get; set; } = null!;
        public virtual DbSet<Prerequisite> Prerequisites { get; set; } = null!;
        public virtual DbSet<Slot> Slots { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;
        public virtual DbSet<TeacherAvailability> TeacherAvailabilities { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:skylineacademy.database.windows.net,1433;Initial Catalog=skylineacademy;Persist Security Info=False;User ID=oskaralkhateeb;Password=BmW310799!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=60;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.ToTable("ADMINISTRATORS");

                entity.HasIndex(e => e.AdministratorId, "UQ__ADMINIST__886E653E1AB87848")
                    .IsUnique();

                entity.Property(e => e.AdministratorId).HasColumnName("ADMINISTRATOR_ID");

                entity.Property(e => e.Aaddress)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("AADDRESS");

                entity.Property(e => e.Aemail)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("AEMAIL");

                entity.Property(e => e.Afname)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("AFNAME");

                entity.Property(e => e.Alname)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("ALNAME");

                entity.Property(e => e.Aphonenumber)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("APHONENUMBER");

                entity.Property(e => e.JobId).HasColumnName("JOB_ID");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Administrators)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ADMINISTR__JOB_I__3587F3E0");
            });

            modelBuilder.Entity<ClassSchedule>(entity =>
            {
                entity.HasKey(e => e.ScheduleId)
                    .HasName("PK__CLASS_SC__A9B60488AC3A50C5");

                entity.ToTable("CLASS_SCHEDULES");

                entity.HasIndex(e => e.ScheduleId, "UQ__CLASS_SC__A9B6048969AE67C4")
                    .IsUnique();

                entity.Property(e => e.ScheduleId).HasColumnName("SCHEDULE_ID");

                entity.Property(e => e.AdministratorId)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ADMINISTRATOR_ID");

                entity.Property(e => e.ClassroomId)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CLASSROOM_ID");

                entity.Property(e => e.CourseId)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("COURSE_ID");

                entity.Property(e => e.SlotId).HasColumnName("SLOT_ID");

                entity.Property(e => e.TeacherId)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("TEACHER_ID");

                entity.HasOne(d => d.Slot)
                    .WithMany(p => p.ClassSchedules)
                    .HasForeignKey(d => d.SlotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CLASS_SCH__SLOT___3E1D39E1");
            });

            modelBuilder.Entity<Classroom>(entity =>
            {
                entity.ToTable("CLASSROOMS");

                entity.HasIndex(e => e.ClassroomId, "UQ__CLASSROO__44667781E34D11FA")
                    .IsUnique();

                entity.Property(e => e.ClassroomId).HasColumnName("CLASSROOM_ID");

                entity.Property(e => e.Capacity)
                    .HasColumnName("CAPACITY")
                    .HasDefaultValueSql("((24))");

                entity.Property(e => e.Facilities)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("FACILITIES");
            });

            modelBuilder.Entity<ClassroomAvailability>(entity =>
            {
                entity.ToTable("CLASSROOM_AVAILABILITY");

                entity.HasIndex(e => e.ClassroomAvailabilityId, "UQ__CLASSROO__7FEDB81C417146FE")
                    .IsUnique();

                entity.Property(e => e.ClassroomAvailabilityId).HasColumnName("CLASSROOM_AVAILABILITY_ID");

                entity.Property(e => e.ClassroomId).HasColumnName("CLASSROOM_ID");

                entity.Property(e => e.Isavailable)
                    .IsRequired()
                    .HasColumnName("ISAVAILABLE")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SlotId).HasColumnName("SLOT_ID");

                entity.HasOne(d => d.Classroom)
                    .WithMany(p => p.ClassroomAvailabilities)
                    .HasForeignKey(d => d.ClassroomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CLASSROOM__CLASS__1DB06A4F");

                entity.HasOne(d => d.Slot)
                    .WithMany(p => p.ClassroomAvailabilities)
                    .HasForeignKey(d => d.SlotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CLASSROOM__SLOT___1CBC4616");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("COURSES");

                entity.HasIndex(e => e.CourseId, "UQ__COURSES__71CB31DAA264D7C7")
                    .IsUnique();

                entity.Property(e => e.CourseId).HasColumnName("COURSE_ID");

                entity.Property(e => e.Cname)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("CNAME");

                entity.Property(e => e.Credits).HasColumnName("CREDITS");

                entity.Property(e => e.MajorId).HasColumnName("MAJOR_ID");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.MajorId)
                    .HasConstraintName("FK__COURSES__MAJOR_I__282DF8C2");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("DEPARTMENTS");

                entity.HasIndex(e => e.DepartmentId, "UQ__DEPARTME__63E6136352FA611F")
                    .IsUnique();

                entity.Property(e => e.DepartmentId).HasColumnName("DEPARTMENT_ID");

                entity.Property(e => e.Ddescription)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("DDESCRIPTION");

                entity.Property(e => e.Dname)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("DNAME");
            });

            modelBuilder.Entity<Enrollement>(entity =>
            {
                entity.ToTable("ENROLLEMENT");

                entity.HasIndex(e => e.EnrollementId, "UQ__ENROLLEM__CDC00D0B6A9AB10B")
                    .IsUnique();

                entity.Property(e => e.EnrollementId).HasColumnName("ENROLLEMENT_ID");

                entity.Property(e => e.EnrollementDate)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("ENROLLEMENT_DATE");

                entity.Property(e => e.ScheduleId).HasColumnName("SCHEDULE_ID");

                entity.Property(e => e.StudentId).HasColumnName("STUDENT_ID");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.Enrollements)
                    .HasForeignKey(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ENROLLEME__SCHED__4B7734FF");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Enrollements)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ENROLLEME__STUDE__4A8310C6");
            });

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.ToTable("FACULTY");

                entity.HasIndex(e => e.FacultyId, "UQ__FACULTY__6755FF5AFB4EFA5B")
                    .IsUnique();

                entity.Property(e => e.FacultyId).HasColumnName("FACULTY_ID");

                entity.Property(e => e.Fname)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("FNAME");
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.ToTable("GRADES");

                entity.HasIndex(e => e.GradeId, "UQ__GRADES__CA41736AE3001181")
                    .IsUnique();

                entity.Property(e => e.GradeId).HasColumnName("GRADE_ID");

                entity.Property(e => e.EnrollementId)
                    .IsUnicode(true)
                    .HasColumnName("ENROLLEMENT_ID");

                entity.Property(e => e.Final).HasColumnName("FINAL");

                entity.Property(e => e.Midterm).HasColumnName("MIDTERM");

                entity.Property(e => e.Total).HasColumnName("TOTAL");

                entity.Property(e => e.Passedcourse).HasColumnName("PASSEDCOURSE");
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("JOBS");

                entity.HasIndex(e => e.JobId, "UQ__JOBS__2AC9D60B9986B1B7")
                    .IsUnique();

                entity.Property(e => e.JobId).HasColumnName("JOB_ID");

                entity.Property(e => e.DepartmentId).HasColumnName("DEPARTMENT_ID");

                entity.Property(e => e.Jdescription)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("JDESCRIPTION");

                entity.Property(e => e.Jname)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("JNAME");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__JOBS__DEPARTMENT__31B762FC");
            });

            modelBuilder.Entity<Major>(entity =>
            {
                entity.ToTable("MAJORS");

                entity.HasIndex(e => e.MajorId, "UQ__MAJORS__5A00AE27647541FD")
                    .IsUnique();

                entity.Property(e => e.MajorId).HasColumnName("MAJOR_ID");

                entity.Property(e => e.FacultyId).HasColumnName("FACULTY_ID");

                entity.Property(e => e.Mdescription)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("MDESCRIPTION");

                entity.Property(e => e.Mname)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("MNAME");

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.Majors)
                    .HasForeignKey(d => d.FacultyId)
                    .HasConstraintName("FK__MAJORS__FACULTY___245D67DE");
            });

            modelBuilder.Entity<Prerequisite>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.PrerequisiteId })
                    .HasName("PK__PREREQUI__4D0344F4E1623559");

                entity.ToTable("PREREQUISITES");

                entity.HasIndex(e => e.CourseId, "UQ__PREREQUI__71CB31DA688F4763")
                    .IsUnique();

                entity.Property(e => e.CourseId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("COURSE_ID");

                entity.Property(e => e.PrerequisiteId)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("PREREQUISITE_ID");
            });

            modelBuilder.Entity<Slot>(entity =>
            {
                entity.ToTable("SLOTS");

                entity.HasIndex(e => e.SlotId, "UQ__SLOTS__50DD09B744CF6064")
                    .IsUnique();

                entity.Property(e => e.SlotId).HasColumnName("SLOT_ID");

                entity.Property(e => e.EndTime).HasColumnName("END_TIME");

                entity.Property(e => e.StartTime).HasColumnName("START_TIME");

                entity.Property(e => e.WeekdayName)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("WEEKDAY_NAME");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("STUDENTS");

                entity.HasIndex(e => e.StudentId, "UQ__STUDENTS__E69FE77A19CF216F")
                    .IsUnique();

                entity.Property(e => e.StudentId).HasColumnName("STUDENT_ID");

                entity.Property(e => e.MajorId).HasColumnName("MAJOR_ID");

                entity.Property(e => e.Saddress)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("SADDRESS");

                entity.Property(e => e.Semail)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("SEMAIL");

                entity.Property(e => e.Sfname)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("SFNAME");

                entity.Property(e => e.Slname)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("SLNAME");

                entity.Property(e => e.Sphonenumber)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("SPHONENUMBER");

                entity.HasOne(d => d.Major)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.MajorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__STUDENTS__MAJOR___46B27FE2");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("TEACHERS");

                entity.HasIndex(e => e.TeacherId, "UQ__TEACHERS__ACD13DF9DDDAE2DD")
                    .IsUnique();

                entity.Property(e => e.TeacherId).HasColumnName("TEACHER_ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.FacultyId).HasColumnName("FACULTY_ID");

                entity.Property(e => e.Ishead).HasColumnName("ISHEAD");

                entity.Property(e => e.Taddress)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("TADDRESS");

                entity.Property(e => e.Tfname)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("TFNAME");

                entity.Property(e => e.Tlname)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("TLNAME");

                entity.Property(e => e.Tphonenumber)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("TPHONENUMBER");

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.Teachers)
                    .HasForeignKey(d => d.FacultyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TEACHERS__FACULT__3A4CA8FD");
            });

            modelBuilder.Entity<TeacherAvailability>(entity =>
            {
                entity.ToTable("TEACHER_AVAILABILITY");

                entity.HasIndex(e => e.TeacherAvailabilityId, "UQ__TEACHER___9A04C2B617AE01D8")
                    .IsUnique();

                entity.Property(e => e.TeacherAvailabilityId).HasColumnName("TEACHER_AVAILABILITY_ID");

                entity.Property(e => e.Isavailable)
                    .IsRequired()
                    .HasColumnName("ISAVAILABLE")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SlotId).HasColumnName("SLOT_ID");

                entity.Property(e => e.TeacherId).HasColumnName("TEACHER_ID");

                entity.HasOne(d => d.Slot)
                    .WithMany(p => p.TeacherAvailabilities)
                    .HasForeignKey(d => d.SlotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TEACHER_A__SLOT___42E1EEFE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
