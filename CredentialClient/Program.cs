using System;
using System.Threading.Tasks;
using Autofac;
using CommandLine;
using Common;
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
            
            builder.RegisterModule<RabbitBusModule>();

            var container = builder.Build();

            var bus = container.Resolve<IBusControl>();

            BusHandle handle = null;

            Task.Run(async () => await bus.StartAsync().ContinueWith(task =>
            {
                handle = task.Result;

                options.WithParsed(async x =>
                {
                    Console.WriteLine($"Requesting validation for username {x.Username}, password {x.Password}, tenantId {x.TenantId}");

                    var response = await bus.CreatePublishRequestClient<ValidateCredential, ICredentialValidated>(TimeSpan.FromSeconds(10)).Request(new ValidateCredential { Username = x.Username, Password = x.Password, TenantId = x.TenantId }).ConfigureAwait(false);

                    Console.WriteLine($"Credential validation response was {response.Status}");
                });

            }).ConfigureAwait(false));

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

            handle?.Stop();
        }
    }
}
