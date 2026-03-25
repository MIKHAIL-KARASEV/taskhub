# TaskHub Architecture

## Overview

TaskHub is a monorepo project that contains both frontend and backend applications.

The architecture is designed to ensure:

* Scalability
* Maintainability
* Clear separation of concerns
* Testability
* Independence of business logic from infrastructure

---

## Core Principles

* **Clean Architecture** — business logic is isolated from frameworks and external dependencies
* **Separation of Concerns** — each layer has a single responsibility
* **Dependency Rule** — dependencies point inward (toward Domain)
* **CQRS (partial)** — separation of read and write operations where it adds value
* **Explicit boundaries** — communication only through defined interfaces

---

## Backend Architecture

The backend follows Clean Architecture with partial CQRS implementation.

### Layers

#### 1. Domain (Core)

**Responsibility:**

* Contains business logic and rules

**Includes:**

* Entities
* Value Objects
* Domain Services (if needed)

**Rules:**

* No dependencies on other layers
* Pure business logic only

---

#### 2. Application

**Responsibility:**

* Orchestrates business use cases

**Includes:**

* Commands (write operations)
* Queries (read operations)
* Handlers (CQRS)
* DTOs
* Interfaces (repositories, services)

**Rules:**

* Depends only on Domain
* No infrastructure code

---

#### 3. Infrastructure

**Responsibility:**

* Implements external concerns

**Includes:**

* Database access (EF Core)
* Repository implementations
* External API integrations
* File storage, email, etc.

**Rules:**

* Depends on Application and Domain
* Implements interfaces defined in Application

---

#### 4. WebAPI

**Responsibility:**

* Entry point of the system

**Includes:**

* Controllers
* Middleware
* Dependency Injection configuration
* Request/response mapping

**Rules:**

* Depends only on Application
* No business logic

---

## Dependency Rules

Strict dependency direction:

* Domain → no dependencies
* Application → Domain
* Infrastructure → Application + Domain
* WebAPI → Application

> ❗ All dependencies must point inward toward the Domain layer.

---

## Data Flow (Simplified)

1. Client → WebAPI (HTTP request)
2. Controller → Application (Command/Query)
3. Application → Domain (business logic)
4. Application → Infrastructure (via interfaces)
5. Response → Client

---

## Frontend Architecture

The frontend uses **Feature-Sliced Design (FSD)**.

### Layers

* **app** — application bootstrap, providers, global config
* **processes** — complex business flows (optional)
* **pages** — route-level components
* **features** — user actions (create task, update profile, etc.)
* **entities** — business entities (Task, User)
* **shared** — reusable UI components, utilities, API clients

### Principles

* Feature isolation
* Reusability
* Minimal cross-layer coupling

---

## Communication

* **REST API (HTTP/JSON)** — main communication
* **SignalR** — real-time updates and notifications

---

## Cross-Cutting Concerns

Handled via middleware or infrastructure:

* Logging
* Validation
* Error handling
* Authentication / Authorization
* Caching

---

## Future Improvements

### Backend

* Full CQRS with MediatR
* Event-driven architecture (RabbitMQ or Kafka)
* Caching layer (Redis)
* Authentication & Authorization (JWT, RBAC)
* Observability:

    * Logging
    * Metrics
    * Distributed tracing

### Frontend

* State management standardization
* Improved code splitting
* Design system

---

## Summary

This architecture ensures:

* Independent business logic
* High testability
* Flexibility to change infrastructure
* Clear scaling path (CQRS, events, microservices)

The system is designed to evolve without breaking core business rules.
