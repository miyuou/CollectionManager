
Collection Manager - Web Application

**Collection Manager** is a web application built with **ASP.NET Core** and **Entity Framework** for managing a personal collection of **films** and **mangas**. It allows users to add, categorize, and track their favorites with direct access to relevant publication platforms. Additionally, it includes a **version history** feature, enabling users to restore previous versions for better data management.

Features

- **Add and Categorize Items:** Easily add films and mangas to your collection and categorize them.
- **Track Favorites:** Mark your favorite items and keep track of them in a separate list.
- **Direct Access to Publication Platforms:** Links to platforms where films and mangas are published.
- **Version History:** Track changes to your collection and restore previous versions to keep your data up-to-date and organized.

Technologies Used

- **Backend:** ASP.NET Core
- **Database:** Entity Framework with a relational database (SQL Server or any supported database)
- **Frontend:** HTML, CSS, JavaScript (Bootstrap for UI design)
- **Version Control:** Git/GitHub for source control

Installation

Follow these steps to get your development environment up and running:

1. Clone the Repository



2. Install Dependencies

Ensure you have **.NET Core SDK** installed. Then, restore the dependencies:

```
dotnet restore
```

3. Set Up the Database

- Update your **`appsettings.json`** file to configure your database connection string.

```
"ConnectionStrings": {
  "DefaultConnection": "Your_Connection_String_Here"
}
```

- Apply migrations to set up the database schema:

```
dotnet ef database update
```

4. Run the Application

Start the application:

```
dotnet run
```

Visit **http://localhost:5000** to view the app.

5. Access the Admin Panel

You can manage the collection via the admin panel. Create a superuser account and log in.

```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Usage

- **Add a new film or manga**: Navigate to the "Add Item" page, fill out the form, and submit.
- **Categorize items**: While adding an item, assign it to predefined categories.
- **Track Favorites**: Click on the heart icon to mark your items as favorites.
- **View Publication Links**: Direct access to where films or mangas can be found online.
- **Version History**: Review past versions of your collection and restore any changes if needed.

Future Enhancements

- Implement **user authentication** for personalized experience.
- Add support for **multiple types of media** (e.g., books, TV shows).
- Improve the **user interface** with more advanced frontend technologies (React, Vue.js, etc.).
- Add **payment gateway integration** for accessing premium content.

Contributing

If you would like to contribute to the project, please fork the repository and submit a pull request. Contributions are always welcome!




