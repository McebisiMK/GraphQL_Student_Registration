using GraphQL.Types;
using Registration.Entities.Models;

namespace Registration.API.GraphQL_Types.QueryTypes
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
