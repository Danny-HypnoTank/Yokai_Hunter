using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private Text movesText;
    [SerializeField]
    private Text defenceText;
    [SerializeField]
    private GameObject focusObject;
    [SerializeField]
    private Text moneyText;
    [SerializeField]
    private ScriptableObjectPlayerStats playerStats;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.UnitName;
        healthText.text = unit.CurHP.ToString() + "/" + unit.MaxHP.ToString();
        healthSlider.maxValue = unit.MaxHP;
        healthSlider.value = unit.CurHP;
        movesText.text = unit.CurMoves.ToString() + "/" + unit.MaxMoves.ToString();
        defenceText.text = unit.Defence.ToString();
        focusObject.SetActive(false);
    }

    public void SetHP(int hp, Unit unit)
    {
        healthText.text = unit.CurHP.ToString() + "/" + unit.MaxHP.ToString();
        healthSlider.value = hp;
    }

    public void SetDefence(Unit unit)
    {

        defenceText.text = unit.Defence.ToString();
    }

    public void SetFocus(Unit unit)
    {
        if (unit.Focused == true)
        {
            focusObject.SetActive(true);
        }
        else
        {
            focusObject.SetActive(false);
        }
    }

    public void SetMoves(Unit unit)
    {
        movesText.text = unit.CurMoves.ToString() + "/" + unit.MaxMoves.ToString();
    }

    public void SetCoins()
    {
        moneyText.text = playerStats.playerCoins.ToString();
    }
}
