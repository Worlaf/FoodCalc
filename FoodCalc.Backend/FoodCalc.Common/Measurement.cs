using System;
using System.Collections.Generic;
using System.Text;

namespace FoodCalc.Common
{
    public class Measurement
    {
        public UnitOfMeasurement Unit { get; set; }
        public int MultiplierPower { get; set; }
        public float Value { get; set; }
    }
}
