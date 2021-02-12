using GraphQL.Types;

namespace Registration.API.GraphQL.Types.MutationTypes
{
    public class SubjectInputType:InputObjectGraphType
    {
        public SubjectInputType()
        {
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<StringGraphType>>("semester");
            Field<NonNullGraphType<IntGraphType>>("courseId");
        }
    }
}
