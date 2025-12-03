
CREATE DATABASE Company;

USE Company;

CREATE TABLE Employees (
    EmployeeID VARCHAR(20),
    FirstName VARCHAR(40),
    LastName VARCHAR(40),
    Email VARCHAR(45),
    PhoneNumber VARCHAR(25),
    HireDate DATE,
    JobTitle VARCHAR(40),
    Salary DECIMAL(5,2),
    Department VARCHAR(40)
);


INSERT INTO Employees 
(FirstName, LastName, Email, PhoneNumber, HireDate, JobTitle, Salary, Department)
VALUES
('Orkhan', 'Mirzayev', 'orkhan@gmail.com', '0501112233', '2020-02-10', 'Backend Developer', 2300, 'IT'),
('Aysel', 'Nuriyeva', 'aysel@gmail.com', '0502223344', '2019-06-12', 'HR Specialist', 2100, 'HR'),
('Elvin', 'Suleymanov', 'elvin@gmail.com', '0503334455', '2021-10-05', 'Accountant', 1750, 'Finance'),
('Zaur', 'Aliyev', 'zaur@gmail.com', '0504445566', '2018-08-20', 'Sales Manager', 2500, 'Sales'),
('Gunel', 'Rahimova', 'gunel@gmail.com', '0505556677', '2022-03-15', 'Frontend Developer', 2600, 'IT');


SELECT * FROM Employees;

SELECT * FROM Employees WHERE Salary > 2000;



SELECT * FROM Employees WHERE Department = 'IT';

SELECT * FROM Employees ORDER BY Salary DESC;


SELECT FirstName, Salary FROM Employees;


SELECT * FROM Employees WHERE HireDate > '2020-01-01';


SELECT * FROM Employees WHERE Email LIKE '%company.az%';


SELECT MAX(Salary) AS HighestSalary FROM Employees;


SELECT MIN(Salary) AS LowestSalary FROM Employees;


SELECT AVG(Salary) AS AverageSalary FROM Employees;


SELECT COUNT(*) AS TotalEmployees FROM Employees;


SELECT SUM(Salary) AS TotalSalary FROM Employees;


SELECT Department, COUNT(*) AS EmployeeCount FROM Employees GROUP BY Department;


SELECT Department, AVG(Salary) AS AvgSalaryc FROM Employees GROUP BY Department;


SELECT Department, MAX(Salary) AS MaxSalary FROM Employees GROUP BY Department;

UPDATE Employees SET Salary = 2800 WHERE EmployeeID = 1;


UPDATE Employees SET Salary = Salary * 1.10 WHERE Department = 'IT';

UPDATE Employees SET JobTitle = 'HR Meneceri' WHERE FirstName = 'Leyla' AND LastName = 'Həsənova';


DELETE FROM Employees WHERE EmployeeID = 5;



DELETE FROM Employees WHERE Salary < 1500;


SELECT * FROM Employees WHERE FirstName LIKE '%a%';


SELECT * FROM Employees WHERE Salary BETWEEN 2000 AND 2500;


SELECT * FROM Employees WHERE Department IN ('Finance', 'IT');
