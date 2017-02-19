namespace Domain.Services
{
    using Entities;
    using Entities.Deposits;
    using Entities.HumanEntity;

    public interface IRentService
    {
        void Take(Client client, Bike bike, Deposit deposit);

        void Return(Bike bike, RentPoint rentPoint, bool IsBroken);
    }
}