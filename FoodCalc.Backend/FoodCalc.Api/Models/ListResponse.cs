using System.Collections.Generic;

namespace FoodCalc.Api.Models
{
    public class ListResponse<TModel>
    {
        public IReadOnlyCollection<TModel> Items { get; set; }
    }
}