-- =========================================
-- Insert Careers
-- =========================================
INSERT INTO [Career] ([Name], [Description], [SemestersNumber], [Modality], [DegreeTitle], [Scholarship], [IsSteam])
VALUES 
('Software Engineering', 'Software design, development, and maintenance.', 10, 'Presential', 'PhD', 0, 1),
('Industrial Engineering', 'Optimizing complex systems and processes.', 8, 'Hybrid', 'Bachelor', 0, 1),
('Biotechnology', 'Engineering and biology integration.', 9, 'Virtual', 'Master', 0, 1);

-- =========================================
-- Insert WorkInformation (base table)
-- 3 Careers × 5 Types = 15 rows
-- =========================================
-- Software Engineering
INSERT INTO [WorkInformation] ([CareerInternalId], [InformationDescription]) VALUES (1, 'Enterprise experience in software consulting.'); --1
INSERT INTO [WorkInformation] ([CareerInternalId], [InformationDescription]) VALUES (1, 'Industry involved in software products.'); --2
INSERT INTO [WorkInformation] ([CareerInternalId], [InformationDescription]) VALUES (1, 'Opportunity available in USA.'); --3
INSERT INTO [WorkInformation] ([CareerInternalId], [InformationDescription]) VALUES (1, 'Recruitment process for dev roles.'); --4
INSERT INTO [WorkInformation] ([CareerInternalId], [InformationDescription]) VALUES (1, 'Balanced work-life for devs.'); --5

-- Industrial Engineering
INSERT INTO [WorkInformation] ([CareerInternalId], [InformationDescription]) VALUES (2, 'Enterprise in logistics consulting.'); --6
INSERT INTO [WorkInformation] ([CareerInternalId], [InformationDescription]) VALUES (2, 'Industry with manufacturing process.'); --7
INSERT INTO [WorkInformation] ([CareerInternalId], [InformationDescription]) VALUES (2, 'Opportunity in Germany.'); --8
INSERT INTO [WorkInformation] ([CareerInternalId], [InformationDescription]) VALUES (2, 'Recruitment for process engineers.'); --9
INSERT INTO [WorkInformation] ([CareerInternalId], [InformationDescription]) VALUES (2, 'Work-life in logistics industry.'); --10

-- Biotechnology
INSERT INTO [WorkInformation] ([CareerInternalId], [InformationDescription]) VALUES (3, 'Enterprise in biotech R&D.'); --11
INSERT INTO [WorkInformation] ([CareerInternalId], [InformationDescription]) VALUES (3, 'Industry in pharmaceuticals.'); --12
INSERT INTO [WorkInformation] ([CareerInternalId], [InformationDescription]) VALUES (3, 'Opportunity in Canada.'); --13
INSERT INTO [WorkInformation] ([CareerInternalId], [InformationDescription]) VALUES (3, 'Recruitment in biotech labs.'); --14
INSERT INTO [WorkInformation] ([CareerInternalId], [InformationDescription]) VALUES (3, 'Work-life in biotech sector.'); --15

-- =========================================
-- Derived Tables
-- =========================================

-- Enterprise
INSERT INTO [Enterprise] ([WorkInformationInternalId], [Name], [Country])
VALUES 
(1, 'CodeWorks Ltd.', 'Costa Rica'),
(6, 'LogistiCore Inc.', 'Mexico'),
(11, 'BioInnovate Co.', 'Germany');

-- Industry
INSERT INTO [Industry] ([WorkInformationInternalId], [Name], [CSRelated])
VALUES 
(2, 'Software Development', 1),
(7, 'Manufacturing', 0),
(12, 'Pharmaceuticals', 0);

-- Opportunity
INSERT INTO [Opportunity] ([WorkInformationInternalId], [Country])
VALUES 
(3, 'United States'),
(8, 'Germany'),
(13, 'Mexico');

-- Recruitment
INSERT INTO [Recruitment] ([WorkInformationInternalId], [Steps], [Requisites])
VALUES 
(4, 'Apply online, technical test, HR interview', 'Bachelor’s degree, GitHub portfolio'),
(9, 'CV review, online test, final interview', 'Engineering background, internship experience'),
(14, 'Submit resume, in-lab assessment, final round', 'Science degree, lab skills');

-- Language for each Recruitment
INSERT INTO [Language] ([LanguageValue], [RecruitmentPK])
VALUES 
('English', 4),
('Spanish', 9),
('French', 14);

-- WorkLife
INSERT INTO [WorkLife] ([WorkInformationInternalId], [AmountFemaleWorkers], [AmountMaleWorkers])
VALUES 
(5, 20, 25),
(10, 15, 30),
(15, 18, 22);
GO

/*
Database procedure to search across all tables for a keyword.
*/
CREATE OR ALTER PROCEDURE SearchAllTables
    @keyword VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SET @keyword = '%' + @keyword + '%';

    -- Career table matches directly
    SELECT 
        'Career' AS TableName,
        C.CareerInternalId AS CareerId,
        C.Name AS CareerName,
        C.Name AS Field,
        'Name' AS ColumnName
    FROM Career C
    WHERE C.Name LIKE @keyword

    UNION ALL

    SELECT 
        'Career',
        C.CareerInternalId,
        C.Name,
        C.Description,
        'Description'
    FROM Career C
    WHERE C.Description LIKE @keyword

    UNION ALL

    SELECT 
        'Career',
        C.CareerInternalId,
        C.Name,
        C.Modality,
        'Modality'
    FROM Career C
    WHERE C.Modality LIKE @keyword

    UNION ALL

    SELECT 
        'Career',
        C.CareerInternalId,
        C.Name,
        C.DegreeTitle,
        'DegreeTitle'
    FROM Career C
    WHERE C.DegreeTitle LIKE @keyword

    -- Enterprise
    UNION ALL
    SELECT 
        'Enterprise',
        C.CareerInternalId,
        C.Name,
        E.Name,
        'Name'
    FROM Enterprise E
    INNER JOIN WorkInformation W ON W.WorkInformationInternalId = E.WorkInformationInternalId
    INNER JOIN Career C ON C.CareerInternalId = W.CareerInternalId
    WHERE E.Name LIKE @keyword

    UNION ALL

    SELECT 
        'Enterprise',
        C.CareerInternalId,
        C.Name,
        E.Country,
        'Country'
    FROM Enterprise E
    INNER JOIN WorkInformation W ON W.WorkInformationInternalId = E.WorkInformationInternalId
    INNER JOIN Career C ON C.CareerInternalId = W.CareerInternalId
    WHERE E.Country LIKE @keyword

    -- Industry
    UNION ALL
    SELECT 
        'Industry',
        C.CareerInternalId,
        C.Name,
        I.Name,
        'Name'
    FROM Industry I
    INNER JOIN WorkInformation W ON W.WorkInformationInternalId = I.WorkInformationInternalId
    INNER JOIN Career C ON C.CareerInternalId = W.CareerInternalId
    WHERE I.Name LIKE @keyword

    -- Language
    UNION ALL
    SELECT 
        'Language',
        C.CareerInternalId,
        C.Name,
        L.LanguageValue,
        'LanguageValue'
    FROM Language L
    INNER JOIN Recruitment R ON R.WorkInformationInternalId = L.RecruitmentPK
    INNER JOIN WorkInformation W ON W.WorkInformationInternalId = R.WorkInformationInternalId
    INNER JOIN Career C ON C.CareerInternalId = W.CareerInternalId
    WHERE L.LanguageValue LIKE @keyword

    -- Opportunity
    UNION ALL
    SELECT 
        'Opportunity',
        C.CareerInternalId,
        C.Name,
        O.Country,
        'Country'
    FROM Opportunity O
    INNER JOIN WorkInformation W ON W.WorkInformationInternalId = O.WorkInformationInternalId
    INNER JOIN Career C ON C.CareerInternalId = W.CareerInternalId
    WHERE O.Country LIKE @keyword

    -- Recruitment
    UNION ALL
    SELECT 
        'Recruitment',
        C.CareerInternalId,
        C.Name,
        R.Steps,
        'Steps'
    FROM Recruitment R
    INNER JOIN WorkInformation W ON W.WorkInformationInternalId = R.WorkInformationInternalId
    INNER JOIN Career C ON C.CareerInternalId = W.CareerInternalId
    WHERE R.Steps LIKE @keyword

    UNION ALL

    SELECT 
        'Recruitment',
        C.CareerInternalId,
        C.Name,
        R.Requisites,
        'Requisites'
    FROM Recruitment R
    INNER JOIN WorkInformation W ON W.WorkInformationInternalId = R.WorkInformationInternalId
    INNER JOIN Career C ON C.CareerInternalId = W.CareerInternalId
    WHERE R.Requisites LIKE @keyword

    -- WorkInformation
    UNION ALL
    SELECT 
        'WorkInformation',
        C.CareerInternalId,
        C.Name,
        W.InformationDescription,
        'InformationDescription'
    FROM WorkInformation W
    INNER JOIN Career C ON C.CareerInternalId = W.CareerInternalId
    WHERE W.InformationDescription LIKE @keyword;
END
