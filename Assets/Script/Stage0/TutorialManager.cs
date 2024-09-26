using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public AudioClip audioClip;
    private void Start()
    {
        Application.targetFrameRate = 60;
        //PlayerPrefs.DeleteAll();
        if (DataManager.Instance.loadStageValue(DataManager.DataName.STAGE0_NAME) == 1)
        {
            gotoTitleScene();
        }
        else
        {
            DataManager.Instance.saveStageValue(DataManager.DataName.STAGE0_NAME, true);
        }
        StartCoroutine(waitSoundPlay());
    }

    public void gotoTitleScene()
    {
        SceneManager.LoadScene("Title");
    }

    IEnumerator waitSoundPlay()
    {
        yield return new WaitForSeconds(0.2f);
        SoundManager.Instance.setSound(audioClip);
    }
}
