using GraphQL;
using GraphQL.Types;
using Registration.API.GraphQL_Mutations;
using Registration.API.GraphQL_Queries;

namespace Registration.API.GraphQL_Schema
{
    public class RegistrationSchema : Schema
    {
        public RegistrationSchema(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
            Query = dependencyResolver.Resolve<RegistrationQuery>();
            Mutation = dependencyResolver.Resolve<RegistrationMutation>();
        }
    }
}
