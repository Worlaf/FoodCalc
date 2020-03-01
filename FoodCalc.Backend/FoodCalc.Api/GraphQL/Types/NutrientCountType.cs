using FoodCalc.Domain;
using GraphQL.Types;

namespace FoodCalc.Api.GraphQL.Types
{
    public class NutrientCountType : ObjectGraphType<NutrientCount>
    {
        public NutrientCountType()
        {
            Field<FoodType>(nameof(NutrientCount.Food));
            Field<NutrientType>(nameof(NutrientCount.Nutrient));
            Field<DecimalValueRangeType>(nameof(NutrientCount.CountInGramsPer100GramsOfFood));
        }
        
    }
}