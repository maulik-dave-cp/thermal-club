using System.Reflection;
using ThermalClub.Modules.Core.Authorization;
using ThermalClub.Modules.Core.Data;

using Autofac;
using MediatR;
using Module = Autofac.Module;

namespace ThermalClub.Modules.Core.Modules
{
	public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //var assembly = Assembly.GetExecutingAssembly();

            //builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().SingleInstance(); //.InstancePerLifetimeScope();
            //builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().SingleInstance(); //.InstancePerLifetimeScope();

            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Validator"))
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Notification"))
                .InstancePerLifetimeScope();

            builder.RegisterType<Auth>();

            // Customs
          //  builder.RegisterType<StoreSqlSqlContext>();
           
        }
    }
}