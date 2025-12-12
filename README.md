# Yfitops (ASP.NET Core Web App(Model-View-Controller))

This project is an ASP.NET Core MVC application designed for musicians and their fans.
The app allows managing users, musicians, albums, and tracks, providing a platform for sharing music and marking favorites.
---
### Role-Based Functionality:
1.Admin

  Can create, edit, and delete all albums and tracks
  
  Full access to manage users and content

2.Artist

  Can manage only their own albums and tracks (create, edit, delete)
  
  Views only their own albums and tracks

3.User

  Can browse musicians, their albums, and tracks
  
  Can mark favorite artists, albums, and tracks

### Key Features:

1.Display a list of musicians (Artists)
2.View albums of a specific musician
3.View tracks within an album
4.Mark artists, albums, and tracks as favorites
5.Content management for admins and artists
6.User authentication and authorization
7.Asynchronous operations and role-based access control



## 🚀 How to Run the Project

### 1. Clone the Repository
```bash
git clone https://github.com/Ados-developer/Library.git
cd Library
```

### 2. Run the app
```bash
dotnet run
```

### Requirements
1. [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Sample Data Included
## 1.Admin
Email: admin@gmail.com
Password: asd123456
## 2.Artist
Emails: test1@gmail.com, test2@gmail.com, test3@gmail.com, test4@gmail.com, test5@gmail.com
Password: asd123456
## 3.User
Email: user@gmail.com
Password: asd123456

All sample accounts use the same password for easier testing.
  
