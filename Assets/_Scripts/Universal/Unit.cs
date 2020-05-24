using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    //Unit Data
    [SerializeField]
    private string unitName;
    [SerializeField]
    private int curHP;
    [SerializeField]
    private int maxHP;
    [SerializeField]
    private int damage;
    [SerializeField]
    private int curMoves;
    [SerializeField]
    private int maxMoves;
    [SerializeField]
    private int defence;
    [SerializeField]
    private bool focused;
    [SerializeField]
    private int focusMultiplier;
    [SerializeField]
    private int coinValue;
    [SerializeField]
    private Animator animator;

    public int handSize;

    [SerializeField]
    private ScriptableObjectPlayerStats unitStats;

    //Properties for Getting Data from other classes
    public string UnitName { get => unitName; set => unitName = value; }
    public int CurHP { get => curHP; set => curHP = value; }
    public int MaxHP { get => maxHP; set => maxHP = value; }
    public int Damage { get => damage; set => damage = value; }
    public int CurMoves { get => curMoves; set => curMoves = value; }
    public int MaxMoves { get => maxMoves; set => maxMoves = value; }
    public int Defence { get => defence; set => defence = value; }
    public bool Focused { get => focused; set => focused = value; }
    public int FocusMultiplier { get => focusMultiplier; set => focusMultiplier = value; }
    public int CoinValue { get => coinValue; set => coinValue = value; }
    public Animator Animator { get => animator; set => animator = value; }
    public ScriptableObjectPlayerStats UnitStats { get => unitStats; set => unitStats = value; }

    private void Awake()
    {
        curHP = unitStats.curHP;
        maxHP = unitStats.maxHP + (unitStats.healthArtefact * 20);

        damage = unitStats.damage;

        curMoves = unitStats.maxMoves;
        maxMoves = unitStats.maxMoves + (unitStats.movesArtefact * 1);
    }

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
