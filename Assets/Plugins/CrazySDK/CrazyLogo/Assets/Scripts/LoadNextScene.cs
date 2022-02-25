using UnityEngine;
using UnityEngine.SceneManagement;

namespace Crazy
{
    public class LoadNextScene : MonoBehaviour
    {
        [SerializeField] private float withDelay = 2;

        private void Start()
        {
            Invoke("loadNextScene", withDelay);
        }

        private void loadNextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}