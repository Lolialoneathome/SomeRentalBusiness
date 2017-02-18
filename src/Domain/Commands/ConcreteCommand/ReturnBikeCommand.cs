using Domain.Commands.CommandContext;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Commands.ConcreteCommand
{
    public class ReturnBikeCommand : ICommand<ReturnBikeCommandContext>
    {
        protected readonly IRentService _rentService;
        public ReturnBikeCommand(IRentService rentService)
        {
            _rentService = rentService;
        }

        public void Execute(ReturnBikeCommandContext commandContext)
        {
            _rentService.Return(commandContext.Bike, commandContext.RentPoint, commandContext.IsBroken);
        }

    }
}
