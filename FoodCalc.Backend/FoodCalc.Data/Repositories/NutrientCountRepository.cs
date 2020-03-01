using FoodCalc.Data.Infrastructure;
using FoodCalc.Domain;

namespace FoodCalc.Data.Repositories
{
    public interface INutrientCountRepository : IEntityRepository<NutrientCount>
    {
    }
    
    public class NutrientCountRepository : EntityRepositoryBase<NutrientCount>, INutrientCountRepository
    {
        public NutrientCountRepository(FoodCalcDbContext dbContext) : base(dbContext)
        {
        }
    }
}