using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;
using Domain.Queries;
using Domain.Commands;
using Domain.Commands.CommandContext;
using Domain.Entities;
using Domain.Queries.Criteries;
using Domain.Queries.Criterion;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class BikeController : Controller
    {

        private readonly IQueryBuilder _queryBuilder;
        private readonly ICommandBuilder _commandBuilder;

        public BikeController(
            IQueryBuilder queryBuilder,
            ICommandBuilder commandBuilder)
        {
            _queryBuilder = queryBuilder;
            _commandBuilder = commandBuilder;
        }

        public IActionResult List()
        {
            var allBikes = _queryBuilder
                .For<IEnumerable<Bike>>()
                .With(new EmptyCriterion()
                );

            return View(allBikes);
        }

        public IActionResult Create(string adress)
        {
            BikeViewModel bikeVm = new BikeViewModel
            {
                RentPointAdress = adress
            };
            return View(bikeVm);
        }

        [HttpPost]
        public IActionResult Create(BikeViewModel vm)
        {
            _commandBuilder.Execute(new AddBikeCommandContext
            {
                Name = vm.Name,
                Cost = vm.Cost,
                HourCost = vm.HourCost
            });

            Bike currentBike = _queryBuilder
                .For<Bike>()
                .With(new BikeNameCriterion
                {
                    Name = vm.Name
                });

            RentPoint currentRentPoint = _queryBuilder
                .For<RentPoint>()
                .With
                (new AdressCriterion
                {
                    Adress = vm.RentPointAdress
                });

            _commandBuilder.Execute(new MoveBikeCommandContext
            {
                Bike = currentBike,
                RentPoint = currentRentPoint
            });

            return RedirectToAction("Edit", "RentPoint", new { adress = vm.RentPointAdress });
        }
    }
}
