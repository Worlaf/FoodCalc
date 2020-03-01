using System.Threading;
using System.Threading.Tasks;
using FoodCalc.Data.Infrastructure;
using FoodCalc.Data.Repositories;
using MediatR;

namespace FoodCalc.Services.Handlers.Food
{
    public class DeleteFoodRequest : IRequest
    {
        public int FoodId { get; set; }
    }
    
    public class DeleteFoodHandler : IRequestHandler<DeleteFoodRequest>
    {
        private readonly IFoodRepository _foodRepository;
        private readonly INutrientCountRepository _nutrientCountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFoodHandler(IUnitOfWork unitOfWork, IFoodRepository foodRepository, INutrientCountRepository nutrientCountRepository)
        {
            _unitOfWork = unitOfWork;
            _foodRepository = foodRepository;
            _nutrientCountRepository = nutrientCountRepository;
        }

        public async Task<Unit> Handle(DeleteFoodRequest request, CancellationToken cancellationToken)
        {
            var food = await _foodRepository.GetByIdAsync(request.FoodId);

            // todo: check that cascade deletion works
            
            _foodRepository.Delete(food);
            await _unitOfWork.CommitAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}