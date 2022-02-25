using Analytics.session.domain;
using Levels.domain.repositories;
using Zenject;

namespace Levels.presentation.analytics
{
    public class SessionEventCurrentLevelIdProviderImpl: ISessionEventLevelIdProvider
    {
        [Inject] private ICurrentLevelRepository currentLevelRepository;
        public long GetCurrentLevelId() => currentLevelRepository.GetCurrentLevel().ID;
    }
}