using GraphQL.Types;
using Registration.API.GraphQL.Types.MutationTypes;
using Registration.API.GraphQL_Types.MutationTypes;
using Registration.API.GraphQL_Types.QueryTypes;
using Registration.Entities.Models;
using Registration.Service.Contracts;
using Registration.Service.Services;

namespace Registration.API.GraphQL_Mutations
{
    public class RegistrationMutation : ObjectGraphType
    {
        public RegistrationMutation(IAddressService addressService, ICourseService courseService, ISemesterService semesterService, IStudentService studentService, ISubjectService subjectService)
        {
            FieldAsync<AddressType>
                (
                    "AddAddress",
                    arguments: new QueryArguments
                    (
                        new QueryArgument<NonNullGraphType<AddressInputType>> { Name = "address"}
                    ),
                    resolve: async ctx =>
                    {
                        var address = ctx.GetArgument<Address>("address");

                        return await addressService.Add(address);
                    }
                );

            FieldAsync<CourseType>
                (
                    "AddCourse",
                    arguments: new QueryArguments
                    (
                        new QueryArgument<NonNullGraphType<CourseInputType>> { Name = "course"}
                     ),
                    resolve: async ctx =>
                    {
                        var course = ctx.GetArgument<Course>("course");

                        return await courseService.Add(course);
                    }
                );

            FieldAsync<SemesterType>
                (
                    "AddSemester",
                    arguments: new QueryArguments
                    {
                        new QueryArgument<NonNullGraphType<SemesterInputType>> { Name = "semester"}
                    },
                    resolve: async ctx =>
                    {
                        var semester = ctx.GetArgument<Semester>("semester");

                        return await semesterService.Add(semester);
                    }
                );

            FieldAsync<StudentType>
                (
                    "addStudent",
                    arguments: new QueryArguments
                    {
                        new QueryArgument<NonNullGraphType<StudentInputType>> { Name = "student"}
                    },
                    resolve: async ctx =>
                    {
                        var student = ctx.GetArgument<Student>("student");

                        return await studentService.Add(student);
                    }
                );

            FieldAsync<SubjectType>
                (
                    "addSubject",
                    arguments: new QueryArguments
                    {
                        new QueryArgument<NonNullGraphType<SubjectInputType>> { Name = "subject"}
                    },
                    resolve: async ctx =>
                    {
                        var subject = ctx.GetArgument<Subject>("subject");

                        return await subjectService.Add(subject);
                    }
                );
        }
    }
}
