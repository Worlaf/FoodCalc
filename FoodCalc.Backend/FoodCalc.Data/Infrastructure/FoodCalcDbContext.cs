using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using FoodCalc.Domain;

namespace FoodCalc.Data.Infrastructure
{
    public class FoodCalcDbContext : DbContext
    {
        public const string ConnectionStringName = "FoodCalcSqlServerConnectionString";

        public DbSet<Nutrient> Nutrients { get; set; }

        public FoodCalcDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
