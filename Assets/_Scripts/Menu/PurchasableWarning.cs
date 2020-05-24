using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchasableWarning : MonoBehaviour
{
    private Text text;
    private void Awake()
    {
        text = this.gameObject.GetComponent<Text>();
        text.text = "See anything you like?";
    }

    public IEnumerator NotPurchasable()
    {
        text.text = "You don't have enough coins for that";
        yield return new WaitForSeconds(2);
        text.text = "See anything else you like?";
    }
}
