using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FoodCalc.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
        Task CommitAsync(CancellationToken cancellationToken);
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly FoodCalcDbContext _dbContext;

        public UnitOfWork(FoodCalcDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
