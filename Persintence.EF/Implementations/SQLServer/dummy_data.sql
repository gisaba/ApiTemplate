
CREATE TABLE dbo.employee (
    id uniqueidentifier NOT NULL,
	seq_id int NOT NULL,
    name varchar(120) NOT NULL,
    salary int NOT NULL,
	company varchar(120),
    CONSTRAINT emp_pk PRIMARY KEY (id)
);

TRUNCATE TABLE dbo.employee;

INSERT INTO dbo.employee(id,seq_id, name,salary,company) VALUES(NEWID(),0, 'Employee', 1000,'Newco') 
GO 100

DECLARE @seq_id int
SET @seq_id = 0

UPDATE
   dbo.employee
SET
  @seq_id = seq_id = @seq_id + 1


UPDATE dbo.employee set name = CONCAT(name,seq_id)


SELECT TOP (1000) [id]
      ,[seq_id]
      ,[name]
      ,[salary]
      ,[company]
  FROM [TEST].[dbo].[employee]