using GraphQL.Types;
using Registration.API.GraphQL_Types.QueryTypes;
using Registration.Service.Contracts;

namespace Registration.API.GraphQL_Queries
{
    public class RegistrationQuery : ObjectGraphType
    {
        public RegistrationQuery(IStudentService studentService, ISubjectService subjectService, ISemesterService semesterService, ICourseService courseService, IAddressService addressService)
        {
            FieldAsync<ListGraphType<StudentType>>
                (
                    "AllStudents",
                    resolve: async ctx => await studentService.GetAll()
                );

            FieldAsync<StudentType>
                (
                    "StudentByStudentNumber",
                    arguments: new QueryArguments
                        (
                            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "studentNumber" }
                        ),
                    resolve: async ctx =>
                    {
                        var studentNumber = ctx.GetArgument<string>("studentNumber");

                        return await studentService.GetByStudentNumber(studentNumber);
                    }
                );

            FieldAsync<StudentType>
                (
                    "StudentByFullName",
                    arguments: new QueryArguments
                        (
                            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "name" },
                            new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "surname" }
                        ),
                    resolve: async ctx =>
                    {
                        var name = ctx.GetArgument<string>("name");
                        var surname = ctx.GetArgument<string>("surname");

                        return await studentService.GetByFullName(name, surname);
                    }
                );

            FieldAsync<ListGraphType<StudentType>>
                (
                    "StudentByCourse",
                    arguments: new QueryArguments
                        (
                            new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "courseId" }
                        ),
                    resolve: async ctx =>
                    {
                        var courseId = ctx.GetArgument<int>("courseId");

                        return await studentService.GetByCourse(courseId);
                    }
                );

            FieldAsync<ListGraphType<SubjectType>>
                (
                    "AllSubjects",
                    resolve: async ctx => await subjectService.GetAll()
                );

            FieldAsync<SubjectType>
                (
                    "subjectById",
                    arguments: new QueryArguments
                        (
                            new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "subjectId" }
                        ),
                    resolve: async ctx =>
                    {
                        var subjectId = ctx.GetArgument<int>("subjectId");

                        return await subjectService.GetById(subjectId);
                    }
                );

            FieldAsync<ListGraphType<SubjectType>>
                (
                    "subjectsByCourse",
                    arguments: new QueryArguments
                        (
                            new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "courseId" }
                        ),
                    resolve: async ctx =>
                    {
                        var courseId = ctx.GetArgument<int>("courseId");

                        return await subjectService.GetByCourse(courseId);
                    }
                );

            FieldAsync<ListGraphType<SemesterType>>
                (
                    "allSemester",
                    resolve: async ctx => await semesterService.GetAll()
                );

            FieldAsync<SemesterType>
                (
                    "semesterById",
                    arguments: new QueryArguments
                        (
                            new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "semesterId" }
                        ),
                    resolve: async ctx =>
                    {
                        var semesterId = ctx.GetArgument<int>("semesterId");

                        return await semesterService.GetById(semesterId);
                    }
                );

            FieldAsync<ListGraphType<CourseType>>
                (
                    "allCourses",
                    resolve: async ctx => await courseService.GetAll()
                );

            FieldAsync<CourseType>
                (
                    "courseById",
                    arguments: new QueryArguments
                        (
                            new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "courseId" }
                        ),
                    resolve: async ctx =>
                    {
                        var courseId = ctx.GetArgument<int>("courseId");

                        return await courseService.GetById(courseId);
                    }
                );

            FieldAsync<ListGraphType<AddressType>>
                (
                    "allAddresses",
                    resolve: async ctx => await addressService.GetAll()
                );

            FieldAsync<AddressType>
                (
                    "addressById",
                    arguments: new QueryArguments
                        (
                            new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "addressId" }
                        ),
                    resolve: async ctx =>
                    {
                        var addressId = ctx.GetArgument<int>("addressId");

                        return await addressService.GetById(addressId);
                    }
                );

            FieldAsync<ListGraphType<AddressType>>
                (
                    "addressesByStreet",
                    arguments: new QueryArguments
                        (
                            new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "street" }
                        ),
                    resolve: async ctx =>
                    {
                        var street = ctx.GetArgument<string>("street");

                        return await addressService.GetByStreet(street);
                    }
                );
        }
    }
}