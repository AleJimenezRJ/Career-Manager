CREATE TABLE [Opportunity] 
(
    [WorkInformationInternalId] INT NOT NULL,
    [Country] VARCHAR(100) NOT NULL,
    PRIMARY KEY ([WorkInformationInternalId]),
    FOREIGN KEY ([WorkInformationInternalId]) 
        REFERENCES [WorkInformation]([WorkInformationInternalId])
);
