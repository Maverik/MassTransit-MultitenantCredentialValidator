using System;
using System.Threading.Tasks;
using Autofac;
using MassTransit;

namespace CredentialValidator
{
    static class Program
    {
        static void Main()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<CredentialValidationModule>();

            builder.RegisterModule<RabbitBusModule>();

            var container = builder.Build();

            var bus = container.Resolve<IBusControl>();

            BusHandle handle = null;

            Task.Run(async () => handle = await bus.StartAsync());

            Console.WriteLine("Listening for messages...");

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

            handle?.Stop();
        }
    }
}
