using System;
using Domain.Commands.CommandContext;
using Domain.Services;

namespace Domain.Commands.ConcreteCommand
{
    public class GetBikeInRentCommand : ICommand<GetBikeInRentCommandContext>
    {
        protected readonly IRentService _rentService;
        public GetBikeInRentCommand(IRentService rentService)
        {
            _rentService = rentService;
        }

        public void Execute(GetBikeInRentCommandContext commandContext)
        {
            _rentService.Take(commandContext.Client, commandContext.Bike, commandContext.Deposit);
        }
    }
}
