using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Microsoft.Extensions.Configuration;
using StackExchange.Core.Repositories;
using StackExchange.Core.Settings;
using StackExchange.Infrastructure.Repositories;

namespace StackExchange.Bootstraper.Modules
{
    public class SettingModule : Module
    {
        private readonly IConfiguration _configuration;

        public SettingModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            
            base.Load(builder);
        }
    }
}
