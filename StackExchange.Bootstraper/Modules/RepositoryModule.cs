using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using StackExchange.Core.Repositories;
using StackExchange.Infrastructure.Repositories;

namespace StackExchange.Bootstraper.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();

            base.Load(builder);
        }
    }
}
