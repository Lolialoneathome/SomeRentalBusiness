namespace ConsoleApp
{
    using App;
    using Autofac;
    using Autofac.TypedFactory;
    using Domain.Commands;
    using Domain.Commands.ConcreteCommand;
    using Domain.Entities;
    using Domain.Entities.Deposits;
    using Domain.Queries;
    using Domain.Queries.Criteries;
    using Domain.Repositories;
    using Domain.Services;
    using System;
    using System.Linq;
    using System.Reflection;

    public class Program
    {
        public static void Main(string[] args)
        {
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

            containerBuilder.RegisterAssemblyTypes(typeof(RentPointCriterion).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IQuery<,>));
            containerBuilder.RegisterGeneric(typeof(QueryFor<>)).As(typeof(IQueryFor<>));
            containerBuilder.RegisterTypedFactory<IQueryBuilder>().InstancePerLifetimeScope();
            containerBuilder.RegisterTypedFactory<IQueryFactory>().InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(typeof(AddRentPointCommand).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(ICommand<>));
            containerBuilder.RegisterType<CommandBuilder>().As<ICommandBuilder>().InstancePerLifetimeScope();
            containerBuilder.RegisterTypedFactory<ICommandFactory>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<App>();

            IContainer container = containerBuilder.Build();



            App app = container.Resolve<App>();

            Employee myEmployee = app.CreateEmployee("Nya", "Nyan", "Nyanyan");
            RentPoint myRentPoint =  app.AddRentPoint(myEmployee);

            Employee otherEmployee = app.CreateEmployee("otherNya", "otherNyan", "otherNyanyan");
            RentPoint otherRentPoint = app.AddRentPoint(otherEmployee);

            Client client = app.CreateClient("Keke", "Ke", "Kekekeke");
            Client clientWhoWantTakeReservedBike = app.CreateClient("aaa", "a", "aaaaa");

            Deposit deposit = new MoneyDeposit(5000);


            app.AddBike("Кама", 50, 4500, myRentPoint);
            app.AddBike("Rainbow Dash", 50, 5000, myRentPoint);
            app.AddBike("Rainbow Crash", 60, 4700, myRentPoint);
            //app.AddBike("Кама", 100, myRentPoint);

            Bike iChooseThisBike = app.GetBikes().FirstOrDefault(x => x.Name == "Кама");
            

            app.ReserveBike(client, iChooseThisBike, DateTime.UtcNow.AddDays(1));

            //app.GetBikeInRent(client, iChooseThisBike, deposit);

            bool iBrokeBike = true;
            app.GetBikeInRent(client, iChooseThisBike, deposit);
            app.ReturnBike(iChooseThisBike, otherRentPoint, iBrokeBike);

            app.GetRentPoints();

            app.GetAllFreeBikesOnRentPoint(myRentPoint);

            Bike likeItBike = app.GetBikes().FirstOrDefault(x => x.Name == "Rainbow Dash");
            app.ReserveBike(client, likeItBike, DateTime.UtcNow.AddDays(1));

            app.GetOpenReserveByBike(likeItBike);

            app.GetBikes();

            container.Dispose();
        }
    }
}
