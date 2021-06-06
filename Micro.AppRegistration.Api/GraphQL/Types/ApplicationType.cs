using Micro.AppRegistration.Api.GraphQL.DataLoaders;
using Micro.AppRegistration.Storage;
using Micro.GraphQL.Federation;

namespace Micro.AppRegistration.Api.GraphQL.Types
{
    public sealed class ApplicationType : ObjectGraphType<Application>
    {
        public ApplicationType(ApplicationByIdLoader applicationLoader)
        {
            Name = "Application";
            Key("id");
            Field("id", x => x.Id).Description("application id");
            Field("name", x => x.Name).Description("name of application given by user");
            Field("secret", x => x.Secret, true).Description("app_secret (only visible during creation of app)");
            Field("created_at", x => x.CreatedAt).Description("application creation time");
            Field<global::GraphQL.Types.NonNullGraphType<UserType>, User>().Name("owner")
                .Resolve(x => new User {Id = x.Source.User});
            ResolveReferenceAsync(async x =>
            {
                var id = x.Arguments["id"].ToString();
                return await applicationLoader.LoadAsync(id).GetResultAsync();
            });
        }
    }
}
