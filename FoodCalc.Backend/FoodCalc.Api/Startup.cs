using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Builder;
using Autofac.Extensions.DependencyInjection;
using FoodCalc.Api.Controllers;
using FoodCalc.Api.GraphQL;
using FoodCalc.Api.GraphQL.Types;
using FoodCalc.Api.Infrastructure;
using FoodCalc.Data.Infrastructure;
using FoodCalc.Services.Handlers.Food;
using FoodCalc.Services.Infrastructure;
using GraphQL;
using GraphQL.Http;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace FoodCalc.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostEnvironment Environment { get; }
        public Func<IRegistrationBuilder<object, object, object>, IRegistrationBuilder<object, object, object>> LifetimeScopeConfigurator { get; }
        
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            LifetimeScopeConfigurator = registrationBuilder => registrationBuilder.InstancePerLifetimeScope();
            Environment = env;
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

            services.AddGraphQL(o => { o.ExposeExceptions = Environment.IsDevelopment(); })
                .AddGraphTypes(ServiceLifetime.Scoped);
            services.AddScoped<IDependencyResolver>(serviceProvider =>
                new FuncDependencyResolver(serviceProvider.GetRequiredService));
            
            // workarounds supposed to make GraphQL work
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            
            services.AddMvc()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                });
            
            
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new DataDiRegistrationModule(LifetimeScopeConfigurator));
            builder.RegisterModule(new ServicesDiRegistrationModule(LifetimeScopeConfigurator));
            builder.RegisterAssemblyTypes(typeof(NutrientsController).Assembly).AssignableTo<ControllerBase>().AsSelf();
            
            builder.RegisterInstance(new DocumentExecuter()).As<IDocumentExecuter>();
            builder.RegisterInstance(new DocumentWriter()).As<IDocumentWriter>();

            builder.RegisterAssemblyTypes(typeof(FoodType).Assembly).AssignableTo<IGraphType>().AsSelf();
            builder.RegisterAssemblyTypes(typeof(FoodSchema).Assembly).AssignableTo<ISchema>().AsSelf();
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

            app.UseGraphQL<FoodSchema>();
            if (!env.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(o =>
                {
                    o.SwaggerEndpoint($"/swagger/{SwaggerDocNames.PublicApi}/swagger.json", "Public API alpha");
                });

                app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());
            }

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
