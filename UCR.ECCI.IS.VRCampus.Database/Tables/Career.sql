CREATE TABLE [Career] 
(
    [CareerInternalId] INT IDENTITY(1,1) NOT NULL,
    [Name] VARCHAR(100) NOT NULl,
    [Description] VARCHAR(700) NOT NULL,
    [SemestersNumber] INT NOT NULL,
    [Modality] VARCHAR(50) NOT NULL,
    [DegreeTitle] VARCHAR(50) NOT NULL,
    [Scholarship] DECIMAL(18,2) NOT NULL,
    [IsSteam] BIT NOT NULL,
    CONSTRAINT PK_Career PRIMARY KEY ([CareerInternalId]),
    CONSTRAINT UQ_Career_Name UNIQUE ([Name])
);
