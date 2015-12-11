namespace Common
{
    public interface ICredentialValidated : IMultiTenantMessage
    {
        bool Status { get; }
    }
}