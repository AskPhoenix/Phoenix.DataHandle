DELETE FROM [dbo].[AspNetRoles]
GO
DBCC CHECKIDENT ('dbo.AspNetRoles', RESEED, 0)
GO

INSERT INTO [dbo].[AspNetRoles] (Type, Name, NormalizedName, CreatedAt)
VALUES 
(0,		'Undefined',	N'Απροσδιόριστος',		SYSDATETIMEOFFSET()),
(1,		'None',			N'Κανένας',				SYSDATETIMEOFFSET()),
(10,	'Student',		N'Μαθητής',				SYSDATETIMEOFFSET()),
(11,	'Parent',		N'Γονέας / Κηδεμόνας',	SYSDATETIMEOFFSET()),
(20,	'Teacher',		N'Εκπαιδευτικός',		SYSDATETIMEOFFSET()),
(21,	'Secretary',	N'Γραμματέας',			SYSDATETIMEOFFSET()),
(22,	'SchoolAdmin',	N'Διαχειριστής',		SYSDATETIMEOFFSET()),
(23,	'SchoolOwner',	N'Ιδιοκτήτης',			SYSDATETIMEOFFSET()),
(30,	'SchoolTester',	N'Δοκιμαστής Σχολείου',	SYSDATETIMEOFFSET()),
(35,	'SuperTester',	N'Υπερδοκιμαστής',		SYSDATETIMEOFFSET()),
(36,	'SuperAdmin',	N'Υπερδιαχειριστής',	SYSDATETIMEOFFSET());