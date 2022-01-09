--DELETE FROM [dbo].[AspNetRoleClaims]
--DELETE FROM [dbo].[AspNetUserClaims]
DELETE FROM [dbo].[AspNetUserLogins]
DELETE FROM [dbo].[AspNetUserRoles]
--DELETE FROM [dbo].[AspNetUserTokens]
DELETE FROM [dbo].[Attendance]
DELETE FROM [dbo].[CourseBook]
DELETE FROM [dbo].[Parenthood]
DELETE FROM [dbo].[StudentCourse]
DELETE FROM [dbo].[StudentExam]
DELETE FROM [dbo].[StudentExercise]
DELETE FROM [dbo].[TeacherCourse]
DELETE FROM [dbo].[UserSchool]

DELETE FROM [dbo].[BotData]
DELETE FROM [dbo].[BotFeedback]
DELETE FROM [dbo].[BotTranscript]

DELETE FROM [dbo].[Material]
GO
DELETE FROM [dbo].[Exam]
DELETE FROM [dbo].[Exercise]
GO
DELETE FROM [dbo].[Book]
DELETE FROM [dbo].[Lecture]
GO
DELETE FROM [dbo].[Schedule]
GO
DELETE FROM [dbo].[Classroom]
DELETE FROM [dbo].[Course]
GO
DELETE FROM [dbo].[SchoolSettings]
DELETE FROM [dbo].[User]
GO
DELETE FROM [dbo].[School]
DELETE FROM [dbo].[AspNetUsers]

GO

DBCC CHECKIDENT ('dbo.AspNetUsers', RESEED, 0)
DBCC CHECKIDENT ('dbo.Book', RESEED, 0)
DBCC CHECKIDENT ('dbo.BotFeedback', RESEED, 0)
DBCC CHECKIDENT ('dbo.Classroom', RESEED, 0)
DBCC CHECKIDENT ('dbo.Course', RESEED, 0)
DBCC CHECKIDENT ('dbo.Exam', RESEED, 0)
DBCC CHECKIDENT ('dbo.Exercise', RESEED, 0)
DBCC CHECKIDENT ('dbo.Lecture', RESEED, 0)
DBCC CHECKIDENT ('dbo.Material', RESEED, 0)
DBCC CHECKIDENT ('dbo.School', RESEED, 0)
DBCC CHECKIDENT ('dbo.Schedule', RESEED, 0)