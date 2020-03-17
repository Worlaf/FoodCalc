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
            await CreateNutrient("Белки", 4.1f, isRequired: true);
            await CreateNutrient("Жиры", 9.29f, isRequired: true);
            await CreateNutrient("Углеводы", 4.1f, isRequired: true);
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
            
            var vitamineGroup = await CreateNutrient("Витамины");
            await CreateNutrient("Ретинол (A)", parentId: vitamineGroup.Id);
            await CreateNutrient("Тиамин (B1)", parentId: vitamineGroup.Id);
            await CreateNutrient("Рибофлавин (B2)", parentId: vitamineGroup.Id);
            await CreateNutrient("Пантотеновая кислота (B5)", parentId: vitamineGroup.Id);
            await CreateNutrient("Фолацин (B9)", parentId: vitamineGroup.Id);
            await CreateNutrient("Кобаламин (B12)", parentId: vitamineGroup.Id);
            
            await CreateNutrient("Холестерин");
        }

        private async Task<Nutrient> CreateNutrient(string name, float? energy = null, int? parentId = null, bool isRequired = false)
        {
            var nutrient = await _mediator.Send(new CreateNutrientRequest
            {
                Name = name,
                Energy = energy,
                ParentId = parentId,
                IsRequired = isRequired
            });
            
            _nutrients.Add(name, nutrient);

            return nutrient;
        }

        private async Task SetupFood()
        {
            var egg = await CreateFood("Куриное яйцо");
            await AddNutrient(egg, "Белки", Gram(12.6M));
            await AddNutrient(egg, "Жиры", Gram(12.02M));
            await AddNutrient(egg, "Углеводы", Gram(0.67M));
            
            await AddNutrient(egg, "Ретинол (A)", Microgram(140M));
            await AddNutrient(egg, "Тиамин (B1)", Microgram(66M));
            await AddNutrient(egg, "Рибофлавин (B2)", Milligram(0.5M));
            await AddNutrient(egg, "Пантотеновая кислота (B5)", Milligram(1.4M));
            await AddNutrient(egg, "Фолацин (B9)", Microgram(44M));
            await AddNutrient(egg, "Кобаламин (B12)", Microgram(1.11M));
            
            await AddNutrient(egg, "Кальций, Ca", Milligram(50M));
            await AddNutrient(egg, "Железо, Fe", Milligram(1.2M));
            await AddNutrient(egg, "Магний, Mg", Milligram(10M));
            await AddNutrient(egg, "Фосфор, P", Milligram(172M));
            await AddNutrient(egg, "Калий, K", Milligram(126M));
            await AddNutrient(egg, "Цинк, Zn", Milligram(1M));
            
            await AddNutrient(egg, "Холестерин", Milligram(424M));
            
            decimal Microgram(decimal value) => value / 1000000;
            decimal Milligram(decimal value) => value / 1000;
            decimal Gram(decimal value) => value;
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