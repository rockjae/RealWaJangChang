using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FieldOutGameOver : MonoBehaviour
{
    [Header("GameOver")]
    public GameObject Sausage;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public GameObject GameOverIMG;
    public GameObject pauseOBJ;
    [Header("GameClear")]
    public GameObject GameClaerIMG;
    public AudioClip audioClip_clear;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Sausage)
        {
            FOGameOver();
        }
    }

    public void FOGameOver()
    {
        GameOverIMG.SetActive(true);
        pauseOBJ.SetActive(false);
        audioSource.clip = audioClip;
        audioSource.Play();
        Time.timeScale = 0;
    }

    public void onClickTitleScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }

    public void GameClear()
    {
        GameClaerIMG.SetActive(true);
        audioSource.clip = audioClip_clear;
        audioSource.Play();
        Time.timeScale = 0;
    }
}
