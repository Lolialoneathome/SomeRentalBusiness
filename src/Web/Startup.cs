using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Domain.Repositories;
using Domain.Services;
using Domain.Queries.Criteries;
using Domain.Queries;
using Domain.Commands.ConcreteCommand;
using Autofac.TypedFactory;
using Domain.Commands;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using InfrastructureDb.Commands;
using InfrastructureDb.Loaders;

namespace Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        public IContainer ApplicationContainer { get; private set; }
        public IConfigurationRoot Configuration { get; private set; }


        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var containerBuilder = new ContainerBuilder();

            containerBuilder
                .RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .SingleInstance();

            containerBuilder
                .RegisterType<BikeNameVerifier>()
                .As<IBikeNameVerifier>();

            containerBuilder
                .RegisterType<BikeService>()
                .As<IBikeService>();

            containerBuilder
                .RegisterType<RentPointService>()
                .As<IRentPointService>();

            containerBuilder
                .RegisterType<EmployeeService>()
                .As<IEmployeeService>();

            containerBuilder
                .RegisterType<RentService>()
                .As<IRentService>();

            containerBuilder
                .RegisterType<DepositCalculator>()
                .As<IDepositCalculator>();

            containerBuilder
                .RegisterType<RentSumCalculate>()
                .As<IRentSumCalculate>();

            containerBuilder
                .RegisterType<ReserveService>()
                .As<IReserveService>();

            containerBuilder
                .RegisterType<RentPointLoader>()
                .As<IRentPointLoader>();

            containerBuilder.RegisterAssemblyTypes(typeof(RentPointCriterion).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IQuery<,>));
            containerBuilder.RegisterGeneric(typeof(QueryFor<>)).As(typeof(IQueryFor<>));
            containerBuilder.RegisterTypedFactory<IQueryBuilder>().InstancePerLifetimeScope();
            containerBuilder.RegisterTypedFactory<IQueryFactory>().InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(typeof(AddRentPointCommand).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(ICommand<>));
            containerBuilder.RegisterType<CommandBuilder>().As<ICommandBuilder>().InstancePerLifetimeScope();
            containerBuilder.RegisterTypedFactory<ICommandFactory>().InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(typeof(LoadRentPointsCommand).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(ICommand<>));

            containerBuilder.Populate(services); 
            this.ApplicationContainer = containerBuilder.Build();

            return new AutofacServiceProvider(this.ApplicationContainer);

        }


        public void Configure(
            IApplicationBuilder app,
            ILoggerFactory loggerFactory,
            IApplicationLifetime appLifetime)
        {
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "RentPoints",
                   defaults: new { controller = "{controller}", action = "List" },
                   template: "{controller}s");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


            appLifetime.ApplicationStopped.Register(() => this.ApplicationContainer.Dispose());
        }
    }
}
