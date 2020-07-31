@echo off
set /p cs="Enter PhoenixDB Connection String: "

dotnet ef dbcontext scaffold "%cs%" Microsoft.EntityFrameworkCore.SqlServer ^
--context PhoenixContext ^
--output-dir "Main\Models" ^
--use-database-names ^
--force ^
--table "dbo.AspNetRoles" ^
--table "dbo.AspNetUserRoles" ^
--table "dbo.AspNetUsers" ^
--table "dbo.Attendance" ^
--table "dbo.Book" ^
--table "dbo.BotFeedback" ^
--table "dbo.Classroom" ^
--table "dbo.Course" ^
--table "dbo.CourseBook" ^
--table "dbo.Exam" ^
--table "dbo.Exercise" ^
--table "dbo.Lecture" ^
--table "dbo.Material" ^
--table "dbo.Schedule" ^
--table "dbo.School" ^
--table "dbo.StudentCourse" ^
--table "dbo.StudentExam" ^
--table "dbo.StudentExercise" ^
--table "dbo.TeacherCourse" ^
--table "dbo.User" ^
--table "dbo.UserSchool"

Pause