using System;
using System.Collections.Generic;
using System.Text;
using FoodCalc.Common;

namespace FoodCalc.Domain
{
    public class NutrientContaining
    {
        public int FoodId { get; set; }
        public Food Food { get; set; }

        public int NutrientId { get; set; }
        public Nutrient Nutrient { get; set; }

        public Measurement NutrientMeasurement { get; set; }
    }
}
