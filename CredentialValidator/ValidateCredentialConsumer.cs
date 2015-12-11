using System;
using System.Threading.Tasks;
using Autofac;
using Common;
using MassTransit;
using MassTransit.AutofacIntegration;

namespace CredentialValidator
{
    class ValidateCredentialConsumer : IConsumer<IValidateCredential>
    {
        readonly ILifetimeScope _scope;

        public ValidateCredentialConsumer(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public Task Consume(ConsumeContext<IValidateCredential> context)
        {
            var scope = _scope.Resolve<ILifetimeScopeRegistry<string>>().GetLifetimeScope(context.Message.TenantId);

            object resolvedRepository;

            //Try to get repository for this tenant using our naming convention
            if (!scope.TryResolveNamed($"{nameof(ICredentialRepository)}-{context.Message.TenantId}", typeof(ICredentialRepository), out resolvedRepository))
                //Default repository that'll always return false
                resolvedRepository = scope.Resolve<ICredentialRepository>();

            var repository = (ICredentialRepository)resolvedRepository;

            Console.WriteLine($"Validating credential for tenant: {context.Message.TenantId}, using repository tenant: {repository.TenantId}");
            return context.RespondAsync(new CredentialValidated(repository.ValidateCredential(context.Message.Username, context.Message.Password), repository.TenantId));

        }
    }
}