## Migrations

## Creating Migration Scripts
 In Visual Studio, Click on the Tools -> Nuget Package Manager -> Package Manager Console
 First migration run the following


Add-Migration InitialMigration


Verify migrations scripts are run successfully and Migrations folder is created




## Database Migrations

The Data folder contains all the required migrations to build out the database. Open up the Package Manager Console by clicking on Tools -> NuGet Package Manager -> Package Manager Console.    Run the following command:

```shell
Update-Database
```

It should output that the following:

```shell
Build started...
Build succeeded.
```

You can also verify that all the tables are created by opening up the SQL Server Object Explorer in Visual Studio. To view it, Click on View -> SQL Server Object Explorer. When in view you will see a SQL Server tree with a (localdb). Expand this and then expand Databases. The database is called movieuidatabase. Again, if you dont see it, click the refresh button in the SQL Server Object Window - Blue circle with Arrow

This has created the database with all the required tables. 
