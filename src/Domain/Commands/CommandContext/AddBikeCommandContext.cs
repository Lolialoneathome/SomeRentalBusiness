using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Commands.CommandContext
{
    public class AddBikeCommandContext : ICommandContext
    {
        public string Name { get; set; }
        public decimal HourCost { get; set; }
        public decimal Cost { get; set; }
    }
}
