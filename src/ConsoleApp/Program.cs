namespace ConsoleApp
{
    using App;
    using Autofac;
    using Domain.Entities;
    using Domain.Entities.Deposits;
    using Domain.Repositories;
    using Domain.Services;
    using System;
    using System.Linq;

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
            //app.AddBike("Кама", 100, myRentPoint);

            Bike iChooseThisBike = app.GetBikes().FirstOrDefault(x => x.Name == "Кама");

            app.ReserveBike(client, iChooseThisBike, DateTime.UtcNow.AddDays(1));

            app.GetBikeInRent(client, iChooseThisBike, deposit);

            bool iBrokeBike = true;
            //app.GetBikeInRent(client, iChooseThisBike, deposit);
            app.ReturnBike(iChooseThisBike, otherRentPoint, iBrokeBike);
            container.Dispose();
        }
    }
}
