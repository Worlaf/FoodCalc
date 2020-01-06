using System;
using System.Collections.Generic;
using System.Text;
using FoodCalc.Data.Infrastructure;
using FoodCalc.Domain;

namespace FoodCalc.Data.Repositories
{
    public interface INutrientRepository : IEntityRepository<Nutrient>
    {
    }

    public class NutrientRepository: EntityRepositoryBase<Nutrient>, INutrientRepository
    {
        public NutrientRepository(FoodCalcDbContext dbContext) : base(dbContext)
        {
        }
    }
}
