using System.Threading;
using System.Threading.Tasks;
using FoodCalc.Data.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace FoodCalc.Services.Infrastructure
{
    public class FoodCalcDbTransactionalBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly FoodCalcDbContext _dbContext;

        public FoodCalcDbTransactionalBehavior(IUnitOfWork unitOfWork, FoodCalcDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var strategy = _dbContext.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync<object, TResponse>(null, async (dbc, s, ct) =>
            {
                await using var transaction = await dbc.Database.BeginTransactionAsync(ct);

                var result = await next();
                
                await _unitOfWork.CommitAsync(ct);
                await transaction.CommitAsync(ct);

                return result;
            },null, cancellationToken);
        }
    }
}