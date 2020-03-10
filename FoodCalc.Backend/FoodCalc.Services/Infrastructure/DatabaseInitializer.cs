using System.Collections.Generic;
using System.Threading.Tasks;
using FoodCalc.Common;
using FoodCalc.Data.Infrastructure;
using FoodCalc.Domain;
using FoodCalc.Services.Handlers.Food;
using FoodCalc.Services.Handlers.Nutrient;
using MediatR;

namespace FoodCalc.Services.Infrastructure
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
    
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly FoodCalcDbContext _dbContext;
        private readonly IMediator _mediator;
        private readonly Dictionary<string, Nutrient> _nutrients = new Dictionary<string, Nutrient>();

        public DatabaseInitializer(IMediator mediator, FoodCalcDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        public async Task InitializeAsync()
        {
            await _dbContext.Database.EnsureCreatedAsync();
            
            await SetupNutrients();
            await SetupFood();
        }

        private async Task SetupNutrients()
        {
            await CreateNutrient("Белки", 4.1f);
            await CreateNutrient("Жиры", 9.29f);
            await CreateNutrient("Углеводы", 4.1f);
            await CreateNutrient("Пищевые волокна");

            var macroelementGroup = await CreateNutrient("Макроэлементы");
            await CreateNutrient("Углерод, C", parentId: macroelementGroup.Id);
            await CreateNutrient("Водород, H", parentId: macroelementGroup.Id);
            await CreateNutrient("Кислород, O", parentId: macroelementGroup.Id);
            await CreateNutrient("Азот, N", parentId: macroelementGroup.Id);
            await CreateNutrient("Фосфор, P", parentId: macroelementGroup.Id);
            await CreateNutrient("Сера, S", parentId: macroelementGroup.Id);
            await CreateNutrient("Калий, K", parentId: macroelementGroup.Id);
            await CreateNutrient("Кальций, Ca", parentId: macroelementGroup.Id);
            await CreateNutrient("Магний, Mg", parentId: macroelementGroup.Id);
            await CreateNutrient("Натрий, Na", parentId: macroelementGroup.Id);
            await CreateNutrient("Хлор, Cl", parentId: macroelementGroup.Id);

            var microelementGroup = await CreateNutrient("Микроэлементы");
            await CreateNutrient("Бром, Br", parentId: microelementGroup.Id);
            await CreateNutrient("Бор, B", parentId: microelementGroup.Id);
            await CreateNutrient("Кобальт, Co", parentId: microelementGroup.Id);
            await CreateNutrient("Кремний, Si", parentId: microelementGroup.Id);
            await CreateNutrient("Марганец, Mn", parentId: microelementGroup.Id);
            await CreateNutrient("Медь, Cu", parentId: microelementGroup.Id);
            await CreateNutrient("Молибден, Mo", parentId: microelementGroup.Id);
            await CreateNutrient("Никель, Ni", parentId: microelementGroup.Id);
            await CreateNutrient("Селен, Se", parentId: microelementGroup.Id);
            await CreateNutrient("Фтор, F", parentId: microelementGroup.Id);
            await CreateNutrient("Железо, Fe", parentId: microelementGroup.Id);
            await CreateNutrient("Йод, I", parentId: microelementGroup.Id);
            await CreateNutrient("Хром, Cr", parentId: microelementGroup.Id);
            await CreateNutrient("Цинк, Zn", parentId: microelementGroup.Id);
        }

        private async Task<Nutrient> CreateNutrient(string name, float? energy = null, int? parentId = null)
        {
            var nutrient = await _mediator.Send(new CreateNutrientRequest
            {
                Name = name,
                Energy = energy,
                ParentId = parentId
            });
            
            _nutrients.Add(name, nutrient);

            return nutrient;
        }

        private async Task SetupFood()
        {
            var egg = await CreateFood("Куриное яйцо");
            await AddNutrient(egg, "Белки", 12.57M);
            await AddNutrient(egg, "Жиры", 12.02M);
            await AddNutrient(egg, "Углеводы", 0.67M);
            
        }

        private Task<Food> CreateFood(string name)
        {
            return _mediator.Send(new CreateFoodRequest
            {
                Name = name
            });
        }

        private Task<NutrientCount> AddNutrient(Food food, string nutrientKey,
            decimal nutrientCountInGramsPer100GramsOfFood)
        {
            return AddNutrient(food, _nutrients[nutrientKey], nutrientCountInGramsPer100GramsOfFood);
        }
        
        private Task<NutrientCount> AddNutrient(Food food, Nutrient nutrient, decimal nutrientCountInGramsPer100GramsOfFood)
        {
            return _mediator.Send(new AddFoodNutrientRequest
            {
                FoodId = food.Id,
                NutrientId = nutrient.Id,
                NutrientCountInGramsPer100GramsOfFood = new DecimalValueRange(nutrientCountInGramsPer100GramsOfFood)
            });
        }
    }
}