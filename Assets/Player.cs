using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int Vie = 100;
    public int Argent = 100;
    

    void Mourir()
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
        if (Input.GetAxis("Fire1") != 0)
        {
            Mourir();
        }
    }
}
