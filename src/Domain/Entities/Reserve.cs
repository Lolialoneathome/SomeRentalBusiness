using System;

namespace Domain.Entities
{
    public class Reserve : IEntity
    {
        public readonly Client Client;
        public readonly Bike Bike;
        public readonly DateTime StartDate;
        public readonly DateTime ToTime;

        public DateTime? RealEndTime { get; protected set; }

        public ReserveStatus Status
        {
            get
            {
                if (RealEndTime != null)
                    return ReserveStatus.SuccessEnded;

                if (DateTime.Now > ToTime)
                    return ReserveStatus.Failed;

                return ReserveStatus.Wait;
            }
        }

        protected internal Reserve(Client client, Bike bike, DateTime toTime)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (bike == null)
                throw new ArgumentNullException(nameof(bike));

            if (toTime == null)
                throw new ArgumentNullException(nameof(toTime));

            Client = client;
            Bike = bike;
            ToTime = toTime;
            StartDate = DateTime.UtcNow;
        }

        public void EndReserve()
        {
            RealEndTime = DateTime.UtcNow;
        }

    }
}
