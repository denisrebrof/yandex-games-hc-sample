using Levels.domain.repositories;
using Purchases.domain.repositories;
using Purchases.presentation.ui;
using Zenject;

namespace Purchases.adapters
{
    public class LevelNumberProviderAdapter : PassLevelRewardPurchaseItem.ILevelNumberProvider
    {
        [Inject] private IPassLevelRewardPurchasesRepository passLevelRewardPurchasesRepository;
        [Inject] private ILevelsRepository levelsRepository;

        public int GetLevelNumber(long purchaseId)
        {
            var targetLevelId = passLevelRewardPurchasesRepository.GetLevelId(purchaseId);
            return levelsRepository.GetLevel(targetLevelId).Number;
        }
    }
}