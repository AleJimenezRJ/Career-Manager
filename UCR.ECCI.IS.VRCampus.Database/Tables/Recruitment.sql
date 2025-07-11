CREATE TABLE [Recruitment] 
(
    [WorkInformationInternalId] INT NOT NULL,
    [Steps] VARCHAR(700),
    [Requisites] VARCHAR(700),
    PRIMARY KEY ([WorkInformationInternalId]),
    FOREIGN KEY ([WorkInformationInternalId]) 
        REFERENCES [WorkInformation]([WorkInformationInternalId])
);
