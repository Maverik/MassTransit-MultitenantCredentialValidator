using System;
using Common;

namespace CredentialValidator
{
    class CredentialValidated : ICredentialValidated, ITenantContext
    {
        public bool Status { get; set; }

        public Uri SenderAddress { get; set; }

        public string TenantId { get; set; }
    }
}