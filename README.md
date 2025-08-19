# ToDo
This is a simple to-do list application built with ASP.NET Core 8.0, Entity Framework Core, and PostgreSQL. It allows users to create, read, update, and delete tasks.







## What to do when cloning this repository
1. Make sure the following prerequisites are met:
    1. https://www.postgresql.org/download/
    2. PostGresQL is installed and running (Currently version 17).
    3. The connection string in the appsettings.json file is correct, or the user secrets are set up correctly.
        -  dotnet user-secrets init
        -  dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=Todo;Username=postgres;Password=[[yourpassword]]"
2. Run the Entity Framework migrations:
    1. In the package manager run 'dotnet ef database update'.
3. Obviously the data won't be transfered but the database will be seeded with test data at runtime.