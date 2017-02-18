using Domain.Entities;
using Domain.Queries.Criteries;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Queries.ConcreteQueries
{
    public class GetOpenReserveByBikeQuery : IQuery<ByBikeCriterion, Reserve>
    {

        protected readonly IReserveService _reserveService;

        public GetOpenReserveByBikeQuery(IReserveService reserveservice)
        {
            _reserveService = reserveservice;
        }

        public Reserve Ask(ByBikeCriterion criterion)
        {
            return _reserveService.GetOpenReserveByBike(criterion.Bike);
        }
    }
}
