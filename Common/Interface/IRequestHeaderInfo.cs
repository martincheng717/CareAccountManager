namespace Gdot.Care.Common.Interface
{
    public interface IRequestHeaderInfo
    {
        string GetSysUserKey();
        string GetSysComponentKey();
        string GetCorrelationId();
        string GetSessionId();
        string GetClientIpAddress();
    }
}
