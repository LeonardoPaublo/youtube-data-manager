# YouTube Data Manager

A .NET-based API for fetching, storing, and managing YouTube videos using the YouTube Data API v3.

## 📌 Features
- Fetch YouTube videos based on a query.
- Store videos in an SQLite database.
- Retrieve and filter videos via API endpoints.
- Soft delete functionality for videos.
- Uses Swagger for API documentation.

## 🛠️ Tech Stack
- **.NET 9**
- **Entity Framework Core**
- **SQLite**
- **YouTube Data API v3**
- **Swagger**

## 🚀 Getting Started

### 1️⃣ Prerequisites
- .NET SDK 9
- SQLite
- A valid **YouTube API Key**

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

## 📝 Notes
- The **FetchAndStoreYouTubeVideos** method pulls data from the YouTube API and saves it to the database.
- Videos can be retrieved, updated, and soft deleted using the API.
- Swagger documentation is available at:
```
http://http://localhost:5271/swagger/index.html
```