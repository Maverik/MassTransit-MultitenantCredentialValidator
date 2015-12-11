using System.Diagnostics;
using Common;

namespace CredentialValidator
{
    class FalseCredentialRespository : ICredentialRepository
    {
        public string TenantId { get; } = "Fallback Tenant";

        public bool ValidateCredential(string username, string password)
        {
            Debug.WriteLine("Default false credentials validating...");
            return false;
        }
    }
}