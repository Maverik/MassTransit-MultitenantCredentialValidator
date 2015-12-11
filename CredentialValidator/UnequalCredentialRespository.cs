using System.Diagnostics;
using Common;

namespace CredentialValidator
{
    class UnequalCredentialRespository : ICredentialRepository
    {
        public string TenantId { get; } = "UNEQUAL";

        public bool ValidateCredential(string username, string password)
        {
            Debug.WriteLine("Unequal credentials validating...");
            return username != password;
        }
    }
}