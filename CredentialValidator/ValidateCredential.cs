using Common;

namespace CredentialValidator
{
    class ValidateCredential : IValidateCredential
    {
        public string TenantId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}