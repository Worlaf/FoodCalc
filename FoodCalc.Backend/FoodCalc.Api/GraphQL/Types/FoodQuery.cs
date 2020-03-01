using System.Collections.Generic;
using FoodCalc.Data.Repositories;
using GraphQL;
using GraphQL.Types;

namespace FoodCalc.Api.GraphQL.Types
{
    public class FoodQuery : ObjectGraphType
    {
        public FoodQuery(IFoodRepository foodRepository)
        {
            const string idArgumentName = "id";
            Field<ListGraphType<FoodType>>("food",
                arguments: new QueryArguments(new List<QueryArgument>
                {
                    new QueryArgument<IdGraphType>
                    {
                        Name = idArgumentName
                    }
                }), resolve: context =>
                {
                    var foodId = context.GetArgument<int?>(idArgumentName);
                    if (foodId.HasValue)
                        return foodRepository.GetByIdAsync(foodId.Value).Result;
                    else
                        return foodRepository.GetAllAsync().Result;
                });
        }
    }
}