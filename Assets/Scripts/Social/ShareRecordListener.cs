using System.Collections;
using Levels.domain.repositories;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class ShareRecordListener : MonoBehaviour
{
    [Inject] private ICurrentLevelRepository currentLevelRepository;
    [SerializeField] private int levelNum = 5;
    [SerializeField] private float delay = 1;
    [SerializeField] private UnityEvent onRecordReached;

    private string prefskey = "ShareInvite";

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        onRecordReached.Invoke();
        PlayerPrefs.SetInt(prefskey, 1);
    }

    public void CheckAndShow()
    {
        if (PlayerPrefs.HasKey(prefskey))
            return;
        var currentLevelNum = currentLevelRepository.GetCurrentLevel().Number;
        if (currentLevelNum == levelNum + 1 && onRecordReached != null)
        {
            StartCoroutine(Delay());
        }
    }
}