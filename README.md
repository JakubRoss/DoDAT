# DoDAT

## Description

ToDo Application is a simple application originally built using ASP.NET in the MVC model but later refactored into an API. The frontend is now powered by **Vue.js**. The application allows users to manage their task lists, including adding, editing, and deleting tasks, as well as managing user accounts with login and registration functionality.

Although the application is API-based, **Cookie Authentication** has been implemented for authentication and authorization. While I understand that using **JWT (JSON Web Token)** is considered a better practice for APIs, Cookie Authentication was chosen for simplicity.

## Features

- Add new tasks
- Edit existing tasks
- Delete tasks
- Display a list of all tasks
- Display tasks for a specific day
- User registration
- User login
- Authentication handled with Cookie Authentication

> **Note:** The original MVC-based implementation is available on the `pixi` branch.

## Requirements

- [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQLite
- Entity Framework
- Node.js
- Vue.js CLI

## Installation

1. **Clone the repository:**

   ```bash
   git clone https://github.com/JakubRoss/DoDAT

   ```

2. **Switch to the API implementation (default branch):**

   ```bash
   git checkout main

   ```

   If you'd like to use the original MVC-based implementation, switch to the pixi branch:

   ```
   git checkout pixi
   ```

3. **Navigate to the backend application directory:**

   ```bash
   cd DoDAT
   ```

4. **Install required NuGet packages:**

   ```bash
   dotnet restore

   ```

5. **Configure the database connection:**

   Edit the appsettings.json file to configure the SQLite database connection:

   ```bash
      "ConnectionStrings": {
      "DefaultConnection": "Data Source=ToDoDb.sqlite"
      }
   ```

6. **Apply migrations:**

   ```bash
   dotnet ef database update

   ```

   This command creates the database schema.

7. **Run the backend application:**

   ```bash
   dotnet run
   ```

   The backend API will be available at http://localhost:5000.

8. **Navigate to the frontend application directory:**

   ```bash
   cd ClientApp
   ```

9. **Install Vue.js dependencies:**

   ```bash
   npm install
   ```

10. **Run the frontend application:**

```bash
npm run serve
```

The frontend will be available at http://localhost:8080.

## Images

![This is an alt text.](/ZrzutEkranu2024-12-02112334.png "This is a sample image.")
