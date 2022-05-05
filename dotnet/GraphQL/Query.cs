using GraphQL;
using GraphQL.Types;
using PackingOptimization.GraphQL.Types;
using PackingOptimization.Data;
using System;

namespace PackingOptimization.GraphQL
{
    [GraphQLMetadata("Query")]
    public class Query : ObjectGraphType<object>
    {
        public Query(IMerchantSettingsRepository _merchantSettingsRepository)
        {
            Name = "Query";

            FieldAsync<AppSettingsType>(
                "getAppSettings",
                resolve: async context =>
                {
                    return await context.TryAsyncResolve(
                        async c => await _merchantSettingsRepository.GetMerchantSettings());
                }
            );
        }
    }
}