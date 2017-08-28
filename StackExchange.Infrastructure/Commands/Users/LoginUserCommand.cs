using StackExchange.Core.Commands;

namespace StackExchange.Infrastructure.Commands.Users
{
    public class LoginUserCommand : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
