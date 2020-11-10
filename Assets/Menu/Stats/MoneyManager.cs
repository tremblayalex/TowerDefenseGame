using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public int startingMoney = 300;

    private int money;

    private UIManager uiManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            SpendMoney(100);
        }
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            AddMoney(100);
        }
    }

    void Start()
    {
        money = startingMoney;

        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        uiManager.DisplayMoney(money);
    }

    public bool SpendMoney(int amount)
    {
        bool successfullySpentMoney = false;

        if (money - amount >= 0)
        {
            money -= amount;
            successfullySpentMoney = true;

            uiManager.DisplayMoney(money);
        }

        if (!successfullySpentMoney)
        {
            uiManager.PlayInsufficientFundsAnimation();
        }

        return successfullySpentMoney;
    }

    public void AddMoney(int amount)
    {
        money += amount;

        uiManager.DisplayMoney(money);
    }
}
