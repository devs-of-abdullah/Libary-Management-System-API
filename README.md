# Smart Libary Management System

 ** Overview **

The Smart Library Management System (SLMS) is a backend API built with ASP.NET Core 8 and Entity Framework Core 8.
It manages books, users, and borrowing operations for a university library workflow.
Includes JWT authentication, role-based access, soft deletes, and late-fee tracking.

 ** Features ** 

* User Management *
Admin and Student roles
JWT-based authentication
Full CRUD operations for users

* Book Management *
Add, update, delete, and soft-delete books
Search and filter books by title, author, or category
Track availability of each book

* Borrowing System *
Borrow and return books
Automatic due-date calculation
Late fee tracking
Borrowing history log

* Admin Dashboard *
View most borrowed books
Track overdue books
Monitor active students

** Tech Stack ** 
Backend: ASP.NET Core 8 Web API
Database: EF Core 8 (Code-First) with SQL Server Provider (UseSqlServer)
4 Layer Architechure
Logging: Serilog

Repository + Service Layer Architecture
