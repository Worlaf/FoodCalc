using System.Threading;
using System.Threading.Tasks;
using FoodCalc.Data.Infrastructure;
using FoodCalc.Data.Repositories;
using MediatR;

namespace FoodCalc.Services.Handlers.Food
{
    public class CreateFoodRequest : IRequest<Domain.Food>
    {
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string DescriptionMarkdown { get; set; }
    }
    
    public class CreateFoodHandler : IRequestHandler<CreateFoodRequest, Domain.Food>
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateFoodHandler(IFoodRepository foodRepository, IUnitOfWork unitOfWork)
        {
            _foodRepository = foodRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Domain.Food> Handle(CreateFoodRequest request, CancellationToken cancellationToken)
        {
            var food = new Domain.Food
            {
                Name = request.Name,
                ParentId = request.ParentId,
                DescriptionMarkdown = request.DescriptionMarkdown
            };
            
            _foodRepository.Save(food);

            await _unitOfWork.CommitAsync(cancellationToken);

            return food;
        }
    }
}