using GraphQL.Types;

namespace Registration.API.GraphQL.Types.MutationTypes
{
    public class CourseInputType : InputObjectGraphType
    {
        public CourseInputType()
        {
            Field<NonNullGraphType<StringGraphType>>("name");
        }
    }
}
