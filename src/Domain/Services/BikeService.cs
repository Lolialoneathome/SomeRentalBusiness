namespace Domain.Services
{
    using System;
    using Entities;
    using Repositories;

    public class BikeService : IBikeService
    {
        private readonly IRepository<Bike> _repository;
        private readonly IBikeNameVerifier _bikeNameVerifier;


        
        public BikeService(IRepository<Bike> repository, IBikeNameVerifier bikeNameVerifier)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));

            if (bikeNameVerifier == null)
                throw new ArgumentNullException(nameof(bikeNameVerifier));

            _repository = repository;
            _bikeNameVerifier = bikeNameVerifier;
        }



        public void AddBike(string name, decimal hourCost)
        {
            if (!_bikeNameVerifier.IsFree(name))
                throw new InvalidOperationException("Bike with same name already exists");

            Bike newBike = new Bike(name, hourCost);

            _repository.Add(newBike);
        }

        public void Rename(Bike bike, string name)
        {
            if (bike == null)
                throw new ArgumentNullException(nameof(bike));

            if (bike.Name == name)
                return;

            if (!_bikeNameVerifier.IsFree(name))
                throw new InvalidOperationException("Bike with same name already exists");

            bike.Rename(name);
        }
    }
}
