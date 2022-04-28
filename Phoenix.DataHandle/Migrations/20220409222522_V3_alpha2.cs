using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Phoenix.DataHandle.Migrations
{
    public partial class V3_alpha2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_Channel",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_AspNetUsers",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Lecture",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_BroadcastCourse_Broadcast",
                table: "BroadcastCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_BroadcastCourse_Course",
                table: "BroadcastCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_Broadcast_AspNetUsers",
                table: "Broadcasts");

            migrationBuilder.DropForeignKey(
                name: "FK_Broadcast_School",
                table: "Broadcasts");

            migrationBuilder.DropForeignKey(
                name: "FK_Classroom_School",
                table: "Classrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseBook_Book",
                table: "CourseBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseBook_Course",
                table: "CourseBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_School",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Exam_Lecture",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_Book",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_Lecture",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Lecture_Classroom",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_Lecture_Course",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_Lecture_Schedule",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_Material_Book",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_Material_Exam",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_Parenthood_AspNetUsers_Child",
                table: "Parenthoods");

            migrationBuilder.DropForeignKey(
                name: "FK_Parenthood_AspNetUsers_Parent",
                table: "Parenthoods");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Classroom",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Course",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolSettings_School",
                table: "SchoolInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolLogins_Channel",
                table: "SchoolLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolLogins_School",
                table: "SchoolLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSchool_AspNetUsers",
                table: "SchoolUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSchool_School",
                table: "SchoolUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_User_AspNetUsers",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSchool",
                table: "SchoolUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parenthood",
                table: "Parenthoods");

            migrationBuilder.DropIndex(
                name: "IX_Lecture_CourseId",
                table: "Lectures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseBook",
                table: "CourseBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BroadcastCourse",
                table: "BroadcastCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attendance",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendance_StudentId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "UQ_AspNetUsers_UserName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NormalizedName",
                table: "AspNetRoles");

            migrationBuilder.RenameIndex(
                name: "IX_UserSchool_SchoolId",
                table: "SchoolUsers",
                newName: "IX_SchoolUsers_School");

            migrationBuilder.RenameIndex(
                name: "IX_School",
                table: "Schools",
                newName: "IX_Schools_Code");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolChannel",
                table: "SchoolLogins",
                newName: "UQ_SchoolChannels_Channel_ProviderKey");

            migrationBuilder.RenameIndex(
                name: "IX_Schedule",
                table: "Schedules",
                newName: "UQ_Schedules_Course_DayOfWeek_StartTime");

            migrationBuilder.RenameIndex(
                name: "IX_Parenthood_ParentId",
                table: "Parenthoods",
                newName: "IX_Parenthoods_Parent");

            migrationBuilder.RenameIndex(
                name: "IX_Parenthood_ChildId",
                table: "Parenthoods",
                newName: "IX_Parenthoods_Child");

            migrationBuilder.RenameIndex(
                name: "IX_Material_ExamId",
                table: "Materials",
                newName: "IX_Materials_Exam");

            migrationBuilder.RenameIndex(
                name: "IX_Lecture_ScheduleId",
                table: "Lectures",
                newName: "IX_Lectures_ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Exercise_LectureId",
                table: "Exercises",
                newName: "IX_Exercises_Lecture");

            migrationBuilder.RenameIndex(
                name: "IX_Exam",
                table: "Exams",
                newName: "IX_Exams_Lecture");

            migrationBuilder.RenameIndex(
                name: "IX_CourseUsers_UserId",
                table: "CourseUsers",
                newName: "IX_CourseUsers_User");

            migrationBuilder.RenameIndex(
                name: "IX_CourseUsers_CourseId",
                table: "CourseUsers",
                newName: "IX_CourseUsers_Course");

            migrationBuilder.RenameIndex(
                name: "IX_Course",
                table: "Courses",
                newName: "IX_Courses_School_Code");

            migrationBuilder.RenameIndex(
                name: "IX_CourseBook_CourseId",
                table: "CourseBooks",
                newName: "IX_CourseBooks_Course");

            migrationBuilder.RenameIndex(
                name: "IX_Classroom",
                table: "Classrooms",
                newName: "IX_Classrooms_School_NormalizedName");

            migrationBuilder.RenameIndex(
                name: "UQ_Channel_Provider",
                table: "Channels",
                newName: "UQ_Channels_Provider");

            migrationBuilder.RenameIndex(
                name: "UQ_Channel_Code",
                table: "Channels",
                newName: "UQ_Channels_Code");

            migrationBuilder.RenameColumn(
                name: "ScheduledDate",
                table: "Broadcasts",
                newName: "ScheduledFor");

            migrationBuilder.RenameIndex(
                name: "IX_Broadcast_SchoolId_Date",
                table: "Broadcasts",
                newName: "IX_Broadcasts_School_ScheduledFor");

            migrationBuilder.RenameIndex(
                name: "IX_Broadcast_Date_Daypart",
                table: "Broadcasts",
                newName: "IX_Broadcasts_ScheduledFor_Daypart");

            migrationBuilder.RenameIndex(
                name: "IX_Broadcast_AuthorId_Date",
                table: "Broadcasts",
                newName: "IX_Broadcasts_Author_ScheduledFor");

            migrationBuilder.RenameIndex(
                name: "IX_BroadcastCourse",
                table: "BroadcastCourses",
                newName: "IX_BroadcastCourses_Broadcast");

            migrationBuilder.RenameIndex(
                name: "IX_BotFeedback_AuthorId",
                table: "BotFeedback",
                newName: "IX_BotFeedback_Author");

            migrationBuilder.RenameIndex(
                name: "IX_Book",
                table: "Books",
                newName: "IX_Books_NormalizedName");

            migrationBuilder.RenameIndex(
                name: "IX_Attendance_LectureId",
                table: "Attendances",
                newName: "IX_Attendances_Lecture");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_AspNetUsers");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_Channel_ProviderKey");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "IdentifierCodeCreatedAt",
                table: "Users",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdentifierCode",
                table: "Users",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Schools",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ObviatedAt",
                table: "Schools",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Schools",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Schools",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Schools",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "AddressLine",
                table: "Schools",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "SchoolLogins",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "SchoolLogins",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0);

            migrationBuilder.AlterColumn<string>(
                name: "TimeZone",
                table: "SchoolInfo",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "SecondaryLocale",
                table: "SchoolInfo",
                type: "nchar(8)",
                fixedLength: true,
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(5)",
                oldFixedLength: true,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "SecondaryLanguage",
                table: "SchoolInfo",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PrimaryLocale",
                table: "SchoolInfo",
                type: "nchar(8)",
                fixedLength: true,
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(5)",
                oldFixedLength: true,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "PrimaryLanguage",
                table: "SchoolInfo",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneCode",
                table: "SchoolInfo",
                type: "nchar(8)",
                fixedLength: true,
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(5)",
                oldFixedLength: true,
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "SchoolInfo",
                type: "nchar(16)",
                fixedLength: true,
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(10)",
                oldFixedLength: true,
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Schedules",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Schedules",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ObviatedAt",
                table: "Schedules",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Schedules",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Schedules",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Materials",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Section",
                table: "Materials",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Materials",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Chapter",
                table: "Materials",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Lectures",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Lectures",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Grades",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Grades",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Exercises",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Page",
                table: "Exercises",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Exercises",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Exams",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Exams",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Courses",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SubCourse",
                table: "Courses",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ObviatedAt",
                table: "Courses",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Courses",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "Courses",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastDate",
                table: "Courses",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Group",
                table: "Courses",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FirstDate",
                table: "Courses",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Courses",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Classrooms",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ObviatedAt",
                table: "Classrooms",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedName",
                table: "Classrooms",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Classrooms",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Classrooms",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Channels",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Provider",
                table: "Channels",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Channels",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Broadcasts",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SentAt",
                table: "Broadcasts",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Broadcasts",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "BotFeedback",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "BotFeedback",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Books",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Publisher",
                table: "Books",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedName",
                table: "Books",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Books",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Books",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValueSql: "(N'')",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AspNetUsers",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PhoneNumberVerificationCodeCreatedAt",
                table: "AspNetUsers",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumberVerificationCode",
                table: "AspNetUsers",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ObviatedAt",
                table: "AspNetUsers",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedUserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValueSql: "(N'')",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LockoutEnd",
                table: "AspNetUsers",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AspNetUserLogins",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUserLogins",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AspNetRoles",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetRoles",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetRoles",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset(0)",
                oldPrecision: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "AspNetUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SchoolUsers",
                table: "SchoolUsers",
                columns: new[] { "UserId", "SchoolId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parenthoods",
                table: "Parenthoods",
                columns: new[] { "ParentId", "ChildId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseBooks",
                table: "CourseBooks",
                columns: new[] { "CourseId", "BookId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BroadcastCourses",
                table: "BroadcastCourses",
                columns: new[] { "BroadcastId", "CourseId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attendances",
                table: "Attendances",
                columns: new[] { "AttendeeId", "LectureId" });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_Course",
                table: "Schedules",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_Course_Schedule",
                table: "Lectures",
                columns: new[] { "CourseId", "ScheduleId" });

            migrationBuilder.CreateIndex(
                name: "UQ_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_AspNetRoles_Type",
                table: "AspNetRoles",
                column: "Type",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_Channels",
                table: "AspNetUserLogins",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_AspNetUsers",
                table: "Attendances",
                column: "AttendeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Lectures",
                table: "Attendances",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BroadcastCourses_Broadcasts",
                table: "BroadcastCourses",
                column: "BroadcastId",
                principalTable: "Broadcasts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BroadcastCourses_Courses",
                table: "BroadcastCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Broadcasts_AspNetUsers",
                table: "Broadcasts",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Broadcasts_Schools",
                table: "Broadcasts",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Classrooms_Schools",
                table: "Classrooms",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseBooks_Books",
                table: "CourseBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseBooks_Courses",
                table: "CourseBooks",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Schools",
                table: "Courses",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Lectures",
                table: "Exams",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Books",
                table: "Exercises",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Lectures",
                table: "Exercises",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Classrooms",
                table: "Lectures",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Courses",
                table: "Lectures",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Schedules",
                table: "Lectures",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Books",
                table: "Materials",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Exams",
                table: "Materials",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parenthoods_AspNetUsers_Child",
                table: "Parenthoods",
                column: "ChildId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parenthoods_AspNetUsers_Parent",
                table: "Parenthoods",
                column: "ParentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Classrooms",
                table: "Schedules",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Courses",
                table: "Schedules",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolSettings_Schools",
                table: "SchoolInfo",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolLogins_Channels",
                table: "SchoolLogins",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolLogins_Schools",
                table: "SchoolLogins",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolUsers_AspNetUsers",
                table: "SchoolUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolUsers_Schools",
                table: "SchoolUsers",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AspNetUsers",
                table: "Users",
                column: "AspNetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_Channels",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_AspNetUsers",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Lectures",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_BroadcastCourses_Broadcasts",
                table: "BroadcastCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_BroadcastCourses_Courses",
                table: "BroadcastCourses");

            migrationBuilder.DropForeignKey(
                name: "FK_Broadcasts_AspNetUsers",
                table: "Broadcasts");

            migrationBuilder.DropForeignKey(
                name: "FK_Broadcasts_Schools",
                table: "Broadcasts");

            migrationBuilder.DropForeignKey(
                name: "FK_Classrooms_Schools",
                table: "Classrooms");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseBooks_Books",
                table: "CourseBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseBooks_Courses",
                table: "CourseBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Schools",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Lectures",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Books",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Lectures",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Classrooms",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Courses",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Schedules",
                table: "Lectures");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Books",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Exams",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_Parenthoods_AspNetUsers_Child",
                table: "Parenthoods");

            migrationBuilder.DropForeignKey(
                name: "FK_Parenthoods_AspNetUsers_Parent",
                table: "Parenthoods");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Classrooms",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Courses",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolSettings_Schools",
                table: "SchoolInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolLogins_Channels",
                table: "SchoolLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolLogins_Schools",
                table: "SchoolLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolUsers_AspNetUsers",
                table: "SchoolUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_SchoolUsers_Schools",
                table: "SchoolUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_AspNetUsers",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SchoolUsers",
                table: "SchoolUsers");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_Course",
                table: "Schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parenthoods",
                table: "Parenthoods");

            migrationBuilder.DropIndex(
                name: "IX_Lectures_Course_Schedule",
                table: "Lectures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseBooks",
                table: "CourseBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BroadcastCourses",
                table: "BroadcastCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attendances",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "UQ_AspNetUsers_UserName",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "UQ_AspNetRoles_Type",
                table: "AspNetRoles");

            migrationBuilder.RenameIndex(
                name: "IX_SchoolUsers_School",
                table: "SchoolUsers",
                newName: "IX_UserSchool_SchoolId");

            migrationBuilder.RenameIndex(
                name: "IX_Schools_Code",
                table: "Schools",
                newName: "IX_School");

            migrationBuilder.RenameIndex(
                name: "UQ_SchoolChannels_Channel_ProviderKey",
                table: "SchoolLogins",
                newName: "IX_SchoolChannel");

            migrationBuilder.RenameIndex(
                name: "UQ_Schedules_Course_DayOfWeek_StartTime",
                table: "Schedules",
                newName: "IX_Schedule");

            migrationBuilder.RenameIndex(
                name: "IX_Parenthoods_Parent",
                table: "Parenthoods",
                newName: "IX_Parenthood_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Parenthoods_Child",
                table: "Parenthoods",
                newName: "IX_Parenthood_ChildId");

            migrationBuilder.RenameIndex(
                name: "IX_Materials_Exam",
                table: "Materials",
                newName: "IX_Material_ExamId");

            migrationBuilder.RenameIndex(
                name: "IX_Lectures_ScheduleId",
                table: "Lectures",
                newName: "IX_Lecture_ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Exercises_Lecture",
                table: "Exercises",
                newName: "IX_Exercise_LectureId");

            migrationBuilder.RenameIndex(
                name: "IX_Exams_Lecture",
                table: "Exams",
                newName: "IX_Exam");

            migrationBuilder.RenameIndex(
                name: "IX_CourseUsers_User",
                table: "CourseUsers",
                newName: "IX_CourseUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseUsers_Course",
                table: "CourseUsers",
                newName: "IX_CourseUsers_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_School_Code",
                table: "Courses",
                newName: "IX_Course");

            migrationBuilder.RenameIndex(
                name: "IX_CourseBooks_Course",
                table: "CourseBooks",
                newName: "IX_CourseBook_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Classrooms_School_NormalizedName",
                table: "Classrooms",
                newName: "IX_Classroom");

            migrationBuilder.RenameIndex(
                name: "UQ_Channels_Provider",
                table: "Channels",
                newName: "UQ_Channel_Provider");

            migrationBuilder.RenameIndex(
                name: "UQ_Channels_Code",
                table: "Channels",
                newName: "UQ_Channel_Code");

            migrationBuilder.RenameColumn(
                name: "ScheduledFor",
                table: "Broadcasts",
                newName: "ScheduledDate");

            migrationBuilder.RenameIndex(
                name: "IX_Broadcasts_School_ScheduledFor",
                table: "Broadcasts",
                newName: "IX_Broadcast_SchoolId_Date");

            migrationBuilder.RenameIndex(
                name: "IX_Broadcasts_ScheduledFor_Daypart",
                table: "Broadcasts",
                newName: "IX_Broadcast_Date_Daypart");

            migrationBuilder.RenameIndex(
                name: "IX_Broadcasts_Author_ScheduledFor",
                table: "Broadcasts",
                newName: "IX_Broadcast_AuthorId_Date");

            migrationBuilder.RenameIndex(
                name: "IX_BroadcastCourses_Broadcast",
                table: "BroadcastCourses",
                newName: "IX_BroadcastCourse");

            migrationBuilder.RenameIndex(
                name: "IX_BotFeedback_Author",
                table: "BotFeedback",
                newName: "IX_BotFeedback_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_NormalizedName",
                table: "Books",
                newName: "IX_Book");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_Lecture",
                table: "Attendances",
                newName: "IX_Attendance_LectureId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_AspNetUsers",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_Channel_ProviderKey",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "IdentifierCodeCreatedAt",
                table: "Users",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdentifierCode",
                table: "Users",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(16)",
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Schools",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ObviatedAt",
                table: "Schools",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Schools",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Schools",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Schools",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "AddressLine",
                table: "Schools",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "SchoolLogins",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "SchoolLogins",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "TimeZone",
                table: "SchoolInfo",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "SecondaryLocale",
                table: "SchoolInfo",
                type: "nchar(5)",
                fixedLength: true,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(8)",
                oldFixedLength: true,
                oldMaxLength: 8);

            migrationBuilder.AlterColumn<string>(
                name: "SecondaryLanguage",
                table: "SchoolInfo",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "PrimaryLocale",
                table: "SchoolInfo",
                type: "nchar(5)",
                fixedLength: true,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(8)",
                oldFixedLength: true,
                oldMaxLength: 8);

            migrationBuilder.AlterColumn<string>(
                name: "PrimaryLanguage",
                table: "SchoolInfo",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneCode",
                table: "SchoolInfo",
                type: "nchar(5)",
                fixedLength: true,
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(8)",
                oldFixedLength: true,
                oldMaxLength: 8);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "SchoolInfo",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(16)",
                oldFixedLength: true,
                oldMaxLength: 16);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Schedules",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "StartTime",
                table: "Schedules",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ObviatedAt",
                table: "Schedules",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "EndTime",
                table: "Schedules",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Schedules",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Materials",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Section",
                table: "Materials",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Materials",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "Chapter",
                table: "Materials",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Lectures",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Lectures",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Grades",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Grades",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Exercises",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Page",
                table: "Exercises",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Exercises",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Exams",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Exams",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Courses",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SubCourse",
                table: "Courses",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ObviatedAt",
                table: "Courses",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Courses",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Level",
                table: "Courses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastDate",
                table: "Courses",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "Group",
                table: "Courses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "FirstDate",
                table: "Courses",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Courses",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Classrooms",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ObviatedAt",
                table: "Classrooms",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedName",
                table: "Classrooms",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Classrooms",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Classrooms",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Channels",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Provider",
                table: "Channels",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Channels",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Broadcasts",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "SentAt",
                table: "Broadcasts",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Broadcasts",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "BotFeedback",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "BotFeedback",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Books",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Publisher",
                table: "Books",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedName",
                table: "Books",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Books",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Books",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldDefaultValueSql: "(N'')");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "AspNetUsers",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "PhoneNumberVerificationCodeCreatedAt",
                table: "AspNetUsers",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumberVerificationCode",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ObviatedAt",
                table: "AspNetUsers",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedUserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldDefaultValueSql: "(N'')");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "AspNetUsers",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: false,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "(getdate())");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "AspNetUserLogins",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "AspNetUserLogins",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "AspNetRoles",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetRoles",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "AspNetRoles",
                type: "datetimeoffset(0)",
                precision: 0,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                table: "AspNetRoles",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "Users",
                column: "AspNetUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSchool",
                table: "SchoolUsers",
                columns: new[] { "UserId", "SchoolId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parenthood",
                table: "Parenthoods",
                columns: new[] { "ParentId", "ChildId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseBook",
                table: "CourseBooks",
                columns: new[] { "CourseId", "BookId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BroadcastCourse",
                table: "BroadcastCourses",
                columns: new[] { "BroadcastId", "CourseId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attendance",
                table: "Attendances",
                columns: new[] { "AttendeeId", "LectureId" });

            migrationBuilder.CreateIndex(
                name: "IX_Lecture_CourseId",
                table: "Lectures",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_StudentId",
                table: "Attendances",
                column: "AttendeeId");

            migrationBuilder.CreateIndex(
                name: "UQ_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_Channel",
                table: "AspNetUserLogins",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_AspNetUsers",
                table: "Attendances",
                column: "AttendeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Lecture",
                table: "Attendances",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BroadcastCourse_Broadcast",
                table: "BroadcastCourses",
                column: "BroadcastId",
                principalTable: "Broadcasts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BroadcastCourse_Course",
                table: "BroadcastCourses",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Broadcast_AspNetUsers",
                table: "Broadcasts",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Broadcast_School",
                table: "Broadcasts",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Classroom_School",
                table: "Classrooms",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseBook_Book",
                table: "CourseBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseBook_Course",
                table: "CourseBooks",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Course_School",
                table: "Courses",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exam_Lecture",
                table: "Exams",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_Book",
                table: "Exercises",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_Lecture",
                table: "Exercises",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lecture_Classroom",
                table: "Lectures",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lecture_Course",
                table: "Lectures",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lecture_Schedule",
                table: "Lectures",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Material_Book",
                table: "Materials",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Material_Exam",
                table: "Materials",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parenthood_AspNetUsers_Child",
                table: "Parenthoods",
                column: "ChildId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parenthood_AspNetUsers_Parent",
                table: "Parenthoods",
                column: "ParentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Classroom",
                table: "Schedules",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Course",
                table: "Schedules",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolSettings_School",
                table: "SchoolInfo",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolLogins_Channel",
                table: "SchoolLogins",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolLogins_School",
                table: "SchoolLogins",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSchool_AspNetUsers",
                table: "SchoolUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSchool_School",
                table: "SchoolUsers",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_AspNetUsers",
                table: "Users",
                column: "AspNetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
