using GraphQL.Types;
using Registration.API.GraphQL.Types;
using Registration.Service.Contracts;

namespace Registration.API.GraphQL_Queries
{
    public class RegistrationQuery : ObjectGraphType
    {
        public RegistrationQuery(IStudentService studentService)
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
                            new QueryArgument<StringGraphType> { Name = "studentNumber" }
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
                            new QueryArgument<StringGraphType> { Name = "name" },
                            new QueryArgument<StringGraphType> { Name = "surname" }
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
                            new QueryArgument<IntGraphType> { Name = "courseId" }
                        ),
                    resolve: async ctx =>
                    {
                        var courseId = ctx.GetArgument<int>("courseId");

                        return await studentService.GetByCourse(courseId);
                    }
                );
        }
    }
}