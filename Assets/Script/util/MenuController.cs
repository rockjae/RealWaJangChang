using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject MenuOBJ;
    bool pauseActive;
    public void pauseBtn()
    {
        if (pauseActive)
        {
            Time.timeScale = 1;
            pauseActive = false;
            MenuOBJ.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            pauseActive = true;
            MenuOBJ.SetActive(true);
        }
    }
}
