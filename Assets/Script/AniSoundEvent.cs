using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniSoundEvent : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<AudioSource>())
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void playAudio()
    {
        if(audioSource != null)
        {
            audioSource.Play();
        }
    }
}
