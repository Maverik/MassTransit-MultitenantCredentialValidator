namespace Common
{
    public interface ICredentialRepository
    {
        string TenantId { get; }

        bool ValidateCredential(string username, string password);
    }
}
