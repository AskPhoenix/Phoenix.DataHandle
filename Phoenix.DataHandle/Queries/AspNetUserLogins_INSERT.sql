INSERT INTO dbo.AspNetUserLogins (
	LoginProvider, 
	ProviderKey, 
	UserId, 
	OneTimeCode, 
	OTCUsed,
	CreatedAt) 
SELECT 
	'Facebook', 
	FacebookId,
	Id, 
	OneTimeCode, 
	OneTimeCodeUsed, 
	SYSDATETIMEOFFSET()
FROM dbo.AspNetUsers
WHERE FacebookId IS NOT NULL
