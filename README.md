# Airline Booking System

## Introduction

This project is an Airline Booking System built using ASP.NET Core, featuring a code-first approach with Entity Framework Core for database management. It utilizes ASP.NET Core Identity for user authentication and authorization, and integrates Open Authentication (OAuth) for login via Facebook. Admin users have full control over airlines, categories, flights, and offers, while regular users can book flights and manage their bookings.

## Features

- **ASP.NET Core MVC**: Web application framework
- **Entity Framework Core**: Code-first approach for database management
- **ASP.NET Core Identity**: Authentication and authorization
- **Open Authentication (OAuth)**: Login via Facebook
- **Admin Functionality**: Manage airlines, categories, flights, offers, view details, and bookings
- **User Functionality**: Book flights, view and manage bookings

## Setup

### Prerequisites

- Visual Studio 2022
- .NET 6.0 SDK
- SQL Server or any preferred database
- Facebook App credentials for OAuth login

### Steps

1. **Clone the Repository**
2. **Open the Solution**:
- Open the solution in Visual Studio 2022.

3. **Run Entity Framework Migrations**:
- Open the Package Manager Console from Tools > NuGet Package Manager > Package Manager Console.
- Run the following commands to add migrations and update the database:
- Add-Migration InitialMigration
- Update-Database

4. **Build and Run the Application**:
- Set AirlineBookingSystem as the startup project.
- Build and run the application using Ctrl + F5.

## Usage
1. **Register as Admin or User**:
- The first registered user will be assigned the Admin role.
- Subsequent users will be regular Users by default.

2. **Admin Functionality**:
- Manage airlines, categories, flights, offers.
- View details and bookings.

3. **User Functionality**:
- Browse available flights.
- Book flights and manage bookings.

## Technologies Used
**ASP.NET Core MVC**: Web framework
**Entity Framework Core**: ORM for database management
**ASP.NET Core Identity**: Authentication and authorization
**OAuth (Facebook)**: External login integration

Contact
For any queries or issues, please contact Vishal Jamwal t [vishaljamwal402@gmail.com].
  
