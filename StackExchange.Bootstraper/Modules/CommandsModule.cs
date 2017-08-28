using System;
using System.Reflection;
using Autofac;
using StackExchange.Core.Commands;
using StackExchange.Infrastructure.Commands;
using Module = Autofac.Module;

namespace StackExchange.Bootstraper.Modules
{
    public class CommandsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            var assembly = typeof(ICommand).GetTypeInfo().Assembly;
            builder.RegisterAssemblyTypes(assembly).Where(x => x.IsAssignableTo<ICommandHandler>())
                .AsImplementedInterfaces();

            builder.Register<Func<Type, ICommandHandler>>(c =>
            {
                var ctx = c.Resolve<IComponentContext>();

                return t =>
                {
                    var handlerType = typeof(ICommandHandler<>).MakeGenericType(t);
                    return (ICommandHandler) ctx.Resolve(handlerType);
                };
            });

            builder.RegisterType<CommandBus>()
                .AsImplementedInterfaces();
        }
    }
}
