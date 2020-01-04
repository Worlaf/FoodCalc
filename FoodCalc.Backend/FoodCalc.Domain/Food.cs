﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCalc.Domain
{
    public class Food : EntityBase
    {
        public int? ParentId { get; set; }
        public Food Parent { get; set; }

        public string Name { get; set; }

        public ICollection<NutrientContaining> NutrientsPer100Gram { get; set; }
    }
}
