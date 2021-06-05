using Micro.GraphQL.Federation;

namespace Micro.AppRegistration.Api.GraphQL.Types
{
    public sealed class UserType : ObjectGraphType<User>
    {
        public UserType()
        {
            Name = "User";
            ExtendByKeys("id");
            Field("id", x => x.Id);
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
