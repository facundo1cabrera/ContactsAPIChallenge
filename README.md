# Contacts API

## Description

This project is a RESTful API that allows users to manage contact records. The API provides endpoints for creating, retrieving, updating, and deleting contact records. It also allows users to search for records by email or phone number and retrieve all records from the same state or city.

The contact record consists of the following information: name, company, profile image, email, birthdate, phone number (work, personal), and address.

The project incorporates the following requirements:

- Implementation of at least one design pattern.
- Application of SOLID principles.
- Unit tests for at least one of the endpoints.

## Installation

To run the project locally, follow these steps:

1. Clone the repository from GitHub.
2. Ensure that Docker is installed on your computer.
3. Open a terminal and navigate to the project directory.
4. Run the following command to start the project:

   ```bash
   docker-compose up
   ```

   This command will build and run the Docker containers required for the API.

5. Once the containers are up and running, you can access the API using the specified endpoints.

## Usage

The API exposes the following endpoints:

- `POST /contacts` - Create a new contact record. The request should include the contact details in the request body.
- `GET /contacts/{id}` - Retrieve a contact record by its ID.
- `PUT /contacts/{id}` - Update an existing contact record with new information. The request should include the updated contact details in the request body.
- `DELETE /contacts/{id}` - Delete a contact record by its ID.
- `GET /contacts/email/{email}` - Retrieve a contact record by its Email.
- `GET /contacts/email/{phoneNumber}` - Retrieve a contact record by either its personal phone number or its work phone number.
- `GET /contacts/state/{state}` - Retrieve all contact records from the same state.
- `GET /contacts/city/{city}` - Retrieve all contact records from the same city.

Replace `{id}`, `{state}`, `{email}`, `{phoneNumber}` and `{city}` with the respective values in the actual requests.

## Design Patterns

The project incorporates Repository pattern design pattern. This design pattern was chosen to address the 
coupling between the business layer and the persistance layer, and because the project also follows the Dependency Inversion Principle,
we have much less coupling and a codebase that is easier to maintain.

This project is built on top of .NET 6 which uses a los of decorators (or attributes depending on what you call them) for endpoints 
definitions, for routing behavior or for model validations. Those are implementations of the decorator pattern.


## SOLID Principles

The project adheres to the SOLID principles, which are fundamental principles for writing maintainable and extensible software. These principles ensure that the codebase is flexible, testable, and easy to understand and maintain. The SOLID principles applied in this project are:

1. Single Responsibility Principle (SRP)
2. Open/Closed Principle (OCP)
3. Liskov Substitution Principle (LSP)
4. Interface Segregation Principle (ISP)
5. Dependency Inversion Principle (DIP)

The adherence to these principles can be observed throughout the codebase.

## Testing

The project includes integration tests made with XUnit. The test suite can be found in ContactsAPI.Tests. These tests verify the functionality and behavior of the endpoints, ensuring that they produce the expected results and handle various scenarios correctly.
Since we are using dependency injection it is easy to set up mocks of the dependencies (we are using Moq) and own implementations, there are some tests that use repositories mocks and some use the Entity Framework on memory provider, probably in the future would be nice to use a real test Database for the tests.
This upgrade can be easily done by changing the CreateContext function from the BaseTests class of the ContactsAPI.Tests project.

To run the tests, execute the following command:

```bash
[dotnet test]
```

Make sure that the project dependencies are installed before running the tests.

## Environment variables

Since this is just a challenge and it doesn't have any intention (at least for now) of being deployed to a production environment, it was easier to set up the project with hardcoded connection strings in the appsettings.{env}.json files. A good approach for improving this could be to use application secrets to override the connection strings, making the code changes close to zero, or we could get the values via Environment variables and change the code that uses the connection string from the appsettings to a function that creates a connection string with the env variables.

## Architecture/Design

![image](https://github.com/facundo1cabrera/ContactsAPIChallenge/assets/83284235/6b3f92ce-e485-44ad-9aae-e7327562daf4)

## Contributing

Contributions are welcome! If you encounter any issues or have suggestions for improvements, please open an issue or submit a pull request.
