CREATE TABLE [Language]
(
	[LanguageInternalId] INT NOT NULL IDENTITY(1,1),
	[LanguageValue] NVARCHAR(50) NOT NULL,
	[RecruitmentPK] INT NOT NULL,
	CONSTRAINT FK_Recruitment FOREIGN KEY (RecruitmentPK) REFERENCES [Recruitment]([WorkInformationInternalId]),
	CONSTRAINT PK_Language PRIMARY KEY (LanguageInternalId)
)
