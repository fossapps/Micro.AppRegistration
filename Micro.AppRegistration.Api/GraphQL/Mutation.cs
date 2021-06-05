using GraphQL;
using GraphQL.Types;
using Micro.AppRegistration.Api.GraphQL.Directives.Extensions;
using Micro.AppRegistration.Api.GraphQL.Inputs;
using Micro.AppRegistration.Api.GraphQL.Types;
using Micro.AppRegistration.Api.Internal.UserDataExtensions;
using Micro.AppRegistration.Business.CreateApplication;
using Micro.AppRegistration.Storage;
using Microsoft.AspNetCore.Http;

namespace Micro.AppRegistration.Api.GraphQL
{
    public class Mutation : ObjectGraphType
    {
        public Mutation(IAppRegistrationService createAppService, IHttpContextAccessor contextAccessor)
        {
            FieldAsync<NonNullGraphType<ApplicationType>, Application>(
                "createApplication",
                arguments: new QueryArguments(CreateApplicationInput.BuildArgument()),
                resolve: x => createAppService.CreateApplication(x.GetArgument<CreateApplicationRequest>("input"),
                    contextAccessor.GetUserId())
            ).Authorize();
        }
    }
}
