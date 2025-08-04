## Todo Application
A full-stack Todo application built with Angular 18 and .NET 8 Web API, demonstrating modern software architecture patterns and best practices.

## 📋 Features

✅ View todo list with pagination

✅ Create new todo items

✅ Edit existing todos

✅ Delete todo items

✅ Form validation

✅ Loading states and error handling


## 🏗️ Architecture

**Backend (.NET 8 Web API)**

- Vertical Slice Architecture: Features organized by business capability

- CQRS Pattern: Clear separation between Commands and Queries

- MediatR: Request/response pipeline with cross-cutting concerns

- FluentValidation: Robust input validation

- AutoMapper: Object-to-object mapping

- In-Memory Data Storage: As per requirements

**Frontend (Angular 18)**

- Component-Based Architecture: Modular, reusable components

- Angular Material: Professional UI components

- Reactive Forms: Type-safe form handling with validation

- Services Pattern: Separation of business logic from presentation

- TypeScript Models: Strong typing throughout the application


## 🚀 Getting Started

**Prerequisites**
- Node.js 18+ and npm
- .NET 8 SDK
- Git

**Installation & Setup**
- Clone the repository
- bash git clone https://github.com/boppaiah/Todo - select branch **master**
- cd Todo

**Backend Setup**
- bash cd API/TodoAPI/TodoAPI
- dotnet restore
- dotnet run
- API will be available at http://localhost:5275
  
**Frontend Setup**
- bash cd UI/TodoApp
- npm install
- ng serve
- Application will be available at http://localhost:4200

## Running the Application

- Start the backend API first
- Start the Angular frontend
- Navigate to http://localhost:4200

## 🛠️ Tech Stack
- Angular 18
- TypeScript
- Angular Material
- Backend.NET 8
- ASP.NET Core Web API
- Validation FluentValidation (Backend)
- Angular Reactive Forms (Frontend)
- CQRS
-  Vertical Slice Architecture
-  MediatR
- Repository Pattern
-  Dependency Injection
       
## 📁 Project Structure

### Backend Architecture (TodoAPI/)
- **Todos/** - Vertical Slice Architecture
  - `CreateTodo/` - Command, Handler, Validator
  - `GetTodos/` - Query, Handler  
  - `UpdateTodo/` - Command, Handler, Validator
  - `DeleteTodo/` - Command, Handler
- **Shared/** - Common concerns
  - `CQRS/` - Base interfaces and infrastructure
  - `Exceptions/` - Custom exception classes
- **Models/** - DTOs and data transfer objects


### Frontend Structure (UI/TodoApp/)
- **src/app/**
  - `components/` - Angular components (todo-list, todo-form)
  - `services/` - HTTP services and business logic
  - `models/` - TypeScript interfaces and types


## 🎯 Key Implementation Highlights
### Backend Architecture Decisions
- **Vertical Slices**: Each feature is self-contained with its own command/query, handler, and validation
- **CQRS**: Separate models for read and write operations for better scalability
- **MediatR Pipeline**: Centralized request handling with cross-cutting concerns
- **Custom Exceptions**: Proper error handling with meaningful messages

### Frontend Best Practices
- **Standalone Components**: Modern Angular architecture
- **Reactive Forms**: Type-safe form handling with custom validators
- **Service Abstraction**: Clean separation between UI and business logic
- **Material Design**: Consistent, accessible UI components
- **Error Handling**: User-friendly error messages and loading states

## 🧪 Testing Strategy
The application includes:

Unit tests for Command/Query handlers
Component tests for Angular components
Form validation tests

## 🔧 Development Practices

EditorConfig: Consistent code formatting
TypeScript: Strong typing throughout
Git Flow: Feature branches with meaningful commits
Code Organization: Clear separation of concerns
Error Handling: Comprehensive error management

## 📝 API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/v1/todo/items` | Get all todos with pagination |
| POST | `/api/v1/todo` | Create a new todo |
| PUT | `/api/v1/todo` | Update an existing todo |
| DELETE | `/api/todo?id=` | Delete a todo |


## 🚀 Production Considerations

For a production deployment, consider:
- Database integration (SQL Server, PostgreSQL)
- Authentication and authorization
- API versioning
- Logging and monitoring
- Unit and integration test coverage
- CI/CD pipeline
- Docker containerization
- Environment-specific configurations

## 🔍 Design Decisions

**Why Vertical Slice Architecture?**
- Each feature is self-contained and easy to modify
- Reduces coupling between features
- Makes testing easier
- Scales well as the application grows

**Why CQRS?**
- Clear separation between read and write operations
- Better performance optimization opportunities
- Easier to reason about data flow

**Why Angular Material?**
- Professional, accessible UI components
- Consistent design system
- Mobile-responsive out of the box
- Well-maintained and documented

📞 Contact
[Boppaiah] - [boppaiah1993@gmail.com]
[(https://www.linkedin.com/in/boppaiah-subbaiah/)]
