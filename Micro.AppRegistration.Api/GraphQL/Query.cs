using GraphQL;
using GraphQL.Types;
using Micro.AppRegistration.Api.GraphQL.DataLoaders;
using Micro.AppRegistration.Api.GraphQL.Directives.Extensions;
using Micro.AppRegistration.Api.GraphQL.Types;
using Micro.AppRegistration.Api.Internal.UserDataExtensions;
using Micro.AppRegistration.Business.ListApplication;
using Micro.AppRegistration.Business.Verification;
using Micro.AppRegistration.Storage;
using Micro.GraphQL.Federation;
using Microsoft.AspNetCore.Http;

namespace Micro.AppRegistration.Api.GraphQL
{
    public sealed class Query : Query<EntityType>
    {
        public Query(
            ApplicationByIdLoader applicationLoader,
            IHttpContextAccessor httpContextAccessor,
            IListApplicationService listApplicationService,
            IVerifySecretService verifySecretService)
        {
            Field<ApplicationType, Application>()
                .Name("application")
                .ResolveAsync(x => verifySecretService.Verify(httpContextAccessor.GetAppCredentials()));
            Field<ApplicationType, Application>()
                .Name("applicationById").Argument<NonNullGraphType<StringGraphType>>("id")
                .ResolveAsync(async x =>
                {
                    var application = await applicationLoader.LoadAsync(x.GetArgument<string>("id")).GetResultAsync();
                    return await listApplicationService.VerifyOwnership(application, httpContextAccessor.GetUserId());
                })
                .Authorize();
        }
    }
}
