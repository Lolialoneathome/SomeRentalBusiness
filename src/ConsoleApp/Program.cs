namespace ConsoleApp
{
    using App;
    using Autofac;
    using Domain.Entities;
    using Domain.Repositories;
    using Domain.Services;

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

            containerBuilder.RegisterType<App>();

            IContainer container = containerBuilder.Build();



            App app = container.Resolve<App>();

            Employee myEmployee = app.CreateEmployee("Nya", "Nyan", "Nyanyan");
            RentPoint myRentPoint =  app.AddRentPoint(myEmployee);
            Client client = app.CreateClient("Keke", "Ke", "Kekekeke");

            app.AddBike("Кама", 50, myRentPoint);
            app.AddBike("Кама", 100, myRentPoint);



            container.Dispose();
        }
    }
}
