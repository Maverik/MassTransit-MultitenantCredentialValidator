using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using CommandLine;
using MassTransit;

namespace CredentialClient
{
    static class Program
    {
        static void Main(string[] args)
        {
            var options = Parser.Default.ParseArguments<CommandLineOptions>(args);

            var builder = new ContainerBuilder();

            options.WithParsed(x => builder.RegisterLifetimeScopeRegistry<string>(x.TenantId));

            builder.RegisterModule<CredentialValidationModule>();
            builder.RegisterModule<RabbitBusModule>();

            var container = builder.Build();

            var bus = container.Resolve<IBusControl>();

            BusHandle handle = null;

            Task.Run(async () => await bus.StartAsync().ContinueWith(task =>
            {
                handle = task.Result;

                options.WithParsed(async x =>
                {
                    await bus.Publish(new ValidateCredential { Username = x.Username, Password = x.Password, TenantId = x.TenantId });
                    Console.WriteLine($"Credential validation message sent for tenant: {x.TenantId}");
                });

            }));

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

            handle?.Stop();
        }
    }
}
