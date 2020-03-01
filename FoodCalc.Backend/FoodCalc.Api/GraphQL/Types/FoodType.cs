using FoodCalc.Domain;
using GraphQL.Types;

namespace FoodCalc.Api.GraphQL.Types
{
    public class FoodType : ObjectGraphType<Food>
    {
        public FoodType()
        {
            Field(f => f.Name);
            Field(f => f.Id);
            Field<ListGraphType<NutrientCountType>>(nameof(Food.NutrientsPer100Gram));
            Field<FoodType>(nameof(Food.Parent));
        }
        
    }
}