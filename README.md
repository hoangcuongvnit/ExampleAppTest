# 🌐 ExampleAppTest

A full-stack web application built with **.NET 8** and **Angular 19**, using **Durable Azure Functions**, **CQRS pattern (with MediatR)**, **JWT Authentication**, and **PostgreSQL** for persistence. Styled with **Tailwind CSS** and deployed with **Azure Storage**.

---

## 🔧 Tech Stack

### 🖥️ Backend (.NET 8 + Azure Functions)
- **.NET 8**
- **Azure Durable Functions**
  - `OrchestrationTrigger`
  - `HttpTrigger`
- **CQRS** pattern using **MediatR**
- **JWT Bearer Authentication**
- **PostgreSQL** (via Entity Framework or Dapper)
- **Azure Storage** (Blob/Table/Queue)

### 🌐 Frontend (Angular 19 + Tailwind)
- **Angular 19**
- **Tailwind CSS**
- **JWT token management** for secure API access

---

## 🧱 Architecture Overview

- **CQRS** separates read/write operations for better scalability and maintainability.
- **MediatR** handles command/query dispatching in the backend.
- **Durable Functions** manage complex workflows and stateful orchestrations.
- **JWT Authentication** secures API endpoints.
- **PostgreSQL** stores business data.
- **Azure Storage** supports file/blob management and queue triggers.

![Dashboard-ui](https://github.com/user-attachments/assets/39450b7f-d0cd-4709-a14e-4b4568368fea)

![log_function](https://github.com/user-attachments/assets/ecb9860b-218c-489b-8837-74c9e42e6e71)


![file-structure](https://github.com/user-attachments/assets/dc1a32eb-046a-4ea3-8b85-41b72e2b3950)

- **ApprovalWorkflow.FunctionApp/local.settings.json**
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "DefaultEndpointsProtocol=.....",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",

    // Jwt config
    "Jwt:Key": "ThisIsASecretKeyThatIsAtLeast32Chars!",
    "Jwt:Issuer": "your-api",
    "Jwt:Audience": "your-api-users",

    // Gmail config
    "Gmail:Username": "....",
    "Gmail:Password": ".....",
    "Gmail:Sender": ".......",

    "ConnectionStrings:DefaultConnection": "Host=127.0.0.1;Port=5432;Database=postgres;Username=postgres;Password=postgres"
  },
  "Host": {
    "CORS": "http://localhost:4200",
    "CORS_SUPPORT_CREDENTIALS": true
  }
}

