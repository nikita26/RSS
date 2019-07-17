CREATE TABLE Employees
(
	Id int NOT NULL PRIMARY KEY IDENTITY,
	Name varchar (30) NOT NULL,
	Active bit NOT NULL
)
 CREATE TABLE Salaries
(
	Id int NOT NULL,
	Salary real NOT NULL,
	Datetime datetime,
	FOREIGN KEY (Id) REFERENCES Employees(Id)
)