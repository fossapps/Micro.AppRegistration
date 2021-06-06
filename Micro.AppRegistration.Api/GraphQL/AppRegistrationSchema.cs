using System;
using Micro.AppRegistration.Api.GraphQL.Directives;
using Micro.AppRegistration.Api.GraphQL.Types;
using Micro.GraphQL.Federation;

namespace Micro.AppRegistration.Api.GraphQL
{
    public class AppRegistrationSchema : Schema<EntityType>
    {
        public AppRegistrationSchema(IServiceProvider services, Query query, Mutation mutation) : base(services)
        {
            Query = query;
            Mutation = mutation;
            Directives.Register(new AuthorizeDirective());
            RegisterVisitor(typeof(AuthorizeDirectiveVisitor));
        }
    }
}
