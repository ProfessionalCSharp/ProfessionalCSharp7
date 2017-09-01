# Readme - Code Samples for Chapter 38, Entity Framework Core

The sample code for this chapter contains this solution:

* EntityFrameworkSamples

consisting of these sample projects

* BooksSample (simple sample to read, write, update, and delete records)
* BooksSampleWithDI (using dependency injection to configure the DbContext)
* MenusSample (migrations using .NET CLI tools, showing relations, object tracking)
* MenusWithDataAnntations (using data annotations instead of the fluent API)
* MenusSampleWithMSBuild (migrations using MSBuild instead of the CLI tools)
* ConflictHandlingSample (conflict handling)
* TransactionsSample (explicit transactions)

Most of the projects of this solution are .NET Core projects. The only project using the full framework is MenusSampleMSBuild to show using the MSBuild based migration commands.

The database that is used with many of these samples is the Books database. Look for the backup file Books.bak. You can use this file to restore the Books database using SQL Server Management Studio.
The Menus database that is also used in some samples is created using the Migrations feature of Entity Framework.

To build and run the .NET Core samples, please install
* Visual Studio 2017 with the .NET Core workload

Please download and install the tools from [.NET Core downloads](https://www.microsoft.com/net/core).
 
For code comments and issues please check [Professional C#'s GitHub Repository](https://github.com/ProfessionalCSharp/ProfessionalCSharp6)

Please check my blog [csharp.christiannagel.com](https://csharp.christiannagel.com "csharp.christiannagel.com") for additional information for topics covered in the book.

Thank you!