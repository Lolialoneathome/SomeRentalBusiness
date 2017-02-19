using Domain.Entities;
using System;
using Domain.Entities.HumanEntity;

namespace Domain.Services
{
    public interface IReserveService
    {
        void ReserveBike(Bike bike, Client client, DateTime toTime);
        bool IsActiveReserveOnBike(Bike bike);
        Reserve GetOpenReserveByBike(Bike bike);
    }
}
