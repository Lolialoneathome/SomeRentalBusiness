using Domain.Entities;
using Domain.Entities.HumanEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Commands.CommandContext
{
    public class ReserveBikeCommandContext : ICommandContext
    {
        public Bike Bike { get; set; }
        public Client Client { get; set; }
        public DateTime ToTime { get; set; }
    }
}
