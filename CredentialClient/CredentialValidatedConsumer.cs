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
            ConsumeContext<ITenantContext> tenantContext;

            Console.WriteLine(context.TryGetMessage(out tenantContext) ?
                $"Got response: {context.Message.Status} for tenantId {tenantContext.Message.TenantId}" :
                $"Got response: {context.Message.Status} without tenant context");
            return TaskUtil.Completed;
        }
    }
}