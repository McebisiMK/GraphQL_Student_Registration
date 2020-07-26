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
                    resolve: async ctx => await courseService.GetById(ctx.Source.CourseId)
                );
            FieldAsync<SemesterType>
                (
                    "Semester",
                    resolve: async ctx => await semesterService.GetById(ctx.Source.SemesterId)
                );
        }
    }
}
