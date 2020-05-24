using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCard : MonoBehaviour
{
    [SerializeField]
    private PlayerDeck playerDeck;
    [SerializeField]
    private Card card;
    [SerializeField]
    private ScriptableObjectPlayerStats playerStats;
    [SerializeField]
    private CardViz cardVizuals;
    [SerializeField]
    private Card[] cards;

    [SerializeField]
    private GameObject[] cardChoices;

    [SerializeField]
    private Text coinText;

    [SerializeField]
    private GameObject proceedButton;
    [SerializeField]
    private GameObject skipButton;

    private void Start()
    {
        card = cards[Random.Range(0, cards.Length)];
        cardVizuals.LoadCard(card);
    }

    public void OnClick()
    {
        playerDeck.playerDeck.Add(card);
        proceedButton.SetActive(true);
        skipButton.SetActive(false);
        cardChoices[0].SetActive(false);
        cardChoices[1].SetActive(false);
        cardChoices[2].SetActive(false);
    }
}
