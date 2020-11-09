using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Money_Manager : MonoBehaviour
{
    public int money = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            ChangerArgent(-1);
        }
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            ChangerArgent(1);
        }
    }

    void ChangerArgent(int inMoney)
    {
        money += inMoney;
        TextMeshProUGUI myText = gameObject.GetComponent<TextMeshProUGUI>();
        myText.text = money.ToString();
        
    }
}
