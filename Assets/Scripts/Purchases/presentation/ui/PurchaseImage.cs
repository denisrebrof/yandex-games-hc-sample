using Purchases.domain.repositories;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Purchases.presentation.ui
{
    public class PurchaseImage : MonoBehaviour
    {
        [Inject] private IPurchaseImageRepository imageRepository;
        [SerializeField] private Image target;

        public void Setup(long purchaseId) => target.sprite = imageRepository.GetImage(purchaseId);
    }
}