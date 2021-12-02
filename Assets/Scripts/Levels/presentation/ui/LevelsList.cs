using Levels.domain;
using Levels.domain.model;
using UnityEngine;
using Zenject;

namespace Levels.presentation.ui
{
    public class LevelsList : MonoBehaviour
    {
        [SerializeField] private Transform listRoot;
        [SerializeField] private LevelItem itemPrefab;
        [Inject] private ILevelsRepository levelsRepository;

        private void Awake()
        {
            if (listRoot == null)
                listRoot = transform;

            levelsRepository.GetLevels().ForEach(CreateItem);
        }

        private void CreateItem(Level level)
        {
            var item = Instantiate(itemPrefab, listRoot);
            item.Setup(level);
        }
    }
}