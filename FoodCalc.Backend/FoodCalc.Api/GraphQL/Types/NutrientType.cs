using FoodCalc.Domain;
using GraphQL.Types;

namespace FoodCalc.Api.GraphQL.Types
{
    public class NutrientType : ObjectGraphType<Nutrient>
    {
        public NutrientType()
        {
            Field(n => n.Id);
            Field(n => n.Name);
            Field(n => n.Energy, nullable: true);
            Field<NutrientType>(nameof(Nutrient.Parent));
        }
    }
}