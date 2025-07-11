CREATE TABLE [Enterprise] 
(
    [WorkInformationInternalId] INT NOT NULL,
    [Name] VARCHAR(100) NOT NULL,
    [Country] VARCHAR(100) NOT NULL,
    PRIMARY KEY ([WorkInformationInternalId]),
    FOREIGN KEY ([WorkInformationInternalId]) 
        REFERENCES [WorkInformation]([WorkInformationInternalId]),
    CONSTRAINT UQ_Enterprise_Name UNIQUE ([Name])
);
