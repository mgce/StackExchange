using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Core.Dtos;

namespace StackExchange.Core.Services
{
    public interface IJwtHandler
    {
        JwtDto CreateToken(string email, string role);
    }
}
