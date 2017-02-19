using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Domain.Entities.HumanEntity;

namespace Domain.Services
{
    public class ReserveService : IReserveService
    {
        private readonly IRepository<Reserve> _reserveRepository;

        public ReserveService(IRepository<Reserve> reserveRepository)
        {
            _reserveRepository = reserveRepository;
        }

        public void ReserveBike(Bike bike, Client client, DateTime toTime)
        {
            if (bike.RentPoint == null)
                throw new InvalidOperationException("Bike is not on rent point");

            if (!bike.IsFree)
                throw new InvalidOperationException("Bike is not free");

            if (bike.IsBroken)
                throw new Exception("Sorry, this bike is broken. Please, choose another.");

            if (IsActiveReserveOnBike(bike))
                throw new InvalidOperationException("Sorry, bike is reserved");

            Reserve reserve = new Reserve(client, bike, toTime);
            _reserveRepository.Add(reserve);
        }


        public bool IsActiveReserveOnBike(Bike bike)
        {
            Reserve existedReserve = _reserveRepository.All().SingleOrDefault(x => (x.Bike == bike && x.Status == ReserveStatus.Wait));
            if (existedReserve != null)
                return true;

            return false;
        }

        public Reserve GetOpenReserveByBike(Bike bike)
        {
            return _reserveRepository.All().SingleOrDefault(x => (x.Bike == bike && x.Status == ReserveStatus.Wait));
        }
    }
}
