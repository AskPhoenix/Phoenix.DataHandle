CREATE TABLE [dbo].[BotTranscript](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Channel] [varchar](256) NOT NULL,
	[Conversation] [varchar](1024) NOT NULL,
    [Activity] [nvarchar](max) NOT NULL,
	[TimeStamp] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_BotTranscript] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY])
GO
CREATE NONCLUSTERED INDEX [IX_TranscriptChannel] ON [dbo].[BotTranscript]
(
	[Channel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_TranscriptTimeStamp] ON [dbo].[BotTranscript]
(
	[TimeStamp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_TranscriptConversation] ON [dbo].[BotTranscript]
(
	[Conversation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BotTranscript] ADD  DEFAULT (getutcdate()) FOR [TimeStamp]
GO