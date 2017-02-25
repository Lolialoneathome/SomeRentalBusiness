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
using Domain.Queries.Criteries;

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

        public IActionResult List()
        {

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
                    Name = vm.Name,
                    Adress = vm.Adress,
                    Employee = new Employee(vm.EmployeeName, vm.EmployeeSurname, vm.EmployeePatronymic)
                });

            var allRentPoints = _queryBuilder
                .For<IEnumerable<RentPoint>>()
                .With(new EmptyCriterion()
                );

            return RedirectToAction("List");
        }

        public IActionResult Edit(string adress)
        {
            RentPoint rp = _queryBuilder
                .For<RentPoint>()
                .With
                (new AdressCriterion
                {
                    Adress = adress
                });
            var vm = new RentPointViewModel(rp);
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(RentPointViewModel rp)
        {
            return RedirectToAction("List");
        }



        public IActionResult Details(string adress)
        {
            RentPoint rp = _queryBuilder
                .For<RentPoint>()
                .With
                (new AdressCriterion
                {
                    Adress = adress
                });
            var vm = new RentPointViewModel(rp);
            return View(vm);
        }


    }
}
