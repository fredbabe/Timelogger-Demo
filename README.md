<div align="center">
  <h1>Timelogger</h1>
  <a href="https://www.buymeacoffee.com/VishwaGauravIn" target="_blank">
    <img alt="" src="https://skillicons.dev/icons?i=ts,dotnet,tailwind,vite,vitest,react,html" style="vertical-align:center" />
  </a>
</div>

<br/>

<details>
  <summary>👋 Introduction</summary>
  <br/>

  This project is a **Timelogger** solution built with a backend in .NET and a frontend in React. It aims to solve the following user stories:

  - **Time Tracking:** As a freelancer, I want to register how I spend time on my projects, so that I can provide my customers with an overview of my work.
  - **Invoice Accuracy:** As a freelancer, I want to get an overview of my time registrations per project, so that I can create correct invoices.
  - **Project Management:** As a freelancer, I want to sort my projects by their deadline, so that I can prioritize my work.

  From this README, you’ll find documentation to help you understand and run the project.
</details>

---

<details>
  <summary>⚙️ Technology & Setup</summary>
  <br/>

  ### 🖥 How to Run Locally

  - Clone the project.
  - In the backend, update `appsettings.Development.json` with your local MSSQL connection string.
  - Navigate to the backend project and run:
    ```bash
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    dotnet run
    ```
  - In the frontend project, make sure you run node v18 or above and run:
    ```bash
    npm install
    npm run dev
    ```

  - Finally, use Swagger to create a few **Customers** so you can start testing.  
    (Note: Projects require existing customers.)

  ### 🛠 Backend

  - **.NET 8**
  - **ASP.NET Core API**
  - **xUnit** for testing
  - **Entity Framework Core**
  - **MSSQL**

  **Structure:**
  - `Controllers/`, `Services/`, `Repositories/`, `Models/`
  - All backend code is contained within one project for simplicity, structured into clean folders.

  **Tests:**
  - Includes unit and E2E tests (e.g., creating registrations and projects)

  ### 🌐 Frontend

  - **React + TailwindCSS**
  - **React Hook Form**
  - **React Query + Axios**
  - **NSwag** for API type generation
  - **Vitest + RTL** for testing

  **Structure:**
  - Organized by key functionality: registrations, projects, viewing data
  - Shared form components for reusability
  - Forms validated using React Hook Form
  - API calls typed via NSwag and handled via Axios + React Query
  - Unit tests on core components using Vitest + RTL

  ### 🗃 Database

  - **Customer** → has many **Projects**
  - **Project** → has many **Registrations**

  This clean relational structure allows you to track time for customers and projects easily.
</details>

---

<details>
  <summary>🚀 Pipeline Overview</summary>
  <br/>

  Due to time constraints, the pipeline is a **partial implementation**. What has been implemented:

  - ✅ A GitHub Action for building and testing both backend and frontend
  - 🔜 Terraform-based Infrastructure as Code for Google CLoud
  - 🔜 NSwag type generation not yet integrated

  ### The pipeline includes:
  - Build & test job for both projects
  - Terraform to deploy resources (skeleton setup only. Minimal code example in TF database)
  - A final GitHub Action to orchestrate all deploy steps to production (mocked for now)

  ![image](https://github.com/user-attachments/assets/cab8a695-9b93-4615-8fe3-e56711d26386)

</details>

---

<details>
  <summary>✍️ Considerations and Future Work</summary>
  <br/>

  **Backend test stability:**  
  The backend tests occasionally fail in CI but pass locally. This non-determinism may be related to how the in-memory database is being shared or initialized across tests. Investigating test isolation will be my first step.

  **Architecture:**  
  The API is kept in one project for simplicity, but this can be modularized later if the project grows or we need to build an intergration to another system. For instance we could split up the backend into seperates projects such as Timelogger.API, Timelogger.Core and Timelogger.Infrastructure. In that way it would be more easy to expand the architecture with other apis etc. The frontend architecture could be seperated into more defined folders to seperate the input components and other type of needed components later on.
</details>

---

<details>
  <summary>🕵️ Suggestions for Improvements</summary>
  <br/>

  **Frontend Testing:**  
  Currently, tests only check whether components render. I'd like to add form validation tests to ensure fields are validated correctly. Since we use React Hook Form, this may require mocking, but it will significantly improve reliability.

  **Backend Testing:**  
  Build a more robust suite, especially for end-to-end tests. Currently, the E2E tests are minimal.

  **CI Improvements:**
  - Disallow direct pushes to `main`
  - Require PR reviews
  - Block merges if tests or pipelines fail

  **Others:**  
  - Add more observability to catch failures in production.
  - Add possibility to adjust registrations.
  - Add total amount of hours registrated on projects.
  - Deactivate projects instead of only deleting them. 

  
</details>
