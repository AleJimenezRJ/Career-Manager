# Career Manager

**Developers**: 

- Alejandro Jiménez Rojas
- Felipe Quesada Parada

## Repository

This project was done for the Software Engineering Course of the University of Costa Rica (UCR), specifically for the technical evaluations 1 and 2. Previously implemented in a private repository but public now for personal display.

## Design

### Class Diagram

The class diagram is a representation of the classes and their relationships in the system. It provides an overview of the structure and organization of the code. It is not exhaustive, but it does provide a good overview of the main classes and their relationships.

![Class Diagram](Docs/class-diagram-updated.svg)

## Provided services

- Add a career
- List a career
- Search a career either by name or content\
- Calculate the scholarship of the career

## Use

- To use it, it's necessary to publish the database project, configure the connection string to the database, and run the solution in Visual Studio. This should open swagger and the UI to test the implemented services.
- In case it's required, the `list-careers` service provides examples of the expected data by the application. Same for the other services, they provide examples of the expected data in the responses since the database was populated with some data in a postdeployment script. There's complete error management in the services, so if an error occurs, it will return a proper error message with the status code.

## Analysis of implementation

The application architecture is based on the clean architecture principles. It's based on the idea of organizing the code in layers, where each layer has a specific responsibility and is independent of the others. Below is a brief overview:

**Core Principles**

- Dependency Inversion: Business logic does not depend on infrastructure or external systems. Domain doesn't depend on any other layer or service.
- Domain-Driven Design (DDD): Focuses on organizing software around the business domain. In the solution the domain contains entities and core business rules.
- Abstractions at the Center: Dependencies point towards the core using interfaces. This was how I implemented it in the solution.

**Layers**

- Domain Layer: Contains entities and core business rules.
- Application Layer: Implements workflows, use cases, and application-specific logic.
- Infrastructure Layer: Handles external concerns like data access and APIs.
- Presentation Layer: Manages user interactions.

**S.O.L.I.D Principles**

- Single Responsibility: Each class has a single reason to change. For example: `EntityName` solely manages validation and encapsulation of the name.
- Open/Closed: Entities are open for extension but closed for modification. For example: `Error` class and `Errors` static factory allow adding new error types without modifying existing logic.
- Liskov Substitution: Subclasses are able to replace their base classes without altering the program's functionality. For example: `ValidationResult` inherits from `Result` and can be returned in its place.
- Interface Segregation: Clients are not forced to depend on interfaces they don't use.
- Dependency Inversion: High-level modules don't depend on low-level modules. Both depend on abstractions. For example: Handlers depend on the repository, not on concrete implementations.

**Good Practices**

- Consistent naming conventions (e.g., PascalCase for classes, camelCase for variables).
- Use of interfaces (I prefix) and private fields (_ prefix).
- To assist in the development of clean code, **SonarLint** tools were used to analyze the code and ensure adherence to best practices.
- Regular use of comments and XML documentation to explain the purpose and functionality of classes and methods.
- Regular commits to the repository to track progress and changes, following a clear commit message structure.
- Pull Requests (PRs) were created once important code was finished, this to ensure code quality, maintainability, and keep the main branch updated.
- Use of the Result Pattern to handle errors and results in a consistent way. This pattern was used to encapsulate the result of operations, allowing for better error handling and separation of concerns. The Result Pattern is implemented in the `Result` class and its subclasses, such as `ValidationResult` and `Error`.

## Definition of Done (DoD)

A user story is considered “Done” when it meets all the established criteria. The criteria include:

* **Completed and approved acceptance criteria**, written in Gherkin format and covering positive, negative, and edge case scenarios. Stories must follow the INVEST principles.
* **Executed and validated tests**, program services were manually tested using Swagger.
* **Updated documentation**, including a description of the implemented functionality.
* **Updated backlog status**, with changes reflected in the corresponding issues.

## Non-Functional Requirements (NFRs)

| Category         | Quantification                                                                                      | Justification|
|------------------|--------------------------------------------------------------------------------------------------|------------------------------------------------|
| **Maintainability** | The codebase should allow a developer to implement a small feature or fix a bug in less than 2 hours | Reduces long-term technical debt and allows for faster iterations and updates. |
| **Usability**       | A new user should be able to perform a basic API request using Swagger in under 3 minutes. | Enhances experience and accelerates onboarding and integration. |
| **Reliability**     | The system must successfully store data with a success rate of at least 99.9% | Ensures data persistence, which is critical for maintaining trust and functionality. |

## Tests

In the pursuit of correct software engineering practices, we implemented unit and integration tests which were carried out under our test design process.
We decided to focus on testing the projects most critical components which under our DDDesign, were our business entities. Opportunity, Industry, WorkLife, Recruitment, and Enterprise were all successfully tested reaching acceptable levels of coverage.
After domain tests, we carried out a solid testing phase for our application layer which includes our entity services relevant in our career context.
These services encapsulate workflows such as retrieving, validating, and adding domain-specific information.
We applied unit testing strategies using mocking tools like Moq to isolate the service logic from external dependencies, allowing us to focus solely on business rules and interaction contracts. Each service method was tested against both expected success paths and failure scenarios, including repository rejections and invalid inputs.

In addition to said unit tests, we developed integration tests for our infrastructure and application layers. 
For the infrastructure layer tests, these tests validated that each SqlRepository implementation correctly interacted with the actual database using VRCampusDatabaseContext.
The integration tests ensured that the repositories could perform CRUD operations as expected, confirming that the database schema and entity mappings were correctly configured.
The application layer tests focused on the services integration with the infrastructure layer, ensuring that they correctly orchestrated the interactions between them.
For the infrastructure layer, we also designed unit tests for repository implementations such as SqlCareerRepository, using MockQueryable and FluentAssertions to simulate realistic database interactions.

- Statement and branch coverage was mostly achieved by testing both positive and negative outcomes of services.
- Path coverage was partially achieved by identifying critical paths through the codebase and designing tests to execute them.

### Recommended code coverage in tests

Since it's a standard practice to have a minimum of 80% code coverage in tests, we aimed for this target. However, we were not able to achieve this. 
The focus should be on covering critical paths and ensuring that the most important functionalities are tested thoroughly.
We do understand that a coverage of 85% or higher is ideal, as this would ensure that most of the code is tested and that the application is robust and reliable.

### Used extension for test coverage

- [Coverlet](https://learn.microsoft.com/es-es/dotnet/core/testing/unit-testing-code-coverage?tabs=windows)

We used the Coverlet extension to measure the test coverage of our code. It was installed via the Nuget Package Manager. It can be checked by using the following commands in the terminal:

Inside the solution directory, run the following command to execute the tests and collect coverage data:

```bash
dotnet test --collect:"XPlat Code Coverage" 
```

To view the coverage report, you can use the following command to install the tool:

```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
```

After installing the tool, you can generate the report by running the following command:
```bash
reportgenerator -reports:**/coverage.cobertura.xml -targetdir:coveragereport
```

## Future improvements

- **UI Tests**: Implement UI tests to ensure that the user interface behaves as expected and that the user experience is smooth.
- **Error Handling**: Improve error handling in the UI to provide more detailed and user-friendly error messages.
- **Code Coverage**: Increase code coverage in tests to ensure that all critical paths are tested and that the application is robust and reliable.
- **UI Design**: Improve the UI design to make it more user-friendly and visually appealing. This includes better layout, styling, and responsiveness.
- **Integrate work information features**: Implement features related to work information, such as adding work information for all the derived entities, such as `Opportunity`, `Industry`, `WorkLife`, `Recruitment`, and `Enterprise`. This will allow users to manage work-related data more effectively.

## Contributions

| Student                 | Value |
|-------------------------|-------|
| Alejandro Jimenez Rojas | 58%   |
| Felipe Quesada Parada   | 42%   |


## Bibliography

1. Use of Result Pattern - Karan Raj - https://github.com/karanraj-tech/result-pattern
2. Course material - Erik Kuhlmann and Cristian Quesada - https://github.com/UCR-PI-IS/git-workshop-ekuhlmann23-1 and https://mv.mediacionvirtual.ucr.ac.cr/course/view.php?id=570
3. ThemePark@UCR project - G1 - https://github.com/UCR-PI-IS/ecci_ci0128_i2025_g01_pi
4. Visitor pattern - Daniel Azevedo - https://dev.to/dazevedo/understanding-the-visitor-pattern-in-c-1plg

## Contact

For questions about use, implementation, errors, or any related matter. Please contact us via:

- E-mail: alejimenezrj@gmail.com
- Telegram: @aleJR125
- GitHub: [AleJimenezRJ](https://github.com/AleJimenezRJ)
- E-mail: felipe.quesadaparada@ucr.ac.cr
- Telegram: @felipequesada21
- GitHub: [Felipequesada21](https://github.com/Felipequesada21)
