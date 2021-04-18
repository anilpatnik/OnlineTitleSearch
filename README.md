# Online Title Search API and UI

### Frameworks and Libraries

- ASP.NET Core 3.1
- Node 10.13.0
- Entity Framework Core (Code First)
- Entity Framework In-Memory Provider (Development)
- Entity Framework Sql Server Provider (Production)
- Dependency Injection
- C# Unit Testing (Moq)
- Swagger (API documentation)
- React Hooks (16.0 or above)
- Redux (State Management)
- React Router 4
- Material-UI
- Formik (Form Validation)
- JS Unit Testing (Yet to be added. I prefer E2E UI automation using CodeCeptJS)

## IDE and Tools

- Visual Studio Code
- Visual Studio 2019
- ReSharper
- GitHub

## Build and Run

1. Git clone the project (master)
2. Build and Run using Visual Studio (run `npm install` manually, if the client project didn't build automatically)
3. UI opens in browser with https://localhost:44348 (automatically)
4. API opens in browser with https://localhost:44348/swagger (manually)

## Note

- Html Agility Pack is **_NOT_** used for HTML scraping
- The output ranking index might not be the same as it appears on the search results page since no 3rd party tools are being used for HTML scraping
- Search Engines can be added, updated, removed and queried via Swagger as no UI page is avaiable part of this exercise
- Architectured using SOLID principles and application development standards

## React UI

![image](https://user-images.githubusercontent.com/17073376/115121009-dbe4b400-9ff3-11eb-91d7-f688ab6445ae.png)

## Swagger API

![image](https://user-images.githubusercontent.com/17073376/115120234-c3729a80-9fef-11eb-87a6-577f6699e639.png)
