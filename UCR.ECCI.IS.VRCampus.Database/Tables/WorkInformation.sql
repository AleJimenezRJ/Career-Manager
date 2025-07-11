CREATE TABLE [WorkInformation] 
(
    [WorkInformationInternalId] INT IDENTITY(1,1) NOT NULL, --PK
    [CareerInternalId] INT NOT NULL, --FK to Career
    [InformationDescription] VARCHAR(700) NOT NULL,
    CONSTRAINT PK_WorkInformation PRIMARY KEY ([WorkInformationInternalId]),
    CONSTRAINT FK_WorkInformation_Career 
        FOREIGN KEY ([CareerInternalId]) 
        REFERENCES [Career]([CareerInternalId])
);