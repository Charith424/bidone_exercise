MVC CRUD Project with  DI, and Unit Testing
# MVC Project with .NET Core 

This project is a Model-View-Controller (MVC) application developed using .NET Core. The primary purpose of the application is to perform CRUD (Create, Read, Update, Delete) operations for storing First Name and Last Name data in a JSON file. The project architecture follows the three-tier architecture pattern with separate layers for UI, Service, and Repository.

## Project Layers

### 1. UI Layer

The UI layer is responsible for all activities related to the user interface. It handles user interactions, displays data, and manages the presentation of the application. This layer provides a user-friendly interface for interacting with the application.

### 2. Service Layer

The Service layer is responsible for handling the core business logic of the application. It serves as an intermediary between the UI and Repository layers. This layer contains functions and logic that process data, validate inputs, and apply business rules.

### 3. Repository Layer

The Repository layer is responsible for managing data. It handles data storage, retrieval, and manipulation. In this project, it is used to interact with a JSON file to store and retrieve First Name and Last Name data.

## Dependency Injection (DI)

Dependency Injection (DI) has been implemented in the project to promote loose coupling between components. This allows for easier testing, maintainability, and scalability. DI is used to inject dependencies into various parts of the application, making it more flexible and adaptable.

## Unit Testing

To ensure the quality and reliability of the application, necessary unit tests have been added. These tests validate the functionality of the application's components, including the Service and Repository layers. Unit tests help catch and prevent bugs, ensuring that the application functions as expected.

## Project Structure

The project structure includes the following components:

- `/UI`: Contains the user interface components.
- `/Service`: Contains the core business logic and services.
- `/Repository`: Manages data storage and interaction with the JSON file.
- `/Tests`: Unit tests for the application components.

## Usage

You can use this project as a foundation for building applications that require CRUD operations with data storage in a JSON file. It is designed to be modular, maintainable, and testable, thanks to the use of dependency injection and unit tests.

## Getting Started

To run the application, follow these steps:

1. Clone the repository.
2. Build and run the project using .NET Core.
3. Access the user interface to perform CRUD operations on First Name and Last Name data.

