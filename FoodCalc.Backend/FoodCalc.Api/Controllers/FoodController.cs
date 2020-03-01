using System.Threading.Tasks;
using FoodCalc.Api.Models;
using FoodCalc.Api.Models.Food;
using FoodCalc.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FoodCalc.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class FoodController :ControllerBase
    {
        private readonly IFoodRepository _foodRepository;

        public FoodController(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        [HttpGet("{foodId}")]
        public async Task<ActionResult<FoodDetailsModel>> Details(int foodId)
        {
            var model = await _foodRepository.GetByIdAsync(foodId, f => new FoodDetailsModel
            {
                Id = f.Id,
                Name = f.Name
            });

            return Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var items = await _foodRepository.GetAllAsync(f => new FoodDetailsModel
            {
                Id = f.Id,
                Name = f.Name
            });

            return Ok(new ListResponse<FoodDetailsModel>
            {
                Items = items
            });
        }
    }
}