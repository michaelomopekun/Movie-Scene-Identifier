# Movie Scene Identifier Backend

## Overview
The current initiative aims to build an intelligent backend service that leverages AI and machine learning to help users identify movies from scene clip.
 <!-- This project is focused on providing a robust RESTful API for movie scene identification, management, and search, enabling seamless integration with various client applications. -->

## Project Structure
The project consists of the following main components:

- **Controllers**: Contains the `MovieScenesController` which handles HTTP requests for movie scenes.
- **Models**: Contains the `MovieScene` class that defines the data structure for a movie scene.
- **Services**: Contains the `SceneIdentifierService` which encapsulates the business logic for identifying scenes.
- **Program.cs**: Configures services and the request pipeline for the application.
- **appsettings.json**: Contains configuration settings such as connection strings and application settings.

## Setup Instructions
1. Clone the repository to your local machine.
2. Navigate to the project directory.
3. Restore the dependencies by running:
   ```
   dotnet restore
   ```
4. Run the application using:
   ```
   dotnet run
   ```
5. The API will be available at `http://localhost:5000`.

## Usage Guidelines
- Use the `MovieScenesController` to perform CRUD operations on movie scenes.
- The API supports this endpoint:
  - `GET /api/Search`: Retrieve a list of movie that matches the uploaded movie scene.

## Contributing
Contributions are welcome! Please submit a pull request or open an issue for any enhancements or bug fixes.

## License
This project is licensed under the MIT License. See the LICENSE file for more details.
