using System.Collections.Generic;
using Purchases.data.model;

namespace Purchases.data
{
    public interface IPurchaseEntitiesDao
    {
        public List<PurchaseEntity> GetLevelEntities();

        public PurchaseEntity FindById(long id);
    }
}