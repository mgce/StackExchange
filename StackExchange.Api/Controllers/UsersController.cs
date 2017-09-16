using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Core.Commands;
using StackExchange.Core.Repositories;
using StackExchange.Infrastructure.Commands.Users;

namespace StackExchange.Api.Controllers
{
    [Produces("application/json")]
    public class UsersController : Controller
    {
        private readonly ICommandBus _bus;
        private readonly IUserRepository _userRepository;

        public UsersController(ICommandBus bus, IUserRepository userRepository)
        {
            _bus = bus;
            _userRepository = userRepository;
        }

        [HttpGet("Users/Get")]
        public async Task<IActionResult> Get()
        {
            var users = await _userRepository.GetAllAsync();

            if (users == null)
            {
                return NotFound();
            }

            return Json(users);
        }

        [HttpGet("Users/Get/{email}")]
        public async Task<IActionResult> Get(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            return Json(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            await _bus.DispatchAsync(command);
            return Created($"users/{command.Email}", new object());
        }
    }
}