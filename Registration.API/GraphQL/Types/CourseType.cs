using GraphQL.Types;
using Registration.Entities.Models;

namespace Registration.API.GraphQL.Types
{
    public class CourseType : ObjectGraphType<Course>
    {
        public CourseType()
        {
            Field(c => c.Id);
            Field(c => c.Name);
        }
    }
}
