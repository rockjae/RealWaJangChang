using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    [HideInInspector]
    public AudioSource audioSource;
    private AudioClip mainSound;
    private AudioClip infinityModeSound;
    private AudioClip endureStageSound;
    private AudioClip highNoonStageSound;
    private AudioClip sadSound;
    private string lastScene;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<SoundManager>();
                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    var newSingleton = new GameObject("SoundManager").AddComponent<SoundManager>();
                    instance = newSingleton;
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        var obj = FindObjectsOfType<SoundManager>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
        mainSound = Resources.Load<AudioClip>("mainSound");
        infinityModeSound = Resources.Load<AudioClip>("infinityModeSound");
        endureStageSound = Resources.Load<AudioClip>("endureStageSound");
        highNoonStageSound = Resources.Load<AudioClip>("wind"); 
        sadSound = Resources.Load<AudioClip>("sadSound");
    }

    public void setSound(AudioClip audio)
    {
        audioSource.clip = audio;
        audioSource.Play();
    }

    public void setRestart()
    {
        lastScene = "";
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals(lastScene))
        {
            return;
        }

        switch (SceneManager.GetActiveScene().name)
        {
            case "Title":
            case "Stage":
            case DataManager.DataName.STAGE1_NAME:
            case DataManager.DataName.STAGE2_NAME:
            case DataManager.DataName.STAGE3_NAME:
            case DataManager.DataName.STAGE5_NAME:
            case DataManager.DataName.STAGE6_NAME:
                {
                    if (SoundManager.Instance.audioSource.clip != SoundManager.Instance.mainSound)
                    {
                        SoundManager.Instance.audioSource.clip = SoundManager.Instance.mainSound;
                        SoundManager.Instance.audioSource.Play();
                    }
                    break;
                }
            case DataManager.DataName.FLAPPY_GAME_NAME:
            case DataManager.DataName.SHOOTINGSAUSAGE_GAME_NAME:
                {
                    if (SoundManager.Instance.audioSource.clip != SoundManager.Instance.infinityModeSound)
                    {
                        SoundManager.Instance.audioSource.clip = SoundManager.Instance.infinityModeSound;
                        SoundManager.Instance.audioSource.Play();
                    }
                    break;
                }
            case DataManager.DataName.STAGE4_NAME:
                {
                    if (SoundManager.Instance.audioSource.clip != SoundManager.Instance.endureStageSound)
                    {
                        SoundManager.Instance.audioSource.clip = SoundManager.Instance.endureStageSound;
                        SoundManager.Instance.audioSource.Play();
                    }
                    break;
                }
            case DataManager.DataName.HIGHNOON_GAME_NAME:
                {
                    if (SoundManager.Instance.audioSource.clip != SoundManager.Instance.highNoonStageSound)
                    {
                        SoundManager.Instance.audioSource.clip = SoundManager.Instance.highNoonStageSound;
                        SoundManager.Instance.audioSource.Play();
                    }
                    break;
                }
            case DataManager.DataName.STAGE0_NAME:
                {
                    SoundManager.Instance.audioSource.Stop();
                    break;
                }
        }
        lastScene = SceneManager.GetActiveScene().name;
    }
}
