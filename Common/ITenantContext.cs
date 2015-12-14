using System;

namespace Common
{
    public interface ITenantContext
    {
        Uri SenderAddress { get; }

        string TenantId { get; }
    }
}
