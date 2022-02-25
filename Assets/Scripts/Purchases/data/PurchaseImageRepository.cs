using Purchases.domain.repositories;
using UnityEngine;
using Zenject;

namespace Purchases.data
{
    public class PurchaseImageRepository: IPurchaseImageRepository
    {
        [Inject] private IPurchaseEntitiesDao entitiesDao;
        
        public Sprite GetImage(long purchaseId) => entitiesDao.FindById(purchaseId).image;
    }
}