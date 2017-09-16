using System.Threading.Tasks;

namespace StackExchange.Core.Commands
{
    public interface ICommandHandler
    { }

    public interface ICommandHandler<T> : ICommandHandler where T : ICommand
    {
        Task HandleAsync(T command);
    }
}
