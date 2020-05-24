using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardLogic/DefendCard")]
public class DefendCard : BaseCard
{
    public override void Execute(BattleSystem battleSys, CardManager cardSys)
    {
        battleSys.CurrentCard = this;
        Debug.Log("Defend Logic");
        if (battleSys.state != BattleState.PLAYERTURN)
        {
            return;
        }
        else
        {
            battleSys.DialogueText.text = "Select a hero to defend";
            battleSys.PlayerAbilityUI.SetActive(false);
            battleSys.PlayerDefendTargetingUI.SetActive(true);
        }
    }
}
