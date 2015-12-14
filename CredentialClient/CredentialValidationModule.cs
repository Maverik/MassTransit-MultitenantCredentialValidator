using Autofac;

namespace CredentialClient
{
    class CredentialValidationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CredentialValidatedConsumer>()
                //.InstancePerMatchingLifetimeScope("MultitenantClient")
                .SingleInstance();
        }
    }
}