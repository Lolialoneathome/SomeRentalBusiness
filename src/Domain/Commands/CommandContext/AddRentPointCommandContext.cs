using Domain.Entities;
using Domain.Entities.HumanEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Commands.CommandContext
{
    public class AddRentPointCommandContext : ICommandContext
    {
        public Employee Employee { get; set; }


        //!!! Это правильный недо-CQRS?
        public RentPoint CreatedRentPoint { get; set;}

    }
}
