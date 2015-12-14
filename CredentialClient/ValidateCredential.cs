using System;
using Common;

namespace CredentialClient
{
    class ValidateCredential : IValidateCredential, ITenantContext
    {
        public Uri SenderAddress { get; set; }

        public string TenantId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}