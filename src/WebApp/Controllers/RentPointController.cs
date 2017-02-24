using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Queries;
using Domain.Commands;
using Domain.Entities.HumanEntity;
using Domain.Entities;
using Domain.Commands.CommandContext;
using Domain.Queries.Criterion;
using InfrastructureDb.Commands.CommandContext;
using WebApp.ViewModels;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class RentPointController : Controller
    {
        private readonly IQueryBuilder _queryBuilder;
        private readonly ICommandBuilder _commandBuilder;

        public RentPointController(
            IQueryBuilder queryBuilder,
            ICommandBuilder commandBuilder)
        {
            _queryBuilder = queryBuilder;
            _commandBuilder = commandBuilder;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            _commandBuilder.Execute(
                new LoadRentPointsCommandContext());
           
            return View();
        }

        public IActionResult List()
        {
            _commandBuilder.Execute(
                new LoadRentPointsCommandContext());

            var allRentPoints = _queryBuilder
                .For<IEnumerable<RentPoint>>()
                .With(new EmptyCriterion()
                );

            return View(allRentPoints);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RentPointViewModel vm)
        {
            _commandBuilder.Execute(
                new AddRentPointCommandContext
                {
                    Name = "nu",
                    Adress = "hz",
                    Employee = new Employee("s", "aa", "fff")
                });

            var allRentPoints = _queryBuilder
                .For<IEnumerable<RentPoint>>()
                .With(new EmptyCriterion()
                );

            return RedirectToAction("List");
        }


    }
}
