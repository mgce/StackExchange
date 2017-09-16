using System;
using System.Threading.Tasks;
using StackExchange.Core.Commands;
using StackExchange.Core.Entities;
using StackExchange.Core.Repositories;
using StackExchange.Core.Services;
using StackExchange.Infrastructure.Commands.Users;

namespace StackExchange.Infrastructure.CommandHandlers.Users
{
    public class UserCommandHandler : 
          ICommandHandler<LoginUserCommand>
        , ICommandHandler<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;

        public UserCommandHandler(IUserRepository userRepository
            , IEncrypter encrypter)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
        }

        public async Task HandleAsync(LoginUserCommand command)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email);
            if (user == null)
            {
                throw new Exception("Invalid credentials");
            }

            var hash = _encrypter.GetHash(command.Password, user.Salt);
            if (user.Password == hash)
            {
                return;
            }
            throw new Exception("Invalid credentials");
        }

        public async Task HandleAsync(CreateUserCommand command)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email);
            if (user != null)
            {
                throw new Exception($"User with email: '{command.Email}' already exists.");
            }

            var salt = _encrypter.GetSalt(command.Password);
            var password = _encrypter.GetHash(command.Password, salt);
            user = new User(command.Email, command.Username, 
                command.FirstName, command.LastName,
                password, salt);

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }
    }
}
