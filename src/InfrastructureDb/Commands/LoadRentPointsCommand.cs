using Domain.Commands;
using InfrastructureDb.Commands.CommandContext;
using InfrastructureDb.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfrastructureDb.Commands
{
    public class LoadRentPointsCommand : ICommand<LoadRentPointsCommandContext>
    {

        private readonly IRentPointLoader _rentPointLoader;

        public LoadRentPointsCommand(IRentPointLoader rentPointLoader)
        {
            _rentPointLoader = rentPointLoader;
        }

        public void Execute(LoadRentPointsCommandContext commandContext)
        {
            _rentPointLoader.GetRentPointsFromMyDb();
        }
    }
}
