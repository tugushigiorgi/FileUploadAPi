# FileUploadAPi

# File Upload API with JWT Authentication

This ASP.NET Core Web API project provides endpoints to upload and manage files, along with JWT authentication for secure access.

## Table of Contents

1. [Requirements](#requirements)
2. [Database Setup](#database-setup)


## Requirements

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Postman](https://www.postman.com/downloads/) or a similar tool for API testing

## Database Setup
- Update the connection string in appsettings.json to point to your preferred database (e.g., SQL Server, SQLite).
- Run the following commands to create and apply the database migrations:
- dotnet ef migrations add init
- dotnet ef database update
- 
## IMPORTANT
-- update appsettings.json FileUploadURL , this is where uploaded files will be saved

## INCLUDES SWAGGER
 








