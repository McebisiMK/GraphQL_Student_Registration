using GraphQL.Types;
using Registration.Entities.Models;

namespace Registration.API.GraphQL.Types
{
    public class SemesterType : ObjectGraphType<Semester>
    {
        public SemesterType()
        {
            Field(s => s.Id);
            Field(s => s.Description);
        }
    }
}
