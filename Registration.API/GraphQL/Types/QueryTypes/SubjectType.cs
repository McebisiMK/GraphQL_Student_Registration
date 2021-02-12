using GraphQL.Types;
using Registration.Entities.Models;
using Registration.Service.Contracts;

namespace Registration.API.GraphQL_Types.QueryTypes
{
    public class SubjectType : ObjectGraphType<Subject>
    {
        public SubjectType(ICourseService courseService)
        {
            Field(s => s.Id);
            Field(s => s.Name);
            Field(s => s.CourseId);
            Field<SemesterEnumType>("Semester", "Enumeration for semester object");
            FieldAsync<CourseType>
                (
                    "Course",
                    resolve: async ctx =>
                    {
                        var courseId = ctx.Source.CourseId;

                        return await courseService.GetById(courseId);
                    }
                );
        }
    }
}
