using Domain.Commands;

namespace Domain.Commands
{
    public class CreateBikeCommandContext : ICommandContext
    {
        public string Name { get; set; }

        public decimal HourCost { get; set; }

        public decimal Cost { get; set; }
    }
}
