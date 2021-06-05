namespace Micro.AppRegistration.Api.GraphQL.Types
{
    public class EntityType : Micro.GraphQL.Federation.Types.EntityType
    {
        public EntityType()
        {
            Type<ApplicationType>();
            Type<UserType>();
        }
    }
}
