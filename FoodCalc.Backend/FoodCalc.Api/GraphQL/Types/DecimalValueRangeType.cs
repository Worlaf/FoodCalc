using FoodCalc.Common;
using GraphQL.Types;

namespace FoodCalc.Api.GraphQL.Types
{
    public class DecimalValueRangeType : ObjectGraphType<DecimalValueRange>
    {
        public DecimalValueRangeType()
        {
            Field(r => r.Min);
            Field(r => r.Max);
        }
    }
}