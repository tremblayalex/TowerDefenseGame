using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI textHP;
    public TextMeshProUGUI textMoney;

    public void DisplayHP(int hp)
    {
        textHP.text = hp.ToString();
    }

    public void DisplayMoney(int money)
    {
        textMoney.text = money.ToString();
    }

    public void PlayInsufficientFundsAnimation()
    {
        SetTextMoneyRedColor();
        Invoke("ResetTextMoneyColor", 0.25f);      
    }

    private void SetTextMoneyRedColor()
    {
        textMoney.color = new Color(1, 0.5f, 0.5f, 1);
    }

    private void ResetTextMoneyColor()
    {
        textMoney.color = Color.black;
    }
}
