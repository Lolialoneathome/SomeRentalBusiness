namespace App
{
    using System;
    using System.Collections.Generic;
    using Domain.Entities;
    using Domain.Repositories;
    using Domain.Services;
    using Domain.Entities.Deposits;
    using System.Linq;
    using Domain.Queries;
    using Domain.Queries.Criteries;
    using Domain.Commands;
    using Domain.Queries.Criterion;
    using Domain.Commands.CommandContext;

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
        private readonly IReserveService _reserveService;

        private readonly IQueryBuilder _queryBuilder;
        private readonly ICommandBuilder _commandBuilder;

        

        public App(
            IRepository<Client> clientRepository,
            IRepository<Employee> employeeRepository,
            IRepository<Bike> bikeRepository,
            IRepository<RentPoint> rentPointRepository,
            IBikeService bikeService,
            IEmployeeService employeeService,
            IRentService rentService,
            IRentPointService rentPointService,
            IReserveService reserveService,
            IQueryBuilder queryBuilder,
            ICommandBuilder commandBuilder)
        {
            _clientRepository = clientRepository;
            _employeeRepository = employeeRepository;
            _bikeRepository = bikeRepository;
            _rentPointRepository = rentPointRepository;
            _bikeService = bikeService;
            _employeeService = employeeService;
            _rentPointService = rentPointService;
            _rentService = rentService;
            _reserveService = reserveService;
            _queryBuilder = queryBuilder;
            _commandBuilder = commandBuilder;
        }


        public IEnumerable<Bike> GetBikes()
        {
            return _queryBuilder
                .For<IEnumerable<Bike>>()
                .With(
                new EmptyCriterion());
        }

        public IEnumerable<RentPoint> GetRentPoints()
        {
            return _queryBuilder
                .For<IEnumerable<RentPoint>>()
                .With(new EmptyCriterion()
                )
            ;
        }

        public IEnumerable<Bike> GetAllFreeBikesOnRentPoint(RentPoint rentPoint)
        {
            return _queryBuilder
                .For<IEnumerable<Bike>>()
                .With(new RentPointCriterion
                {
                    RentPoint = rentPoint
                }
                )
            ;
        }

        public Reserve GetOpenReserveByBike(Bike bike)
        {
            return _queryBuilder
                .For<Reserve>()
                .With(new ByBikeCriterion
                {
                    Bike = bike
                }
                )
            ;
        }

        public Bike GetBikeByName(string name)
        {
            return _queryBuilder
                .For<Bike>()
                .With(new BikeNameCriterion
                {
                    Name = name
                });
        }

        public void AddBike(string name, decimal hourCost, decimal cost, RentPoint myRentPoint)
        {
            _commandBuilder.Execute( new AddBikeCommandContext
            {
                Name = name,
                Cost = cost,
                HourCost = hourCost
            });

            Bike currentBike = GetBikeByName(name);

            _commandBuilder.Execute(new MoveBikeCommandContext
            {
                Bike = currentBike,
                RentPoint = myRentPoint
            });
        }

        public RentPoint AddRentPoint(Employee myEmployee)
        {
            var context = new AddRentPointCommandContext
            {
                Employee = myEmployee
            };
            _commandBuilder.Execute(context);

            return context.CreatedRentPoint;
        }

        public Employee CreateEmployee(string surname, string firstname, string patronymic)
        {
            Employee employee = new Employee(surname, firstname, patronymic);
            _employeeRepository.Add(employee);
            return employee;
        }

        public Client CreateClient(string surname, string firstname, string patronymic)
        {
            Client client = new Client(surname, firstname, patronymic);
            _clientRepository.Add(client);
            return client;
        }

        public void GetBikeInRent(Client client, Bike bike, Deposit deposit)
        {
            _commandBuilder.Execute(new GetBikeInRentCommandContext
            {
                Client = client,
                Bike = bike,
                Deposit = deposit
            });
        }

        public void ReturnBike(Bike bike, RentPoint rentPoint, bool isBroken)
        {
            _commandBuilder.Execute(
                new ReturnBikeCommandContext
                {
                    Bike = bike,
                    RentPoint = rentPoint,
                    IsBroken = isBroken
                });
        }

        public void ReserveBike(Client client, Bike bike, DateTime endTime)
        {
            _reserveService.ReserveBike(bike, client, endTime);
        }
    }
}
