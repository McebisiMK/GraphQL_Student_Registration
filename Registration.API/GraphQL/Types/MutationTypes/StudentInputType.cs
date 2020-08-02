using GraphQL.Types;

namespace Registration.API.GraphQL.Types.MutationTypes
{
    public class StudentInputType:InputObjectGraphType
    {
        public StudentInputType()
        {
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<StringGraphType>>("surname");
            Field<NonNullGraphType<IntGraphType>>("addressId");
            Field<NonNullGraphType<StringGraphType>>("cellphone");
            Field<NonNullGraphType<StringGraphType>>("idNumber");
            Field<NonNullGraphType<IntGraphType>>("courseId");
        }
    }
}
