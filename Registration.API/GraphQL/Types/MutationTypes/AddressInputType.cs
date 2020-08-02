using GraphQL.Types;

namespace Registration.API.GraphQL_Types.MutationTypes
{
    public class AddressInputType : InputObjectGraphType
    {
        public AddressInputType()
        {
            Field<StringGraphType>("unit");
            Field<NonNullGraphType<StringGraphType>>("street");
            Field<NonNullGraphType<StringGraphType>>("town");
            Field<NonNullGraphType<StringGraphType>>("province");
        }
    }
}
