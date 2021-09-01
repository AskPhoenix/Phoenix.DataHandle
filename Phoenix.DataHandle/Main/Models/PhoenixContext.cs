using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Phoenix.DataHandle.Main.Models
{
    public partial class PhoenixContext : DbContext
    {
        public PhoenixContext()
        {
        }

        public PhoenixContext(DbContextOptions<PhoenixContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Attendance> Attendance { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<BotFeedback> BotFeedback { get; set; }
        public virtual DbSet<Broadcast> Broadcast { get; set; }
        public virtual DbSet<Classroom> Classroom { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<CourseBook> CourseBook { get; set; }
        public virtual DbSet<Exam> Exam { get; set; }
        public virtual DbSet<Exercise> Exercise { get; set; }
        public virtual DbSet<Lecture> Lecture { get; set; }
        public virtual DbSet<Material> Material { get; set; }
        public virtual DbSet<Parenthood> Parenthood { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<School> School { get; set; }
        public virtual DbSet<SchoolSettings> SchoolSettings { get; set; }
        public virtual DbSet<StudentCourse> StudentCourse { get; set; }
        public virtual DbSet<StudentExam> StudentExam { get; set; }
        public virtual DbSet<StudentExercise> StudentExercise { get; set; }
        public virtual DbSet<TeacherCourse> TeacherCourse { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserSchool> UserSchool { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                throw new Exception("Connection string not specified for PhoenixContext.");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.CreatedAt).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetimeoffset(0)");
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.CreatedAt).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetimeoffset(0)");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_AspNetUserLogins_AspNetUsers");
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_AspNetUserRoles_AspNetRoles");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_AspNetUserRoles_AspNetUsers");
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.HasIndex(e => e.PhoneNumber)
                    .HasName("PhoneNumberIndex");

                entity.Property(e => e.CreatedApplicationType).HasDefaultValueSql("((-1))");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetimeoffset(0)")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumberVerificationCode).HasMaxLength(50);

                entity.Property(e => e.PhoneNumberVerificationCode_at).HasColumnType("datetime2(0)");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.LectureId });

                entity.HasOne(d => d.Lecture)
                    .WithMany(p => p.Attendance)
                    .HasForeignKey(d => d.LectureId)
                    .HasConstraintName("FK_Attendance_Lecture");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Attendance)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_Attendance_AspNetUsers");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("IX_Book")
                    .IsUnique();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetimeoffset(0)")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.NormalizedName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Publisher).HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetimeoffset(0)");
            });

            modelBuilder.Entity<BotFeedback>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetimeoffset(0)");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.BotFeedback)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BotFeedback_AspNetUsers");
            });

            modelBuilder.Entity<Broadcast>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.Message).IsRequired();

                entity.Property(e => e.ScheduledDate).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetimeoffset(0)");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Broadcast)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Broadcast_Course");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Broadcast)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Broadcast_School");
            });

            modelBuilder.Entity<Classroom>(entity =>
            {
                entity.HasIndex(e => new { e.SchoolId, e.NormalizedName })
                    .HasName("IX_Classroom")
                    .IsUnique();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetimeoffset(0)")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.NormalizedName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetimeoffset(0)");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Classroom)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK_Classroom_School");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasIndex(e => new { e.SchoolId, e.Code })
                    .HasName("IX_Course")
                    .IsUnique();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetimeoffset(0)")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FirstDate).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.Group)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastDate).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.Level)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.SubCourse).HasMaxLength(150);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetimeoffset(0)");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Course)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK_Course_School");
            });

            modelBuilder.Entity<CourseBook>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.BookId });

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.CourseBook)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_CourseBook_Book");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseBook)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_CourseBook_Course");
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.HasIndex(e => e.LectureId)
                    .HasName("ExamLectureIdIndex")
                    .IsUnique();

                entity.Property(e => e.CreatedAt).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetimeoffset(0)");

                entity.HasOne(d => d.Lecture)
                    .WithOne(p => p.Exam)
                    .HasForeignKey<Exam>(d => d.LectureId)
                    .HasConstraintName("FK_Exam_Lecture");
            });

            modelBuilder.Entity<Exercise>(entity =>
            {
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetimeoffset(0)")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Page).HasMaxLength(256);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetimeoffset(0)");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Exercise)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Exercise_Book");

                entity.HasOne(d => d.Lecture)
                    .WithMany(p => p.Exercise)
                    .HasForeignKey(d => d.LectureId)
                    .HasConstraintName("FK_Exercise_Lecture");
            });

            modelBuilder.Entity<Lecture>(entity =>
            {
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetimeoffset(0)")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EndDateTime).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.StartDateTime).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetimeoffset(0)");

                entity.HasOne(d => d.Classroom)
                    .WithMany(p => p.Lecture)
                    .HasForeignKey(d => d.ClassroomId)
                    .HasConstraintName("FK_Lecture_Classroom");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Lecture)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Lecture_Course");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.Lecture)
                    .HasForeignKey(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Lecture_Schedule");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.Property(e => e.Chapter).HasMaxLength(50);

                entity.Property(e => e.CreatedAt).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.Section).HasMaxLength(50);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetimeoffset(0)");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Material)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Material_Book");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.Material)
                    .HasForeignKey(d => d.ExamId)
                    .HasConstraintName("FK_Material_Exam");
            });

            modelBuilder.Entity<Parenthood>(entity =>
            {
                entity.HasKey(e => new { e.ParentId, e.ChildId });

                entity.HasOne(d => d.Child)
                    .WithMany(p => p.ParenthoodChild)
                    .HasForeignKey(d => d.ChildId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Parenthood_AspNetUsers_Child");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.ParenthoodParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Parenthood_AspNetUsers_Parent");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.HasIndex(e => new { e.CourseId, e.DayOfWeek, e.StartTime })
                    .HasName("UQ_Schedule")
                    .IsUnique();

                entity.Property(e => e.CreatedAt).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.EndTime).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.StartTime).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetimeoffset(0)");

                entity.HasOne(d => d.Classroom)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.ClassroomId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Schedule_Classroom");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_Course");
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.HasIndex(e => e.Slug)
                    .HasName("IX_Slug");

                entity.HasIndex(e => new { e.NormalizedName, e.NormalizedCity })
                    .HasName("IX_NameCity")
                    .IsUnique();

                entity.Property(e => e.AddressLine)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetimeoffset(0)")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FacebookPageId).HasMaxLength(20);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.NormalizedCity)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.NormalizedName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetimeoffset(0)");
            });

            modelBuilder.Entity<SchoolSettings>(entity =>
            {
                entity.HasKey(e => e.SchoolId);

                entity.Property(e => e.SchoolId).ValueGeneratedNever();

                entity.Property(e => e.Language)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Locale2)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.Property(e => e.TimeZone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.School)
                    .WithOne(p => p.SchoolSettings)
                    .HasForeignKey<SchoolSettings>(d => d.SchoolId)
                    .HasConstraintName("FK_SchoolSettings_School");
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.StudentId });

                entity.Property(e => e.Grade).HasColumnType("decimal(6, 3)");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StudentCourse)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_StudentCourse_Course");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentCourse)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_StudentCourse_AspNetUsers");
            });

            modelBuilder.Entity<StudentExam>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.ExamId });

                entity.Property(e => e.Grade).HasColumnType("decimal(6, 3)");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.StudentExam)
                    .HasForeignKey(d => d.ExamId)
                    .HasConstraintName("FK_StudentExam_Exam");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentExam)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_StudentExam_AspNetUsers");
            });

            modelBuilder.Entity<StudentExercise>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.ExerciseId });

                entity.Property(e => e.Grade).HasColumnType("decimal(6, 3)");

                entity.HasOne(d => d.Exercise)
                    .WithMany(p => p.StudentExercise)
                    .HasForeignKey(d => d.ExerciseId)
                    .HasConstraintName("FK_StudentExercise_Exercise");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentExercise)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_StudentExercise_AspNetUsers");
            });

            modelBuilder.Entity<TeacherCourse>(entity =>
            {
                entity.HasKey(e => new { e.TeacherId, e.CourseId });

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TeacherCourse)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_TeacherCourse_Course");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.TeacherCourse)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK_TeacherCourse_AspNetUsers");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.AspNetUserId);

                entity.Property(e => e.AspNetUserId).ValueGeneratedNever();

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.IdentifierCode).HasMaxLength(10);

                entity.Property(e => e.IdentifierCodeCreatedAt).HasColumnType("datetimeoffset(0)");

                entity.Property(e => e.LastName).HasMaxLength(255);

                entity.HasOne(d => d.AspNetUser)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.AspNetUserId)
                    .HasConstraintName("FK_User_AspNetUsers");
            });

            modelBuilder.Entity<UserSchool>(entity =>
            {
                entity.HasKey(e => new { e.AspNetUserId, e.SchoolId });

                entity.Property(e => e.EnrolledOn).HasColumnType("datetimeoffset(0)");

                entity.HasOne(d => d.AspNetUser)
                    .WithMany(p => p.UserSchool)
                    .HasForeignKey(d => d.AspNetUserId)
                    .HasConstraintName("FK_UserSchool_AspNetUsers");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.UserSchool)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK_UserSchool_School");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
