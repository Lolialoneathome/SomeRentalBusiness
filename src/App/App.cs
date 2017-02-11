namespace App
{
    using System;
    using System.Collections.Generic;
    using Domain.Entities;
    using Domain.Repositories;
    using Domain.Services;
    using Domain.Entities.Deposits;
    using System.Linq;

    public class App
    {
        //test
        //NYANYAN
        private readonly IRepository<Bike> _bikeRepository;
        private readonly IBikeService _bikeService;
        private readonly IRentService _rentService;
        private readonly IEmployeeService _employeeService;
        private readonly IRentPointService _rentPointService;
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<RentPoint> _rentPointRepository;


        public App(
            IRepository<Client> clientRepository,
            IRepository<Employee> employeeRepository,
            IRepository<Bike> bikeRepository,
            IRepository<RentPoint> rentPointRepository,
            IBikeService bikeService,
            IEmployeeService employeeService,
            IRentService rentService,
            IRentPointService rentPointService)
        {
            _clientRepository = clientRepository;
            _employeeRepository = employeeRepository;
            _bikeRepository = bikeRepository;
            _rentPointRepository = rentPointRepository;
            _bikeService = bikeService;
            _employeeService = employeeService;
            _rentPointService = rentPointService;
            _rentService = rentService;
        }



        public void AddBike(string name, decimal hourCost, RentPoint myRentPoint)
        {
            _bikeService.AddBike(name, hourCost);
            Bike currentBike = _bikeRepository.All().SingleOrDefault(x => x.Name == name);
            _bikeService.MoveBike(currentBike, myRentPoint);
        }

        public RentPoint AddRentPoint(Employee myEmployee)
        {
            return _rentPointService.AddRentPoint(myEmployee);
        }

        public IEnumerable<Bike> GetBikes()
        {
            return _bikeRepository.All();
        }

        public IEnumerable<RentPoint> GetRentPoints()
        {
            return _rentPointRepository.All();
        }

        public Employee CreateEmployee(string surname, string firstname, string patronymic)
        {
            Employee employee = new Employee(surname, firstname, patronymic);
            _employeeRepository.Add(employee);
            return employee;
            //_employeeService.AddEmployee(surname, firstname, patronymic);
        }

        public Client CreateClient(string surname, string firstname, string patronymic)
        {
            Client client = new Client(surname, firstname, patronymic);
            _clientRepository.Add(client);
            return client;
        }

        public void GetBikeInRent(Client client, Bike bike, Deposit deposit)
        {
            _rentService.Take(client, bike, deposit);
        }

        public void ReturnBike(Bike bike, RentPoint rentPoint)
        {
            _rentService.Return(bike, rentPoint);
        }
    }
}
