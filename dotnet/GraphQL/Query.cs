using GraphQL;
using GraphQL.Types;
using System;
namespace PackingOptimization.GraphQL
{
    [GraphQLMetadata("Query")]
    public class Query : ObjectGraphType<object>
    {
        public Query()
        {
            Name = "Query";

            Field<StringGraphType>(
                "test",
                resolve: context =>
                {
                    return "result";
                }
            );
        }
    }
}