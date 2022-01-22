using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMoneyUpdate : MonoBehaviour
{
    public TMPro.TextMeshProUGUI moneyText;

    public void UpdateCoins(int coins)
    {
        moneyText.text = ": " + coins + " coins";
    }
}
