using System;
using Micro.AppRegistration.Api.GraphQL.Types;
using Micro.AppRegistration.Storage;
using Micro.GraphQL.Federation;

namespace Micro.AppRegistration.Api.GraphQL
{
    public sealed class Query : Query<EntityType>
    {
        public Query()
        {
            Field<ApplicationType, Application>().Name("application").Resolve(x => new Application
            {
                Id = "something",
                Name = "test-app",
                Secret = null,
                User = "user",
                CreatedAt = DateTime.Now
            });
        }
    }
}
