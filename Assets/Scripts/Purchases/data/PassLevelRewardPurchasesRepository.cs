using System.Linq;
using Purchases.domain;
using Purchases.domain.model;
using Zenject;

namespace Purchases.data
{
    public class PassLevelRewardPurchasesRepository: IPassLevelRewardPurchasesRepository
    {
        [Inject] private IPurchaseEntitiesDao entitiesDao;
        
        public bool HasForLevel(long levelId)
        {
            return entitiesDao.GetLevelEntities().Any(entity => entity.passRewardLevelId == levelId);
        }

        public long GetLevelId(long purchaseId)
        {
            return entitiesDao.FindById(purchaseId).passRewardLevelId;
        }

        public Purchase GetForLevel(long levelId) => entitiesDao
            .GetLevelEntities()
            .Where(entity => entity.Type == PurchaseType.PassLevelReward)
            .First(entity => entity.passRewardLevelId == levelId)
            .toPurchase();
    }
}