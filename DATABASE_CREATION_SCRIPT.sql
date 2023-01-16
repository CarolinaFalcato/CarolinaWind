-- Creating the QEnergy database
CREATE DATABASE QEnergy;

-- In Microsoft Visual Studio:
-- Execute the following in Package Manager Console:
-- cd QEnergy.Services.Persistence.EntityFramework
-- dotnet ef -v --startup-project ..\QEnergy.API database update

-- Inserting companies
USE [QEnergy]
GO

INSERT INTO [dbo].[Companies]
           ([Name]
           ,[Created]
           ,[Modified]
           ,[CreatedBy]
           ,[ModifiedBy])
     VALUES
           ('Company 1' , GETUTCDATE() , NULL , '00000000-0000-0000-0000-000000000000' ,NULL),
           ('Company 2' , GETUTCDATE() , NULL , '00000000-0000-0000-0000-000000000000' ,NULL),
           ('Company 3' , GETUTCDATE() , NULL , '00000000-0000-0000-0000-000000000000' ,NULL),
           ('Company 4' , GETUTCDATE() , NULL , '00000000-0000-0000-0000-000000000000' ,NULL),
           ('Company 5' , GETUTCDATE() , NULL , '00000000-0000-0000-0000-000000000000' ,NULL),
           ('Company 6' , GETUTCDATE() , NULL , '00000000-0000-0000-0000-000000000000' ,NULL),
           ('Company 7' , GETUTCDATE() , NULL , '00000000-0000-0000-0000-000000000000' ,NULL),
           ('Company 8' , GETUTCDATE() , NULL , '00000000-0000-0000-0000-000000000000' ,NULL),
           ('Company 9' , GETUTCDATE() , NULL , '00000000-0000-0000-0000-000000000000' ,NULL),
           ('Company 10' , GETUTCDATE() , NULL , '00000000-0000-0000-0000-000000000000' ,NULL),
           ('Company 11' , GETUTCDATE() , NULL , '00000000-0000-0000-0000-000000000000' ,NULL),
           ('Company 12' , GETUTCDATE() , NULL , '00000000-0000-0000-0000-000000000000' ,NULL),
           ('Company 13' , GETUTCDATE() , NULL , '00000000-0000-0000-0000-000000000000' ,NULL),
           ('Company 14' , GETUTCDATE() , NULL , '00000000-0000-0000-0000-000000000000' ,NULL),
           ('Company 15' , GETUTCDATE() , NULL , '00000000-0000-0000-0000-000000000000' ,NULL),
           ('Company 16' , GETUTCDATE() , NULL , '00000000-0000-0000-0000-000000000000' ,NULL),
           ('Company 17' , GETUTCDATE() , NULL , '00000000-0000-0000-0000-000000000000' ,NULL),
           ('Company 18' , GETUTCDATE() , NULL , '00000000-0000-0000-0000-000000000000' ,NULL),
           ('Company 19' , GETUTCDATE() , NULL , '00000000-0000-0000-0000-000000000000' ,NULL)
GO

-- Inserting default User
INSERT INTO [dbo].[Users]
           ([Id]
           ,[Username]
           ,[PasswordHash]
           ,[Name]
           ,[Surname]
           ,[Email]
           ,[CompanyId])
     VALUES
           ('98110625-64fe-42c2-a0c7-8465f20fffb9', 'Admin', '123', 'Carolina', 'Falcato', 'falcato.carolina@gmail.com', 1)