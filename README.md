# CarpoolManagementCA

## Requires:
  - .NET Core 3.1
  - npm
  
## Build
If you wish to build the project, you can do so following these steps:
  1. Clone this repository
  2. Navigate to the root of this project, where the solution file is located, and run:
      ```
      dotnet restore
      ```
  3. After restoring the packages, build the project by running:
      ```
      dotnet build
      ```
  4. Now you can start the backend by navigating to `./src/WebUI/` and running:
      ```
      dotnet run
      ```
  5. Frontend can be started by navigating to `./src/WebUI/ClientApp/` and running:
      ```
      npm start
      ```
  
## Publish
The project can be published using `dotnet` while targeting the WebUI project. (Works using only binaries, too).

```
dotnet publish -c Release ./src/WebUI/
```

This will build the .NET project and run `npm` to install dependencies needed for the frontend. It will also make a static build of the frontend.
By default, the project should be published to a path similar to this one:
`./src/bin/Release/netcoreapp3.1/publish`

From here, you can again use `dotnet` to run the .NET project:
```
dotnet run ./src/WebUI/bin/Release/netcoreapp3.1/publish/WebUI.dll
```
By default the project should be hosted on `https://localhost:5001` and `http://localhost:5000`.

The frontend can be served by any simple http server. Upon completing the project build, you are given an example of using a tool `serve` which is installed via `npm`.
```
npm install -g serve
serve -s -l 3000 ./CarpoolManagementCA/src/WebUI/bin/Release/netcoreapp3.1/publish/ClientApp/build
```

### Exposed endpoints include:
- `/api/rideshares`
  - GET - gets a list of all rideshares
  - POST - creates a new rideshare
- '/api/rideshares/{id}`
  - GET - gets a rideshare detailed info by ID
  - PUT - updates a rideshare by ID
  - DELETE - deletes a rideshare by ID
  
- `/api/cars`
  - GET - gets a list of all cars
- `/api/cars/{id}` 
  - GET - gets a car detailed info by ID
  
- `/api/employees`
  - GET - gets a list of all employees
- `/api/employees/{id}`
  - GET - gets an employee detailed info by ID
  
### [Što je moglo bolje...](./possibleImprovements.md)
