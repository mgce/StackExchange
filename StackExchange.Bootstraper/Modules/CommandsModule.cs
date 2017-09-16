using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using StackExchange.Core.Commands;
using StackExchange.Infrastructure.CommandHandlers.Users;
using StackExchange.Infrastructure.Commands;
using Module = Autofac.Module;

namespace StackExchange.Bootstraper.Modules
{
    public class CommandsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var assemblyFirst = typeof(UserCommandHandler).GetTypeInfo().Assembly;
            var assemblySeccond = typeof(ICommandHandler<>).GetTypeInfo().Assembly;

            builder.RegisterAssemblyTypes(assemblyFirst)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assemblySeccond)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<CommandBus>()
                .As<ICommandBus>()
                .InstancePerLifetimeScope();

        }
    }
}
