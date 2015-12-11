using Common;

namespace CredentialValidator
{
    class CredentialValidated : ICredentialValidated
    {
        public CredentialValidated(bool status, string tenantId)
        {
            Status = status;
            TenantId = tenantId;
        }

        public bool Status { get; }

        public string TenantId { get; }
    }
}