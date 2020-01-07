# Solution structure

The solution is consisted of the following projects:
- Application
- Domain
- Infrastructure
- WebUI

### Domain
Contains entities that can be used on an enterprise-scale.

### Application
Contains all application-specific logic, entities, interfaces etc. This project heavily relies on MediatR, a library which serves as an implementation of the "mediator" pattern. Its' main goal is to decouple the processes of sending and receiving messages. 

Using it we can follow the CQS (Command Query Separation) principle.
As seen in the project, all methods are grouped according to the entites they operate on but also depending on whether they are commands or queries.

A command performs an action (usually with side-effects) while a query returns data.

### Infrastructure
The infrastructure layer is concerned with the matters of all external things, such as services or persistence.

In this solution, Infrastructure simply holds the implementation of the Persistence layer. Persistence is implemented using Entity Framework Core and Sqlite. It holds the definition of the DbContext used throughout the Application layer and also defines configurations for specified database entities.

### WebUI
This is the startup project of the solution. It holds the controllers which define API endpoints and also contains the frontend application under /ClientApp.

The frontend application is a simple SPA using Reactjs.

### Tests
The solution contains two test projects: 
  - Application.UnitTests, which test the defined commands and queries against a sample database, and
  - WebUI.Tests, which is an integration test project that interacts with the controllers in the WebUI project.