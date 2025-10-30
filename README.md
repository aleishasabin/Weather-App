# Weather App

## Overview

This is a simple weather app I built to show how I approach backend development, while still providing a functional frontend for searching weather. You can look up cities and see the temperature, wind, and humidity.

The frontend is mostly there to consume the API and keep things simple, but the backend shows patterns I’d use in real world industry projects.

## Tech Stack

- **Backend:** `.NET 8 (C#), Entity Framework, SQLite`
- **Frontend:** `React, TypeScript, Material UI`

## Running Locally

**Backend:**

    cd Backend/WeatherApp.Api
    dotnet run --launch-profile "https"

Before running the backend for the first time, make sure to add your **OpenWeather** API key to `appsettings.json`.

**Frontend:**

    cd Frontend
    npm install
    npm run dev

The frontend will run on `http://localhost:5173`.

## How I Approached the Backend

The backend is larger than it needs to be for something this simple, but that was intentional. I wanted to show how I’d approach things in a scalable, real world scenario:

- **Repository Pattern:**  
  All data access is managed through repositories, which isolates business logic from the data layer.
- **Result Pattern:**  
  Services return a `Result` object, so success and failure are explicit. This makes handling errors predictable instead of scattered across controllers.
- **Centralized Error Handling:**  
  I’ve kept all error messages in one place and mapped them to `ActionResult`s through a custom mapper. Changing an error message or status code only requires touching one file.
- **Entities vs DTOs:**  
  I separated models into entities and DTOs to keep the domain clean and avoid exposing internal details directly to the API.
- **Automapper & Minimal Controllers:**  
  Automapper handles entity-to-DTO mapping. I also wrote extensions to map `Result` objects to `ActionResult` objects, so the controller code doesn’t get messy.
- **Unit Tests & Interfaces:**  
  The services, repositories and HTTP clients all implement interfaces, making them easy to test and mock. I included some simple unit tests to show my approach.
- **Swagger Documentation:**  
  I added Swagger annotations to each controller.
- **Migrations:**  
  I set up migrations so schema changes can be applied cleanly. All data access is managed through SQLite using Entity Framework Core.

## How the Frontend Connects

Since the focus of this project is the backend, the frontend mostly exists to consume it cleanly. A few points worth noting:

- I added fetch avoidance logic so repeated searches don’t hammer the API.
- The UI components and hooks are structured to separate concerns rather than for scalability or reusability. This was an intentional choice for simplicity but could easily be reworked if the frontend grows.

## Future Improvements

- **Caching:**  
  Right now repeated searches avoid extra API calls where possible, but proper caching in both the frontend and backend would be ideal.
- **UX enhancements:**  
  Adding loading indicators when fetching data would improve user experience.
- **Styling/Theme Improvements:**  
  Inline styles work for now, but a proper theme or external styles would make the frontend more maintainable as it grows.
- **Autocomplete Precision:**  
  The backend provides a dataset of cities ordered by popularity. I initially assumed `{city}`, `{country}` would be unique enough for the frontend, but some countries have multiple cities with the same name. To handle this, the frontend deduplicates the results, showing just one of each duplicate in the dropdown. There’s no indication which duplicate is shown, but it works for now. If I kept building this, I’d expand the dataset to include states or regions so the dropdown could show `{city}`, `{state}`, `{country}`.

## Credits & Data Sources

- Weather data provided by [OpenWeather](https://openweathermap.org).
- A subset of the `World Cities Database` from [SimpleMaps](https://simplemaps.com/data/world-cities) is stored in the SQLite database. Licensed under [CC BY 4.0](https://creativecommons.org/licenses/by/4.0/).
