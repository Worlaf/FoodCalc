using System.Collections.Generic;
using System.Linq;
using FoodCalc.Data.Repositories;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace FoodCalc.Api.GraphQL.Types
{
    public class FoodCalcQuery : ObjectGraphType
    {
        public FoodCalcQuery(IFoodRepository foodRepository, INutrientRepository nutrientRepository)
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
                    var query = foodRepository
                        .QueryAll()
                        .Include(f => f.NutrientsPer100Gram)
                        .ThenInclude(c => c.Nutrient);
                    
                    if (foodId.HasValue)
                        return query.Where(f => f.Id == foodId).ToArray();
                    return
                        query.ToArray();
                });
            
            Field<ListGraphType<NutrientType>>("nutrients",
                arguments: new QueryArguments(new List<QueryArgument>
                {
                    new QueryArgument<IdGraphType>
                    {
                        Name = idArgumentName
                    }
                }), resolve: context =>
                {
                    var nutrientId = context.GetArgument<int?>(idArgumentName);
                    var query = nutrientRepository
                        .QueryAll()
                        .Include(n => n.Parent);
                    
                    if (nutrientId.HasValue)
                        return query.Where(f => f.Id == nutrientId).ToArray();
                    return
                        query.ToArray();
                });
        }
    }
}