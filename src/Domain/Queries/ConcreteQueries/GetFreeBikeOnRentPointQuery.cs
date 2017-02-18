using Domain.Entities;
using Domain.Queries.Criteries;
using System.Collections.Generic;
using System;
using System.Linq;
using Domain.Repositories;

namespace Domain.Queries.ConcreteQueries
{
    public class GetFreeBikeOnRentPointQuery : IQuery<RentPointCriterion, IEnumerable<Bike>>
    {

        
        private readonly IRepository<Bike> _bikeRepository;
        public GetFreeBikeOnRentPointQuery(IRepository<Bike> bikeRepository)
        {
            _bikeRepository = bikeRepository;
        }


        //!!! Или не нужно было инъектить _bikeRepository, а искать по criterion.RentPoint.Bikes
        //!!! Или вообще вынести в RentPointService весь поиск и здесь дергать только сервис?
        public IEnumerable<Bike> Ask(RentPointCriterion criterion)
        {
            return _bikeRepository.All().Where(x => x.RentPoint == criterion.RentPoint && x.IsFree);
        }
    }
}
