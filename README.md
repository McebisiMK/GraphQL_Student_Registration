# GraphQL_Student_Registration

### Student registration API using GraphQL

**Tech and tools used:**

- .Net Core 2.1.
- GraphQL
- .Net Core Dependency Injection.
- Entity Framework Core.
- .Net Core Web API.
  - Cross Origin Resource Sharing (CORS).
- SQL.
- Repository Pattern.
  - Generic Repository Pattern.
- Unit testing.

**Instructions for use:**

- Clone the repository and do the following:

    - Build Solution:
        - Navigate to parent folder and run:
            > dotnet build
    - Run tests:
        - Navigate to tests folder (Registration.Tests):
            > dotnet test
    - Run application:
        - Create Database using migrations:
            > Update-Database (VS Package Manager Console)
        - Navigate to parent folder and run:
            > dotnet run --project Registration.API

**Possible Queries:**
- Check: Registration.API\GraphQL\Queries\RegistrationQuery.cs
    - Examples:
    ```
        query($studentNumber: String, $name: String, $surname: String, $courseId: Int){
            allStudents{
                studentNumber
                name
                surname
            },
            studentByStudentNumber(studentNumber: $studentNumber){
                name
            },
            studentByFullName(name: $name, surname: $surname){
                name
                surname
            },
            studentByCourse(courseId: $courseId){
                name
                surname
            }
        }