using GraphQL.Types;

namespace Micro.AppRegistration.Api.GraphQL.Inputs
{
    public class CreateApplicationInput : InputObjectGraphType
    {
        public static QueryArgument BuildArgument()
        {
            return new QueryArgument<NonNullGraphType<CreateApplicationInput>> { Name = "input"};
        }

        public CreateApplicationInput()
        {
            Name = "CreateApplicationInput";
            Field<NonNullGraphType<StringGraphType>>("name");
        }
    }
}
