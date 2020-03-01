using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCalc.Domain
{
    public class Nutrient : EntityBase
    {
        public int? ParentId { get; set; }
        public Nutrient Parent { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Энергия в кКал/г
        /// </summary>
        public float? Energy { get; set; }
    }
}
