# YouTube Data Manager

A .NET-based API for fetching, storing, and managing YouTube videos using the YouTube Data API v3.

## 📌 Features
- Fetch YouTube videos based on a query.
- Store videos in an SQLite database.
- Retrieve and filter videos via API endpoints.
- Soft delete functionality for videos.
- Uses Swagger for API documentation.
- Includes a **Postman Collection** for easy API testing.
- Unit tests implemented with MSTest and Moq.

## 🛠️ Tech Stack
- **.NET 9**
- **Entity Framework Core**
- **SQLite**
- **YouTube Data API v3**
- **Swagger**
- **MSTest & Moq** (for unit testing)
- **Postman** (for API testing)

## 🚀 Getting Started

### 1️⃣ Prerequisites
- .NET SDK 9
- SQLite
- A valid **YouTube API Key**
- **Postman** (optional, for API testing)

### 2️⃣ Clone the Repository
```sh
git clone https://github.com/LeonardoPaublo/youtube-data-manager.git
cd youtube-data-manager
```

### 3️⃣ Configure YouTube API Key
Add your YouTube API Key on the **launchSettings.json**:
```json
{
  "YouTubeApiKey": "your_api_key_here"
}
```

### 4️⃣ Install Dependencies
```sh
dotnet restore
```

### 5️⃣ Run Migrations (SQLite)
```sh
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 6️⃣ Run the Application
```sh
dotnet run
```

## 📈 API Documentation
- **Swagger UI:**  
  ```
  http://localhost:5271/swagger/index.html
  ```
- **Postman Collection:**  
  Import the `YouTubeDataManager.postman_collection.json` file into Postman to test API endpoints easily.

## ✅ Running Tests
This project includes unit tests using **MSTest** and **Moq** to ensure functionality.

To run tests, use:
```sh
dotnet test
```

## 📝 Notes
- The **FetchAndStoreYouTubeVideos** method pulls data from the YouTube API and saves it to the database.
- Videos can be retrieved, updated, and soft deleted using the API.

---