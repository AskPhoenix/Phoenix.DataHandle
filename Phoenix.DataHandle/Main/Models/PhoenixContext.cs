using System;
using System.Collections.Generic;
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

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<BotFeedback> BotFeedbacks { get; set; } = null!;
        public virtual DbSet<Broadcast> Broadcasts { get; set; } = null!;
        public virtual DbSet<Channel> Channels { get; set; } = null!;
        public virtual DbSet<Classroom> Classrooms { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Exam> Exams { get; set; } = null!;
        public virtual DbSet<Exercise> Exercises { get; set; } = null!;
        public virtual DbSet<Grade> Grades { get; set; } = null!;
        public virtual DbSet<Lecture> Lectures { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<Schedule> Schedules { get; set; } = null!;
        public virtual DbSet<School> Schools { get; set; } = null!;
        public virtual DbSet<SchoolInfo> SchoolInfos { get; set; } = null!;
        public virtual DbSet<SchoolLogin> SchoolLogins { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=PhoenicopterusDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasPrecision(0);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);

                entity.Property(e => e.UpdatedAt).HasPrecision(0);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => new { e.PhoneNumber, e.PhoneNumberDependanceOrder }, "IX_AspNetUsers_Phone")
                    .IsUnique();

                entity.HasIndex(e => e.UserName, "UQ_AspNetUsers_UserName")
                    .IsUnique();

                entity.Property(e => e.CreatedApplicationType).HasDefaultValueSql("((-1))");

                entity.Property(e => e.CreatedAt)
                    .HasPrecision(0)
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.ObviatedAt).HasPrecision(0);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.PhoneNumberVerificationCode).HasMaxLength(50);

                entity.Property(e => e.PhoneNumberVerificationCodeCreatedAt).HasPrecision(0);

                entity.Property(e => e.UpdatedAt).HasPrecision(0);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Children)
                    .WithMany(p => p.Parents)
                    .UsingEntity<Dictionary<string, object>>(
                        "Parenthood",
                        l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("ChildId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Parenthood_AspNetUsers_Child"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("ParentId").HasConstraintName("FK_Parenthood_AspNetUsers_Parent"),
                        j =>
                        {
                            j.HasKey("ParentId", "ChildId").HasName("PK_Parenthood");

                            j.ToTable("Parenthoods");

                            j.HasIndex(new[] { "ChildId" }, "IX_Parenthood_ChildId");

                            j.HasIndex(new[] { "ParentId" }, "IX_Parenthood_ParentId");
                        });

                entity.HasMany(d => d.Courses)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "CourseUser",
                        l => l.HasOne<Course>().WithMany().HasForeignKey("CourseId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_CourseUsers_Courses"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_CourseUsers_AspNetUsers"),
                        j =>
                        {
                            j.HasKey("UserId", "CourseId");

                            j.ToTable("CourseUsers");

                            j.HasIndex(new[] { "CourseId" }, "IX_CourseUsers_CourseId");

                            j.HasIndex(new[] { "UserId" }, "IX_CourseUsers_UserId");
                        });

                entity.HasMany(d => d.Lectures)
                    .WithMany(p => p.Attendees)
                    .UsingEntity<Dictionary<string, object>>(
                        "Attendance",
                        l => l.HasOne<Lecture>().WithMany().HasForeignKey("LectureId").HasConstraintName("FK_Attendance_Lecture"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("AttendeeId").HasConstraintName("FK_Attendance_AspNetUsers"),
                        j =>
                        {
                            j.HasKey("AttendeeId", "LectureId").HasName("PK_Attendance");

                            j.ToTable("Attendances");

                            j.HasIndex(new[] { "LectureId" }, "IX_Attendance_LectureId");

                            j.HasIndex(new[] { "AttendeeId" }, "IX_Attendance_StudentId");
                        });

                entity.HasMany(d => d.Parents)
                    .WithMany(p => p.Children)
                    .UsingEntity<Dictionary<string, object>>(
                        "Parenthood",
                        l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("ParentId").HasConstraintName("FK_Parenthood_AspNetUsers_Parent"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("ChildId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Parenthood_AspNetUsers_Child"),
                        j =>
                        {
                            j.HasKey("ParentId", "ChildId").HasName("PK_Parenthood");

                            j.ToTable("Parenthoods");

                            j.HasIndex(new[] { "ChildId" }, "IX_Parenthood_ChildId");

                            j.HasIndex(new[] { "ParentId" }, "IX_Parenthood_ParentId");
                        });

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId").HasConstraintName("FK_AspNetUserRoles_AspNetRoles"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId").HasConstraintName("FK_AspNetUserRoles_AspNetUsers"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "UserId" }, "IX_AspNetUserRoles");
                        });

                entity.HasMany(d => d.Schools)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "SchoolUser",
                        l => l.HasOne<School>().WithMany().HasForeignKey("SchoolId").HasConstraintName("FK_UserSchool_School"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId").HasConstraintName("FK_UserSchool_AspNetUsers"),
                        j =>
                        {
                            j.HasKey("UserId", "SchoolId").HasName("PK_UserSchool");

                            j.ToTable("SchoolUsers");

                            j.HasIndex(new[] { "SchoolId" }, "IX_UserSchool_SchoolId");
                        });
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ChannelId });

                entity.HasIndex(e => new { e.ChannelId, e.ProviderKey }, "IX_AspNetUserLogins")
                    .IsUnique();

                entity.Property(e => e.CreatedAt).HasPrecision(0);

                entity.Property(e => e.ProviderKey).HasMaxLength(256);

                entity.Property(e => e.UpdatedAt).HasPrecision(0);

                entity.HasOne(d => d.Channel)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.ChannelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AspNetUserLogins_Channel");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_AspNetUserLogins_AspNetUsers");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "IX_Book")
                    .IsUnique();

                entity.Property(e => e.CreatedAt)
                    .HasPrecision(0)
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.NormalizedName).HasMaxLength(255);

                entity.Property(e => e.Publisher).HasMaxLength(255);

                entity.Property(e => e.UpdatedAt).HasPrecision(0);
            });

            modelBuilder.Entity<BotFeedback>(entity =>
            {
                entity.ToTable("BotFeedback");

                entity.Property(e => e.CreatedAt).HasPrecision(0);

                entity.Property(e => e.UpdatedAt).HasPrecision(0);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.BotFeedbacks)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BotFeedback_AspNetUsers");
            });

            modelBuilder.Entity<Broadcast>(entity =>
            {
                entity.HasIndex(e => new { e.AuthorId, e.ScheduledDate }, "IX_Broadcast_AuthorId_Date");

                entity.HasIndex(e => new { e.ScheduledDate, e.Daypart }, "IX_Broadcast_Date_Daypart");

                entity.HasIndex(e => new { e.SchoolId, e.ScheduledDate }, "IX_Broadcast_SchoolId_Date");

                entity.Property(e => e.CreatedAt).HasPrecision(0);

                entity.Property(e => e.ScheduledDate).HasPrecision(0);

                entity.Property(e => e.SentAt).HasPrecision(0);

                entity.Property(e => e.UpdatedAt).HasPrecision(0);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Broadcasts)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Broadcast_AspNetUsers");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Broadcasts)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Broadcast_School");

                entity.HasMany(d => d.Courses)
                    .WithMany(p => p.Broadcasts)
                    .UsingEntity<Dictionary<string, object>>(
                        "BroadcastCourse",
                        l => l.HasOne<Course>().WithMany().HasForeignKey("CourseId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_BroadcastCourse_Course"),
                        r => r.HasOne<Broadcast>().WithMany().HasForeignKey("BroadcastId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_BroadcastCourse_Broadcast"),
                        j =>
                        {
                            j.HasKey("BroadcastId", "CourseId").HasName("PK_BroadcastCourse");

                            j.ToTable("BroadcastCourses");

                            j.HasIndex(new[] { "BroadcastId" }, "IX_BroadcastCourse");
                        });
            });

            modelBuilder.Entity<Channel>(entity =>
            {
                entity.HasIndex(e => e.Code, "UQ_Channel_Code")
                    .IsUnique();

                entity.HasIndex(e => e.Provider, "UQ_Channel_Provider")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasPrecision(0);

                entity.Property(e => e.Provider).HasMaxLength(50);

                entity.Property(e => e.UpdatedAt).HasPrecision(0);
            });

            modelBuilder.Entity<Classroom>(entity =>
            {
                entity.HasIndex(e => new { e.SchoolId, e.NormalizedName }, "IX_Classroom")
                    .IsUnique();

                entity.Property(e => e.CreatedAt)
                    .HasPrecision(0)
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.NormalizedName).HasMaxLength(255);

                entity.Property(e => e.ObviatedAt).HasPrecision(0);

                entity.Property(e => e.UpdatedAt).HasPrecision(0);

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Classrooms)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK_Classroom_School");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasIndex(e => new { e.SchoolId, e.Code }, "IX_Course")
                    .IsUnique();

                entity.Property(e => e.CreatedAt)
                    .HasPrecision(0)
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FirstDate).HasPrecision(0);

                entity.Property(e => e.Group).HasMaxLength(50);

                entity.Property(e => e.LastDate).HasPrecision(0);

                entity.Property(e => e.Level).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.ObviatedAt).HasPrecision(0);

                entity.Property(e => e.SubCourse).HasMaxLength(150);

                entity.Property(e => e.UpdatedAt).HasPrecision(0);

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.SchoolId)
                    .HasConstraintName("FK_Course_School");

                entity.HasMany(d => d.Books)
                    .WithMany(p => p.Courses)
                    .UsingEntity<Dictionary<string, object>>(
                        "CourseBook",
                        l => l.HasOne<Book>().WithMany().HasForeignKey("BookId").HasConstraintName("FK_CourseBook_Book"),
                        r => r.HasOne<Course>().WithMany().HasForeignKey("CourseId").HasConstraintName("FK_CourseBook_Course"),
                        j =>
                        {
                            j.HasKey("CourseId", "BookId").HasName("PK_CourseBook");

                            j.ToTable("CourseBooks");

                            j.HasIndex(new[] { "CourseId" }, "IX_CourseBook_CourseId");
                        });
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.HasIndex(e => e.LectureId, "IX_Exam");

                entity.Property(e => e.CreatedAt).HasPrecision(0);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.UpdatedAt).HasPrecision(0);

                entity.HasOne(d => d.Lecture)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.LectureId)
                    .HasConstraintName("FK_Exam_Lecture");
            });

            modelBuilder.Entity<Exercise>(entity =>
            {
                entity.HasIndex(e => e.LectureId, "IX_Exercise_LectureId");

                entity.Property(e => e.CreatedAt)
                    .HasPrecision(0)
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.Page).HasMaxLength(256);

                entity.Property(e => e.UpdatedAt).HasPrecision(0);

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Exercises)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Exercise_Book");

                entity.HasOne(d => d.Lecture)
                    .WithMany(p => p.Exercises)
                    .HasForeignKey(d => d.LectureId)
                    .HasConstraintName("FK_Exercise_Lecture");
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.HasIndex(e => e.CourseId, "IX_Grades_Course");

                entity.HasIndex(e => e.ExamId, "IX_Grades_Exam");

                entity.HasIndex(e => e.ExerciseId, "IX_Grades_Exercise");

                entity.HasIndex(e => e.StudentId, "IX_Grades_Student");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasPrecision(0);

                entity.Property(e => e.Score).HasColumnType("decimal(9, 3)");

                entity.Property(e => e.Topic).HasMaxLength(256);

                entity.Property(e => e.UpdatedAt).HasPrecision(0);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Grades_Courses");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.ExamId)
                    .HasConstraintName("FK_Grades_Exams");

                entity.HasOne(d => d.Exercise)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.ExerciseId)
                    .HasConstraintName("FK_Grades_Exercises");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Grades_AspNetUsers");
            });

            modelBuilder.Entity<Lecture>(entity =>
            {
                entity.HasIndex(e => e.CourseId, "IX_Lecture_CourseId");

                entity.HasIndex(e => e.ScheduleId, "IX_Lecture_ScheduleId");

                entity.Property(e => e.CreatedAt)
                    .HasPrecision(0)
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EndDateTime).HasPrecision(0);

                entity.Property(e => e.StartDateTime).HasPrecision(0);

                entity.Property(e => e.UpdatedAt).HasPrecision(0);

                entity.HasOne(d => d.Classroom)
                    .WithMany(p => p.Lectures)
                    .HasForeignKey(d => d.ClassroomId)
                    .HasConstraintName("FK_Lecture_Classroom");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Lectures)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_Lecture_Course");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.Lectures)
                    .HasForeignKey(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Lecture_Schedule");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasIndex(e => e.ExamId, "IX_Material_ExamId");

                entity.Property(e => e.Chapter).HasMaxLength(50);

                entity.Property(e => e.CreatedAt).HasPrecision(0);

                entity.Property(e => e.Section).HasMaxLength(50);

                entity.Property(e => e.UpdatedAt).HasPrecision(0);

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Material_Book");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.ExamId)
                    .HasConstraintName("FK_Material_Exam");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.HasIndex(e => new { e.CourseId, e.DayOfWeek, e.StartTime }, "IX_Schedule")
                    .IsUnique();

                entity.Property(e => e.CreatedAt).HasPrecision(0);

                entity.Property(e => e.EndTime).HasPrecision(0);

                entity.Property(e => e.ObviatedAt).HasPrecision(0);

                entity.Property(e => e.StartTime).HasPrecision(0);

                entity.Property(e => e.UpdatedAt).HasPrecision(0);

                entity.HasOne(d => d.Classroom)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.ClassroomId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Schedule_Classroom");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_Course");
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.HasIndex(e => new { e.NormalizedName, e.NormalizedCity }, "IX_NameCity")
                    .IsUnique();

                entity.Property(e => e.AddressLine).HasMaxLength(255);

                entity.Property(e => e.City).HasMaxLength(200);

                entity.Property(e => e.CreatedAt)
                    .HasPrecision(0)
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.NormalizedCity).HasMaxLength(200);

                entity.Property(e => e.NormalizedName).HasMaxLength(200);

                entity.Property(e => e.ObviatedAt).HasPrecision(0);

                entity.Property(e => e.Slug).HasMaxLength(64);

                entity.Property(e => e.UpdatedAt).HasPrecision(0);
            });

            modelBuilder.Entity<SchoolInfo>(entity =>
            {
                entity.HasKey(e => e.SchoolId)
                    .HasName("PK_SchoolSettings");

                entity.ToTable("SchoolInfo");

                entity.Property(e => e.SchoolId).ValueGeneratedNever();

                entity.Property(e => e.Country)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.PhoneCode)
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.PrimaryLanguage).HasMaxLength(50);

                entity.Property(e => e.PrimaryLocale)
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.SecondaryLanguage).HasMaxLength(50);

                entity.Property(e => e.SecondaryLocale)
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.TimeZone).HasMaxLength(50);

                entity.HasOne(d => d.School)
                    .WithOne(p => p.SchoolInfo)
                    .HasForeignKey<SchoolInfo>(d => d.SchoolId)
                    .HasConstraintName("FK_SchoolSettings_School");
            });

            modelBuilder.Entity<SchoolLogin>(entity =>
            {
                entity.HasKey(e => new { e.SchoolId, e.ChannelId })
                    .HasName("PK_SchoolChannel");

                entity.HasIndex(e => new { e.ChannelId, e.ProviderKey }, "IX_SchoolChannel")
                    .IsUnique();

                entity.Property(e => e.CreatedAt).HasPrecision(0);

                entity.Property(e => e.ProviderKey).HasMaxLength(256);

                entity.Property(e => e.UpdatedAt).HasPrecision(0);

                entity.HasOne(d => d.Channel)
                    .WithMany(p => p.SchoolLogins)
                    .HasForeignKey(d => d.ChannelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SchoolLogins_Channel");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.SchoolLogins)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SchoolLogins_School");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.AspNetUserId)
                    .HasName("PK_User");

                entity.Property(e => e.AspNetUserId).ValueGeneratedNever();

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.IdentifierCode).HasMaxLength(10);

                entity.Property(e => e.IdentifierCodeCreatedAt).HasPrecision(0);

                entity.Property(e => e.LastName).HasMaxLength(255);

                entity.HasOne(d => d.AspNetUser)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.AspNetUserId)
                    .HasConstraintName("FK_User_AspNetUsers");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
