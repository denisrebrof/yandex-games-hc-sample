using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Levels.presentation.ui
{
    [RequireComponent(typeof(Button))]
    public class CompleteCurrentLevelDebugButton : MonoBehaviour
    {
        [Inject] private ILevelCompletedListener levelCompletedListener;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(
                () => levelCompletedListener.CompleteCurrentLevel()
            );
        }
    }
}