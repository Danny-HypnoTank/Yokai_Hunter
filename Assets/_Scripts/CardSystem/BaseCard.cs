using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCard : ScriptableObject
{
    [SerializeField]
    private int resourceCost;
    [SerializeField]
    private int power;

    public int ResourceCost { get => resourceCost; set => resourceCost = value; }
    public int Power { get => power; set => power = value; }

    public abstract void Execute(BattleSystem battleSys, CardManager cardSys);
}
