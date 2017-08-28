using System;
using System.Threading.Tasks;
using StackExchange.Core.Commands;

namespace StackExchange.Infrastructure.Commands
{
    public class CommandBus : ICommandBus
    {
        private readonly Func<Type, ICommandHandler> _handlersFactory;

        public CommandBus(Func<Type, ICommandHandler> handlersFactory)
        {
            _handlersFactory = handlersFactory;
        }
        public async Task DispatchAsync<T>(T command) where T : ICommand
        {
            var handler = (ICommandHandler<T>) _handlersFactory(typeof(T));
            await handler.HandleAsync(command);
        }
    }
}
