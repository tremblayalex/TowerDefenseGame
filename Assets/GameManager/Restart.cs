using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void RestartGame(int indexScene)
    {
        SceneManager.LoadScene(indexScene);
        Time.timeScale = 1;
    }
}
