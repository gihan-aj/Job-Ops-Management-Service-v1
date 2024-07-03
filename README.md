# Job Operation Management System
This ASP.NET Web API project is designed for managing operations in a manufacturing plant. The initial focus is on the master data module, where entities such as department, section, machines, employee, and job roles are defined. The database is created using SQL Server and EF Core, with completed operations for the department entity.

## What I learned
* **ASP.NET Web API:** Developed RESTful APIs for managing entities.
* **Entity Framework Core:** Used EF Core for database operations and relationships.
* **Soft Delete and Auditing:** Implemented soft delete and auditing for data changes.
* **Pagination:** Added pagination to improve data retrieval performance.
* **Logging:** Integrated custom logger library for recording activities.

## Features
* **Master Data Module:** Define and manage entities like
  * Department,
  * Section,
  * Machine,
  * Employee, and 
  * Job roles.
* **Database Relationships:** Configured one-to-many and many-to-many relationships between tables.
* **Soft Delete:** Implemented soft delete for data with a user ID and timestamp recording.
* **Pagination:** Added pagination for GET methods to avoid retrieving all data at once.
* **CRUD Operations:** Functionality for
  * Getting a page size,
  * Searching by name or id,
  * Getting by ID,
  * Adding, updating,
  * Soft deleting, and
  * Activating/deactivating entities.
* **Logging:** Integrated custom logger library to record activities.

## Installation
1. Clone the repository.
2. Update the connection string in `appsettings.json` to point to your SQL Server instance.
3. Run database migrations:
   ```
   dotnet ef database update
   ```
4. Run the application:
   ```
   dotnet run
   ```

## Usage
### Logging
* Integrated custom logger library to log information, warnings, and errors.
* Example usage:
  ```C#
  _logger.LogInfo("Department created successfully.");
  ```

### Soft Delete and Auditing
* Soft delete functionality to mark records as deleted without physically removing them.
* Records who created, updated, or deleted an entity with timestamps.

### Pagination
* Added pagination to GET methods to improve performance by not retrieving all data at once.
* Example usage:
  ```http
  GET /api/departments?pageNumber=1&pageSize=10
  ```
