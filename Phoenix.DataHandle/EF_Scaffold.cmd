﻿dotnet ef dbcontext scaffold "ConnectionString" Microsoft.EntityFrameworkCore.SqlServer ^
--context PhoenixContext ^
--output-dir Models ^
--use-database-names ^
--force ^
--table "dbo.AspNetRoles" ^
--table "dbo.AspNetUserRoles" ^
--table "dbo.AspNetUsers" ^
--table "dbo.Classroom" ^
--table "dbo.Course" ^
--table "dbo.Lecture" ^
--table "dbo.School" ^
--table "dbo.User"