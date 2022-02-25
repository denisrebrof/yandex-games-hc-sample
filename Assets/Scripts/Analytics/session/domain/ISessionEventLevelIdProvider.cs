namespace Analytics.session.domain
{
    public interface ISessionEventLevelIdProvider
    {
        long GetCurrentLevelId();
    }
}