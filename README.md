# Task Manager Backend
## Introduction
This is a .NET controller-based web API designed to work with the Task Manager frontend, which can be found in this repository: [https://github.com/hk-15/task-manager-frontend.git](https://github.com/hk-15/task-manager-frontend.git). Users can add tasks to the database and the endpoint will return the newly created task.

## Installation Guide
* Clone this repository [https://github.com/hk-15/task-manager-backend](https://github.com/hk-15/task-manager-backend).
* Run `dotnet restore` to install all dependencies.

### Setting Up the Database
The database is PostgreSQL, so please ensure you have pgAdmin installed before continuing. See installation instructions on [the pgAdmin website](https://www.pgadmin.org/download/).
Create a new role in pgAdmin called `task_manager` with the password `task_manager` and the following privileges:
- Can login
- Create databases
- Inherit rights from the parent roles

Next, run `dotnet ef database update` to create the database and apply the migrations. You can check this has worked by refreshing the databases in pgAdmin.

## Usage
* Run `dotnet run` to start the application.
* Run `dotnet build` to build the application ready for deployment.
* Run `dotnet test` to run the tests.

Once the app is running, you can visit [http://localhost:5200/swagger/](http://localhost:5200/swagger/) to view the Swagger documentation for the API.

## API Endpoints
| HTTP Verbs | Endpoints | Action |
| --- | --- | --- |
| POST | /tasks | To add a new task |
| GET | /tasks/:id | To retrieve details of a single task |

### /tasks | POST
This endpoint expects a `CaseworkerTaskRequest` model, which has the following properties:
- title: string (REQUIRED)
- description: string (OPTIONAL)
- status: string (REQUIRED)
- dueTime: DateTime string (REQUIRED)

### /tasks/:id | GET
This endpoint expects an integer id and returns the task associated with this id in the form of `CaseworkerTaskResponse`:
- id: integer
- title: string
- description: string
- status: string 
- dueTime: string
