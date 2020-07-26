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
                    resolve: async ctx => await studentService.GetByStudentNumber(ctx.GetArgument<string>("studentNumber"))
                );

            FieldAsync<StudentType>
                (
                    "StudentByFullName",
                    arguments: new QueryArguments
                        (
                            new QueryArgument<StringGraphType> { Name = "name" },
                            new QueryArgument<StringGraphType> { Name = "surname" }
                        ),
                    resolve: async ctx => await studentService
                                                    .GetByFullName(ctx.GetArgument<string>("name"), ctx.GetArgument<string>("surname"))
                );

            FieldAsync<ListGraphType<StudentType>>
                (
                    "StudentByCourse",
                    arguments: new QueryArguments
                        (
                            new QueryArgument<IntGraphType> { Name = "courseId" }
                        ),
                    resolve: async ctx => await studentService.GetByCourse(ctx.GetArgument<int>("courseId"))
                );
        }
    }
}