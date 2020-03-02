using FoodCalc.Domain;
using GraphQL.Types;

namespace FoodCalc.Api.GraphQL.Types
{
    public class NutrientCountType : ObjectGraphType<NutrientCount>
    {
        public NutrientCountType()
        {
            Field<FoodType>(nameof(NutrientCount.Food));
            Field(c => c.FoodId);
            Field<NutrientType>(nameof(NutrientCount.Nutrient));
            Field(c => c.NutrientId);
            Field<DecimalValueRangeType>(nameof(NutrientCount.CountInGramsPer100GramsOfFood));
        }
    }

    public class NutrientCountInputType : InputObjectGraphType<NutrientCount>
    {
        public NutrientCountInputType()
        {
            Field(c => c.FoodId);
            Field(c => c.NutrientId);
            Field<DecimalValueRangeInputType>(nameof(NutrientCount.CountInGramsPer100GramsOfFood));
        }
    }
}