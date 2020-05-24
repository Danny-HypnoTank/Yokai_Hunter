using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField]
    List<Card> deck = new List<Card>();
    [SerializeField]
    List<Card> hand = new List<Card>();
    [SerializeField]
    List<Card> graveyard = new List<Card>();
    [SerializeField]
    List<Card> exile = new List<Card>();

    [SerializeField]
    private BattleSystem battleSyst;

    [SerializeField]
    private GameObject[] cardSlots;

    [SerializeField]
    private PlayerDeck playerDeck;

    public List<Card> Deck { get => deck; set => deck = value; }

    public void SetDeck()
    {
        for (int i = 0; i < playerDeck.playerDeck.Count; i++)
        {
            deck.Add(playerDeck.playerDeck[i]);
        }
        //deck = playerDeck.playerDeck;
        ShuffleDeck();
    }

    public void DrawHand(int handSize)
    {
        //if (deck.Count <= 0)
        //{
        //    RecycleDeck();
        //}
        if (deck.Count >= handSize)
        {
            for (int i = 0; i < handSize; i++)
            {
                if (deck.Count > 0)
                {
                    MoveCardToDestination(deck[0], 0, 1);
                }
                else
                {
                    RecycleDeck();
                    MoveCardToDestination(deck[0], 0, 1);
                }
            }
        }
        else if (deck.Count < handSize)
        {
            RecycleDeck();
            for (int i = 0; i < handSize; i++)
            {
                if (deck.Count > 0)
                {
                    MoveCardToDestination(deck[0], 0, 1);
                }
                else
                {
                    RecycleDeck();
                    MoveCardToDestination(deck[0], 0, 1);
                }
            }
        }


        for (int i = 0; i < hand.Count; i++)
        {
            CardViz cv = cardSlots[i].GetComponent<CardViz>();
            cv.card = hand[i];
            cardSlots[i].GetComponent<CardSlot>().Card = hand[i];
            cv.LoadCard(cv.card);
            cardSlots[i].SetActive(true);
        }
    }


    public void MoveCardToDestination(Card c, int from, int to)
    {
        switch (from)
        {
            case 0:
                {
                    deck.Remove(c); 
                    break;
                }
            case 1:
                {
                    hand.Remove(c);
                    break;
                }
            case 2:
                {
                    graveyard.Remove(c);
                    break;
                }
        }
        switch (to)
        {
            case 0:
                {
                    deck.Add(c);
                    break;
                }
            case 1:
                {
                    hand.Add(c);
                    break;
                }
            case 2:
                {
                    graveyard.Add(c);
                    break;
                }
            case 3:
                {
                    exile.Add(c);
                    break;
                }
        }
    }

    public void TurnEndCardCycle()
    {
        int handCardCount = hand.Count;
        for (int i = 0; i < handCardCount; i++)
        {
            MoveCardToDestination(hand[0], 1, 2);
        }
    }

    public void RecycleDeck()
    {
        int graveyardCount = graveyard.Count;      
        for (int i = 0; i < graveyardCount; i++)
        {
            MoveCardToDestination(graveyard[0], 2, 0);
        }
        ShuffleDeck();
    }

    private void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Card temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }
}
