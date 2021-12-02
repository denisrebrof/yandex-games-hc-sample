using Levels.domain.model;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Levels.presentation.ui
{
    public class LevelItem : MonoBehaviour
    {
        [Inject] private ILevelItemController itemController;

        [SerializeField] private Text numberText;
        [SerializeField] private GameObject checkmark;
        private long? levelId;

        public void Setup(Level level)
        {
            levelId = level.ID;
            numberText.text = level.Number.ToString();
            checkmark.SetActive(level.CompletedState);
        }

        public void HandleClick()
        {
            if (levelId.HasValue) itemController.OnItemClick(levelId.Value);
        }
        
        public interface ILevelItemController
        {
            void OnItemClick(long levelId);
        }
    }
}