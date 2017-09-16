using Autofac;
using StackExchange.Core.Services;
using StackExchange.Infrastructure.Services;

namespace StackExchange.Bootstraper.Modules
{
    public class OtherModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<Encrypter>()
                .As<IEncrypter>()
                .InstancePerLifetimeScope();

        }
    }
}
