namespace FoodCalc.Common
{
    public class DecimalValueRange
    {
        public decimal Min { get; set; }
        public decimal Max { get; set; }

        public DecimalValueRange()
        {
            Min = 0M;
            Max = 0M;
        }
        
        public DecimalValueRange(decimal average, decimal deviation = 0M)
        {
            Min = average - deviation;
            Max = average + deviation;
        }
    }
}