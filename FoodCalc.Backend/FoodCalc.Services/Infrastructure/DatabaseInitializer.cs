using System.Threading.Tasks;
using FoodCalc.Data.Infrastructure;
using FoodCalc.Domain;
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

        public DatabaseInitializer(IMediator mediator, FoodCalcDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        public async Task InitializeAsync()
        {
            await _dbContext.Database.EnsureCreatedAsync();
            
            await SetupNutrients();
        }

        private async Task SetupNutrients()
        {
            await CreateNutrient("Белки", 4.1f);
            await CreateNutrient("Жиры", 9.29f);
            await CreateNutrient("Углеводы", 4.1f);

            var macroelementGroup = await CreateNutrient("Макроэлементы");
            await CreateNutrient("Углерод", parentId: macroelementGroup.Id);
            await CreateNutrient("Водород", parentId: macroelementGroup.Id);
            await CreateNutrient("Кислород", parentId: macroelementGroup.Id);
            await CreateNutrient("Азот", parentId: macroelementGroup.Id);
            await CreateNutrient("Фосфор", parentId: macroelementGroup.Id);
            await CreateNutrient("Сера", parentId: macroelementGroup.Id);
            await CreateNutrient("Калий", parentId: macroelementGroup.Id);
            await CreateNutrient("Кальций", parentId: macroelementGroup.Id);
            await CreateNutrient("Магний", parentId: macroelementGroup.Id);
            await CreateNutrient("Натрий", parentId: macroelementGroup.Id);
            await CreateNutrient("Хлор", parentId: macroelementGroup.Id);

            var microelementGroup = await CreateNutrient("Микроэлементы");
            await CreateNutrient("Бром", parentId: microelementGroup.Id);
            await CreateNutrient("Железо", parentId: microelementGroup.Id);
            await CreateNutrient("Йод", parentId: microelementGroup.Id);
            await CreateNutrient("Кобальт", parentId: microelementGroup.Id);
            await CreateNutrient("Марганец", parentId: microelementGroup.Id);
            await CreateNutrient("Медь", parentId: microelementGroup.Id);
            await CreateNutrient("Молибден", parentId: microelementGroup.Id);
            await CreateNutrient("Селен", parentId: microelementGroup.Id);
            await CreateNutrient("Фтор", parentId: microelementGroup.Id);
            await CreateNutrient("Хром", parentId: microelementGroup.Id);
            await CreateNutrient("Цинк", parentId: microelementGroup.Id);
        }

        private Task<Nutrient> CreateNutrient(string name, float? energy = null, int? parentId = null)
        {
            return _mediator.Send(new CreateNutrientRequest
            {
                Name = name,
                Energy = energy,
                ParentId = parentId
            });
        }
    }
}