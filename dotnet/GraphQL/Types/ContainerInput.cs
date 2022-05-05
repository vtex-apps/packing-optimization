using GraphQL;
using GraphQL.Types;
using PackingOptimization.Models;

namespace PackingOptimization.GraphQL.Types
{
    [GraphQLMetadata("ContainerInput")]
    public class ContainerInput : InputObjectGraphType<ContainerObject>
    {
        public ContainerInput()
        {
            Name = "ContainerInput";

            Field(x => x.Id).Description("Id");
            Field(x => x.Length).Description("Length");
            Field(x => x.Width).Description("Width");
            Field(x => x.Height).Description("Height");
            Field(x => x.Description).Description("Description");
        }
    }
}