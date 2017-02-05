namespace Domain.Services
{
    using Entities;

    public interface IBikeService
    {
        void AddBike(string name, decimal hourCost);

        void Rename(Bike bike, string name);
    }
}
