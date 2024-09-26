using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject MenuOBJ;
    bool pauseActive;
    public bool isStopTime;
    public void pauseBtn()
    {
        if (pauseActive)
        {
            if (!isStopTime)
            {
                Time.timeScale = 1;
            }
            pauseActive = false;
            MenuOBJ.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            Debug.Log("Time.timeScale = 0;");
            pauseActive = true;
            MenuOBJ.SetActive(true);
        }
    }

    public void restartBtn()
    {
        Time.timeScale = 1;
        SoundManager.Instance.setRestart();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void returnStage()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Stage");
    }
}
