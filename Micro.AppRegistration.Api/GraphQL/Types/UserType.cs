using System.Collections.Generic;
using GraphQL.DataLoader;
using GraphQL.Types;
using Micro.AppRegistration.Api.GraphQL.DataLoaders;
using Micro.AppRegistration.Storage;
using Micro.GraphQL.Federation;

namespace Micro.AppRegistration.Api.GraphQL.Types
{
    public sealed class UserType : Micro.GraphQL.Federation.ObjectGraphType<User>
    {
        public UserType(ApplicationByOwnerLoader applicationLoader)
        {
            Name = "User";
            ExtendByKeys("id");
            Field("id", x => x.Id).External();
            Field<NonNullGraphType<ListGraphType<ApplicationType>>, IEnumerable<Application>>().Name("applications")
                .ResolveAsync(x => applicationLoader.LoadAsync(x.Source.Id));

            ResolveReferenceAsync(async ctx =>
            {
                var id = ctx.Arguments["id"].ToString();
                return new User
                {
                    Id = id,
                };
            });
        }
    }

    public class User
    {
        public string Id { set; get; }
    }
}
