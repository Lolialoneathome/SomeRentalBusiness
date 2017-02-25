using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Queries;
using Domain.Commands;
using Domain.Entities;
using Domain.Queries.Criterion;
using Domain.Entities.HumanEntity;
using Domain.Commands.CommandContext;
using InfrastructureDb.Commands.CommandContext;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQueryBuilder _queryBuilder;
        private readonly ICommandBuilder _commandBuilder;

        public void Init()
        {
            _commandBuilder.Execute(
                new LoadRentPointsCommandContext());
        }

        public HomeController(
            IQueryBuilder queryBuilder,
            ICommandBuilder commandBuilder)
        {
            _queryBuilder = queryBuilder;
            _commandBuilder = commandBuilder;

            Init();
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View( );
        }
    }
}
