using GraphQL.Types;

namespace Registration.API.GraphQL.Types.MutationTypes
{
    public class SemesterInputType:InputObjectGraphType
    {
        public SemesterInputType()
        {
            Field<NonNullGraphType<StringGraphType>>("description");
        }
    }
}
