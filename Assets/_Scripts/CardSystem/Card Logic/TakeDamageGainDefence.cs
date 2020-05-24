using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardLogic/DmgDefenceCard")]
public class TakeDamageGainDefence : BaseCard
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
            battleSys.PlayerUnit.CurHP -= 2;
            battleSys.DialogueText.text = "Select a hero to defend";
            battleSys.PlayerAbilityUI.SetActive(false);
            battleSys.PlayerDefendTargetingUI.SetActive(true);
        }
    }
}
