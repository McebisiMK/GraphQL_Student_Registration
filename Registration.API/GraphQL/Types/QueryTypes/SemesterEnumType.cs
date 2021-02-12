using GraphQL.Types;
using Registration.Entities.Models;

namespace Registration.API.GraphQL_Types.QueryTypes
{
    public class SemesterEnumType : EnumerationGraphType<Semester>
    {
        public SemesterEnumType()
        {
            Name = "Semester";
            Description = "Enumeration for semester object";
        }
    }
}
