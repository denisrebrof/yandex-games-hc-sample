using Levels.domain.repositories;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Levels.presentation.ui
{
    [RequireComponent(typeof(Text))]
    public class CurrentLevelText : MonoBehaviour
    {
        [Inject] private ICurrentLevelRepository currentLevelRepository;
        private Text target;
        private string template;
    
        void Start()
        {
            target = GetComponent<Text>();
            template = target.text;
            OnEnable();
        }

        private void OnEnable()
        {
            if(target==null)
                return;
            var levelNum = currentLevelRepository.GetCurrentLevel().Number.ToString();
            target.text = template.Replace("$", levelNum);
        }
    }
}
