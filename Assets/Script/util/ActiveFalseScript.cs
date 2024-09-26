using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveFalseScript : MonoBehaviour
{
    public float timer=1;
    public bool isLast;
    public GameObject MainManager;

    void OnEnable()
    {
        Time.timeScale = 1;
        StartCoroutine(waitTimer());
    }

    IEnumerator waitTimer()
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);

        if (isLast)
        {
            MainManager.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
