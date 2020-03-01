using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Builder;
using Autofac.Extensions.DependencyInjection;
using FoodCalc.Api.Controllers;
using FoodCalc.Api.Infrastructure;
using FoodCalc.Data.Infrastructure;
using FoodCalc.Services.Handlers.Food;
using FoodCalc.Services.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace FoodCalc.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Func<IRegistrationBuilder<object, object, object>, IRegistrationBuilder<object, object, object>> LifetimeScopeConfigurator { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            LifetimeScopeConfigurator = registrationBuilder => registrationBuilder.InstancePerLifetimeScope();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateFoodHandler).Assembly);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FoodCalcDbTransactionalBehavior<,>));
            
            services.AddSingleton(Configuration);
            
            services.AddControllers();

            services.AddSwaggerGen(o =>
                {
                    o.SwaggerDoc(SwaggerDocNames.PublicApi, new OpenApiInfo {Title = "Public API", Version = "alpha"});
                });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new DataDiRegistrationModule(LifetimeScopeConfigurator));
            builder.RegisterModule(new ServicesDiRegistrationModule(LifetimeScopeConfigurator));
            builder.RegisterAssemblyTypes(typeof(NutrientsController).Assembly).AssignableTo<ControllerBase>().AsSelf();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
                if (!SqlServerDbUtil.DatabaseExists(Configuration.GetConnectionString(FoodCalcDbContext.ConnectionStringName)))
                {
                    var container = app.ApplicationServices.GetAutofacRoot();
                    var databaseInitializer = container.Resolve<IDatabaseInitializer>();

                    databaseInitializer.InitializeAsync().Wait();
                }
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            if (!env.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(o =>
                {
                    o.SwaggerEndpoint($"/swagger/{SwaggerDocNames.PublicApi}/swagger.json", "Public API alpha");
                });
            }

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
