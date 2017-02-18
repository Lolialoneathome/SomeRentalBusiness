using Domain.Entities;
using Domain.Queries.Criterion;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Queries.ConcreteQueries
{
    public class GetAllBikesQuery : IQuery<EmptyCriterion, IEnumerable<Bike>>
    {
        protected readonly IRepository<Bike> _bikeRepository;
        public GetAllBikesQuery(IRepository<Bike> bikeRepository)
        {
            _bikeRepository = bikeRepository;
        }

        public IEnumerable<Bike> Ask(EmptyCriterion criterion)
        {
            return _bikeRepository.All();
        }
    }
}
