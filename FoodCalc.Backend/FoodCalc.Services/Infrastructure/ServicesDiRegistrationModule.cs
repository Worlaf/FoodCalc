using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Builder;
using FoodCalc.Common.Infrastructure;

namespace FoodCalc.Services.Infrastructure
{
    public class ServicesDiRegistrationModule : DiRegistrationModuleBase
    {
        public ServicesDiRegistrationModule(Func<IRegistrationBuilder<object, object, object>, IRegistrationBuilder<object, object, object>> lifetimeScopeConfigurator) : base(lifetimeScopeConfigurator)
        {
        }

        protected override IEnumerable<IRegistrationBuilder<object, object, object>> RegisterTypesWithDefaultLifetimeScope(ContainerBuilder builder)
        {
            yield return builder.RegisterType<DatabaseInitializer>().AsImplementedInterfaces();
        }
    }
}