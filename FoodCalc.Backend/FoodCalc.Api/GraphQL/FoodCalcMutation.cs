using FoodCalc.Api.GraphQL.Types;
using FoodCalc.Domain;
using FoodCalc.Services.Handlers.Food;
using FoodCalc.Services.Handlers.Nutrient;
using GraphQL.Types;
using MediatR;

namespace FoodCalc.Api.GraphQL
{
    public class FoodCalcMutation : ObjectGraphType
    {
        public FoodCalcMutation(IMediator mediator)
        {
            const string nutrientArgumentName = "nutrient";
            Field<NutrientType>("createNutrient",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<NutrientInputType>>
                {
                    Name = nutrientArgumentName
                }),
                resolve: context =>
                {
                    var createNutrientRequest = context.GetArgument<CreateNutrientRequest>(nutrientArgumentName);
                    return mediator.Send(createNutrientRequest).Result;
                }
            );
            
            const string foodArgumentName = "food";
            Field<FoodType>("createFood",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<FoodInputType>>
                {
                    Name = foodArgumentName
                }),
                resolve: context =>
                {
                    var createFoodRequest = context.GetArgument<CreateFoodRequest>(foodArgumentName);
                    return mediator.Send(createFoodRequest).Result;
                }
            );
            
            const string foodNutrientCountArgumentName = "foodNutrientCount";
            Field<NutrientCountType>("addNutrientToFood",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<NutrientCountInputType>>
                {
                    Name = foodNutrientCountArgumentName
                }),
                resolve: context =>
                {
                    // todo: разобраться с моделями для GraphQL
                    var nutrientCount = context.GetArgument<NutrientCount>(foodNutrientCountArgumentName);
                    return mediator.Send(new AddFoodNutrientRequest
                    {
                        FoodId = nutrientCount.FoodId,
                        NutrientId = nutrientCount.NutrientId,
                        NutrientCountInGramsPer100GramsOfFood = nutrientCount.CountInGramsPer100GramsOfFood
                    }).Result;
                }
            );
        }
    }
}