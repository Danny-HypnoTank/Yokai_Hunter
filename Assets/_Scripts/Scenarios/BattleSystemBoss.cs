using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class BattleSystemBoss : BattleSystem
{
    private void Start() //Setting up battle on Start
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    private void Update()
    {
        if (Enemy1Unit.CurHP == 0)
        {
            Enemy1Unit.Animator.SetInteger("UnitAS", 6);
        }

        if (PlayerUnit.CurHP == 0)
        {
            PlayerUnit.Animator.SetInteger("UnitAS", 6);
        }
    }

#region Methods

    private void PlayerTurn() //Setting player turn on transition from any state to player turn
    {
        PlayerUnit.CurMoves = PlayerUnit.MaxMoves; //Reseting Player moves to allow players to choose actions
        Enemy1Unit.CurMoves = Enemy1Unit.MaxMoves; //Reseting enemy 1 moves for enemy 1 next turn
        if (PlayerUnit.UnitStats.defenceArtefact == true)
        {
            PlayerUnit.Defence = 0;
            PlayerUnit.Defence += 5;
        }
        else
        {
            PlayerUnit.Defence = 0; //Reseting player defence
        }
        PlayerHUD.SetMoves(PlayerUnit); //Setting player moves UI
        PlayerHUD.SetFocus(PlayerUnit); //Setting player focus UI to check to see if they are still focused
        PlayerHUD.SetDefence(PlayerUnit); //Setting player defence UI to show updated defence
        CardManager.DrawHand(PlayerUnit.handSize);
        PlayerAbilityUI.SetActive(true); //Setting ability cards to active so that player can select what abilities they would like to do
        Enemy1TargetingUI.SetActive(false); //Setting enemy targeting UI to not active so that players can't target enemies until they have chosen an action
        PlayerHealTargetingUI.SetActive(false);
        PlayerDefendTargetingUI.SetActive(false);
        PlayerFocusTargetingUI.SetActive(false);
        //Check to see if enemies have 0 health, if so then proceed to the WON state of the battle
        if (Enemy1Unit.CurHP <= 0)
        {
            state = BattleState.WON;
        }

        EndTurnButton.SetActive(true);
        DialogueText.text = "Choose an action:"; 

    }

    public void TargetEnemy1Button() //Button to target enemy 1
    {
        if (state != BattleState.PLAYERTURN) //Check to make sure it is still the players turn
        {
            return;
        }
        else //Else Run attack and disable targeting so players can't target multiple enemies
        {
            Enemy1TargetingUI.SetActive(false);
            StartCoroutine(PlayerAttackEnemy1());
        }
    }

    public void TargetHero1HealButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        else
        {
            PlayerHealTargetingUI.SetActive(false);
            StartCoroutine(PlayerHealHero1());
        }
    }//Button to target hero 1 with a heal

    public void TargetHero1DefendButton() //Player Defend Card Action
    {
        if (state != BattleState.PLAYERTURN) //Check to see if it is the players turn
        {
            return;
        }
        else //Else run player defend and disable ability cards to make it so players can spam abilities
        {
            PlayerDefendTargetingUI.SetActive(false);
            StartCoroutine(PlayerDefendHero1());
        }
    }

    public void TargetHero1FocusButton() //Player Focus Card Action
    {
        if (state != BattleState.PLAYERTURN) //Check to make sure that it is the players turn
        {
            return;
        }
        else //Else run player focus and set player abilities to disabled so that players can spam abilities
        {
            PlayerFocusTargetingUI.SetActive(false);
            StartCoroutine(PlayerFocusHero1());
        }
    }

    public void EndTurnButtonClick()
    {
        StartCoroutine(EndTurn());
    }

    private void EndBattle() //End Battle Check
    {
        if (state == BattleState.WON) //Check to see if Game has transitioned to WON state
        {
            PlayerUnit.UnitStats.playerCoins += 200;
            PlayerHUD.SetCoins();
            StartCoroutine(Victory());
        }
        else if (state == BattleState.LOST) //Check to see if Game has transitioned to LOST state
        {
            PlayerAbilityUI.SetActive(false);
            DialogueText.text = "You were defeated.";
        }
    }
    #endregion

#region Coroutines
    IEnumerator SetupBattle() //Setting Up the Battle
    {
        PlayerAbilityUI.SetActive(false);

        //Instantiate Player at Spawnpoint
        GameObject playerGo = Instantiate(PlayerPrefab, PlayerSpawnPoint);
        PlayerUnit = playerGo.GetComponent<Unit>();
        CardManager.SetDeck();

        //Instantiate Enemy 1 at Spawnpoint
        GameObject enemy1Go = Instantiate(Enemy1Prefab[Random.Range(0, 2)], Enemy1SpawnPoint);
        Enemy1Unit = enemy1Go.GetComponent<Unit>();

        DialogueText.text = "A wild " + Enemy1Unit.UnitName + " approaches...";

        PlayerHUD.SetHUD(PlayerUnit);
        Enemy1HUD.SetHUD(Enemy1Unit);
        PlayerHUD.SetCoins();
        PlayerUnit.Focused = false;

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator Victory()
    {
        PlayerAbilityUI.SetActive(false);
        DialogueText.text = "You've won the battle!";

        yield return new WaitForSeconds(2);

        WinMenu.SetActive(true);
        //LoadingUI.SetActive(true);
    }

    //Player Attacking Enemy 1 Action
    IEnumerator PlayerAttackEnemy1()
    {
        if (PlayerUnit.Focused == true)
        {
            Enemy1Unit.TakeDamage(CurrentCard.Power + PlayerUnit.FocusMultiplier + (PlayerUnit.UnitStats.damageArtefact * 10));
            PlayerUnit.Focused = false;
            PlayerHUD.SetFocus(PlayerUnit);
        }
        else
        {
            Enemy1Unit.TakeDamage(CurrentCard.Power + (PlayerUnit.UnitStats.damageArtefact * 10));
        }
        PlayerUnit.CurMoves -= CurrentCard.ResourceCost;

        PlayerHUD.SetMoves(PlayerUnit);
        Enemy1HUD.SetHP(Enemy1Unit.CurHP, Enemy1Unit);
        DialogueText.text = "The attack is successful!";
        PlayerUnit.Animator.SetInteger("UnitAS", 1);
        Enemy1Unit.Animator.SetInteger("UnitAS", 5);

        yield return new WaitForSeconds(2f);

        PlayerUnit.Animator.SetInteger("UnitAS", 0);
        Enemy1Unit.Animator.SetInteger("UnitAS", 0);

        if (Enemy1Unit.CurHP <= 0 && Enemy2Unit.CurHP <= 0)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else if (PlayerUnit.CurMoves > 0)
        {
            DialogueText.text = "Choose an action:";
            PlayerAbilityUI.SetActive(true);
        }
        else if (PlayerUnit.CurMoves <= 0)
        {
            StartCoroutine(EndTurn());
        }
    }

    //Player Heal Action
    IEnumerator PlayerHealHero1()
    {
        if (PlayerUnit.Focused == true)
        {
            PlayerUnit.Heal(CurrentCard.Power + PlayerUnit.FocusMultiplier);
            PlayerUnit.Focused = false;
            PlayerHUD.SetFocus(PlayerUnit);
        }
        else
        {
            PlayerUnit.Heal(CurrentCard.Power);
        }

        PlayerUnit.CurMoves -= CurrentCard.ResourceCost;

        PlayerHUD.SetHP(PlayerUnit.CurHP, PlayerUnit);
        PlayerHUD.SetMoves(PlayerUnit);

        PlayerUnit.Animator.SetInteger("UnitAS", 4);

        DialogueText.text = "You feel renewed strength!";

        yield return new WaitForSeconds(3f);

        PlayerUnit.Animator.SetInteger("UnitAS", 0);

        if (PlayerUnit.CurMoves > 0)
        {
            DialogueText.text = "Choose an action:";
            PlayerAbilityUI.SetActive(true);
        }
        else if (PlayerUnit.CurMoves <= 0)
        {
            StartCoroutine(EndTurn());
        }
    }

    //Player Defend Action
    IEnumerator PlayerDefendHero1()
    {
        PlayerUnit.Defence += CurrentCard.Power;
        PlayerUnit.CurMoves -= CurrentCard.ResourceCost;

        PlayerHUD.SetMoves(PlayerUnit);
        PlayerHUD.SetDefence(PlayerUnit);

        DialogueText.text = "You prepare for the enemy to strike!";
        PlayerUnit.Animator.SetInteger("UnitAS",2);

        yield return new WaitForSeconds(2f);

        PlayerUnit.Animator.SetInteger("UnitAS", 0);

        if (PlayerUnit.CurMoves > 0)
        {
            DialogueText.text = "Choose an action:";
            PlayerAbilityUI.SetActive(true);
        }
        else if (PlayerUnit.CurMoves <= 0)
        {
            StartCoroutine(EndTurn());
        }
    }

    //Player Focus Action
    IEnumerator PlayerFocusHero1()
    {
        PlayerUnit.CurMoves -= CurrentCard.ResourceCost;
        PlayerUnit.Focused = true;

        PlayerHUD.SetMoves(PlayerUnit);
        PlayerHUD.SetFocus(PlayerUnit);

        DialogueText.text = "You focus your energy for a leathal strike!";
        PlayerUnit.Animator.SetInteger("UnitAS", 3);

        yield return new WaitForSeconds(2.5f);

        PlayerUnit.Animator.SetInteger("UnitAS", 0);

        if (PlayerUnit.CurMoves > 0)
        {
            DialogueText.text = "Choose an action:";
            PlayerAbilityUI.SetActive(true);
        }
        else if (PlayerUnit.CurMoves <= 0)
        {
            StartCoroutine(EndTurn());
        }
    }

    //Enemy 1 Turn Pattern
    IEnumerator Enemy1Turn()
    {
        CardManager.TurnEndCardCycle();
        if (Enemy1Unit.CurHP > 0)
        {
            DialogueText.text = Enemy1Unit.UnitName + " attacks!";
            Enemy1Unit.CurMoves -= 1;

            yield return new WaitForSeconds(1f);

            bool isDead = PlayerUnit.TakeDamage(Enemy1Unit.Damage);

            PlayerHUD.SetHP(PlayerUnit.CurHP, PlayerUnit);
            PlayerHUD.SetDefence(PlayerUnit);
            Enemy1HUD.SetMoves(Enemy1Unit);

            PlayerUnit.Animator.SetInteger("UnitAS", 5);
            Enemy1Unit.Animator.SetInteger("UnitAS", 1);
            yield return new WaitForSeconds(2f);

            PlayerUnit.Animator.SetInteger("UnitAS", 0);
            Enemy1Unit.Animator.SetInteger("UnitAS", 0);
            if (isDead)
            {
                state = BattleState.LOST;
                EndBattle();
            }
            else if (!isDead && Enemy1Unit.CurMoves > 0)
            {
                StartCoroutine(Enemy1Turn());
            }
            else if (!isDead && Enemy1Unit.CurMoves <= 0)
            {
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
        }
    }

    IEnumerator EndTurn()
    {
        EndTurnButton.SetActive(false);
        PlayerAbilityUI.SetActive(false);
        CardManager.TurnEndCardCycle();
        Debug.Log("Cards cycled");
        DialogueText.text = "Enemies are Preparing to attack";
        yield return new WaitForSeconds(4);
        state = BattleState.ENEMY1TURN;
        StartCoroutine(Enemy1Turn());
    }
    #endregion

#region EnemyBehaviour Options
    //Enemy Attack
    private void EnemyAttack()
    {
        DialogueText.text = Enemy1Unit.UnitName + " attacks!";
        Enemy1Unit.CurMoves -= 1;
    }

    //Enemy Panic
    private void EnemyPanic()
    {
        DialogueText.text = Enemy1Unit.UnitName + " Panics!";
        Enemy1Unit.CurMoves -= 1;
    }
    //Further Option Ideas:
    #endregion
}
