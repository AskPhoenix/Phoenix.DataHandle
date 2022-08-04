using Microsoft.EntityFrameworkCore;

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

        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<BotFeedback> BotFeedbacks { get; set; } = null!;
        public virtual DbSet<Broadcast> Broadcasts { get; set; } = null!;
        public virtual DbSet<Classroom> Classrooms { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Exam> Exams { get; set; } = null!;
        public virtual DbSet<Exercise> Exercises { get; set; } = null!;
        public virtual DbSet<Grade> Grades { get; set; } = null!;
        public virtual DbSet<Lecture> Lectures { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<OneTimeCode> OneTimeCodes { get; set; } = null!;
        public virtual DbSet<Schedule> Schedules { get; set; } = null!;
        public virtual DbSet<School> Schools { get; set; } = null!;
        public virtual DbSet<SchoolConnection> SchoolConnections { get; set; } = null!;
        public virtual DbSet<SchoolSetting> SchoolSettings { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserConnection> UserConnections { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=PhoenicopterusDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "IX_Books_NormalizedName");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);

                entity.Property(e => e.Publisher).HasMaxLength(256);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<BotFeedback>(entity =>
            {
                entity.ToTable("BotFeedback");

                entity.HasIndex(e => e.AuthorId, "IX_BotFeedback_Author");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.BotFeedbacks)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_BotFeedback_Users");
            });

            modelBuilder.Entity<Broadcast>(entity =>
            {
                entity.HasIndex(e => new { e.AuthorId, e.ScheduledFor }, "IX_Broadcasts_Author_ScheduledFor");

                entity.HasIndex(e => new { e.ScheduledFor, e.Daypart }, "IX_Broadcasts_ScheduledFor_Daypart");

                entity.HasIndex(e => new { e.SchoolId, e.ScheduledFor }, "IX_Broadcasts_School_ScheduledFor");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.ScheduledFor).HasColumnType("datetime");

                entity.Property(e => e.SentAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Broadcasts)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Broadcasts_Users");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Broadcasts)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Broadcasts_Schools");

                entity.HasMany(d => d.Courses)
                    .WithMany(p => p.Broadcasts)
                    .UsingEntity<Dictionary<string, object>>(
                        "BroadcastCourse",
                        l => l.HasOne<Course>().WithMany().HasForeignKey("CourseId").OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_BroadcastCourses_Courses"),
                        r => r.HasOne<Broadcast>().WithMany().HasForeignKey("BroadcastId").OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_BroadcastCourses_Broadcasts"),
                        j =>
                        {
                            j.HasKey("BroadcastId", "CourseId");

                            j.ToTable("BroadcastCourses");

                            j.HasIndex(new[] { "BroadcastId" }, "IX_BroadcastCourses_Broadcast");

                            j.HasIndex(new[] { "CourseId" }, "IX_BroadcastCourses_Course");
                        });
            });

            modelBuilder.Entity<Classroom>(entity =>
            {
                entity.HasIndex(e => new { e.SchoolId, e.NormalizedName }, "IX_Classrooms_School_NormalizedName")
                    .IsUnique();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);

                entity.Property(e => e.ObviatedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Classrooms)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Classrooms_Schools");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasIndex(e => new { e.SchoolId, e.Code }, "IX_Courses_School_Code")
                    .IsUnique();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.FirstDate).HasColumnType("datetime");

                entity.Property(e => e.Group).HasMaxLength(64);

                entity.Property(e => e.LastDate).HasColumnType("datetime");

                entity.Property(e => e.Level).HasMaxLength(64);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.Property(e => e.ObviatedAt).HasColumnType("datetime");

                entity.Property(e => e.SubCourse).HasMaxLength(128);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Courses_Schools");

                entity.HasMany(d => d.Books)
                    .WithMany(p => p.Courses)
                    .UsingEntity<Dictionary<string, object>>(
                        "CourseBook",
                        l => l.HasOne<Book>().WithMany().HasForeignKey("BookId").OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_CourseBooks_Books"),
                        r => r.HasOne<Course>().WithMany().HasForeignKey("CourseId").OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_CourseBooks_Courses"),
                        j =>
                        {
                            j.HasKey("CourseId", "BookId");

                            j.ToTable("CourseBooks");

                            j.HasIndex(new[] { "BookId" }, "IX_CourseBooks_Book");

                            j.HasIndex(new[] { "CourseId" }, "IX_CourseBooks_Course");
                        });
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.HasIndex(e => e.LectureId, "IX_Exams_Lecture");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Lecture)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.LectureId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Exams_Lectures");
            });

            modelBuilder.Entity<Exercise>(entity =>
            {
                entity.HasIndex(e => e.BookId, "IX_Exercises_Book");

                entity.HasIndex(e => e.LectureId, "IX_Exercises_Lecture");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.Page).HasMaxLength(32);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Exercises)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Exercises_Books");

                entity.HasOne(d => d.Lecture)
                    .WithMany(p => p.Exercises)
                    .HasForeignKey(d => d.LectureId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Exercises_Lectures");
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.HasIndex(e => e.CourseId, "IX_Grades_Course");

                entity.HasIndex(e => e.ExamId, "IX_Grades_Exam");

                entity.HasIndex(e => e.ExerciseId, "IX_Grades_Exercise");

                entity.HasIndex(e => e.StudentId, "IX_Grades_Student");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Score).HasColumnType("decimal(9, 3)");

                entity.Property(e => e.Topic).HasMaxLength(256);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Grades_Courses");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Grades_Exams");

                entity.HasOne(d => d.Exercise)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.ExerciseId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Grades_Exercises");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Grades_Users");
            });

            modelBuilder.Entity<Lecture>(entity =>
            {
                entity.HasIndex(e => e.ClassroomId, "IX_Lectures_Classroom");

                entity.HasIndex(e => new { e.CourseId, e.ScheduleId }, "IX_Lectures_Course_Schedule");

                entity.HasIndex(e => e.ScheduleId, "IX_Lectures_Schedule");

                entity.HasIndex(e => e.Id, "UQ_Lectures_Course_StartDateTime")
                    .IsUnique();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.EndDateTime).HasPrecision(0);

                entity.Property(e => e.ObviatedAt).HasColumnType("datetime");

                entity.Property(e => e.StartDateTime).HasPrecision(0);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Classroom)
                    .WithMany(p => p.Lectures)
                    .HasForeignKey(d => d.ClassroomId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Lectures_Classrooms");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Lectures)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Lectures_Courses");

                entity.HasOne(d => d.ReplacementLecture)
                    .WithMany(p => p.InverseReplacementLecture)
                    .HasForeignKey(d => d.ReplacementLectureId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Lectures_Lectures");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.Lectures)
                    .HasForeignKey(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Lectures_Schedules");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasIndex(e => e.BookId, "IX_Materials_Book");

                entity.HasIndex(e => e.ExamId, "IX_Materials_Exam");

                entity.Property(e => e.Chapter).HasMaxLength(64);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Section).HasMaxLength(64);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Materials_Books");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.Materials)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Materials_Exams");
            });

            modelBuilder.Entity<OneTimeCode>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_OneTimeCodes_User");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.ExpiresAt).HasColumnType("datetime");

                entity.Property(e => e.Token).HasMaxLength(16);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OneTimeCodes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_OneTimeCodes_Users");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.HasIndex(e => e.ClassroomId, "IX_Schedules_Classroom");

                entity.HasIndex(e => e.CourseId, "IX_Schedules_Course");

                entity.HasIndex(e => new { e.CourseId, e.DayOfWeek, e.StartTime }, "UQ_Schedules_Course_DayOfWeek_StartTime")
                    .IsUnique();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.ObviatedAt).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Classroom)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.ClassroomId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Schedules_Classrooms");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_Schedules_Courses");
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.HasIndex(e => e.Code, "IX_Schools_Code")
                    .IsUnique();

                entity.Property(e => e.AddressLine).HasMaxLength(256);

                entity.Property(e => e.City).HasMaxLength(256);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.ObviatedAt).HasColumnType("datetime");

                entity.Property(e => e.Slug).HasMaxLength(64);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<SchoolConnection>(entity =>
            {
                entity.HasIndex(e => new { e.Channel, e.ChannelKey }, "IX_SchoolConnections")
                    .IsUnique();

                entity.Property(e => e.ActivatedAt).HasColumnType("datetime");

                entity.Property(e => e.Channel).HasMaxLength(128);

                entity.Property(e => e.ChannelKey).HasMaxLength(128);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.SchoolConnections)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_SchoolConnections_Schools");
            });

            modelBuilder.Entity<SchoolSetting>(entity =>
            {
                entity.HasKey(e => e.SchoolId)
                    .HasName("PK_SchoolSetting");

                entity.Property(e => e.SchoolId).ValueGeneratedNever();

                entity.Property(e => e.Country).HasMaxLength(64);

                entity.Property(e => e.PhoneCountryCode).HasMaxLength(4);

                entity.Property(e => e.PrimaryLanguage).HasMaxLength(64);

                entity.Property(e => e.PrimaryLocale).HasMaxLength(5);

                entity.Property(e => e.SecondaryLanguage).HasMaxLength(64);

                entity.Property(e => e.SecondaryLocale).HasMaxLength(5);

                entity.Property(e => e.TimeZone).HasMaxLength(64);

                entity.HasOne(d => d.School)
                    .WithOne(p => p.SchoolSetting)
                    .HasForeignKey<SchoolSetting>(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_SchoolSettings_Schools");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.AspNetUserId)
                    .HasName("PK_User");

                entity.Property(e => e.AspNetUserId).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(256);

                entity.Property(e => e.LastName)
                    .HasMaxLength(256);

                entity.Property(e => e.ObviatedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasMany(d => d.Children)
                    .WithMany(p => p.Parents)
                    .UsingEntity<Dictionary<string, object>>(
                        "Parenthood",
                        l => l.HasOne<User>().WithMany().HasForeignKey("ChildId").OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Parenthoods_Users_Child"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("ParentId").OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Parenthoods_Users_Parent"),
                        j =>
                        {
                            j.HasKey("ParentId", "ChildId");

                            j.ToTable("Parenthoods");

                            j.HasIndex(new[] { "ChildId" }, "IX_Parenthoods_Child");

                            j.HasIndex(new[] { "ParentId" }, "IX_Parenthoods_Parent");
                        });

                entity.HasMany(d => d.Courses)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "CourseUser",
                        l => l.HasOne<Course>().WithMany().HasForeignKey("CourseId").OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_CourseUsers_Courses"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_CourseUsers_Users"),
                        j =>
                        {
                            j.HasKey("UserId", "CourseId");

                            j.ToTable("CourseUsers");

                            j.HasIndex(new[] { "CourseId" }, "IX_CourseUsers_Course");

                            j.HasIndex(new[] { "UserId" }, "IX_CourseUsers_User");
                        });

                entity.HasMany(d => d.Lectures)
                    .WithMany(p => p.Attendees)
                    .UsingEntity<Dictionary<string, object>>(
                        "Attendance",
                        l => l.HasOne<Lecture>().WithMany().HasForeignKey("LectureId").OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Attendances_Lectures"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("AttendeeId").OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Attendances_Users"),
                        j =>
                        {
                            j.HasKey("AttendeeId", "LectureId");

                            j.ToTable("Attendances");

                            j.HasIndex(new[] { "LectureId" }, "IX_Attendances_Lecture");
                        });

                entity.HasMany(d => d.Parents)
                    .WithMany(p => p.Children)
                    .UsingEntity<Dictionary<string, object>>(
                        "Parenthood",
                        l => l.HasOne<User>().WithMany().HasForeignKey("ParentId").OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Parenthoods_Users_Parent"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("ChildId").OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Parenthoods_Users_Child"),
                        j =>
                        {
                            j.HasKey("ParentId", "ChildId");

                            j.ToTable("Parenthoods");

                            j.HasIndex(new[] { "ChildId" }, "IX_Parenthoods_Child");

                            j.HasIndex(new[] { "ParentId" }, "IX_Parenthoods_Parent");
                        });

                entity.HasMany(d => d.Schools)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "SchoolUser",
                        l => l.HasOne<School>().WithMany().HasForeignKey("SchoolId").OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_SchoolUsers_Schools"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_SchoolUsers_Users"),
                        j =>
                        {
                            j.HasKey("UserId", "SchoolId");

                            j.ToTable("SchoolUsers");

                            j.HasIndex(new[] { "SchoolId" }, "IX_SchoolUsers_School");
                        });
            });

            modelBuilder.Entity<UserConnection>(entity =>
            {
                entity.HasIndex(e => new { e.Channel, e.ChannelKey }, "IX_UserConnections")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ActivatedAt).HasColumnType("datetime");

                entity.Property(e => e.Channel).HasMaxLength(128);

                entity.Property(e => e.ChannelKey).HasMaxLength(128);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.UserConnections)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_UserConnections_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
