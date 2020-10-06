using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HP_Manager : MonoBehaviour
{
    public int Vie = 10;
    


    public void LoseHP()
    {
        TextMeshProUGUI myText = gameObject.GetComponent<TextMeshProUGUI>();

        if (Vie > 0)
        {
            Vie = Vie - 1;
        }
        if (Vie <= 0)
        {
            myText.text = "Vous êtes mort";
        }
        else
        {
            myText.text = Vie.ToString();
        }
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

}
