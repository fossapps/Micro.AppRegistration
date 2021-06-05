using System;
using Micro.AppRegistration.Api.GraphQL.Types;
using Micro.GraphQL.Federation;

namespace Micro.AppRegistration.Api.GraphQL
{
    public class AppRegistrationSchema : Schema<EntityType>
    {
        public AppRegistrationSchema(IServiceProvider services, Query query) : base(services)
        {
            Query = query;
        }
    }
}
