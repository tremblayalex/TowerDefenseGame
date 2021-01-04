using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCounterManager : MonoBehaviour
{
    private long NumberOfKills;

    private UIManager uiManager;


    private void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        NumberOfKills = 0;
        uiManager.DisplayKills(0);

    }

    public void AddOneKill()
    {
        NumberOfKills = NumberOfKills + 1;
        uiManager.DisplayKills(NumberOfKills);
    }
}
