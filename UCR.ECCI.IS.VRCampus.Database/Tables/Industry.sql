CREATE TABLE [Industry] 
(
    [WorkInformationInternalId] INT NOT NULL,
    [Name] VARCHAR(100),
    [CSRelated] BIT NOT NULL,
    PRIMARY KEY (WorkInformationInternalId),
    FOREIGN KEY (WorkInformationInternalId) 
    REFERENCES WorkInformation(WorkInformationInternalId),
);