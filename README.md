# Training Management System

## 📌 Overview
A basic **ASP.NET MVC** web application for managing **courses, sessions, users, and grades**.  
The system uses the **Repository Pattern** for data access and applies both **server-side** and **client-side** validation.

---

## 🎯 Core Features
### 1. Course Management
- Add, edit, delete courses.
- Assign an instructor to a course.
- Search courses by **name** or **category**.
- **Validation:**
  - Name: Required, 3–50 characters, must be unique.
  - Category: Required.

### 2. Session Management
- Add, edit, delete sessions for a course.
- Set start and end dates.
- Search sessions by course name.
- **Validation:**
  - StartDate: Required, cannot be in the past.
  - EndDate: Required, must be after StartDate.
  - CourseId: Required.

### 3. User Management
- Add, edit, delete users.
- Roles: **Admin**, **Instructor**, **Trainee** (no login system).
- **Validation:**
  - Name: Required, 3–50 characters.
  - Email: Required, valid email format.
  - Role: Required.

### 4. Grades Management
- Record grades for each trainee in a session.
- View grades for each trainee.
- **Validation:**
  - Value: Required, between 0 and 100.
  - SessionId: Required.
  - TraineeId: Required.

---

## 🗄 Entities
- **Course**: Id, Name, Category, InstructorId  
- **Session**: Id, CourseId, StartDate, EndDate  
- **User**: Id, Name, Email, Role  
- **Grade**: Id, SessionId, TraineeId, Value  

---

## 👥 Team Members
| Name            | Faculty                              |
|-----------------|--------------------------------------|
| **Mohamed Mohsen** | Computers & Information - Kafr El-Sheikh |
| **Mohamed Khaled** | Computers & Information - Kafr El-Sheikh |
| **Manar Mohamed**  | Computers & Information - Kafr El-Sheikh |
| **Mohamed Ashraf** | Computers & Information - Tanta |
| **Mohamed Arafa**  | Engineering - Tanta |

---

## 📌 Task Distribution
- **Mohamed Mohsen** *(Team Lead / Backend Integration)*:  
  - Project setup, Repository Pattern, common validation, integration, code review, final testing.
- **Mohamed Khaled** *(Backend Developer)*:  
  - Business logic, controllers, server-side validation.
- **Manar Mohamed** *(Frontend Developer)*:  
  - Razor views, HTML/CSS/Bootstrap, client-side validation.
- **Mohamed Ashraf** *(Database Developer)*:  
  - Database schema, migrations, relationships, seed data.
- **Mohamed Arafa** *(Tester & Documentation)*:  
  - Manual testing, bug reporting, documentation, final presentation.

---

## ⚙️ Technologies Used
- **ASP.NET MVC**
- **Entity Framework**
- **SQL Server**
- **Bootstrap**
- **jQuery**

---

## 🚀 How to Run
1. Clone the repository:
   ```bash
   git clone <repo-url>
