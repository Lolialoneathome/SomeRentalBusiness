namespace App
{
    using System.Collections.Generic;
    using Domain.Entities;
    using Domain.Repositories;
    using Domain.Services;

    public class App
    {
        //test
        private readonly IRepository<Bike> _bikeRepository;
        private readonly IBikeService _bikeService;
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<Employee> _employeeRepository;



        public App(
            IRepository<Client> clientRepository,
            IRepository<Employee> employeeRepository,
            IRepository<Bike> bikeRepository,
            IBikeService bikeService)
        {
            _clientRepository = clientRepository;
            _employeeRepository = employeeRepository;
            _bikeRepository = bikeRepository;
            _bikeService = bikeService;

        }



        public void AddBike(string name, decimal hourCost)
        {
            _bikeService.AddBike(name, hourCost);
        }

        public IEnumerable<Bike> GetBikes()
        {
            return _bikeRepository.All();
        }
    }
}
