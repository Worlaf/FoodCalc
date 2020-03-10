using FoodCalc.Domain;
using GraphQL.Language.AST;
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
            Field(n => n.ParentId, nullable: true);
            Field(n => n.DescriptionMarkdown, nullable: true);
            Field<NutrientType>(nameof(Nutrient.Parent));
        }
    }

    public class NutrientInputType : InputObjectGraphType<Nutrient>
    {
        public NutrientInputType()
        {
            Field(n => n.Name);
            Field(n => n.Energy, nullable: true);
            Field(n => n.ParentId, nullable: true);
        }
    }
}