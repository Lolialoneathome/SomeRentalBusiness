using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Domain.Entities.HumanEntity;

namespace Domain.Services
{
    public class RentPointService : IRentPointService
    {
        private readonly IRepository<RentPoint> _rentPointRepository;

        public RentPointService(
            IRepository<RentPoint> rentPointRepository)
        {

            if (rentPointRepository == null)
                throw new ArgumentNullException(nameof(rentPointRepository));

            _rentPointRepository = rentPointRepository;

        }

        public RentPoint AddRentPoint(string name, string adress, Employee employee, decimal money)
        {
            if (money < 0)
                throw new ArgumentOutOfRangeException(nameof(money));

            CashBox cashbox = new CashBox(money);
            Safe safe = new Safe();

            RentPoint rentPoint = new RentPoint(name, adress, employee, safe, cashbox);
            _rentPointRepository.Add(rentPoint);

            return rentPoint;
        }

        public void CloseRentPoint(RentPoint rentPoint)
        {
            throw new NotImplementedException();
        }
    }
}
