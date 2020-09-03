USE [DepartmentDatabaseContext]
GO

UPDATE [dbo].[DepartmentUsers]
   SET [IsLocked] = 0
      ,[CountAttempt] = 0
 WHERE Id='BA2B0B5E-56BE-4675-8B9C-70FB4FE2E03A'
GO


