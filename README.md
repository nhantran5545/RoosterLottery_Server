# RoosterLottery

## Microsoft SQL Server Installation Instructions

Open Terminal and use [winget](https://learn.microsoft.com/en-us/windows/package-manager/winget/) to install Microsoft SQL Server by typing the command:

```shell
> winget install Microsoft.SQLServer.2022.Developer
```

(Optional) Install Microsoft SQL Server Management Studio for developing stored procedures.

```shell
> winget install Microsoft.SQLServerManagementStudio
```

Developers can backup the database and push the .bak file to the repository or use an optional migration framework.

## How to create database?
-   **Clone code** (This likely refers to downloading the codebase from a version control system like Git)
-   **Open project "RoosterLottery_Server" with Visual Studio 2019 or 2022** 
-   **Build project** (This typically involves compiling the code to create an executable or deployable artifact)
-   **Configure the Connection String** (Before running the project, make sure the connection string in the "appsettings.json" file is updated with the correct "User ID" and "Password" for your SQL Server)
-   **Tools -> NuGet Package Manager -> Package Manager Console** (This indicates using NuGet, a package manager for .NET projects, to access the Package Manager Console)
-   **Note:** Set Default project at Package Manager Console is DAL
-   **Run script:** "Add-Migration InitialCreate"; "Update-Database" 
    
## How to run project?
- Running the Application (using Ctrl + F5)
