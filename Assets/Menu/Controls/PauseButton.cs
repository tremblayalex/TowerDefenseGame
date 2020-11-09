using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public Sprite pauseSprite;
    public Sprite playSprite;

    void Update()
    {
        Button boutton = gameObject.GetComponent<Button>();
        if (Time.timeScale == 0)
        {
            boutton.image.sprite = playSprite;
        }
        else
        {
            boutton.image.sprite = pauseSprite;
        }
    }
}
