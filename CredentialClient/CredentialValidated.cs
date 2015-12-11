using Common;

namespace CredentialClient
{
    class CredentialValidated : ICredentialValidated {

        public bool Status { get; set; }

        public string TenantId { get; set; }
    }
}