using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Phoenix.DataHandle.Migrations
{
    public partial class V3_alpha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Broadcast_Course",
                table: "Broadcast");

            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "CourseBook");

            migrationBuilder.DropTable(
                name: "Parenthood");

            migrationBuilder.DropTable(
                name: "SchoolSettings");

            migrationBuilder.DropTable(
                name: "StudentCourse");

            migrationBuilder.DropTable(
                name: "StudentExam");

            migrationBuilder.DropTable(
                name: "StudentExercise");

            migrationBuilder.DropTable(
                name: "TeacherCourse");

            migrationBuilder.DropTable(
                name: "UserSchool");

            migrationBuilder.DropIndex(
                name: "EmailIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "PhoneNumberIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_School",
            //    table: "School");

            migrationBuilder.DropIndex(
                name: "IX_NameCity",
                table: "School");

            migrationBuilder.DropIndex(
                name: "IX_Slug",
                table: "School");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Schedule",
            //    table: "Schedule");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Material",
            //    table: "Material");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Lecture",
            //    table: "Lecture");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Exercise",
            //    table: "Exercise");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Exam",
            //    table: "Exam");

            migrationBuilder.DropIndex(
                name: "ExamLectureIdIndex",
                table: "Exam");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Course",
            //    table: "Course");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Classroom",
            //    table: "Classroom");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Broadcast",
            //    table: "Broadcast");

            migrationBuilder.DropIndex(
                name: "IX_Broadcast_CourseId",
                table: "Broadcast");

            migrationBuilder.DropIndex(
                name: "IX_Broadcast_CreatedByUserId",
                table: "Broadcast");

            migrationBuilder.DropIndex(
                name: "IX_Broadcast_SchoolId",
                table: "Broadcast");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Book",
            //    table: "Book");

            migrationBuilder.DropColumn(
                name: "PhoneNumberVerificationCode_at",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LoginProvider",
                table: "AspNetUserLogins");

            migrationBuilder.DropColumn(
                name: "ProviderDisplayName",
                table: "AspNetUserLogins");

            migrationBuilder.DropColumn(
                name: "FacebookPageId",
                table: "School");

            migrationBuilder.DropColumn(
                name: "NormalizedCity",
                table: "School");

            migrationBuilder.DropColumn(
                name: "NormalizedName",
                table: "School");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Broadcast");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "School",
                newName: "Schools");

            migrationBuilder.RenameTable(
                name: "Schedule",
                newName: "Schedules");

            migrationBuilder.RenameTable(
                name: "Material",
                newName: "Materials");

            migrationBuilder.RenameTable(
                name: "Lecture",
                newName: "Lectures");

            migrationBuilder.RenameTable(
                name: "Exercise",
                newName: "Exercises");

            migrationBuilder.RenameTable(
                name: "Exam",
                newName: "Exams");

            migrationBuilder.RenameTable(
                name: "Course",
                newName: "Courses");

            migrationBuilder.RenameTable(
                name: "Classroom",
                newName: "Classrooms");

            migrationBuilder.RenameTable(
                name: "Broadcast",
                newName: "Broadcasts");

            migrationBuilder.RenameTable(
                name: "Book",
                newName: "Books");

            migrationBuilder.RenameColumn(
                name: "Info",
                table: "Schools",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Info",
                table: "Schedules",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "UQ_Schedule",
                table: "Schedules",
                newName: "IX_Schedule");

            migrationBuilder.RenameIndex(
                name: "IX_Schedule_ClassroomId",
                table: "Schedules",
                newName: "IX_Schedules_ClassroomId");

            migrationBuilder.RenameIndex(
                name: "IX_Material_BookId",
                table: "Materials",
                newName: "IX_Materials_BookId");

            migrationBuilder.RenameColumn(
                name: "Info",
                table: "Lectures",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_Lecture_ClassroomId",
                table: "Lectures",
                newName: "IX_Lectures_ClassroomId");

            migrationBuilder.RenameIndex(
                name: "IX_Exercise_BookId",
                table: "Exercises",
                newName: "IX_Exercises_BookId");

            migrationBuilder.RenameColumn(
                name: "Info",
                table: "Courses",
                newName: "Comments");

            migrationBuilder.RenameColumn(
                name: "Info",
                table: "Classrooms",
                newName: "Comments");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Broadcasts",
                newName: "AuthorId");

            migrationBuilder.RenameColumn(
                name: "Info",
                table: "Books",
                newName: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "BotFeedback",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedUserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhoneNumberDependanceOrder",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "PhoneNumberVerificationCodeCreatedAt",
                table: "AspNetUsers",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<int>(
                name: "ChannelId",
                table: "AspNetUserLogins",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "Schools",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "AttendancesNoted",
                table: "Lectures",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "UserId", "ChannelId" });

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Schools",
            //    table: "Schools",
            //    column: "Id");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Schedules",
            //    table: "Schedules",
            //    column: "Id");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Materials",
            //    table: "Materials",
            //    column: "Id");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Lectures",
            //    table: "Lectures",
            //    column: "Id");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Exercises",
            //    table: "Exercises",
            //    column: "Id");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Exams",
            //    table: "Exams",
            //    column: "Id");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Courses",
            //    table: "Courses",
            //    column: "Id");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Classrooms",
            //    table: "Classrooms",
            //    column: "Id");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Broadcasts",
            //    table: "Broadcasts",
            //    column: "Id");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Books",
            //    table: "Books",
            //    column: "Id");

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    AttendeeId = table.Column<int>(type: "int", nullable: false),
                    LectureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => new { x.AttendeeId, x.LectureId });
                    table.ForeignKey(
                        name: "FK_Attendance_AspNetUsers",
                        column: x => x.AttendeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendance_Lecture",
                        column: x => x.LectureId,
                        principalTable: "Lectures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BroadcastCourses",
                columns: table => new
                {
                    BroadcastId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BroadcastCourse", x => new { x.BroadcastId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_BroadcastCourse_Broadcast",
                        column: x => x.BroadcastId,
                        principalTable: "Broadcasts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BroadcastCourse_Course",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset(0)", precision: 0, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset(0)", precision: 0, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseBooks",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseBook", x => new { x.CourseId, x.BookId });
                    table.ForeignKey(
                        name: "FK_CourseBook_Book",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseBook_Course",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseUsers", x => new { x.UserId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_CourseUsers_AspNetUsers",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CourseUsers_Courses",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    ExamId = table.Column<int>(type: "int", nullable: true),
                    ExerciseId = table.Column<int>(type: "int", nullable: true),
                    Score = table.Column<decimal>(type: "decimal(9,3)", nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Justification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset(0)", precision: 0, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset(0)", precision: 0, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grades_AspNetUsers",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Grades_Courses",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Grades_Exams",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Grades_Exercises",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Parenthoods",
                columns: table => new
                {
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    ChildId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parenthood", x => new { x.ParentId, x.ChildId });
                    table.ForeignKey(
                        name: "FK_Parenthood_AspNetUsers_Child",
                        column: x => x.ChildId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Parenthood_AspNetUsers_Parent",
                        column: x => x.ParentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SchoolInfo",
                columns: table => new
                {
                    SchoolId = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    PrimaryLanguage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PrimaryLocale = table.Column<string>(type: "nchar(5)", fixedLength: true, maxLength: 5, nullable: false),
                    SecondaryLanguage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SecondaryLocale = table.Column<string>(type: "nchar(5)", fixedLength: true, maxLength: 5, nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneCode = table.Column<string>(type: "nchar(5)", fixedLength: true, maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolSettings", x => x.SchoolId);
                    table.ForeignKey(
                        name: "FK_SchoolSettings_School",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchoolUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SchoolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSchool", x => new { x.UserId, x.SchoolId });
                    table.ForeignKey(
                        name: "FK_UserSchool_AspNetUsers",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSchool_School",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchoolLogins",
                columns: table => new
                {
                    SchoolId = table.Column<int>(type: "int", nullable: false),
                    ChannelId = table.Column<int>(type: "int", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset(0)", precision: 0, nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset(0)", precision: 0, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolChannel", x => new { x.SchoolId, x.ChannelId });
                    table.ForeignKey(
                        name: "FK_SchoolLogins_Channel",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SchoolLogins_School",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Phone",
                table: "AspNetUsers",
                columns: new[] { "PhoneNumber", "PhoneNumberDependanceOrder" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "ChannelId", "ProviderKey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_School",
                table: "Schools",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exam",
                table: "Exams",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_Broadcast_AuthorId_Date",
                table: "Broadcasts",
                columns: new[] { "AuthorId", "ScheduledDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Broadcast_Date_Daypart",
                table: "Broadcasts",
                columns: new[] { "ScheduledDate", "Daypart" });

            migrationBuilder.CreateIndex(
                name: "IX_Broadcast_SchoolId_Date",
                table: "Broadcasts",
                columns: new[] { "SchoolId", "ScheduledDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_LectureId",
                table: "Attendances",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_StudentId",
                table: "Attendances",
                column: "AttendeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BroadcastCourse",
                table: "BroadcastCourses",
                column: "BroadcastId");

            migrationBuilder.CreateIndex(
                name: "IX_BroadcastCourses_CourseId",
                table: "BroadcastCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "UQ_Channel_Code",
                table: "Channels",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Channel_Provider",
                table: "Channels",
                column: "Provider",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseBook_CourseId",
                table: "CourseBooks",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseBooks_BookId",
                table: "CourseBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseUsers_CourseId",
                table: "CourseUsers",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseUsers_UserId",
                table: "CourseUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_Course",
                table: "Grades",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_Exam",
                table: "Grades",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_Exercise",
                table: "Grades",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_Student",
                table: "Grades",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Parenthood_ChildId",
                table: "Parenthoods",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_Parenthood_ParentId",
                table: "Parenthoods",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolChannel",
                table: "SchoolLogins",
                columns: new[] { "ChannelId", "ProviderKey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSchool_SchoolId",
                table: "SchoolUsers",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_Channel",
                table: "AspNetUserLogins",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_Channel",
                table: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "BroadcastCourses");

            migrationBuilder.DropTable(
                name: "CourseBooks");

            migrationBuilder.DropTable(
                name: "CourseUsers");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "Parenthoods");

            migrationBuilder.DropTable(
                name: "SchoolInfo");

            migrationBuilder.DropTable(
                name: "SchoolLogins");

            migrationBuilder.DropTable(
                name: "SchoolUsers");

            migrationBuilder.DropTable(
                name: "Channels");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Phone",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "UQ_AspNetUsers_UserName",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserLogins",
                table: "AspNetUserLogins");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Schools",
            //    table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_School",
                table: "Schools");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Schedules",
            //    table: "Schedules");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Materials",
            //    table: "Materials");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Lectures",
            //    table: "Lectures");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Exercises",
            //    table: "Exercises");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Exams",
            //    table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exam",
                table: "Exams");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Courses",
            //    table: "Courses");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Classrooms",
            //    table: "Classrooms");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Broadcasts",
            //    table: "Broadcasts");

            migrationBuilder.DropIndex(
                name: "IX_Broadcast_AuthorId_Date",
                table: "Broadcasts");

            migrationBuilder.DropIndex(
                name: "IX_Broadcast_Date_Daypart",
                table: "Broadcasts");

            migrationBuilder.DropIndex(
                name: "IX_Broadcast_SchoolId_Date",
                table: "Broadcasts");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Books",
            //    table: "Books");

            migrationBuilder.DropColumn(
                name: "PhoneNumberDependanceOrder",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhoneNumberVerificationCodeCreatedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ChannelId",
                table: "AspNetUserLogins");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "AttendancesNoted",
                table: "Lectures");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Schools",
                newName: "School");

            migrationBuilder.RenameTable(
                name: "Schedules",
                newName: "Schedule");

            migrationBuilder.RenameTable(
                name: "Materials",
                newName: "Material");

            migrationBuilder.RenameTable(
                name: "Lectures",
                newName: "Lecture");

            migrationBuilder.RenameTable(
                name: "Exercises",
                newName: "Exercise");

            migrationBuilder.RenameTable(
                name: "Exams",
                newName: "Exam");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "Course");

            migrationBuilder.RenameTable(
                name: "Classrooms",
                newName: "Classroom");

            migrationBuilder.RenameTable(
                name: "Broadcasts",
                newName: "Broadcast");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "Book");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "School",
                newName: "Info");

            migrationBuilder.RenameColumn(
                name: "Comments",
                table: "Schedule",
                newName: "Info");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_ClassroomId",
                table: "Schedule",
                newName: "IX_Schedule_ClassroomId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedule",
                table: "Schedule",
                newName: "UQ_Schedule");

            migrationBuilder.RenameIndex(
                name: "IX_Materials_BookId",
                table: "Material",
                newName: "IX_Material_BookId");

            migrationBuilder.RenameColumn(
                name: "Comments",
                table: "Lecture",
                newName: "Info");

            migrationBuilder.RenameIndex(
                name: "IX_Lectures_ClassroomId",
                table: "Lecture",
                newName: "IX_Lecture_ClassroomId");

            migrationBuilder.RenameIndex(
                name: "IX_Exercises_BookId",
                table: "Exercise",
                newName: "IX_Exercise_BookId");

            migrationBuilder.RenameColumn(
                name: "Comments",
                table: "Course",
                newName: "Info");

            migrationBuilder.RenameColumn(
                name: "Comments",
                table: "Classroom",
                newName: "Info");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Broadcast",
                newName: "CreatedByUserId");

            migrationBuilder.RenameColumn(
                name: "Comments",
                table: "Book",
                newName: "Info");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "BotFeedback",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedUserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddColumn<DateTime>(
                name: "PhoneNumberVerificationCode_at",
                table: "AspNetUsers",
                type: "datetime2(0)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProviderDisplayName",
                table: "AspNetUserLogins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacebookPageId",
                table: "School",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedCity",
                table: "School",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                table: "School",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Broadcast",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_School",
            //    table: "School",
            //    column: "Id");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Schedule",
            //    table: "Schedule",
            //    column: "Id");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Material",
            //    table: "Material",
            //    column: "Id");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Lecture",
            //    table: "Lecture",
            //    column: "Id");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Exercise",
            //    table: "Exercise",
            //    column: "Id");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Exam",
            //    table: "Exam",
            //    column: "Id");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Course",
            //    table: "Course",
            //    column: "Id");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Classroom",
            //    table: "Classroom",
            //    column: "Id");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Broadcast",
            //    table: "Broadcast",
            //    column: "Id");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Book",
            //    table: "Book",
            //    column: "Id");

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    LectureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => new { x.StudentId, x.LectureId });
                    table.ForeignKey(
                        name: "FK_Attendance_AspNetUsers",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendance_Lecture",
                        column: x => x.LectureId,
                        principalTable: "Lecture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseBook",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseBook", x => new { x.CourseId, x.BookId });
                    table.ForeignKey(
                        name: "FK_CourseBook_Book",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseBook_Course",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parenthood",
                columns: table => new
                {
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    ChildId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parenthood", x => new { x.ParentId, x.ChildId });
                    table.ForeignKey(
                        name: "FK_Parenthood_AspNetUsers_Child",
                        column: x => x.ChildId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Parenthood_AspNetUsers_Parent",
                        column: x => x.ParentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchoolSettings",
                columns: table => new
                {
                    SchoolId = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Locale2 = table.Column<string>(type: "nchar(2)", fixedLength: true, maxLength: 2, nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolSettings", x => x.SchoolId);
                    table.ForeignKey(
                        name: "FK_SchoolSettings_School",
                        column: x => x.SchoolId,
                        principalTable: "School",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourse",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<decimal>(type: "decimal(6,3)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourse", x => new { x.CourseId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_StudentCourse_AspNetUsers",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourse_Course",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentExam",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<decimal>(type: "decimal(6,3)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentExam", x => new { x.StudentId, x.ExamId });
                    table.ForeignKey(
                        name: "FK_StudentExam_AspNetUsers",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentExam_Exam",
                        column: x => x.ExamId,
                        principalTable: "Exam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentExercise",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<decimal>(type: "decimal(6,3)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentExercise", x => new { x.StudentId, x.ExerciseId });
                    table.ForeignKey(
                        name: "FK_StudentExercise_AspNetUsers",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentExercise_Exercise",
                        column: x => x.ExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherCourse",
                columns: table => new
                {
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherCourse", x => new { x.TeacherId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_TeacherCourse_AspNetUsers",
                        column: x => x.TeacherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherCourse_Course",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSchool",
                columns: table => new
                {
                    AspNetUserId = table.Column<int>(type: "int", nullable: false),
                    SchoolId = table.Column<int>(type: "int", nullable: false),
                    EnrolledOn = table.Column<DateTimeOffset>(type: "datetimeoffset(0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSchool", x => new { x.AspNetUserId, x.SchoolId });
                    table.ForeignKey(
                        name: "FK_UserSchool_AspNetUsers",
                        column: x => x.AspNetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSchool_School",
                        column: x => x.SchoolId,
                        principalTable: "School",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "PhoneNumberIndex",
                table: "AspNetUsers",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "([NormalizedUserName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "([NormalizedName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_NameCity",
                table: "School",
                columns: new[] { "NormalizedName", "NormalizedCity" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Slug",
                table: "School",
                column: "Slug");

            migrationBuilder.CreateIndex(
                name: "ExamLectureIdIndex",
                table: "Exam",
                column: "LectureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Broadcast_CourseId",
                table: "Broadcast",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Broadcast_CreatedByUserId",
                table: "Broadcast",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Broadcast_SchoolId",
                table: "Broadcast",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_LectureId",
                table: "Attendance",
                column: "LectureId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseBook_BookId",
                table: "CourseBook",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Parenthood_ChildId",
                table: "Parenthood",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_StudentId",
                table: "StudentCourse",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExam_ExamId",
                table: "StudentExam",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExercise_ExerciseId",
                table: "StudentExercise",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCourse_CourseId",
                table: "TeacherCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSchool_SchoolId",
                table: "UserSchool",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Broadcast_Course",
                table: "Broadcast",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id");
        }
    }
}
