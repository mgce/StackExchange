using System;
using System.Threading.Tasks;
using Autofac;
using StackExchange.Core.Commands;

namespace StackExchange.Infrastructure.Commands
{
    public class CommandBus : ICommandBus
    {
        private readonly IComponentContext _context;

        //private readonly Func<Type, ICommandHandler> _handlersFactory;

        //public CommandBus(Func<Type, ICommandHandler> handlersFactory)
        //{
        //    _handlersFactory = handlersFactory;
        //}
        //public async Task DispatchAsync<T>(T command) where T : ICommand
        //{
        //    var handler = (ICommandHandler<T>) _handlersFactory(typeof(T));
        //    await handler.HandleAsync(command);
        //}

        public CommandBus(IComponentContext context)
        {
            _context = context;
        }

        public async Task DispatchAsync<T>(T command) where T : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command),
                    $"Command: '{typeof(T).Name}' can not be null.");
            }
            var handler = _context.Resolve<ICommandHandler<T>>();
            await handler.HandleAsync(command);
        }
    }
}
