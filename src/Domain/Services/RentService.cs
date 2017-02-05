namespace Domain.Services
{
    using System;
    using System.Linq;
    using Entities;
    using Entities.Deposits;
    using Repositories;

    public class RentService : IRentService
    {
        private readonly IDepositCalculator _depositCalculator;
        private readonly IRepository<Rent> _rentRepository;



        public RentService(
            IDepositCalculator depositCalculator, 
            IRepository<Rent> rentRepository)
        {
            if (depositCalculator == null)
                throw new ArgumentNullException(nameof(depositCalculator));

            if (rentRepository == null)
                throw new ArgumentNullException(nameof(rentRepository));

            _depositCalculator = depositCalculator;
            _rentRepository = rentRepository;
        }



        public void Take(Client client, Bike bike, Deposit deposit)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (bike == null)
                throw new ArgumentNullException(nameof(bike));

            if (deposit == null)
                throw new ArgumentNullException(nameof(deposit));

            if (bike.RentPoint == null)
                throw new InvalidOperationException("Bike is not on rent point");

            if (!bike.IsFree)
                throw new InvalidOperationException("Bike is not free");


            if (deposit.Type == DepositTypes.Money)
            {
                decimal depositSum = _depositCalculator.Calculate(bike);

                if (((MoneyDeposit)deposit).Sum < depositSum)
                    throw new InvalidOperationException("Deposit sum is not enough");
            }

            bike.RentPoint.PutDeposit(deposit);
            
            Rent rent = new Rent(client, bike, deposit);

            _rentRepository.Add(rent);
        }

        public void Return(Bike bike, RentPoint rentPoint)
        {
            if (bike == null)
                throw new ArgumentNullException(nameof(bike));

            if (rentPoint == null)
                throw new ArgumentNullException(nameof(rentPoint));

            Rent rent = _rentRepository
                .All()
                .SingleOrDefault(
                    x => x.Bike == bike && !x.IsEnded);

            if (rent == null)
                throw new InvalidOperationException("Rent not found");

            rent.End(rentPoint);
        }
    }
}