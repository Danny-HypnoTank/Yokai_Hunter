using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapSceneMenuManager : MonoBehaviour
{
    [SerializeField]
    private Map.MapManager mapManager;
    [SerializeField]
    private PlayerDeck deckList;
    [SerializeField]
    private ScriptableObjectPlayerStats playerStats;
    [SerializeField]
    private PurchasableWarning purchaseWarning;

    [SerializeField]
    private Text coinText;

    [SerializeField]
    private GameObject treasure;
    [SerializeField]
    private GameObject store;
    [SerializeField]
    private GameObject[] mysteries;

    public void RetireRun()
    {
        ResetPlayerStatsOnRetire();
        PlayerPrefs.DeleteKey("Map");
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void SaveAndQuit()
    {
        mapManager.SaveMap();
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

#region Mystery Buttons
    //Mystery 1
    //Option 1
    public void HealthOption1()
    {
        playerStats.playerCoins += 100;
        coinText.text = playerStats.playerCoins.ToString();
        mysteries[0].SetActive(false);
    }
    //Option 2
    public void HealthOption2()
    {
        playerStats.maxHP += 30;
        mysteries[0].SetActive(false);
    }

    //Mystery 2
    //Option 1
    public void DamageOption1()
    {
        playerStats.damageArtefact += 1;
        mysteries[1].SetActive(false);
    }
    //Option 2
    public void DamageOption2()
    {
        mysteries[1].SetActive(false);
    }

    //Mystery 3
    //Option 1
    public void TakeDMGOption1()
    {
        if (playerStats.playerCoins >= 75)
        {
            playerStats.playerCoins -= 75;
            coinText.text = playerStats.playerCoins.ToString();
            mysteries[2].SetActive(false);
        }
        else if (playerStats.playerCoins <= 75)
        {
            playerStats.playerCoins -= playerStats.playerCoins;
            coinText.text = playerStats.playerCoins.ToString();
            mysteries[2].SetActive(false);
        }
        else
        {
            playerStats.playerCoins += 25;
            coinText.text = playerStats.playerCoins.ToString();
            mysteries[2].SetActive(false);
        }
    }
    //Option 2
    public void TakeDMGOption2()
    {
        playerStats.playerCoins += 25;
        coinText.text = playerStats.playerCoins.ToString();
        mysteries[2].SetActive(false);
    }

    //Mystery 4
    //Option 1
    public void MoveOption1()
    {
        playerStats.maxMoves += 1;
        mysteries[3].SetActive(false);
    }
    //Option 2
    public void MoveOption2()
    {
        if (playerStats.damageArtefact >= 1)
        {
            playerStats.damageArtefact -= 1;
            mysteries[3].SetActive(false);
        }
        else
        {
            mysteries[3].SetActive(false);
        }
    }
    #endregion

#region Treasure Buttons
    public void AquireTreasure1()
    {
        playerStats.healthArtefact += 1;
        treasure.SetActive(false);
    }
    public void AquireTreasure2()
    {
        if (playerStats.defenceArtefact == false)
        {
            playerStats.defenceArtefact = true;
            treasure.SetActive(false);
        }
        else
        {
            playerStats.playerCoins += 70;
            coinText.text = playerStats.playerCoins.ToString();
            treasure.SetActive(false);
        }
    }
    public void AquireTreasure3()
    {
        playerStats.damageArtefact += 1;
        treasure.SetActive(false);
    }

    public void SellTreasure1()
    {
        playerStats.playerCoins += 50;
        coinText.text = playerStats.playerCoins.ToString();
        treasure.SetActive(false);
    }
    public void SellTreasure2()
    {
        playerStats.playerCoins += 50;
        coinText.text = playerStats.playerCoins.ToString();
        treasure.SetActive(false);
    }
    public void SellTreasure3()
    {
        playerStats.playerCoins += 50;
        coinText.text = playerStats.playerCoins.ToString();
        treasure.SetActive(false);
    }


    public void LeaveTreasure()
    {
        treasure.SetActive(false);
    }
    #endregion

#region Store Buttons
    public void LeaveStore()
    {
        store.SetActive(false);
    }

    public void RemoveRandomCard()
    {
        if (playerStats.playerCoins >= 75 && deckList.playerDeck.Count > 5)
        {
            deckList.playerDeck.RemoveAt(Random.Range(0, deckList.playerDeck.Count));
            playerStats.playerCoins -= 75;
            coinText.text = playerStats.playerCoins.ToString();
        }
        else
        {
            StartCoroutine(purchaseWarning.NotPurchasable());
        }
    }
    #endregion


    private void ResetPlayerStatsOnRetire()
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

    private void SetCoins()
    {
        coinText.text = playerStats.playerCoins.ToString();
    }
}
