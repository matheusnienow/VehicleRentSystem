# VehicleRentSystem
Final project for Advanced Programming with C# at University West.

This solution is separated in different projects as explained below.

# General structure of the system
The Website is used by the customer that makes a reservation for a vehicle rental. The Website access the VRS.Logic's controllers for each business action. The VRS.Logic project accesses the VRS.Repository if a CRUD action is needded from the database. While in the background the VRS.Service keeps inserting new vehicle models in the database and the VRS.WebApi exposes some of the business logic from the VRS.Logic's controllers.

# VRS.Logic
All the business logic is contained here in the controllers of each entity. If additional logic needs to be implemented, it should also be here. The "SecurityHelper" class provides utility methods for hashin in the login process.

# VRS.Repository
This project abstract the access to the database using the entity framework. All of the database access should use this project's classes and all of the future implementation regarding the database should be done here.

There is a generic repository that can be used to access the database of all models that inherits from the class "BaseEntity", it provides the CRUD methods and also some methods that receive custom parameters. If more in-depth or customized manipulation of data should be made, the creation of a model specific repository is recommended.

# VRS.Service
This service is responsible for the inserting new vehicle models in the database. Right now it is reading a XML file in the following path: C:\temp\VehicleModelSource.xml. This service should be extended to handle data for other tables as well.

# VRS.WebApi
This project exposes the logic from the VRS.Logic project to be used by the admin app (https://github.com/matheusnienow/VRS.AdminApp). All the access to login and repository from the admin app should use this api.

# VRS.WebSite
This is the website used by the user to make reservation of vehicle rentals. The system uses a secure login system that hashes the password and verify the hash using the user's salt each time a login is made.
