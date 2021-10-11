using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FieldOutGameOver : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    public GameObject GameOverIMG;

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameOverIMG.SetActive(true);
        audioSource.clip = audioClip;
        audioSource.Play();
        Time.timeScale = 0;
    }

    public void onClickTitleScene()
    {
        SceneManager.LoadScene("Title");
    }
}
