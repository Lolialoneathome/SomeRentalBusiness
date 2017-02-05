namespace ConsoleApp
{
    using App;
    using Autofac;
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

            containerBuilder.RegisterType<App>();

            IContainer container = containerBuilder.Build();



            App app = container.Resolve<App>();

            app.AddBike("Кама", 50);
            app.AddBike("Кама", 100);



            container.Dispose();
        }
    }
}
