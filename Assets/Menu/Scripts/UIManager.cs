using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI textHP;
    public TextMeshProUGUI textMoney;

    public TextMeshProUGUI textCostTower1;
    public TextMeshProUGUI textCostTower2;
    public TextMeshProUGUI textCostTower3;

    public TextMeshProUGUI textDPSTower1;
    public TextMeshProUGUI textDPSTower2;

    private void Start()
    {
        TowerSettings towerSettings = GameObject.FindObjectOfType<TowerSettings>();

        MachineGunTowerScriptableObject mgso = towerSettings.machineGunTowerScriptableObjects[0];
        MissileLauncherTowerScriptableObject mlso = towerSettings.missileLauncherTowerScriptableObjects[0];
        FreezerTowerScriptableObject fso = towerSettings.freezerTowerScriptableObjects[0];

        DisplayTowerPrices(mgso.price, mlso.price, fso.price);
        DisplayTowerDPS(mgso.damage, mgso.fireRate, mlso.damage, mlso.fireRate);
    }

    private void DisplayTowerPrices(float firstTowerPrice, float secondTowerPrice, float thirdTowerPrice)
    {
        textCostTower1.text = firstTowerPrice.ToString();
        textCostTower2.text = secondTowerPrice.ToString();
        textCostTower3.text = thirdTowerPrice.ToString();
    }

    private void DisplayTowerDPS(float firstTowerDamage, float firstTowerFireRate, float secondTowerDamage, float secondTowerFireRate)
    {
        float firstTowerDPS = firstTowerDamage * (1f / firstTowerFireRate);
        float secondTowerDPS = secondTowerDamage * (1f / secondTowerFireRate);

        textDPSTower1.text = "DPS: " + firstTowerDPS.ToString("#.#");
        textDPSTower2.text = "DPS: " + secondTowerDPS.ToString("#.#");
    }

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
