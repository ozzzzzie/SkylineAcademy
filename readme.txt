To view a live version of this application, please visit:

https://skylineacademy.azurewebsites.net/

----------------------------------------------------------------------------------------------------------------------------------------------------------------

To load the database on your local computer using ISS Express, please ensure that you run the sql files, then extract the database string and put it into the context.

You do this by:

1. Open both sql files and run the scripts.
2. In SQL Server Object Explorer (View > SQL Server Object Explorer), and confirm that the databases have been generated.
3. Next, in Solution Explorer (View > Solution Explorer) navigate to and open a file called 'appsettings.json'.
4. In between the curly braces that belong to connection strings, delete anything inside it and paste the following: 

    //main connection to the database
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=skylineacademy;Trusted_Connection=True;MultipleActiveResultSets=true"
    //local connection to identity database for testing purposes
    //"IdentityContextConnection": "Server=(localdb)\\mssqllocaldb;Database=SkylineAcademyIdentity;Trusted_Connection=True;MultipleActiveResultSets=true"
5. Save and run the file as normal.


