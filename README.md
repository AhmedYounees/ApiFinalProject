# Online Learning Platform
==============================================


## Entities and Relationships:
==============================================

### Courses
    
    *   Attributes: Course ID, Course Name, Duration, Price, Rating, Img, Total Enrollments, Start Date, Update Date
    *   Relationships:
        *   **Belongs to**: Instructor, Category
        *   **Has**: Chapters, Enrollments
### Instructor
    
    *   Attributes: Instructor ID, Instructor Name, Total Courses, Img
    *   Relationships:
        *   **Belongs to**: Specialization
        *   **Has**: Courses
### Student
    
    *   Attributes: Student ID, Student Name
    *   Relationships:
        *   **Has many**: Enrollments
### Category
    
    *   Attributes: Category ID, Category Name, Description
    *   Relationships:
        *   **Has many**: Courses
        *   **Belongs to**: Specialization
### Specialization
    
    *   Attributes: Specialization ID, Specialization Name, Description
    *   Relationships:
        *   **Has many**: Instructors, Categories
### Feedback
    
    *   Attributes: Feedback ID, Rating, Comment, Date
    *   Relationships:
        *   **Belongs to**: Courses
### Enrollment
    
    *   Attributes: Enrollment ID, Course ID, Student ID, Enrollment Date
    *   Relationships:
        *   **Belongs to**: Courses, Students
### Chapter
    
    *   Attributes: Chapter ID, Chapter Name, Chapter Order
    *   Relationships:
        *   **Belongs to**: Courses
        *   **Has many**: Lessons
### Lesson
    
    *   Attributes: Lesson ID, Lesson Name, Lesson Duration, Img
    *   Relationships:
        *   **Belongs to**: Chapters
==============================================



## API Structure
==============================================

### Authentication
- **POST** `/api/auth/register` - Register a new user (All)
- **POST** `/api/auth/login` - User login (All)
- **POST** `/api/auth/logout` - User logout (Authenticated users)
- **POST** `/api/auth/refresh-token` - Refresh authentication token (Authenticated users)
- **POST** `/api/auth/forgot-password` - Initiate password reset process (All)
- **POST** `/api/auth/reset-password` - Reset password with token (All)

### Users
- **GET** `/api/users` - List all users (Admin only)
  - Query parameters: `page`, `limit`, `sort`, `order`, `search`
- **GET** `/api/users/{id}` - Get a specific user (Admin or the user themselves)
- **PUT** `/api/users/{id}` - Update a user (Admin or the user themselves)
- **DELETE** `/api/users/{id}` - Soft delete a user (Admin only)

### Courses
- **GET** `/api/courses` - List all approved courses (All)
  - Query parameters: `page`, `limit`, `sort`, `order`, `search`, `category`, `instructor`
- **GET** `/api/courses/{id}` - Get a specific approved course (All)
- **POST** `/api/courses` - Create a new course (Instructor) - Requires admin approval
- **PUT** `/api/courses/{id}` - Update a course (Admin, Course Instructor)
- **DELETE** `/api/courses/{id}` - Soft delete a course (Admin only)
- **GET** `/api/courses/{id}/feedback` - Get feedback for a course (All)
  - Query parameters: `page`, `limit`, `sort`, `order`
- **POST** `/api/courses/{id}/feedback` - Add feedback for a course (Enrolled Students)
- **GET** `/api/courses/{id}/chapters` - Get chapters for a course (Admin, Course Instructor, Enrolled Students)
  - Query parameters: `page`, `limit`, `sort`, `order`

### Course Approval
- **GET** `/api/course-approvals` - List courses pending approval (Admin only)
  - Query parameters: `page`, `limit`, `sort`, `order`, `search`
- **PUT** `/api/course-approvals/{id}/approve` - Approve a course (Admin only)
- **PUT** `/api/course-approvals/{id}/reject` - Reject a course (Admin only)

### Instructors
- **GET** `/api/instructors` - List all instructors (All)
  - Query parameters: `page`, `limit`, `sort`, `order`, `search`, `specialization`
- **GET** `/api/instructors/{id}` - Get a specific instructor (All)
- **PUT** `/api/instructors/{id}` - Update an instructor (Admin, The Instructor themselves)
- **DELETE** `/api/instructors/{id}` - Soft delete an instructor (Admin only)
- **GET** `/api/instructors/{id}/courses` - Get approved courses by an instructor (All)
  - Query parameters: `page`, `limit`, `sort`, `order`, `search`

### Students
- **GET** `/api/students` - List all students (Admin only)
  - Query parameters: `page`, `limit`, `sort`, `order`, `search`
- **GET** `/api/students/{id}` - Get a specific student (Admin, The Student themselves)
- **PUT** `/api/students/{id}` - Update a student (Admin, The Student themselves)
- **DELETE** `/api/students/{id}` - Soft delete a student (Admin only)
- **GET** `/api/students/{id}/enrollments` - Get enrollments for a student (Admin, The Student themselves)
  - Query parameters: `page`, `limit`, `sort`, `order`, `status`

### Categories
- **GET** `/api/categories` - List all categories (All)
  - Query parameters: `page`, `limit`, `sort`, `order`, `search`
- **GET** `/api/categories/{id}` - Get a specific category (All)
- **POST** `/api/categories` - Create a new category (Admin only)
- **PUT** `/api/categories/{id}` - Update a category (Admin only)
- **DELETE** `/api/categories/{id}` - Soft delete a category (Admin only)
- **GET** `/api/categories/{id}/courses` - Get approved courses in a category (All)
  - Query parameters: `page`, `limit`, `sort`, `order`, `search`

### Specializations
- **GET** `/api/specializations` - List all specializations (All)
  - Query parameters: `page`, `limit`, `sort`, `order`, `search`
- **GET** `/api/specializations/{id}` - Get a specific specialization (All)
- **POST** `/api/specializations` - Create a new specialization (Admin only)
- **PUT** `/api/specializations/{id}` - Update a specialization (Admin only)
- **DELETE** `/api/specializations/{id}` - Soft delete a specialization (Admin only)
- **GET** `/api/specializations/{id}/instructors` - Get instructors in a specialization (All)
  - Query parameters: `page`, `limit`, `sort`, `order`, `search`
- **GET** `/api/specializations/{id}/categories` - Get categories in a specialization (All)
  - Query parameters: `page`, `limit`, `sort`, `order`, `search`

### Enrollments
- **GET** `/api/enrollments` - List all enrollments (Admin)
  - Query parameters: `page`, `limit`, `sort`, `order`, `search`, `status`, `course`, `student`
- **GET** `/api/enrollments/{id}` - Get a specific enrollment (Admin, Course Instructor, Enrolled Student)
- **POST** `/api/enrollments` - Create a new enrollment (Admin, Students for self-enrollment)
- **PUT** `/api/enrollments/{id}` - Update an enrollment status (Admin, Course Instructor)
- **DELETE** `/api/enrollments/{id}` - Soft delete an enrollment (Admin only)

### Chapters
- **GET** `/api/courses/{courseId}/chapters` - List all chapters in a course (Admin, Course Instructor, Enrolled Students)
  - Query parameters: `page`, `limit`, `sort`, `order`
- **GET** `/api/courses/{courseId}/chapters/{id}` - Get a specific chapter (Admin, Course Instructor, Enrolled Students)
- **POST** `/api/courses/{courseId}/chapters` - Create a new chapter (Admin, Course Instructor)
- **PUT** `/api/courses/{courseId}/chapters/{id}` - Update a chapter (Admin, Course Instructor)
- **DELETE** `/api/courses/{courseId}/chapters/{id}` - Soft delete a chapter (Admin, Course Instructor)

### Lessons
- **GET** `/api/courses/{courseId}/chapters/{chapterId}/lessons` - List all lessons in a chapter (Admin, Course Instructor, Enrolled Students)
  - Query parameters: `page`, `limit`, `sort`, `order`
- **GET** `/api/courses/{courseId}/chapters/{chapterId}/lessons/{id}` - Get a specific lesson (Admin, Course Instructor, Enrolled Students)
- **POST** `/api/courses/{courseId}/chapters/{chapterId}/lessons` - Create a new lesson (Admin, Course Instructor)
- **PUT** `/api/courses/{courseId}/chapters/{chapterId}/lessons/{id}` - Update a lesson (Admin, Course Instructor)
- **DELETE** `/api/courses/{courseId}/chapters/{chapterId}/lessons/{id}` - Soft delete a lesson (Admin, Course Instructor)

### Search
- **GET** `/api/search` - Global search across courses, instructors, and categories (All)
  - Query parameters: `q`, `type`, `page`, `limit`, `sort`, `order`

==============================================


## User Stories
==============================================

User Roles
----------

1.  Student
2.  Instructor
3.  Administrator
4.  Unauthenticated User

Website Goals
-------------

1.  Provide an accessible and user-friendly platform for online learning
2.  Offer a wide range of courses across various categories and specializations
3.  Enable instructors to create and manage courses effectively
4.  Facilitate student enrollment and engagement with course content
5.  Ensure efficient and secure platform administration
6.  Encourage guest users to explore the platform and register

* * *

User Stories by Role
--------------------

### 1\. Student

#### **Identify**

*   As a student, I want to easily find courses that match my interests and skill level.
*   As a student, I want to interact with course materials seamlessly.
*   As a student, I want to provide feedback on courses to improve content quality.

#### **Functional**

1.  **Search and Discovery**: As a student, I want to search for courses using keywords, categories, or instructor names so that I can easily find relevant learning materials.
2.  **Enrollment**: As a student, I want to enroll in courses and access course materials at any time so that I can learn at my own pace.
3.  **Feedback**: As a student, I want to rate and review courses that I’ve completed so that I can help improve future content.

#### **UI/UX**

1.  **Course Catalog**: As a student, I want to see a clear and intuitive course catalog with filtering options (category, level, price) so that I can quickly find what I’m looking for.
2.  **Dashboard**: As a student, I want a personalized dashboard that displays my enrolled courses.
3.  **Navigation**: As a student, I want easy navigation between lessons and chapters within a course so that I can seamlessly move through the content.
4.  **Lesson Interface**: As a student, I want a distraction-free interface for viewing course videos and text to stay focused on learning.
5.  **Mobile-Responsive Design**: As a student, I want the platform to be mobile-responsive so I can learn on-the-go without losing functionality.

* * *

### 2\. Instructor

#### **Identify**

*   As an instructor, I want to create and manage my courses efficiently.
*   As an instructor, I want to see the student reviews in my course.

#### **Functional**

1.  **Course Creation**: As an instructor, I want to create new courses with multiple chapters and lessons so that I can structure my content effectively.
2.  **Content Upload**: As an instructor, I want to upload videos, PDFs, and text documents to my lessons so that students can access a variety of resources.
3.  **Course Management**: As an instructor, I want to edit, delete, or update course materials at any time to keep my content relevant and up-to-date.

#### **UI/UX**

1.  **Course Builder**: As an instructor, I want a user-friendly course creation interface with drag-and-drop functionality for lesson arrangement so that I can easily structure my content.
2.  **Dashboard**: As an instructor, I want an overview of my courses and their enrollment numbers in a single dashboard.
3.  **Content Management**: As an instructor, I want intuitive tools for managing course structure, such as reordering chapters and adding prerequisites.

* * *

### 3\. Administrator

#### **Identify**

*   As an administrator, I want to manage users, courses, and content across the platform.
*   As an administrator, I want to ensure the quality and appropriateness of courses.

#### **Functional**

1.  **Course Approval**: As an administrator, I want to approve or reject new course submissions based on content quality and relevance.
2.  **User Management**: As an administrator, I want to manage user accounts (students, instructors) by creating, updating, or deleting profiles.
3.  **Category Management**: As an administrator, I want to manage categories and specializations to ensure that courses are properly classified and organized.

#### **UI/UX**

1.  **Admin Dashboard**: As an administrator, I want a comprehensive dashboard that displays key platform metrics such as total enrollments, course approvals pending.
2.  **Approval Interface**: As an administrator, I want efficient tools for reviewing course content, with options to approve, reject.
3.  **User Management Interface**: As an administrator, I want easy access to user account management features, including the ability to reset passwords, ban users.

* * *

### 4\. Unauthenticated User

#### **Identify**

*   As an unauthenticated user, I want to explore the platform and its course offerings.
*   As an unauthenticated user, I want to easily create an account or log in to access course content.

#### **Functional**

1.  **Browse Courses**: As an unauthenticated user, I want to browse the course catalog by category, popularity, or instructor so that I can explore the available offerings.
2.  **View Course Details**: As an unauthenticated user, I want to view course descriptions, lesson previews, and instructor profiles so that I can decide whether to sign up.
3.  **Registration**: As an unauthenticated user, I want to create a new account (student or instructor) to gain access to the platform.
4.  **Login**: As an unauthenticated user, I want to log in to an existing account so that I can resume learning or teaching.

#### **UI/UX**

1.  **Homepage**: As an unauthenticated user, I want a clear and attractive homepage showcasing popular courses, categories, and testimonials from successful students.
2.  **Registration Process**: As an unauthenticated user, I want an easy-to-find and user-friendly registration page, with minimal form fields and a quick account creation process.
4.  **Login Interface**: As an unauthenticated user, I want a simple and secure login process, with options to reset my password or log in with a social media account.

* * *

