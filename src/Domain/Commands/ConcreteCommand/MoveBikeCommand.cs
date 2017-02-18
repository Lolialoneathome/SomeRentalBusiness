using Domain.Commands.CommandContext;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Commands.ConcreteCommand
{
    public class MoveBikeCommand : ICommand<MoveBikeCommandContext>
    {
        protected readonly IBikeService _bikeService;

        public MoveBikeCommand(IBikeService bikeService)
        {
            _bikeService = bikeService;
        }

        public void Execute(MoveBikeCommandContext commandContext)
        {
            _bikeService.MoveBike(commandContext.Bike, commandContext.RentPoint);
        }
    }
}
