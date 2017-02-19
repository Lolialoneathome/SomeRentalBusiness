using Domain.Commands.CommandContext;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Commands.ConcreteCommand
{
    public class ReserveBikeCommand : ICommand<ReserveBikeCommandContext>
    {

        protected readonly IReserveService _reserveService;
        public ReserveBikeCommand(IReserveService reserveService)
        {
            _reserveService = reserveService;
        }

        public void Execute(ReserveBikeCommandContext commandContext)
        {
            _reserveService.ReserveBike(commandContext.Bike, commandContext.Client, commandContext.ToTime);
        }
    }
}
