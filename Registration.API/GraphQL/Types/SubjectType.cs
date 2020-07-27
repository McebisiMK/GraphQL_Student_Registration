using GraphQL.Types;
using Registration.Entities.Models;
using Registration.Service.Contracts;

namespace Registration.API.GraphQL.Types
{
    public class SubjectType : ObjectGraphType<Subject>
    {
        public SubjectType(ICourseService courseService, ISemesterService semesterService)
        {
            Field(s => s.Id);
            Field(s => s.Name);
            Field(s => s.CourseId);
            Field(s => s.SemesterId);
            FieldAsync<CourseType>
                (
                    "Course",
                    resolve: async ctx =>
                    {
                        var courseId = ctx.Source.CourseId;

                        return await courseService.GetById(courseId);
                    }
                );
            FieldAsync<SemesterType>
                (
                    "Semester",
                    resolve: async ctx =>
                    {
                        var semesterId = ctx.Source.SemesterId;

                        return await semesterService.GetById(semesterId);
                    }
                );
        }
    }
}
