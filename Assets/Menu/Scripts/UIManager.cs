using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI textHP;
    public TextMeshProUGUI textMoney;

    public TextMeshProUGUI textKills;

    public TextMeshProUGUI textCostTower1;
    public TextMeshProUGUI textCostTower2;
    public TextMeshProUGUI textCostTower3;

    public TextMeshProUGUI textDPSTower1;
    public TextMeshProUGUI textDPSTower2;

    public TextMeshProUGUI textUpgrade;
    public TextMeshProUGUI textSell;

    private CanvasGroup selectionPanel;
    private CanvasGroup upgradePanel;

    private void Start()
    {
        TowerSettings towerSettings = GameObject.FindObjectOfType<TowerSettings>();

        selectionPanel = GameObject.Find("SelectedOption").GetComponent<CanvasGroup>();
        upgradePanel = GameObject.Find("UpgradeOption").GetComponent<CanvasGroup>();

        MachineGunTowerScriptableObject mgso = towerSettings.machineGunTowerScriptableObjects[0];
        MissileLauncherTowerScriptableObject mlso = towerSettings.missileLauncherTowerScriptableObjects[0];
        FreezerTowerScriptableObject fso = towerSettings.freezerTowerScriptableObjects[0];

        DisplayTowerPrices(mgso.price, mlso.price, fso.price);
        DisplayTowerDPS(mgso.damage, mgso.fireRate, mlso.damage, mlso.fireRate);

        HideShowTowerInformation(false);
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

    public void DisplayInformationsTowerSelected(int MoneyToUpgrade, int MoneyGotForSell)
    {
        textUpgrade.text = MoneyToUpgrade.ToString();
        upgradePanel.alpha = 1;
        upgradePanel.interactable = true;
        textSell.text = MoneyGotForSell.ToString();
    }
    public void DisplayInformationsTowerSelected(int MoneyGotForSell)
    {
        textUpgrade.text = "MAX";
        upgradePanel.alpha = 0;
        upgradePanel.interactable = false;
        textSell.text = MoneyGotForSell.ToString();
    }

    public void HideShowTowerInformation(bool enabled)
    {
        if (enabled)
        {
            selectionPanel.alpha = 1;
        }
        else
        {
            selectionPanel.alpha = 0;
        }
        selectionPanel.interactable = enabled;
        upgradePanel.interactable = true;
    }

    public void DisplayHP(int hp)
    {
        textHP.text = hp.ToString();
    }

    public void DisplayMoney(long money)
    {
        textMoney.text = money.ToString();
    }

    public void DisplayKills(long kills)
    {
        textKills.text = kills.ToString();
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
