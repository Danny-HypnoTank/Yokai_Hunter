using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreCard : MonoBehaviour
{
    [SerializeField]
    private PlayerDeck playerDeck;
    [SerializeField]
    private Card card;
    [SerializeField]
    private ScriptableObjectPlayerStats playerStats;
    [SerializeField]
    private PurchasableWarning purchaseWarning;
    [SerializeField]
    private CardViz cardVizuals;

    [SerializeField]
    private int price;

    [SerializeField]
    private Text coinText;

    private void Awake()
    {
        cardVizuals.LoadCard(card);
    }

    public void OnClick()
    {
        if (playerStats.playerCoins >= price)
        {
            playerStats.playerCoins -= price;
            playerDeck.playerDeck.Add(card);
            coinText.text = playerStats.playerCoins.ToString();
            this.gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(purchaseWarning.NotPurchasable());
        }
    }
}
