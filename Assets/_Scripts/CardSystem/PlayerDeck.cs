using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Deck")]
public class PlayerDeck : ScriptableObject
{
    public List<Card> playerDeck = new List<Card>();
}
