using GraphQL.Types;
using Registration.API.GraphQL.Types.MutationTypes;
using Registration.API.GraphQL_Types.MutationTypes;
using Registration.API.GraphQL_Types.QueryTypes;
using Registration.Entities.Models;
using Registration.Service.Contracts;

namespace Registration.API.GraphQL_Mutations
{
    public class RegistrationMutation : ObjectGraphType
    {
        public RegistrationMutation(IAddressService addressService, ICourseService courseService, IStudentService studentService, ISubjectService subjectService)
        {
            FieldAsync<AddressType>
                (
                    "AddAddress",
                    arguments: new QueryArguments
                    (
                        new QueryArgument<NonNullGraphType<AddressInputType>> { Name = "address" }
                    ),
                    resolve: async ctx =>
                    {
                        var address = ctx.GetArgument<Address>("address");

                        return await addressService.Add(address);
                    }
                );

            FieldAsync<AddressType>
                (
                    "updateAddress",
                    arguments: new QueryArguments
                    (
                        new QueryArgument<NonNullGraphType<AddressInputType>> { Name = "address" },
                        new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "addressId" }
                    ),
                    resolve: async ctx =>
                    {
                        var address = ctx.GetArgument<Address>("address");
                        var addressId = ctx.GetArgument<int>("addressId");
                        var existingAddress = addressService.GetById(addressId);

                        if (existingAddress == null)
                            return null;

                        address.Id = addressId;

                        return await addressService.Update(addressId, address);
                    }
                );

            FieldAsync<CourseType>
                (
                    "AddCourse",
                    arguments: new QueryArguments
                    (
                        new QueryArgument<NonNullGraphType<CourseInputType>> { Name = "course" }
                     ),
                    resolve: async ctx =>
                    {
                        var course = ctx.GetArgument<Course>("course");

                        return await courseService.Add(course);
                    }
                );

            FieldAsync<CourseType>
                (
                    "updateCourse",
                    arguments: new QueryArguments
                    (
                        new QueryArgument<NonNullGraphType<CourseInputType>> { Name = "course" },
                        new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "courseId" }
                    ),
                    resolve: async ctx =>
                    {
                        var course = ctx.GetArgument<Course>("course");
                        var courseId = ctx.GetArgument<int>("courseId");
                        var existingCourse = await courseService.GetById(courseId);

                        if (existingCourse == null)
                            return null;

                        course.Id = courseId;

                        return await courseService.Update(courseId, course);
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

            FieldAsync<StudentType>
                (
                    "updateStudent",
                    arguments: new QueryArguments
                    (
                        new QueryArgument<NonNullGraphType<StudentInputType>> { Name = "student" },
                        new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "studentNumber" }
                    ),
                    resolve: async ctx =>
                    {
                        var student = ctx.GetArgument<Student>("student");
                        var studentNumber = ctx.GetArgument<string>("studentNumber");
                        var existingStudent = await studentService.GetByStudentNumber(studentNumber);

                        if (existingStudent == null)
                            return null;

                        student.StudentNumber = studentNumber;

                        return await studentService.Update(studentNumber, student);
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

            FieldAsync<SubjectType>
                (
                    "updateSubject",
                    arguments: new QueryArguments
                    (
                        new QueryArgument<NonNullGraphType<SubjectInputType>> { Name = "subject" },
                        new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "subjectId" }
                    ),
                    resolve: async ctx =>
                    {
                        var subject = ctx.GetArgument<Subject>("subject");
                        var subjectId = ctx.GetArgument<int>("subjectId");
                        var existingSubject = subjectService.GetById(subjectId);

                        if (existingSubject == null)
                            return null;

                        subject.Id = subjectId;

                        return await subjectService.Update(subjectId, subject);
                    }
                );
        }
    }
}
