using Autofac;
using Common;
using MassTransit;

namespace CredentialValidator
{
    class RabbitBusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RabbitBusSettings>();

            builder.Register(context =>
            {
                var busSettings = context.Resolve<RabbitBusSettings>();

                return Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(busSettings.HostAddress, h =>
                    {
                        h.Username(busSettings.Username);
                        h.Password(busSettings.Password);
                        h.UseSsl(x => x.ServerName = busSettings.SslServerName);
                    });

                    cfg.Durable = false;
                    cfg.AutoDelete = true;

                    cfg.ReceiveEndpoint(host, busSettings.QueueName, ec =>
                    {
                        ec.Durable = false;
                        ec.AutoDelete = false;

                        ec.ConsumerInScope<ValidateCredentialConsumer, string>(context, "MultiTenantCredentialValidation");
                    });
                });
            })
                .SingleInstance()
                .As<IBusControl>()
                .As<IBus>();
        }
    }
}