# School Management System 🎓

![License](https://img.shields.io/badge/license-MIT-blue.svg)
![Frontend](https://img.shields.io/badge/frontend-Next.js%2016-black)
![Backend](https://img.shields.io/badge/backend-.NET%208%2B-purple)
![Architecture](https://img.shields.io/badge/architecture-Clean%20Architecture-brightgreen)

A modern, full-stack School Management System built with a powerful **.NET Web API** backend utilizing Clean Architecture principles, and a lightning-fast, premium **Next.js** frontend dashboard.

This application provides a comprehensive suite of tools to manage students, teachers, parents, classes, attendance, and exams in one centralized platform.

## ✨ Features

- **🎓 Student Management**: Add, update, view, and manage student profiles and their data.
- **👩‍🏫 Teacher Management**: Keep track of teaching staff and their assigned roles.
- **👪 Parent Portal**: Manage parent data and their relationships with students.
- **🏫 Class & Course Organization**: Structure the academic year with standardized classes and subjects.
- **📅 Attendance Tracking**: Easy-to-use interface for logging and monitoring daily attendance.
- **📝 Exam Management**: Schedule exams, input scores, and track overall academic performance.
- **🔐 Secure Authentication**: JWT-based secure login and direct cookie-management.
- **🎨 Premium UI/UX**: Designed using modern Web Aesthetics, Glassmorphism, and Tailwind CSS.

## 🛠️ Technology Stack

### Frontend
- **Framework**: [Next.js 16](https://nextjs.org/)
- **UI Library**: [React 19](https://react.dev/)
- **Styling**: [Tailwind CSS v4](https://tailwindcss.com/)
- **State Management**: [Zustand](https://zustand-demo.pmnd.rs/) (Client State), [React Query](https://tanstack.com/query/latest) (Server State)
- **HTTP Client**: [Axios](https://axios-http.com/)
- **Icons**: [Lucide React](https://lucide.dev/)

### Backend
- **Framework**: .NET Web API
- **Architecture**: Domain-Driven Design (DDD), Clean Architecture, CQRS Pattern
- **Database Access**: Entity Framework Core
- **Patterns**: Repository Pattern, Result Pattern for robust error handling

## 📂 Project Structure

The project is structured into two main directories:

```
📦 SchoolManagement
 ┣ 📂 frontend/                  # Next.js Application
 ┃  ┣ 📂 app/                    # App Router and Pages
 ┃  ┣ 📂 components/             # Reusable UI Components
 ┃  ┣ 📂 features/               # Feature-sliced modules (Exams, Attendance, etc.)
 ┃  ┗ 📜 package.json
 ┣ 📂 StudentManagement.Api      # Presentation Layer (Controllers, Endpoints)
 ┣ 📂 StudentManagement.Application # Use Cases, CQRS handlers, Interfaces
 ┣ 📂 StudentManagement.Domain   # Entities, Value Objects, Domain Events
 ┣ 📂 StudentManagement.Infrastructure # EF Core DB Context, Repositories, External Services
 ┗ 📜 StudentManagement.slnx     # Backend Solution File
```

## 🚀 Getting Started

### Prerequisites
- [Node.js](https://nodejs.org/) (v20+ recommended)
- [.NET SDK](https://dotnet.microsoft.com/download) (v8+)
- Database Server (SQL Server / PostgreSQL depending on configuration)

### Backend Setup

1. Open a terminal in the root directory.
2. Navigate to the API folder:
   ```bash
   cd StudentManagement.Api
   ```
3. Update connection strings in `appsettings.json`.
4. Apply Entity Framework migrations to your database:
   ```bash
   dotnet ef database update --project ../StudentManagement.Infrastructure --startup-project .
   ```
5. Run the API:
   ```bash
   dotnet run
   ```

### Frontend Setup

1. Open a new terminal instance.
2. Navigate to the frontend directory:
   ```bash
   cd frontend
   ```
3. Install dependencies:
   ```bash
   npm install
   ```
4. Start the development server:
   ```bash
   npm run dev
   ```
5. Open [http://localhost:3000](http://localhost:3000) in your browser.

## 🤝 Contributing

Contributions are what make the open-source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

Distributed under the MIT License. See `LICENSE` for more information.
