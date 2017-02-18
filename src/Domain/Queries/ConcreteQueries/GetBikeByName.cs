using Domain.Entities;
using Domain.Queries.Criteries;
using Domain.Repositories;
using System.Linq;

namespace Domain.Queries.ConcreteQueries
{
    public class GetBikeByName : IQuery<BikeNameCriterion, Bike>
    {

        protected readonly IRepository<Bike> _bikerepository;

        public GetBikeByName(IRepository<Bike> bikerepository)
        {
            _bikerepository = bikerepository;
        }

        public Bike Ask(BikeNameCriterion criterion)
        {
            return _bikerepository.All().SingleOrDefault(x => x.Name == criterion.Name);
        }
    }
}
