using Domain.Commands.CommandContext;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Commands.ConcreteCommand
{
    public class AddBikeCommand : ICommand<AddBikeCommandContext>
    {
        protected readonly IBikeService _bikeService;

        public AddBikeCommand(IBikeService bikeService)
        {
            _bikeService = bikeService;
        }

        public void Execute(AddBikeCommandContext commandContext)
        {
            _bikeService.AddBike(commandContext.Name, commandContext.HourCost, commandContext.Cost);
        }
    }
}
