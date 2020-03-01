using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using FoodCalc.Common;
using FoodCalc.Domain;

namespace FoodCalc.Data.Infrastructure
{
    public class FoodCalcDbContext : DbContext
    {
        public const string ConnectionStringName = "FoodCalcSqlServerConnectionString";

        public DbSet<Nutrient> Nutrients { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<NutrientCount> NutrientCount { get; set; }

        public FoodCalcDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NutrientCount>().OwnsOne<DecimalValueRange>(c => c.CountInGramsPer100GramsOfFood);
        }
    }
}
