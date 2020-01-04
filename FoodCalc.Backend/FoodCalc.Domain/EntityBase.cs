using System;

namespace FoodCalc.Domain
{
    public abstract class EntityBase
    {
        public int Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
