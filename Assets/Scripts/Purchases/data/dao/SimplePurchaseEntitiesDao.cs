using System.Collections.Generic;
using System.Linq;
using Purchases.data.model;
using UnityEngine;

namespace Purchases.data.dao
{
    [CreateAssetMenu(menuName = "Purchases/PurchasesDao/SimplePurchaseEntitiesDao")]
    public class SimplePurchaseEntitiesDao: ScriptableObject, IPurchaseEntitiesDao
    {
        [SerializeField]
        private List<PurchaseEntity> entities = new List<PurchaseEntity>();

        public List<PurchaseEntity> GetLevelEntities() => entities;
        
        public PurchaseEntity FindById(long id) => entities.First(entity => entity.Id == id);
    }
}