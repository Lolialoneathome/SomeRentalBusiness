using Domain.Commands.CommandContext;
using Domain.Services;

namespace Domain.Commands.ConcreteCommand
{
    public class AddRentPointCommand : ICommand<AddRentPointCommandContext>
    {
        protected readonly IRentPointService _rentPointService;

        public AddRentPointCommand(IRentPointService rentPointService)
        {
            _rentPointService = rentPointService;
        }

        public void Execute(AddRentPointCommandContext commandContext)
        {
            commandContext.CreatedRentPoint = _rentPointService.AddRentPoint(commandContext.Employee);
        }
    }
}
