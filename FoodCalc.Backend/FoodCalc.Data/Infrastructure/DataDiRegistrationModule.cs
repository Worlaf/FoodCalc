using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Builder;
using FoodCalc.Common.Infrastructure;
using FoodCalc.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FoodCalc.Data.Infrastructure
{
    public class DataDiRegistrationModule : DiRegistrationModuleBase
    {
        public DataDiRegistrationModule(Func<IRegistrationBuilder<object, object, object>, IRegistrationBuilder<object, object, object>> lifetimeScopeConfigurator) : base(lifetimeScopeConfigurator)
        {
        }

        protected override IEnumerable<IRegistrationBuilder<object, object, object>> RegisterTypesWithDefaultLifetimeScope(ContainerBuilder builder)
        {
            yield return builder.RegisterAssemblyTypes(typeof(EntityRepositoryBase<>).Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces();

            yield return builder.Register(c =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<FoodCalcDbContext>();
                optionsBuilder.UseSqlServer(c.Resolve<IConfiguration>().GetConnectionString(FoodCalcDbContext.ConnectionStringName));

                return optionsBuilder.Options;
            }).As<DbContextOptions>();

            yield return builder.RegisterType<FoodCalcDbContext>().AsSelf();
            yield return builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
        }
    }
}
