﻿using Domain.Entities;
using Domain.Entities.Deposits;

namespace Domain.Commands.CommandContext
{
    public class GetBikeInRentCommandContext : ICommandContext
    {
        public Client Client { get; set; }
        public Bike Bike { get; set; }
        public Deposit Deposit { get; set; }
    }
}
