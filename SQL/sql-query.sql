--Creation of Database

CREATE DATABASE ClaySysTechnologies


--Creation of Employee Table

CREATE TABLE [dbo].[Employee](
	[EmployeeId] [int] NOT NULL PRIMARY KEY,
	[EmployeeName] [nvarchar](50) NOT NULL,
	[EmployeeBirthDate] [date] NULL,
	[EmployeeAge]  AS (datediff(year,[EmployeeBirthDate],getdate())),
	[FK_PositionId] [int] NOT NULL FOREIGN KEY REFERENCES POSITION,
	[EmployeeSalary] [decimal](10, 2) NULL
)


--Creation of Position Table

CREATE TABLE [dbo].[Position](
	[PositionId] [int] NOT NULL PRIMARY KEY,
	[PositionName] [nvarchar](50) NOT NULL)
)


--Stored Procedure for GetAllEmployee

CREATE PROCEDURE [dbo].[spGetAllEmployee]
AS
	SET NOCOUNT ON;
	SELECT Employee.EmployeeId,Employee.EmployeeName,Employee.EmployeeBirthDate,Employee.EmployeeAge,Position.PositionName,
	Employee.EmployeeSalary
	from Employee INNER JOIN Position on Employee.FK_PositionId = Position.PositionId;
GO


--Stored Procedure for GetEmployeeById

Create PROCEDURE [dbo].[spGetEmployeeById]
(
	@EmployeeId int
)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Employee.EmployeeId,Employee.EmployeeName,Employee.EmployeeBirthDate,Employee.EmployeeAge,Position.PositionName,
	Employee.EmployeeSalary
	from Employee INNER JOIN Position on Employee.FK_PositionId = Position.PositionId
	WHERE Employee.EmployeeId = @EmployeeId;
END


--Stored Procedure for InsertEmployee

create procedure [dbo].[spInsertEmployee]
(
	@EmployeeId int,
	@EmployeeName nvarchar(50),
	@EmployeeBirthDate date,
	@EmployeePositionId int
)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO Employee(EmployeeId,EmployeeName,EmployeeBirthDate,FK_PositionId) 
	values
	(@EmployeeId,@EmployeeName,@EmployeeBirthDate,@EmployeePositionId);
END


--Stored Procedure for UpdateEmployee

CREATE procedure [dbo].[spUpdateEmployee]
(
@EmployeeId int,
@EmployeeName nvarchar(50) = NULL,
@EmployeeBirthDate date = NULL,
@EmployeePositionId int = NULL
)
AS
BEGIN
	UPDATE Employee SET 
	EmployeeName = ISNULL(@EmployeeName,EmployeeName),
	EmployeeBirthDate = ISNULL(@EmployeeBirthDate,EmployeeBirthDate),
	FK_PositionId = ISNULL(@EmployeePositionId,FK_PositionId)
	WHERE EmployeeId = @EmployeeId;
END


--Stored Procedure for DeleteEmployee

create procedure [dbo].[spDeleteEmployee]
(@EmployeeId int)
AS
BEGIN
	SET NOCOUNT ON;
	DELETE FROM Employee WHERE EmployeeId = @EmployeeId;
END


--Stored Procedure for GetAllPosition

CREATE PROCEDURE [dbo].[spGetAllPosition]
AS
BEGIN
	select PositionId,PositionName from Position;
END


--Stored Procedure for GetPositionById

Create PROCEDURE [dbo].[spGetPositionById]
(
	@PositionId int
)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT PositionName from Position where PositionId = @PositionId;
END


--Stored Procedure for InsertPosition

CREATE PROCEDURE [dbo].[spInsertPosition]
(
	@PositionId int,
	@PositionName nvarchar(50)
)
AS
BEGIN
	insert into Position(PositionId,PositionName) values (@PositionId,@PositionName);
END


--Stored Procedure for UpdatePosition

Create PROCEDURE [dbo].[spUpdatePosition]
(
	@PositionId int,
	@PositionName nvarchar(50)
)
AS
BEGIN
	update Position set PositionName=@PositionName where PositionId = @PositionId;
END


--Stored Procedure for DeletePosition

Create PROCEDURE [dbo].[spDeletePosition]
(
	@PositionId int
)
AS
BEGIN
	delete Position where PositionId = @PositionId;
END


