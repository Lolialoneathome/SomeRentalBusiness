using Domain.Entities;
using Domain.Queries.Criteries;
using Domain.Queries.Criterion;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Queries.ConcreteQueries
{
    public class GetAllRentPointQuery : IQuery<EmptyCriterion, IEnumerable<RentPoint>>
    {

        protected readonly IRepository<RentPoint> _rentPointRepository;
        public GetAllRentPointQuery(IRepository<RentPoint> rentPointRepository)
        {
            _rentPointRepository = rentPointRepository;
        }
        
        public IEnumerable<RentPoint> Ask(EmptyCriterion criterion)
        {
            return _rentPointRepository.All();
        }
    }
}
