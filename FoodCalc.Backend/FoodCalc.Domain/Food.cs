using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCalc.Domain
{
    public class Food : EntityBase
    {
        public int? ParentId { get; set; }
        public Food Parent { get; set; }

        public string Name { get; set; }
        public string DescriptionMarkdown { get; set; }

        public ICollection<NutrientCount> NutrientsPer100Gram { get; set; }
    }
}
