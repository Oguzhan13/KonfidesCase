# Konfides Case Project

Authentication database migration is completed

<img src="https://github.com/Oguzhan13/KonfidesCase/assets/108337929/988f04ab-b5d0-46c2-b047-6f1065597865" alt="Image2" width="300" height="400" />
<img src="https://github.com/Oguzhan13/KonfidesCase/assets/108337929/8750a7f1-a581-4575-9e6d-f6cd3d9b2c7e" alt="Image2" width="300" height="250" />
<img src="https://github.com/Oguzhan13/KonfidesCase/assets/108337929/4b522135-367b-4cc6-a19e-4b17eda7d49d" alt="Image2" width="300" height="250" />

## Dependencies

**Project: Authentication Layer:**

Package Reference: Microsoft.EntityFrameworkCore (Version: 7.0.5)
Package Reference: Microsoft.EntityFrameworkCore.SqlServer (Version: 7.0.5)

**Project: Business Logic Layer (BLL):**

Package Reference: AutoMapper.Extensions.Microsoft.DependencyInjection (Version: 12.0.1)
Package Reference: Microsoft.AspNetCore.Identity.EntityFrameworkCore (Version: 7.0.5)
Package Reference: Microsoft.EntityFrameworkCore (Version: 7.0.5)
Package Reference: Microsoft.EntityFrameworkCore.SqlServer (Version: 7.0.5)
Package Reference: Microsoft.EntityFrameworkCore.Tools (Version: 7.0.5)

**Project: Data Access Layer (DAL):**

Project Reference: KonfidesCase.Authentication
Project Reference: KonfidesCase.DTO

**Project: API Layer for Authentication:**

Package Reference: Microsoft.VisualStudio.Web.CodeGeneration.Design (Version: 7.0.6)
Package Reference: Newtonsoft.Json (Version: 13.0.3)

**Project: API Layer for CRUD Operations:**

Package Reference: Microsoft.AspNetCore.OpenApi (Version: 7.0.5)
Package Reference: Microsoft.EntityFrameworkCore.Design (Version: 7.0.5)
Package Reference: Swashbuckle.AspNetCore (Version: 6.5.0)
Project Reference: KonfidesCase.Authentication
Project Reference: KonfidesCase.BLL

**Project: Entity Layer for Authentication:**

Package Reference: Microsoft.EntityFrameworkCore.Tools (Version: 7.0.5)
Package Reference: Microsoft.Extensions.Configuration.Abstractions (Version: 7.0.0)
Project Reference: KonfidesCase.Entity

**Project: Entity Layer for CRUD Operations:**

Package Reference: Microsoft.EntityFrameworkCore (Version: 7.0.5)
Package Reference: Microsoft.EntityFrameworkCore.SqlServer (Version: 7.0.5)

**Project Reference: KonfidesCase.Authentication:**

## Project Details
The Konfides Case project is divided into multiple layers to handle different aspects of the application. These layers work together to provide a secure and functional application. The project includes the following components:

**1. Authentication Layer**
The Authentication layer is responsible for managing all authentication-related operations. It is structured into separate folders, each handling specific authentication tasks. This layer ensures the security of user data and access control. The API project references the Authentication layer to utilize its authentication functionalities.

**2. Business Logic Layer (BLL)**
The Business Logic Layer (BLL) acts as an intermediary between the API project and the Data Access Layer (DAL). It handles the business logic and business rules of the application. The BLL references the DAL and the Authentication layer to ensure proper data flow and user authentication.

**3. Data Access Layer (DAL)**
The Data Access Layer (DAL) is responsible for accessing the database and performing CRUD (Create, Read, Update, Delete) operations. It references the Entity layer to interact with the database tables. Additionally, it references the Authentication layer for authentication-specific data.

**4. Entity Layer**
The Entity layer defines the data model used throughout the application. It contains various entities that represent database tables. The Entity layer references the Authentication project specifically for the AuthUser object, as it shares some attributes with the AppUser object used in other CRUD operations.

Please note that separate databases are used for Authentication and other CRUD operations, which is why the Entity layer references the Authentication project.

**Role-Based Authorization:**
In the Konfides Case project, role-based authorization is implemented to control access to different parts of the application. Users with specific roles are granted or denied access to certain functionalities and pages.

**MVC with Areas:**
The user interface (UI) part of the application is implemented using the MVC pattern, and Areas are used to organize and manage different sections of the application. This ensures a modular and structured UI with easy navigation.

**Project Structure**
The project follows a modular approach, allowing for better organization and separation of concerns. Each layer focuses on its specific responsibilities, promoting code reusability and maintainability.

To get started with the Konfides Case project, follow these steps:

Clone the repository to your local machine.
Set up the required databases for the Authentication and other CRUD operations.
Make sure to install the necessary dependencies specified in each layer's package files.
Run the application using the appropriate entry point, typically located in the API project.
Contributions are welcome! If you want to contribute to the project, please read our contribution guidelines.
