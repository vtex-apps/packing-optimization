using GraphQL;
using GraphQL.Types;
using PackingOptimization.Data;
using PackingOptimization.Models;
using PackingOptimization.GraphQL.Types;
using System;

namespace PackingOptimization.GraphQL
{
  [GraphQLMetadata("Mutation")]
  public class Mutation : ObjectGraphType<object>
  {
    public Mutation(IMerchantSettingsRepository _merchantSettingsRepository)
    {
        Name = "Mutation";

        FieldAsync<BooleanGraphType>(
            "saveAppSetting",
            arguments: new QueryArguments(
                new QueryArgument<AppSettingsInput> { Name = "appSetting" }
            ),
            resolve: async context =>
            {
                var appSettings = context.GetArgument<MerchantSettings>("appSetting");
                return await _merchantSettingsRepository.SetMerchantSettings(appSettings);
            }
        );
    }
  }
}