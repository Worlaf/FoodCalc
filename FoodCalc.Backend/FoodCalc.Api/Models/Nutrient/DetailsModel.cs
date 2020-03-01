namespace FoodCalc.Api.Models.Nutrient
{
    public class DetailsModel
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public float? Energy { get; set; }
    }
}