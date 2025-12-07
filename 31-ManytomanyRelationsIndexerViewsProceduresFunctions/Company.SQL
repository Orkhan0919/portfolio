create DATABASE CompanyMM

use CompanyMM

CREATE TABLE Employees
(
    EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    BirthDate DATE CHECK (BirthDate <= GETDATE()),
    Email NVARCHAR(100) NOT NULL UNIQUE
);

INSERT INTO Employees
    (FirstName, LastName, BirthDate, Email)
VALUES
    ('Leyla', 'Hasanova', '1990-05-10', 'leyla@gmail.com'),
    ('Arif', 'Mammadov', '1988-08-22', 'arif@gmail.com'),
    ('Nigar', 'Quliyeva', '1992-03-15', 'nigar@gmail.com'),
    ('Rashad', 'Aliyev', '1985-11-01', 'rashad@gmail.com'),
    ('Kamran', 'Sefarov', '1993-07-30', 'kamran@gmail.com');

CREATE TABLE Projects
(
    ProjectID INT PRIMARY KEY IDENTITY(1,1),

    ProjectName NVARCHAR(100) NOT NULL,

    StartDate DATE,

    EndDate DATE
);

INSERT INTO Projects
    (ProjectName, StartDate, EndDate)
VALUES
    ('Corporate Training Platform', '2025-11-01', '2026-03-31'),
    ('Cloud Infrastructure Migration', '2025-12-01', '2025-12-20'),
    ('Annual Security Audit', '2025-12-05', '2025-12-20');


CREATE TABLE EmployeeProjects
(
    EmployeeID INT NOT NULL,
    ProjectID INT NOT NULL,
    AssignedDate DATE NOT NULL,

    PRIMARY KEY (EmployeeID, ProjectID),

    FOREIGN KEY (EmployeeID) 
        REFERENCES Employees(EmployeeID),

    FOREIGN KEY (ProjectID) 
        REFERENCES Projects(ProjectID)
);

ALTER TABLE Projects
ADD 
    CONSTRAINT UQ_ProjectName UNIQUE (ProjectName),
    CONSTRAINT CK_DateCheck CHECK (EndDate >= StartDate);


SELECT *
from Projects


INSERT INTO EmployeeProjects
    (EmployeeID, ProjectID, AssignedDate)
VALUES
    (1, 1, '2025-10-25'),
    (1, 2, '2025-11-20'),
    (2, 1, '2025-10-25'),
    (3, 3, '2025-12-01'),
    (4, 2, '2025-11-25'),
    (5, 3, '2025-12-01');
(1, 3, '2025-10-25'),
(5, 1, '2025-12-01');

SELECT *
FROM EmployeeProjects
SELECT *
FROM Projects
SELECT *
FROM Employees


SELECT
    E.FirstName + ' ' + E.LastName AS EmployeeName,
    P.ProjectName,
    EP.AssignedDate
FROM
    Employees AS E
    INNER JOIN
    EmployeeProjects AS EP ON E.EmployeeID = EP.EmployeeID
    INNER JOIN
    Projects AS P ON EP.ProjectID = P.ProjectID;


SELECT P.ProjectName AS LayiheAdi, COUNT(EP.EmployeeID) AS IsciSayi
FROM Projects AS P
    INNER JOIN EmployeeProjects AS EP ON P.ProjectID = EP.ProjectID
GROUP BY  P.ProjectName;

SELECT E.FirstName + ' ' + E.LastName AS IsciAdi, COUNT(EP.ProjectID) AS ProyektSayi
FROM Employees AS E
    INNER JOIN EmployeeProjects AS EP ON E.EmployeeID = EP.EmployeeID
GROUP BY E.EmployeeID, E.FirstName, E.LastName
HAVING COUNT(EP.ProjectID) >= 2;





SELECT *
FROM EmployeeProjects


CREATE VIEW EmployeeProjectView
AS
    SELECT
        E.EmployeeID,
        E.FirstName + ' ' + E.LastName AS FullName,
        P.ProjectID,
        P.ProjectName,
        EP.AssignedDate
    FROM
        Employees AS E
        INNER JOIN
        EmployeeProjects AS EP ON E.EmployeeID = EP.EmployeeID
        INNER JOIN
        Projects AS P ON EP.ProjectID = P.ProjectID;



SELECT
    FullName,
    ProjectName,
    AssignedDate
FROM
    EmployeeProjectView
WHERE
    EmployeeID = 1;


CREATE PROCEDURE sp_AssignEmployeeToProject
    @empId INT,
    @projId INT
AS
BEGIN
    IF NOT EXISTS (
        SELECT 1
    FROM EmployeeProjects
    WHERE EmployeeID = @empId AND ProjectID = @projId
    )
    BEGIN
        INSERT INTO EmployeeProjects
            (EmployeeID, ProjectID, AssignedDate)
        VALUES
            (@empId, @projId, GETDATE());

        SELECT N'Teyinat elave edildi' AS Status;
    END
    ELSE
    BEGIN
        SELECT N'Bu teyinat elave edile bilmedi' AS Status;
    END
END

EXEC sp_AssignEmployeeToProject @empId = 3, @projId = 2;

SELECT * FROM EmployeeProjects



CREATE FUNCTION fn_GetProjectCount
(
    @empId INT
)
RETURNS INT
AS
BEGIN
    DECLARE @projectCount INT;
    
    SELECT @projectCount = COUNT(ProjectID)
    FROM EmployeeProjects
    WHERE EmployeeID = @empId;
    
    RETURN @projectCount;
END


SELECT 
    dbo.fn_GetProjectCount(1) AS LeylaPrjctSayi;


DELETE FROM EmployeeProjects
WHERE EmployeeID = 4;

SELECT * FROM EmployeeProjects
