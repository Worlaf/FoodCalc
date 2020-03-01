using System.Threading.Tasks;
using FoodCalc.Api.Models;
using FoodCalc.Api.Models.Nutrient;
using FoodCalc.Data.Repositories;
using FoodCalc.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FoodCalc.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NutrientsController : ControllerBase
    {
        private readonly INutrientRepository _nutrientRepository;

        public NutrientsController(INutrientRepository nutrientRepository)
        {
            _nutrientRepository = nutrientRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NutrientDetailsModel>> Details(int id)
        {
            var model = await _nutrientRepository.GetByIdAsync(id, n => new NutrientDetailsModel
            {
                Energy = n.Energy,
                Id = n.Id,
                Name = n.Name,
                ParentId = n.ParentId
            });

            return Ok(model);
        }

        [HttpGet]
        public async Task<ActionResult<ListResponse<NutrientDetailsModel>>> List()
        {
            var items = await _nutrientRepository.GetAllAsync(n => new NutrientDetailsModel
            {
                Energy = n.Energy,
                Id = n.Id,
                Name = n.Name,
                ParentId = n.ParentId
            });

            return Ok(new ListResponse<NutrientDetailsModel>
            {
                Items = items
            });
        }
    }
}