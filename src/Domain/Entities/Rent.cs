namespace Domain.Entities
{
    using System;
    using Deposits;

    public class Rent : IEntity
    {
        protected internal Rent(Client client, Bike bike, Deposit deposit)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (bike == null)
                throw new ArgumentNullException(nameof(bike));

            if (deposit == null)
                throw new ArgumentNullException(nameof(deposit));

            StartedAt = DateTime.UtcNow;
            StartRentPoint = bike.RentPoint;
            Client = client;
            Bike = bike;
            Deposit = deposit;
            HourCost = bike.HourCost;
        }



        public readonly RentPoint StartRentPoint;

        public readonly DateTime StartedAt;

        public RentPoint EndRentPoint { get; protected set; }

        public DateTime? EndedAt { get; protected set; }
        
        public bool IsEnded => EndedAt.HasValue;

        // TODO Сделать нормальный читаемый геттер
        public decimal? Sum => IsEnded
            ? (decimal?) Math.Round(Math.Ceiling((EndedAt.Value - StartedAt).TotalHours) * (double) HourCost, 2)
            : null;

        public readonly Client Client;

        public readonly Bike Bike;

        public readonly Deposit Deposit;

        public readonly decimal HourCost;



        protected internal void End(RentPoint rentPoint)
        {
            if (rentPoint == null)
                throw new ArgumentNullException(nameof(rentPoint));
            if (IsEnded)
                throw new InvalidOperationException("Rent is already ended");

            EndedAt = DateTime.UtcNow;
            EndRentPoint = rentPoint;
            EndRentPoint.CashBox.PutMoney(Sum.Value);
            Bike.Return();
            EndRentPoint.AddBike(Bike);
            if (StartRentPoint == EndRentPoint)
            {
                EndRentPoint.Safe.ReturnDeposit(Deposit);
            }
            else
            {
                if (Deposit.Type == DepositTypes.Passport)
                        throw new InvalidOperationException("No such passport here");

                EndRentPoint.CashBox.TakeMoney( ((MoneyDeposit)Deposit).Sum );
                StartRentPoint.Safe.MoveMoneyToCashBox(StartRentPoint.CashBox, ((MoneyDeposit)Deposit).Sum);

            }

            
        }
    }
}
