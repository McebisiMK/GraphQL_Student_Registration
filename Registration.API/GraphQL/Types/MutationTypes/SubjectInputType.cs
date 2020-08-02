using GraphQL.Types;

namespace Registration.API.GraphQL.Types.MutationTypes
{
    public class SubjectInputType:InputObjectGraphType
    {
        public SubjectInputType()
        {
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<IntGraphType>>("courseId");
            Field<NonNullGraphType<IntGraphType>>("semesterId");
        }
    }
}
