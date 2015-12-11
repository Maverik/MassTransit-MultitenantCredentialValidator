using Autofac;
using Common;
using MassTransit;

namespace CredentialValidator
{
    class CredentialValidationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Use this as default credential store - always returns false
            builder.RegisterType<FalseCredentialRespository>()
                .As<ICredentialRepository>()
                .SingleInstance();

            //Use this for Tenant A
            builder.RegisterType<EqualCredentialRespository>()
                .Named<ICredentialRepository>($"{nameof(ICredentialRepository)}-A")
                .SingleInstance();

            //Use this for Tenant B
            builder.RegisterType<UnequalCredentialRespository>()
                .Named<ICredentialRepository>($"{nameof(ICredentialRepository)}-B")
                .SingleInstance();

            builder.RegisterType<ValidateCredentialConsumer>()
                .InstancePerMatchingLifetimeScope("MultiTenantCredentialValidation");

            builder.RegisterLifetimeScopeRegistry<string>("MultiTenantCredentialValidation");
            
            builder.RegisterLifetimeScopeIdAccessor<ValidateCredential, string>(x => x.TenantId);
        }
    }
}