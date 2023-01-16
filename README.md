# CarolinaWind

Fullstack development task project about wind developed using framework .NET 6.0.

## Summary
* Project status
* Setup
* API functionalities
* Web application functionalities

## Project status
<h4 align="center">
:construction: Project under construction :construction:
</h4>

## 📁 Setup:
<em> Having Microsoft Visual Studio&reg; and Microsoft SQL Server Management Studio&reg; installed is highly recommended. </em>

1. Download this project from GitHub.
2. Open project with Microsoft Visual Studio&reg;.
3. Open Microsoft SQL Server Management Studio&reg; and create the database using the following:
~~~
-- Creating the QEnergy database
CREATE DATABASE QEnergy;
~~~
4. In VS Package Manager Console execute the following to apply migrations.
~~~
cd QEnergy.Services.Persistence.EntityFramework
dotnet ef -v --startup-project ..\QEnergy.API database update
~~~
5. Insert *companies* from Microsoft SQL Server:
~~~
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
~~~
6. Insert *default user* from Microsoft SQL Server (for future login):
~~~
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
~~~
7. To load the projects data in the database from the Excel file, set **QEnergy.ExcelUploadApp** as Startup project in VS and run it (F5): 
    1. Choose the file "Project_view_table.xlsx". 
    2. Select the sheet in the combo.
    3. Press *Import*.
    
![image](https://user-images.githubusercontent.com/92476120/212664059-e86dc63a-b288-422f-904c-c5c81cb7f5fc.png)

8. To run the web application set **QEnergy.API** as Startup project in VS and run it (F5). The IIS Express server will be deployed in localhost:

![image](https://user-images.githubusercontent.com/92476120/212665110-00ffc397-8fe6-4743-9c1b-64fc5ca84dc3.png)

9. API endpoints are available to be tested through *Swagger* in the route */swagger/index.html*:

![screencapture-localhost-44348-swagger-index-html-2023-01-16-12_16_35](https://user-images.githubusercontent.com/92476120/212665793-04a572ba-697d-43ce-bdaf-de36343dcda5.png)


## :hammer:API functionalities

In Swagger is very easy to test the endpoints functionality clicking on **Try it out** button of each endpoint.

- `Authorization`: Get JWT authorization token using the credentials for User default as apiKey (username) and apiSecret (password)
    - Use the token with the previous word "Bearer" to get the authorization. Write it in modal shown clicking on *Authorize* button:
    
    ![image](https://user-images.githubusercontent.com/92476120/212669293-2d3fbc23-604a-4e09-9cd7-48f6e0b75fd0.png)
    
    ~~~
    {
      "apiKey": "Admin",
      "apiSecret": "123"
    }
    ~~~
    
    ![image](https://user-images.githubusercontent.com/92476120/212669470-4caed7db-4075-4078-9a54-69a7f6b21634.png)
    
    ![image](https://user-images.githubusercontent.com/92476120/212669583-3cf228d7-8123-4677-80e4-8a1ff84623f6.png)

- `GET API`: Endpoint that returns all elements of the table/database.
- `POST API`: Endpoint to add a new entry into the database.
- `PUT API`: that updates the content of an entry in the database provided its id.
- `DELETE API`: Endpoint that deletes an entry in the database provided its id.


## :hammer:Web application functionalities
Material Design Bootstrap [MDB Bootstrap](https://mdbootstrap.com/) has been used to build the front-end.
- `Login`: Login page in the route */Login/Login*

![screencapture-localhost-44348-Login-Login-2023-01-16-12_47_40](https://user-images.githubusercontent.com/92476120/212671345-46d0cafe-139c-4618-bc5e-23392c38924f.png)
- `Projects`: Projects table in the route */Projects/Projects*
    - Project deal type and status have been displayed as an icon. The meaning can be checked in the tooltip that appears on hover:
    
    ![image](https://user-images.githubusercontent.com/92476120/212673944-bb5a84e2-c10b-4f6c-b439-b43fe6bce456.png)
    
    ![image](https://user-images.githubusercontent.com/92476120/212674008-9a845558-38a1-4d89-8cd5-cc79c6d8cd19.png)

  ![screencapture-localhost-44348-Projects-List-2023-01-16-12_48_14](https://user-images.githubusercontent.com/92476120/212671455-8ec6f1ac-5862-4927-9f3e-8eaa329cfffc.png)
    
    - Delete function is available clicking on ![image](https://user-images.githubusercontent.com/92476120/212672903-cf5bad5a-5113-4935-bfed-a0d3460fe3f4.png) button. 
    - Add and Edit functions are not available at this moment. They are the following tasks on the roadmap.
    - Info about WTG can be checked clicking on ![image](https://user-images.githubusercontent.com/92476120/212673321-cac4891d-136d-465e-9239-abee88e4fbaa.png) button.
    
    ![image](https://user-images.githubusercontent.com/92476120/212674172-e23a7d40-d103-4adf-9181-43f524712e0a.png)




