using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingToBoss : MonoBehaviour
{
    [SerializeField]
    private Image progressBar;
    [SerializeField]
    private int levelToLoad = 0;

    public int LevelToLoad { get => levelToLoad; set => levelToLoad = value; }

    void OnEnable()
    {
        StartCoroutine(LoadAsyncOperation());
    }

    IEnumerator LoadAsyncOperation()
    {
        yield return new WaitForSeconds(2);

        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(levelToLoad);
        while (gameLevel.progress < 1)
        {
            progressBar.fillAmount = gameLevel.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}
