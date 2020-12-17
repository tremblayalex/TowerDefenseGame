using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    protected SoundPlayer soundPlayer;
    public AudioSoundEffect ThemeSongSound;

    private static readonly int gameAreaSceneIndex = 1;

    public Button pauseButton;
    public Sprite pauseSprite;
    public Sprite playSprite;

    public Button[] towerPurchaseButtons;
    public Button[] towerOptionButtons;

    public GameObject pauseMessagePrefab;
    public GameObject deathCanvasPrefab;

    private GameObject pauseMessageInstantiation;
    private GameObject deathCanvasPrefabInstantiation;

    private TowerManager towerManager;

    private enum GameState
    {
        Running,
        Paused,
        Ended
    }

    private GameState gameState;

    void Start()
    {
        pauseButton.image.sprite = pauseSprite;
        gameState = GameState.Running;

        towerManager = GameObject.Find("TowerManager").GetComponent<TowerManager>();
        PlayThemeSongSound();
    }

    void Update()
    {
        VerifyKeystrokes();
    }

    private void VerifyKeystrokes()
    {
        if (Input.GetKeyDown("p") || Input.GetKeyDown("space"))
        {
            TogglePause();
        }
        else if (Input.GetKeyDown("r"))
        {
            RestartGame();
        }
    }

    public void TogglePause()
    {
        if (gameState == GameState.Running)
        {
            PauseGame();
        }
        else if (gameState == GameState.Paused)
        {
            UnPauseGame();
        }
        else
        {
            RestartGame();
        }
    }

    private void UnPauseGame()
    {
        Time.timeScale = 1;
        
        Destroy(pauseMessageInstantiation);
        pauseButton.image.sprite = pauseSprite;
        EnableAllTowerButtons();

        gameState = GameState.Running;
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        
        pauseMessageInstantiation = Instantiate(pauseMessagePrefab, new Vector3(0, 0, 0), gameObject.transform.rotation);
        pauseMessageInstantiation.layer = 5;
        pauseButton.image.sprite = playSprite;
        DisableAllTowerButtons();
        towerManager.DisableTowerPurchaseMode();

        gameState = GameState.Paused;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(gameAreaSceneIndex);

        gameState = GameState.Running;
    }

    public void EndGame()
    {
        if (gameState != GameState.Ended)
        {
            Time.timeScale = 0;

            deathCanvasPrefabInstantiation = Instantiate(deathCanvasPrefab, new Vector3(0, 0, 0), gameObject.transform.rotation);
            deathCanvasPrefabInstantiation.layer = 5;
            pauseButton.image.sprite = playSprite;
            DisableAllTowerButtons();
            towerManager.DisableTowerPurchaseMode();

            gameState = GameState.Ended;
        }    
    }

    private void EnableAllTowerButtons()
    {
        foreach (Button button in towerPurchaseButtons)
        {
            button.interactable = true;
        }
        foreach (Button button in towerOptionButtons)
        {
            button.interactable = true;
        }
    }

    private void DisableAllTowerButtons()
    {
        foreach (Button button in towerPurchaseButtons)
        {
            button.interactable = false;
        }
        foreach (Button button in towerOptionButtons)
        {
            button.interactable = false;
        }
    }
    private void PlayThemeSongSound()
    {
        soundPlayer = FindObjectOfType<SoundPlayer>();
        soundPlayer.PlaySound(ThemeSongSound);
    }
}
