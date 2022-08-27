@echo off
set /p cs="Enter PhoenixDB Connection String: "

dotnet ef dbcontext scaffold "%cs%" Microsoft.EntityFrameworkCore.SqlServer ^
--context PhoenixContext ^
--output-dir "Main\Models" ^
--use-database-names ^
--force ^
--table "dbo.Attendances" ^
--table "dbo.Books" ^
--table "dbo.BotFeedback" ^
--table "dbo.BroadcastCourses" ^
--table "dbo.Broadcasts" ^
--table "dbo.Classrooms" ^
--table "dbo.CourseBooks" ^
--table "dbo.Courses" ^
--table "dbo.CourseUsers" ^
--table "dbo.Exams" ^
--table "dbo.Exercises" ^
--table "dbo.Grades" ^
--table "dbo.Lectures" ^
--table "dbo.Materials" ^
--table "dbo.OneTimeCodes" ^
--table "dbo.Parenthoods" ^
--table "dbo.Schedules" ^
--table "dbo.SchoolConnections" ^
--table "dbo.Schools" ^
--table "dbo.SchoolSettings" ^
--table "dbo.SchoolUsers" ^
--table "dbo.UserConnections" ^
--table "dbo.Users"

Pause