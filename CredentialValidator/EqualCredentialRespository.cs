using System.Diagnostics;
using Common;

namespace CredentialValidator
{
    class EqualCredentialRespository : ICredentialRepository
    {
        public string TenantId { get; } = "EQUAL";

        public bool ValidateCredential(string username, string password)
        {
            Debug.WriteLine("Equal credentials validating...");
            return username == password;
        }
    }
}
