using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Levels.presentation.ui
{
    [RequireComponent(typeof(Button))]
    public class CompleteCurrentLevelDebugButton : MonoBehaviour
    {
        [Inject] private ILevelCompletionHandler levelCompletionHandler;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(
                () => levelCompletionHandler.CompleteCurrentLevel()
            );
        }
    }
}