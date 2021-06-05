using GraphQL.Types;

namespace Micro.AppRegistration.Api.GraphQL.Directives
{
    public class AuthorizeDirective : DirectiveGraphType
    {
        public const string DirectiveName = "authorize";
        public override bool? Introspectable => true;

        public AuthorizeDirective() : base(
            DirectiveName,
            DirectiveLocation.Field,
            DirectiveLocation.Mutation,
            DirectiveLocation.Query,
            DirectiveLocation.FieldDefinition)
        {
        }
    }
}
