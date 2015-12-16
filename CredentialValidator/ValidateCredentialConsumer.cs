using System;
using System.Threading.Tasks;
using Autofac;
using Common;
using MassTransit;
using MassTransit.AutofacIntegration;
using MassTransit.Util;

namespace CredentialValidator
{
    class ValidateCredentialConsumer : IConsumer<IValidateCredential>
    {
        readonly ILifetimeScope _scope;

        public ValidateCredentialConsumer(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public async Task Consume(ConsumeContext<IValidateCredential> context)
        {
            ConsumeContext<ITenantContext> tenantContext;

            //Default repository that'll always return false
            var repository = _scope.Resolve<ICredentialRepository>();

            if (context.TryGetMessage(out tenantContext))
            {
                var scope = _scope.Resolve<ILifetimeScopeRegistry<string>>().GetLifetimeScope(tenantContext.Message.TenantId);

                //Try to get repository for this tenant using our naming convention
                var repositoryOverride = scope.ResolveOptionalNamed<ICredentialRepository>($"{nameof(ICredentialRepository)}-{tenantContext.Message.TenantId}");

                if (repositoryOverride != null) repository = repositoryOverride;
            }

            Console.WriteLine($"Validating credential for tenant: {tenantContext.Message.TenantId}, using repository tenant: {repository.TenantId}");

            await context.RespondAsync(new CredentialValidated {Status = repository.ValidateCredential(context.Message.Username, context.Message.Password), TenantId = repository.TenantId}).ConfigureAwait(false);
        }
    }
}