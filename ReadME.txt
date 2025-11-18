This project was developed by Abdullah(me) to showcase my skills and ability to write clean, well-structured, and maintainable backend code.

The backend is built using .NET Core (C#) with Entity Framework Core for database integration and JWT for secure user authentication.

The architecture follows a 4-tier structure for better organization and scalability:

(API -> Business -> Data -> Entity)


														** Entity Framework Commands **

To add a migration:

dotnet ef migrations add InitialCreate --project Data --startup-project API

To update the database:

dotnet ef database update --project Data --startup-project API

to drop all database 
dotnet ef database drop --project Data --startup-project API
