CREATE TABLE [WorkLife] 
(
    [WorkInformationInternalId] INT,
    [AmountFemaleWorkers] INT NOT NULL,
    [AmountMaleWorkers] INT NOT NULL,
    PRIMARY KEY (WorkInformationInternalId),
    FOREIGN KEY (WorkInformationInternalId) 
    REFERENCES WorkInformation(WorkInformationInternalId),
);