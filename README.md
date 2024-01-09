# Car Catalogue

### Database
Follow these steps to set up the database correctly:

 - Download **Docker**
 - Pull the SQL Server image:
	 - Run the command **docker pull mcr.microsoft.com/mssql/server:latest** in the console.
 - Run the image:
	 - Run this command in the console **docker run --name car-sql-server -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=@CarSQLPass" -p 1433:1433 -d mcr.microsoft.com/mssql/server:latest**
 - ***Run the .NET API***
 - Done!

### API
Follow these steps to run the API:
* Go to the API folder: "Car-Catalogue/Car-Catalogue.API"
* Run this command in the console **dotnet restore**
* After run the command **dotnet run**
* Done!

### Client

