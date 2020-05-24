using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerStats")]
public class ScriptableObjectPlayerStats : ScriptableObject
{
    public int maxHP;
    public int curHP;

    public int curMoves;
    public int maxMoves;

    public int damage;

    public int defence;

    public int playerCoins;

    public int healthArtefact;
    public int movesArtefact;
    public int damageArtefact;
    public bool defenceArtefact;

    public bool TakeDamage(int dmg)
    {
        if (defence > dmg)
        {
            defence -= dmg;
        }
        else if (defence == dmg)
        {
            defence -= dmg;
        }
        else if (defence < dmg)
        {
            curHP -= dmg;
        }

        if (curHP <= 0)
        {
            curHP = 0;
            return true;
        }
        else
        {
            return false;
        }
    }


    public void Heal(int amount)
    {
        curHP += amount;
        if (curHP > maxHP)
        {
            curHP = maxHP;
        }
    }
}
