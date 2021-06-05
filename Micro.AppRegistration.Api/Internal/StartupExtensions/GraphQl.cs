using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Execution;
using GraphQL.Server;
using GraphQL.Server.Ui.GraphiQL;
using GraphQL.Server.Ui.Playground;
using GraphQL.SystemTextJson;
using GraphQL.Types;
using Micro.AppRegistration.Api.GraphQL;
using Micro.AppRegistration.Api.GraphQL.Types;
using Micro.GraphQL.Federation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.AppRegistration.Api.Internal.StartupExtensions
{
    public static class GraphQl
    {
        public static void ConfigureGraphql(this IServiceCollection services)
        {
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
            services.AddSingleton<IDocumentExecutionListener, DataLoaderDocumentListener>();

            services.AddTransient<Query>();
            services.AddTransient<ISchema, AppRegistrationSchema>();
            services.AddTransient<ApplicationType>();
            services.AddTransient<UserType>();

            services.EnableFederation<EntityType>();
            services
                .AddGraphQL(options =>
                {
                    options.UnhandledExceptionDelegate = ctx =>
                    {
                        ctx.ErrorMessage = ctx.OriginalException.Message;
                    };
                })
                .AddDataLoader()
                .AddSystemTextJson()
                .AddErrorInfoProvider(opts => opts.ExposeExceptionStackTrace = false);
        }

        public static void SetupGraphQl(this IApplicationBuilder app)
        {
            app.UseGraphQL<ISchema>();

            app.UseGraphQLGraphiQL(new GraphiQLOptions
            {
                SubscriptionsEndPoint = null,
                GraphQLEndPoint = "/graphql"
            }, "/ui/graphql");
            app.UseGraphQLPlayground(new PlaygroundOptions
            {
                GraphQLEndPoint = "/graphql",
            }, "/ui/playground");
        }
    }
}
