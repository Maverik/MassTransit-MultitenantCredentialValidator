namespace Common
{
    public interface IMultiTenantMessage
    {
        string TenantId { get; }
    }
}
