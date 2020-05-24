using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardLogic/FocusCard")]
public class FocusCard : BaseCard
{
    public override void Execute(BattleSystem battleSys, CardManager cardSys)
    {
        battleSys.CurrentCard = this;
        Debug.Log("Focus Logic");
        if (battleSys.state != BattleState.PLAYERTURN)
        {
            return;
        }
        else
        {
            battleSys.DialogueText.text = "Select a hero to focus";
            battleSys.PlayerAbilityUI.SetActive(false);
            battleSys.PlayerFocusTargetingUI.SetActive(true);
        }
    }
}
