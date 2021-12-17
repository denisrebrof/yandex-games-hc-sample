using Levels.domain.repositories;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Levels.presentation.ui
{
    [RequireComponent(typeof(Text))]
    public class SimpleCurrentLevelText : MonoBehaviour
    {
        [Inject] private ICurrentLevelRepository currentLevelRepository;
        private Text target;

        private void Awake() => target = GetComponent<Text>();

        private void Start() => currentLevelRepository
            .GetCurrentLevelFlow()
            .Select(level => level.Number.ToString())
            .Subscribe(number => target.text = number)
            .AddTo(this);
    }
}