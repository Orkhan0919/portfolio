CREATE DATABASE Company
GO

USE Company
GO

CREATE TABLE Nations(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE Towns(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    NationId INT NOT NULL
        CONSTRAINT FK_Towns_Nations FOREIGN KEY REFERENCES Nations(Id)
);

CREATE TABLE Employees(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Surname NVARCHAR(100) NOT NULL,
    Age INT,
    Salary DECIMAL(10,2),
    Position NVARCHAR(100),
    IsDeleted BIT DEFAULT 0,
    CityId INT NOT NULL
        CONSTRAINT FK_Employees_Cities FOREIGN KEY REFERENCES Towns(Id)
);

INSERT INTO Nations (Name) VALUES ('Azerbaijan'), ('Turkey'), ('Germany');

INSERT INTO Towns (Name, NationId) VALUES 
('Baku', 1),
('Ganja', 1),
('Sumqayit', 1),
('Mingachevir', 1),
('Shaki', 1);

INSERT INTO Employees (Name, Surname, Age, Salary, Position, IsDeleted, CityId) VALUES
('Ramin', 'Aliyev', 29, 2700, 'Developer', 0, 1),
('Aynur', 'Huseynova', 24, 1800, 'Reception', 0, 2),
('Elvin', 'Mammadov', 35, 3200, 'Manager', 0, 3),
('Nigar', 'Quliyeva', 26, 1900, 'Reception', 1, 4),
('Farid', 'Ismayilov', 30, 2100, 'Developer', 0, 1),
('Leyla', 'Sadigova', 32, 2200, 'Reception', 1, 5);

SELECT e.Name, e.Surname, t.Name AS TownName, n.Name AS NationName
FROM Employees e
INNER JOIN Towns t ON e.CityId = t.Id
INNER JOIN Nations n ON t.NationId = n.Id;

SELECT e.Name, e.Surname, e.Salary, n.Name AS NationName
FROM Employees e
INNER JOIN Towns t ON e.CityId = t.Id
INNER JOIN Nations n ON t.NationId = n.Id
WHERE e.Salary > 2000;

SELECT t.Name AS TownName, n.Name AS NationName
FROM Towns t
INNER JOIN Nations n ON t.NationId = n.Id;

SELECT e.Name, e.Surname, e.Age, e.Salary, e.Position, e.IsDeleted, t.Name AS TownName, n.Name AS NationName
FROM Employees e
INNER JOIN Towns t ON e.CityId = t.Id
INNER JOIN Nations n ON t.NationId = n.Id
WHERE e.Position = 'Reception';

SELECT e.Name, e.Surname, t.Name AS TownName, n.Name AS NationName
FROM Employees e
INNER JOIN Towns t ON e.CityId = t.Id
INNER JOIN Nations n ON t.NationId = n.Id
WHERE e.IsDeleted = 1;

