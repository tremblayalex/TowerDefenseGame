using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HP_Manager : MonoBehaviour
{
    public int Vie = 10;
    public GameObject deathCanvasPrefab;
    public GameObject deathCanvasPrefabInstantiation;

    public void LoseHP(int amount)
    {
        TextMeshProUGUI myText = gameObject.GetComponent<TextMeshProUGUI>();

        Vie = Vie - amount;
        myText.text = Vie.ToString();
        if (Vie <= 0)
        {
            myText.text = "0";
            Time.timeScale = 0;
            deathCanvasPrefabInstantiation = Instantiate(deathCanvasPrefab, new Vector3(0, 0, 0), gameObject.transform.rotation);
            deathCanvasPrefabInstantiation.layer = 5;
        }
    }

    public void LoseHP()
    {
        TextMeshProUGUI myText = gameObject.GetComponent<TextMeshProUGUI>();

        Vie = Vie - 1;
        myText.text = Vie.ToString();
        if (Vie <= 0)
        {
            myText.text = "0";

            Camera.main.GetComponent<GameManager>().EndGame();
        }
    }
}
