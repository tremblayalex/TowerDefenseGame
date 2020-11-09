using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseMessagePrefab;
    public GameObject pauseMessageInstantiation;

    void Update()
    {
        if (Input.GetKeyDown("p") || Input.GetKeyDown("space"))
        {
            PauseGame();
        }
        else if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(1);
        }
    }

    public void PauseGame()
    {
        if (Time.timeScale == 1)
        {
            print("pause");
            Time.timeScale = 0;
            pauseMessageInstantiation = Instantiate(pauseMessagePrefab, new Vector3(0,0,0), gameObject.transform.rotation);
            pauseMessageInstantiation.layer = 5;
        }
        else
        {
            print("unpause");
            Time.timeScale = 1;
            Destroy(pauseMessageInstantiation);
        }
    }
}
