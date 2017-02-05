namespace Domain.Entities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Deposits;

    public class RentPoint : IEntity
    {
        protected readonly IList<Bike> _bikes = new List<Bike>();
        protected readonly IList<PassportDeposit> _passportDeposits = new List<PassportDeposit>();


        public RentPoint(
            Employee employee,
            decimal money)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));
            if (money < 0)
                throw new ArgumentOutOfRangeException(nameof(money));

            Employee = employee;
            Money = money;
        }


        public decimal Money { get; protected set; }

        public readonly Employee Employee;
        
        public IEnumerable<Bike> Bikes => _bikes.AsEnumerable();

        public IEnumerable<PassportDeposit> PassportDeposits => _passportDeposits.AsEnumerable();


        public void TakeMoney(decimal money)
        {
            if (money < 0)
                throw new ArgumentOutOfRangeException(nameof(money));

            if (money > Money)
                throw new InvalidOperationException("Not enough money");

            Money -= money;
        }

        public void PutMoney(decimal money)
        {
            if (money < 0)
                throw new ArgumentOutOfRangeException(nameof(money));

            Money += money;
        }

        public void PutDeposit(Deposit deposit)
        {
            if (deposit == null)
                throw new ArgumentNullException(nameof(deposit));

            switch (deposit.Type)
            {
                case DepositTypes.Money:
                    PutMoney(((MoneyDeposit)deposit).Sum);
                    break;

                case DepositTypes.Passport:
                    _passportDeposits.Add((PassportDeposit)deposit);
                    break;
            }
        }

        public void ReturnDeposit(Deposit deposit)
        {
            switch (deposit.Type)
            {
                case DepositTypes.Money:
                    TakeMoney(((MoneyDeposit)deposit).Sum);
                    break;

                case DepositTypes.Passport:

                    PassportDeposit currentPassportDeposit = (PassportDeposit) deposit;

                    PassportDeposit passportDeposit = _passportDeposits
                        .SingleOrDefault(x =>
                            x.Number == currentPassportDeposit.Number &&
                            x.Series == currentPassportDeposit.Series);

                    if (passportDeposit == null)
                        throw new InvalidOperationException("No such passport");

                    _passportDeposits.Remove(passportDeposit);

                    break;
            }
        }

        protected internal void AddBike(Bike bike)
        {
            if (bike == null)
                throw new ArgumentNullException(nameof(bike));

            _bikes.Add(bike);
        }

        protected internal void RemoveBike(Bike bike)
        {
            if (bike == null)
                throw new ArgumentNullException(nameof(bike));

            _bikes.Remove(bike);
        }
    }
}