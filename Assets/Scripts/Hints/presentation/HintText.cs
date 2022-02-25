using Hints.domain;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Hints.presentation
{
    [RequireComponent(typeof(Text))]
    public class HintText : MonoBehaviour
    {
        [Inject] private ICurrentHintRepository repository;
        private Text target;

        private void Awake() => target = GetComponent<Text>();

        private void OnEnable() => Update();

        public void Update()
        {
            try
            {
                target.text = repository.GetHintText();
            }
            catch
            {
            }
        }
    }
}