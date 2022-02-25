using UnityEngine;

namespace Purchases.domain.repositories
{
    public interface IPurchaseImageRepository
    {
        public Sprite GetImage(long purchaseId);
    }
}