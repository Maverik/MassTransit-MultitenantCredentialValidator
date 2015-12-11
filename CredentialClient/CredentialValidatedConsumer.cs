using System;
using System.Threading.Tasks;
using Common;
using MassTransit;
using MassTransit.Util;

namespace CredentialClient
{
    class CredentialValidatedConsumer : IConsumer<ICredentialValidated>
    {
        public Task Consume(ConsumeContext<ICredentialValidated> context)
        {
            Console.WriteLine($"Got response: {context.Message.Status} for tenantId {context.Message.TenantId}");
            return TaskUtil.Completed;
        }
    }
}