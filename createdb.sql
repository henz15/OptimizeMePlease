USE master
GO
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'OptimizeMePlease')
BEGIN
  CREATE DATABASE [OptimizeMePlease];
END;