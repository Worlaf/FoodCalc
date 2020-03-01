using System.Threading;
using System.Threading.Tasks;
using FoodCalc.Data.Infrastructure;
using FoodCalc.Data.Repositories;
using MediatR;

namespace FoodCalc.Services.Handlers.Nutrient
{
    public class CreateNutrientRequest : IRequest<Domain.Nutrient>
    {
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public float? Energy { get; set; }
    }
    
    public class CreateNutrientHandler : IRequestHandler<CreateNutrientRequest, Domain.Nutrient>
    {
        private readonly INutrientRepository _nutrientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateNutrientHandler(IUnitOfWork unitOfWork, INutrientRepository nutrientRepository)
        {
            _unitOfWork = unitOfWork;
            _nutrientRepository = nutrientRepository;
        }

        public async Task<Domain.Nutrient> Handle(CreateNutrientRequest request, CancellationToken cancellationToken)
        {
            var nutrient = new Domain.Nutrient
            {
                ParentId = request.ParentId,
                Energy = request.Energy,
                Name = request.Name
            };

            _nutrientRepository.Save(nutrient);

            await _unitOfWork.CommitAsync(cancellationToken);

            return nutrient;
        }
    }
}