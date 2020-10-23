IF NOT EXISTS (
    select * from sysobjects where name='Employee' and xtype='U'
) CREATE TABLE Employee (
    [id]  INT PRIMARY KEY,
    [employee_name] NVARCHAR(40),
    [employee_salary] INT,
    [employee_age] INT,
    [profile_image] NVARCHAR(255),
);
INSERT INTO Employee VALUES
    (1111,'Ernesto Chong',999999,30,'');
Go
Create procedure sp_Employee  
as        
begin        
       SELECT * FROM Employee Order by employee_name  
End;
Go
Create procedure sp_EmployeeRangeSalary    
(        
   @PsearchValue1 INTEGER,      
   @PsearchValue2 INTEGER
)        
as        
begin        
       SELECT * FROM Employee where employee_salary >= @PsearchValue1 AND employee_salary <= @PsearchValue2  
End;
