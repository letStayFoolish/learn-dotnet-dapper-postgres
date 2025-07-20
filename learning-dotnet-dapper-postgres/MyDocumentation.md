# .NET 9.0 + Dapper API Code Documentation
## Controllers
Define the end points / routes for the web api, controllers are the entry point into the web api from client applications via http requests.

## Models
Represent request and response models for controller methods, request models define parameters for incoming requests and response models define custom data returned in responses when required. The example only contains request models because it doesn't contain any routes that require custom response models, entities are returned directly by the user GET routes.

## Services
Contain business logic and validation code, services are the interface between controllers and repositories for performing actions or retrieving data.

## Repositories
Contain database access code and SQL queries.

## Entities
Represent the application data that is stored in the database.
Dapper maps relational data from the database to instances of C# entity objects to be used within the application for data management and CRUD operations.

## Helpers
Anything that doesn't fit into the above folders.

