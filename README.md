
# Ambev.DeveloperEvaluation

[![.NET](https://img.shields.io/badge/.NET-8.0-blue)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![Redis](https://img.shields.io/badge/Redis-Cache-red)](https://redis.io/)
[![Docker](https://img.shields.io/badge/Docker-Compose-blue)](https://docs.docker.com/compose/)

Enterprise-grade .NET 8 application with Redis caching, event-driven architecture, and comprehensive testing suite.

## Table of Contents
- [Features](#features)
- [Prerequisites](#prerequisites)
- [How to Run](#how-to-run)
- [API Documentation](#api-documentation)
- [Tests](#tests)
- [Event Architecture](#event-architecture)
- [Upcoming Features](#upcoming-features)

## Features
- **Redis Output Caching**: All endpoints cached
- **Event-Driven Architecture**:
  - `UserRegistered` - User created
  - `SaleCreated` - Sale created
  - `SaleModified` - Sale modifications
  - `SaleCancelled` - Sale cancellations
  - `ItemCancelled` - Item cancellations
- **Containerized Environment**: Full Docker support
- **Testing Suite**:
  - Unit tests (`Ambev.DeveloperEvaluation.Unit`)
  - Functional tests with Docker (`Ambev.DeveloperEvaluation.Functional`)

## Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Redis 7+](https://redis.io/download) (optional - included in Docker Compose)

## How to Run

### Docker Compose (Production-like)
```bash
docker-compose up --build -d
```
Access: `http://localhost:8080`

### Local Development
```bash
dotnet run --project .src/Ambev.DeveloperEvaluation.WebApi
```
Requires:
- Redis running on `localhost:6379`
- Postgres Server on `localhost:5432`

### Test Projects
```bash
# Unit tests
dotnet test Tests/Ambev.DeveloperEvaluation.Unit

# Functional tests (starts test containers)
dotnet test Tests/Ambev.DeveloperEvaluation.Functional
```
## API Documentation

### Swagger UI
Available at `/swagger` when running:
- Docker: `https://localhost:8081/swagger` |  `http://localhost:8081/swagger` 
- Local: `https://localhost:7181/swagger`  |  `http://localhost:5119/swagger`

## API Endpoints

### Authentication
| Endpoint          | Method | Action                          |
|-------------------|--------|---------------------------------|
| `/api/Auth`       | POST   | Authenticate user and get token |

### Cart Items
| Endpoint                     | Method | Action                          |
|------------------------------|--------|---------------------------------|
| `/api/CartItems/{id}`        | DELETE | Remove item from cart           |
| `/api/CartItems/add`         | POST   | Add item to cart                |
| `/api/CartItems/decrease`    | POST   | Decrease item quantity          |

### Carts
| Endpoint                     | Method | Action                          |
|------------------------------|--------|---------------------------------|
| `/api/Carts/create`          | POST   | Create new cart                 |
| `/api/Carts/{id}`            | GET    | Get cart details                |
| `/api/Carts/cancel/{id}`     | POST   | Cancel cart                     |
| `/api/Carts/close/{id}`      | POST   | Close cart                      |
| `/api/Carts/update`          | PUT    | Update cart                     |

### Products
| Endpoint                     | Method | Action                          |
|------------------------------|--------|---------------------------------|
| `/api/Products`              | POST   | Create new product              |
| `/api/Products/{id}`         | GET    | Get product details             |
| `/api/Products/{id}`         | DELETE | Delete product                  |
| `/api/Products/all`          | GET    | List all products               |

### Users
| Endpoint                     | Method | Action                          |
|------------------------------|--------|---------------------------------|
| `/api/Users`                 | POST   | Create new user                 |
| `/api/Users/{id}`            | GET    | Get user details                |
| `/api/Users/{id}`            | DELETE | Delete user                     |

> All endpoints are protected with JWT authentication except `/api/Auth`
> Cached endpoints include `X-Cached` header

## Upcoming Features

### Immediate Roadmap
```markdown
- [ ] Complete integration test suite
- [ ] Complete functional test suite
- [ ] Implement nosql databases for:
  - [ ] Full-text search
  - [ ] Advanced filtering
```
