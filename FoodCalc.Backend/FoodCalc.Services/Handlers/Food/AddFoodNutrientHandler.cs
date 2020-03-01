using System.Threading;
using System.Threading.Tasks;
using FoodCalc.Common;
using FoodCalc.Data.Infrastructure;
using FoodCalc.Data.Repositories;
using FoodCalc.Domain;
using MediatR;

namespace FoodCalc.Services.Handlers.Food
{
    public class AddFoodNutrientRequest : IRequest<NutrientCount>
    {
        public int FoodId { get; set; }
        public int NutrientId { get; set; }
        public DecimalValueRange NutrientCountInGramsPer100GramsOfFood { get; set; }
    }
    
    public class AddFoodNutrientHandler : IRequestHandler<AddFoodNutrientRequest, NutrientCount>
    {
        private readonly INutrientCountRepository _nutrientCountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddFoodNutrientHandler(INutrientCountRepository nutrientCountRepository, IUnitOfWork unitOfWork)
        {
            _nutrientCountRepository = nutrientCountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<NutrientCount> Handle(AddFoodNutrientRequest request, CancellationToken cancellationToken)
        {
            var nutrientCount = new NutrientCount
            {
                FoodId = request.FoodId,
                NutrientId = request.NutrientId,
                CountInGramsPer100GramsOfFood = request.NutrientCountInGramsPer100GramsOfFood
            };
            
            _nutrientCountRepository.Save(nutrientCount);

            await _unitOfWork.CommitAsync(cancellationToken);

            return nutrientCount;
        }
    }
}