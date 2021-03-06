﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    protected SoundPlayer soundPlayer;
    public AudioSoundEffect damagedSound;

    public int startingLife = 10;
    
    private int life;
    private UIManager uiManager;
    private GameManager gameManager;

    private void Start()
    {
        life = startingLife;

        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        gameManager = Camera.main.GetComponent<GameManager>();
        uiManager.DisplayHP(life);
    }

    public void LoseHP(int amount)
    {
        life -= amount;

        if (life <= 0)
        {
            life = 0;

            uiManager.DisplayHP(life);
            gameManager.EndGame();
        }
        else
        {
            uiManager.DisplayHP(life);
        }
        PlayDamagedSound();
    }
    private void PlayDamagedSound()
    {
        soundPlayer = FindObjectOfType<SoundPlayer>();
        soundPlayer.PlaySound(damagedSound);
    }
}
