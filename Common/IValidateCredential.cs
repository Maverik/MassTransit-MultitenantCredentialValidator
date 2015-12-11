namespace Common
{
    public interface IValidateCredential : IMultiTenantMessage
    {
        string Username { get; }
        string Password { get; }
    }
}