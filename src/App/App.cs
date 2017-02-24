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
    using Domain.Entities.HumanEntity;

    public class App
    {

        private readonly IEmployeeService _employeeService;
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<Employee> _employeeRepository;

        private readonly IQueryBuilder _queryBuilder;
        private readonly ICommandBuilder _commandBuilder;

        

        public App(
            IRepository<Client> clientRepository,
            IRepository<Employee> employeeRepository,
            IEmployeeService employeeService,
            IQueryBuilder queryBuilder,
            ICommandBuilder commandBuilder)
        {
            _clientRepository = clientRepository;
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
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

        public RentPoint AddRentPoint(string name, string adress, Employee myEmployee)
        {
            var context = new AddRentPointCommandContext
            {
                Name = name,
                Adress = adress,
                Employee = myEmployee
            };
            _commandBuilder.Execute(context);

            return context.CreatedRentPoint;
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

        public void ReserveBike(Client client, Bike bike, DateTime toTime)
        {
            _commandBuilder.Execute(
                new ReserveBikeCommandContext
                {
                    Bike = bike,
                    Client = client,
                    ToTime = toTime
                });
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

    }
}
