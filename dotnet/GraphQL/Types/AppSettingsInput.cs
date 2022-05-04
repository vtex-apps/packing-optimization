using GraphQL;
using GraphQL.Types;
using PackingOptimization.Models;
using PackingOptimization.Data;

namespace PackingOptimization.GraphQL.Types
{
    [GraphQLMetadata("AppSettingsInput")]
    public class AppSettingsInput : InputObjectGraphType<MerchantSettings>
    {
        public AppSettingsInput()
        {
            Name = "AppSettingsInput";

            Field(b => b.ContainerList, type: typeof(ListGraphType<ContainerInput>)).Description("List of Containers");
        }
    }
}