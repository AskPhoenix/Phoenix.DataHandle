CREATE TABLE [dbo].[BotData](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RealId] [varchar](1024) NOT NULL UNIQUE,
	[Document] [nvarchar](max) NOT NULL,
	[CreatedTime] [datetimeoffset](7) Not NULL,
	[TimeStamp] [datetimeoffset](7) Not NULL,
 CONSTRAINT [PK_BotData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[BotData] ADD  DEFAULT (getutcdate()) FOR [CreatedTime]
GO
ALTER TABLE [dbo].[BotData] ADD  DEFAULT (getutcdate()) FOR [TimeStamp]
GO