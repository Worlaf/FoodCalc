using System;
using System.Collections.Generic;
using System.Text;
using FoodCalc.Common;

namespace FoodCalc.Domain
{
    public class NutrientCount : EntityBase
    {
        public int FoodId { get; set; }
        public Food Food { get; set; }

        public int NutrientId { get; set; }
        public Nutrient Nutrient { get; set; }

        public DecimalValueRange CountInGramsPer100GramsOfFood { get; set; }
    }
}
