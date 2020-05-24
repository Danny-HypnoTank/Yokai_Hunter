using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardLogic/DmgAttackCard")]
public class TakeDamageAttack : BaseCard
{
    public override void Execute(BattleSystem battleSys, CardManager cardSys)
    {
        battleSys.CurrentCard = this;
        Debug.Log("Dmg Attack Logic");

        if (battleSys.state != BattleState.PLAYERTURN) //Check to make sure it is the players turn so they can do this action
        {
            return;
        }
        else //Else make it so goes into targeting so they can attack multiple enemies
        {
            battleSys.DialogueText.text = "Select an enemy to attack";
            battleSys.PlayerAbilityUI.SetActive(false);
            battleSys.PlayerUnit.CurHP -= 2;

            if (battleSys.Enemy1Unit.CurHP > 0)
            {
                battleSys.Enemy1TargetingUI.SetActive(true);
            }

            if (battleSys.Enemy2Unit != null)
            {
                if (battleSys.Enemy2Unit.CurHP > 0)
                {
                    battleSys.Enemy2TargetingUI.SetActive(true);
                }
            }

            if (battleSys.Enemy3Unit != null)
            {
                if (battleSys.Enemy3Unit.CurHP > 0)
                {
                    battleSys.Enemy3TargetingUI.SetActive(true);
                }
            }

            if (battleSys.Enemy4Unit != null)
            {
                if (battleSys.Enemy4Unit.CurHP > 0)
                {
                    battleSys.Enemy4TargetingUI.SetActive(true);
                }
            }
        }
    }
}
