using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundToggle : MonoBehaviour
{
    public GameObject xBtn;

    private void Start()
    {
        Debug.Log("rockjae0 : " + DataManager.Instance.getAudioSetting());
        xBtn.SetActive(DataManager.Instance.getAudioSetting() == 0 ? true : false);
    }

    public void ToggleAudioVolume()
    {
        DataManager.Instance.setAudioSetting((int)AudioListener.volume);
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
        xBtn.SetActive(!xBtn.activeSelf);
        if (DataManager.Instance.getAudioSetting() != (int)AudioListener.volume)
        {
            setOnMoreTimer();
        }
        else
        {
            Debug.Log("rockjae0 match : " + DataManager.Instance.getAudioSetting());
        }
    }

    void setOnMoreTimer()
    {
        DataManager.Instance.setAudioSetting((int)AudioListener.volume);
        if (DataManager.Instance.getAudioSetting() != (int)AudioListener.volume)
        {
            Debug.Log("rockjae0 notmatch : " + DataManager.Instance.getAudioSetting());
            setOnMoreTimer();
        }
        else
        {
            Debug.Log("rockjae0 match : " + DataManager.Instance.getAudioSetting());
        }
    }
}
