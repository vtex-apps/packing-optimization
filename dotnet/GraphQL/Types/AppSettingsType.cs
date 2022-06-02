using GraphQL;
using GraphQL.Types;
using PackingOptimization.Models;
using PackingOptimization.Data;

namespace PackingOptimization.GraphQL.Types
{
    [GraphQLMetadata("AppSettings")]
    public class AppSettingsType : ObjectGraphType<MerchantSettings>
    {
        public AppSettingsType(IMerchantSettingsRepository _merchantSettingsRepository)
        {
            Name = "AppSettings";

            Field(b => b.ContainerList, type: typeof(ListGraphType<ContainerType>)).Description("List of Containers");
            Field(b => b.AccessKey).Description("Access Key");
        }
    }
}