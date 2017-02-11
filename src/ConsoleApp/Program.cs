namespace ConsoleApp
{
    using App;
    using Autofac;
    using Domain.Entities;
    using Domain.Entities.Deposits;
    using Domain.Repositories;
    using Domain.Services;
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

            containerBuilder.RegisterType<App>();

            IContainer container = containerBuilder.Build();



            App app = container.Resolve<App>();

            Employee myEmployee = app.CreateEmployee("Nya", "Nyan", "Nyanyan");
            RentPoint myRentPoint =  app.AddRentPoint(myEmployee);
            Client client = app.CreateClient("Keke", "Ke", "Kekekeke");
            Deposit deposit = new MoneyDeposit(5000);


            app.AddBike("Кама", 50, myRentPoint);
            //app.AddBike("Кама", 100, myRentPoint);

            Bike iChooseThisBike = app.GetBikes().FirstOrDefault(x => x.Name == "Кама");
            app.GetBikeInRent(client, iChooseThisBike, deposit);


            container.Dispose();
        }
    }
}
