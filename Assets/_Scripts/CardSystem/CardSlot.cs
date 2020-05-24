using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    [SerializeField]
    private BattleSystem battleManager;
    [SerializeField]
    private CardManager cardManager;
    [SerializeField]
    private Card card;

    public Card Card { get => card; set => card = value; }

    public void OnClick()
    {
        if (battleManager.PlayerUnit.CurMoves >= card.cardLogic.ResourceCost)
        {
            Debug.Log("bUTTONcLICKED");
            card.cardLogic.Execute(battleManager, cardManager);
            cardManager.MoveCardToDestination(card, 1, 2);
            this.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Pick a different card");
            battleManager.DialogueText.text = "You cannot cast this card";
        }
    }

}
