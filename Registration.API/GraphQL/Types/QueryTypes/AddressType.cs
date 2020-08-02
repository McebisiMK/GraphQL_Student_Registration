using GraphQL.Types;
using Registration.Entities.Models;

namespace Registration.API.GraphQL_Types.QueryTypes
{
    public class AddressType : ObjectGraphType<Address>
    {
        public AddressType()
        {
            Field(a => a.Id);
            Field(a => a.Unit);
            Field(a => a.Street);
            Field(a => a.Town);
            Field(a => a.Province);
        }
    }
}
