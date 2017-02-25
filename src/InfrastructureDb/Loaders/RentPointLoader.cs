using Domain.Commands;
using Domain.Commands.CommandContext;
using Domain.Entities;
using Domain.Entities.HumanEntity;
using Domain.Queries;
using Domain.Queries.Criteries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfrastructureDb.Loaders
{
    public class RentPointLoader : IRentPointLoader
    {

        private readonly IQueryBuilder _queryBuilder;
        private readonly ICommandBuilder _commandBuilder;

        public RentPointLoader(
            IQueryBuilder queryBuilder,
            ICommandBuilder commandBuilder)
        {
            _queryBuilder = queryBuilder;
            _commandBuilder = commandBuilder;
        }

        public void GetRentPointsFromMyDb()
        {
            //Это мы как будто из базы получаем эти точки
            var context = new AddRentPointCommandContext
            {
                Name = "rp1",
                Adress = "adress1",
                Employee = new Employee("Nya", "Nyan", "Nyanyan")
            };

            _commandBuilder.Execute(context);

            context = new AddRentPointCommandContext
            {
                Name = "rp2",
                Adress = "adress2",
                Employee = new Employee("Nya2", "Nyan2", "Nyanyan2")
            };

            _commandBuilder.Execute(context);

            context = new AddRentPointCommandContext
            {
                Name = "rp3",
                Adress = "adress3",
                Employee = new Employee("Nya3", "Nyan3", "Nyanyan3")
            };

            _commandBuilder.Execute(context);


            _commandBuilder.Execute(new AddBikeCommandContext
            {
                Name = "Raindow Crash",
                Cost = 10000,
                HourCost = 100
            });

            Bike currentBike = _queryBuilder
                .For<Bike>()
                .With(new BikeNameCriterion
                {
                    Name = "Raindow Crash"
                });

            _commandBuilder.Execute(new MoveBikeCommandContext
            {
                Bike = currentBike,
                RentPoint = context.CreatedRentPoint
            });




        }
    }
}
