using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Attendance> Attendance { get; set; }
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<BotFeedback> BotFeedback { get; set; }
        public virtual DbSet<Classroom> Classroom { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<CourseBook> CourseBook { get; set; }
        public virtual DbSet<Exam> Exam { get; set; }
        public virtual DbSet<Exercise> Exercise { get; set; }
        public virtual DbSet<Lecture> Lecture { get; set; }
        public virtual DbSet<Material> Material { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<School> School { get; set; }
        public virtual DbSet<StudentCourse> StudentCourse { get; set; }
        public virtual DbSet<StudentExam> StudentExam { get; set; }
        public virtual DbSet<StudentExercise> StudentExercise { get; set; }
        public virtual DbSet<TeacherCourse> TeacherCourse { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:askphoenix.database.windows.net,1433;Initial Catalog=PhoenixDB;Persist Security Info=False;User ID=phoenix;Password=20Ph0eniX20!");
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

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
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

                entity.Property(e => e.CreatedAt).HasColumnType("datetime2(0)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FacebookId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.OneTimeCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime2(0)");

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.LectureId });

                entity.HasOne(d => d.Lecture)
                    .WithMany(p => p.Attendance)
                    .HasForeignKey(d => d.LectureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Attendance_Lecture");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Attendance)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Attendance_User");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<BotFeedback>(entity =>
            {
                entity.Property(e => e.Category).HasMaxLength(50);

                entity.Property(e => e.Occasion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.BotFeedback)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BotFeedback_User");
            });

            modelBuilder.Entity<Classroom>(entity =>
            {
                entity.HasIndex(e => new { e.SchoolId, e.Name })
                    .HasName("UQ__Classroo__A5B5856930D0EBE6")
                    .IsUnique();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime2(0)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime2(0)");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Classroom)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Classroom_School");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasIndex(e => new { e.SchoolId, e.Level })
                    .HasName("UQ__Course__BE9836D8D4139A41")
                    .IsUnique();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime2(0)");

                entity.Property(e => e.FirstDate).HasColumnType("datetime2(0)");

                entity.Property(e => e.Group)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Level)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime2(0)");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Course)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Course_School");
            });

            modelBuilder.Entity<CourseBook>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.BookId });

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.CourseBook)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseBook_Book");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseBook)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseBook_Course");
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.HasIndex(e => e.LectureId)
                    .HasName("ExamLectureIdIndex")
                    .IsUnique();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime2(0)");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime2(0)");

                entity.HasOne(d => d.Lecture)
                    .WithOne(p => p.Exam)
                    .HasForeignKey<Exam>(d => d.LectureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exam_Lecture");
            });

            modelBuilder.Entity<Exercise>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime2(0)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime2(0)");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Exercise)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exercise_Book");

                entity.HasOne(d => d.Lecture)
                    .WithMany(p => p.Exercise)
                    .HasForeignKey(d => d.LectureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exercise_Lecture");
            });

            modelBuilder.Entity<Lecture>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime2(0)");

                entity.Property(e => e.EndDateTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.StartDateTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime2(0)");

                entity.HasOne(d => d.Classroom)
                    .WithMany(p => p.Lecture)
                    .HasForeignKey(d => d.ClassroomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lecture_Classroom");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Lecture)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lecture_Course");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.Property(e => e.Chapter).HasMaxLength(50);

                entity.Property(e => e.Section).HasMaxLength(50);

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Material)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_Material_Book");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.Material)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Material_Exam");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime2(0)");

                entity.Property(e => e.EndTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.StartTime).HasColumnType("datetime2(0)");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime2(0)");

                entity.HasOne(d => d.Classroom)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.ClassroomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
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
                    .HasName("SchoolSlugIndex")
                    .IsUnique();

                entity.Property(e => e.AddressLine)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime2(0)");

                entity.Property(e => e.Endpoint)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FacebookPageId)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime2(0)");
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.StudentId });

                entity.Property(e => e.Grade).HasColumnType("decimal(6, 3)");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StudentCourse)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentCourse_Course");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentCourse)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentCourse_User");
            });

            modelBuilder.Entity<StudentExam>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.ExamId });

                entity.Property(e => e.Grade).HasColumnType("decimal(6, 3)");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.StudentExam)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentExam_Exam");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentExam)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentExam_User");
            });

            modelBuilder.Entity<StudentExercise>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.ExerciseId });

                entity.Property(e => e.Grade).HasColumnType("decimal(6, 3)");

                entity.HasOne(d => d.Exercise)
                    .WithMany(p => p.StudentExercise)
                    .HasForeignKey(d => d.ExerciseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentExercise_Exercise");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentExercise)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentExercise_User");
            });

            modelBuilder.Entity<TeacherCourse>(entity =>
            {
                entity.HasKey(e => new { e.TeacherId, e.CourseId });

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TeacherCourse)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherCourse_Course");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.TeacherCourse)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherCourse_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.AspNetUserId);

                entity.Property(e => e.AspNetUserId).ValueGeneratedNever();

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.LastName).HasMaxLength(255);

                entity.HasOne(d => d.AspNetUser)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.AspNetUserId)
                    .HasConstraintName("FK_User_AspNetUsers");
            });

            this.OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
