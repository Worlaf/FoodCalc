using FoodCalc.Data.Infrastructure;
using FoodCalc.Domain;

namespace FoodCalc.Data.Repositories
{
    public interface IFoodRepository : IEntityRepository<Food>
    {
    }

    public class FoodRepository : EntityRepositoryBase<Food>, IFoodRepository
    {
        public FoodRepository(FoodCalcDbContext dbContext) : base(dbContext)
        {
        }
    }
}