using Micro.AppRegistration.Storage;
using Micro.GraphQL.Federation;

namespace Micro.AppRegistration.Api.GraphQL.Types
{
    public sealed class ApplicationType : ObjectGraphType<Application>
    {
        public ApplicationType()
        {
            Name = "Application";
            Key("id");
            Field("id", x => x.Id).Description("application id");
            Field("name", x => x.Name).Description("name of application given by user");
            Field("secret", x => x.Secret).Description("app_secret (only visible during creation of app)");
            Field("created_at", x => x.CreatedAt).Description("application creation time");
        }
    }
}
