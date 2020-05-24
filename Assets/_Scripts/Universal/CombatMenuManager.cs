using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject quitMenu;
    [SerializeField]
    private GameObject battleUI;

    [SerializeField]
    private ScriptableObjectPlayerStats playerStats;

    private void Start()
    {
        pauseMenu.SetActive(false);
        quitMenu.SetActive(false);
    }

    public void PauseButton()
    {
        pauseMenu.SetActive(true);
        battleUI.SetActive(false);
    }
    public void PauseCloseButton()
    {
        pauseMenu.SetActive(false);
        battleUI.SetActive(true);
    }

    public void SkipCard()
    {
        SceneManager.LoadScene(4);
    }

    public void RetireRun()
    {
        PlayerReset();
        PlayerPrefs.DeleteKey("Map");
        SceneManager.LoadScene(1);
    }

    public void RestartRun()
    {
        PlayerReset();
        PlayerPrefs.DeleteKey("Map");
        SceneManager.LoadScene(4);
    }

    private void PlayerReset()
    {
        playerStats.maxHP = 100;
        playerStats.curHP = playerStats.maxHP;

        playerStats.maxMoves = 5;
        playerStats.curMoves = playerStats.maxMoves;

        playerStats.playerCoins = 0;
        playerStats.healthArtefact = 0;
        playerStats.movesArtefact = 0;
        playerStats.damageArtefact = 0;
        playerStats.defenceArtefact = false;
    }
}
