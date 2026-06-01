# eShop Microservices - Docker Setup

## Overview
This is a full microservices e-commerce application built with .NET 10
containerized using Docker and orchestrated with Docker Compose.

## Architecture
The application consists of the following services:

### APIs
| Service | Port | Description |
|---------|------|-------------|
| Catalog.API | 5222 | Product catalog service |
| Basket.API | 5221 | Shopping cart service |
| Ordering.API | 5224 | Order management service |
| Identity.API | 5223 | Authentication service |
| Webhooks.API | 5227 | Webhook registration service |
| WebhookClient | 5062 | Webhook client |
| OrderProcessor | 16888 | Background order processor |
| PaymentProcessor | 5226 | Payment processing service |
| WebApp | 5045 | Frontend Blazor application |

### Infrastructure
| Service | Port | Description |
|---------|------|-------------|
| PostgreSQL (catalogdb) | 5432 | Catalog database |
| PostgreSQL (orderingdb) | 5432 | Ordering database |
| PostgreSQL (identitydb) | 5432 | Identity database |
| PostgreSQL (webhooksdb) | 5432 | Webhooks database |
| Redis | 6379 | Basket cache |
| RabbitMQ | 5672 | Message broker |

## Prerequisites
- Docker
- Docker Compose

## Project Structure
eShop/
├── docker/
│    ├── catalog/Dockerfile
│    ├── basket/Dockerfile
│    ├── ordering/Dockerfile
│    ├── identity/Dockerfile
│    ├── webhooks/Dockerfile
│    ├── webhookclient/Dockerfile
│    ├── webapp/Dockerfile
│    ├── orderprocessor/Dockerfile
│    └── paymentprocessor/Dockerfile
├── src/
│    └── (application source code)
└── docker-compose.yml

## How to Run

### 1. Clone the repository
```bash
git clone <your-repo-url>
cd eShop
```

### 2. Update IP Address
In docker-compose.yml replace 13.58.87.236 with your server IP:
```bash
sed -i 's/13.58.87.236/YOUR_IP/g' docker-compose.yml
```

### 3. Start all services
```bash
docker compose up -d
```

### 4. Access the application
Open browser and go to:
http://YOUR_IP:5045

### 5. Default credentials
Username: alice
Password: Pass123$

## Dockerfile Structure
Each service uses multi-stage builds:
- Stage 1 (build): Uses SDK image to compile the app
- Stage 2 (runner): Uses ASP.NET runtime image to run the app

## Docker Compose Services
All services are connected via Docker network.
Each service has:
- Health checks for databases
- depends_on to ensure correct startup order
- Environment variables for configuration

## Known Issues
- Cart and Orders features require .NET Aspire for full functionality
- Chatbot requires OpenAI API key

## What I Learned
- Writing Dockerfiles for .NET applications
- Multi-stage Docker builds
- Docker Compose for microservices
- Service discovery and networking
- Health checks and dependencies
- Container debugging and log analysis
