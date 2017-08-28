using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StackExchange.Infrastructure.Commands
{
    public interface ICommandBus
    {
        Task DispatchAsync<T>(T command) where T : ICommand;
    }
}
