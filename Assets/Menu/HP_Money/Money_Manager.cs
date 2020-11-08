using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class Money_Manager : MonoBehaviour
{
    public int money = 300;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            ChangerArgent(-100);
        }
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            ChangerArgent(100);
        }
    }

    public bool ChangerArgent(int inMoney) // Changer le nom
    {
        bool changeInMoney = false;
        TextMeshProUGUI myText = gameObject.GetComponent<TextMeshProUGUI>();
        

        if ((money+=inMoney) > 0)
        {
            money += inMoney;
            myText.text = money.ToString();
            changeInMoney = true;
        }
        else 
        {
            AnimationArgentInsufisant(myText);
        }
        return changeInMoney;
    }

    void AnimationArgentInsufisant(TextMeshProUGUI myText)
    {
        myText.color = new Color(1, 0.5f, 0.5f, 1);
    }
}
