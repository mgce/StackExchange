using Autofac;
using StackExchange.Core.Repositories;
using StackExchange.Infrastructure.Repositories;

namespace StackExchange.Bootstraper.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CompanyRepository>().As<ICompanyRepository>().InstancePerLifetimeScope();
            builder.RegisterType<StackPriceRepository>().As<IStackPriceRepository>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
