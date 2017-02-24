using Domain.Entities;
using Domain.Queries.Criteries;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Queries.ConcreteQueries
{
    public class GetRentPointByAdressQuery : IQuery<AdressCriterion, RentPoint>
    {
        private readonly IRepository<RentPoint> _rentPointRepository;
        public GetRentPointByAdressQuery(IRepository<RentPoint> rentPointRepository)
        {
            _rentPointRepository = rentPointRepository;
        }
        public RentPoint Ask(AdressCriterion criterion)
        {
            return _rentPointRepository.All().SingleOrDefault(x => x.Adress == criterion.Adress);
        }
    }
}
