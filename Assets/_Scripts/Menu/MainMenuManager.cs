using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingBar;
    [SerializeField]
    private GameObject settingsPanel;
    [SerializeField]
    private GameObject quitPanel;

    [SerializeField]
    private Loading loadingScreen;

    private void Start()
    {
        loadingBar.SetActive(false);
        settingsPanel.SetActive(false);
        quitPanel.SetActive(false);
    }

    public void TutorialButton()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }

    public void SettingsOpenButton()
    {
        settingsPanel.SetActive(true);
    }

    public void SettingsCloseButton()
    {
        settingsPanel.SetActive(false);
    }

    public void QuitButton()
    {
        quitPanel.SetActive(true);
    }

    public void QuitYesButton()
    {
        Application.Quit();
    }

    public void QuitNoButton()
    {
        quitPanel.SetActive(false);
    }
}
