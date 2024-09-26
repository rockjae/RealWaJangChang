using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage4Manager : MonoBehaviour
{
    private bool isStart;
    public GameObject StageGuide;
    [Header("Boss")]
    public GameObject BossText;
    public Text Text;
    int textCount;
    [Header("Clear")]
    public GameObject GameClaerIMG;
    public AudioClip audioClip_clear;

    private void Awake()
    {
        Time.timeScale = 0;
    }
    private void Update()
    {
        if (!isStart && Input.anyKeyDown)
        {
            Time.timeScale = 1;
            isStart = true;
            StageGuide.SetActive(false);
        }
    }

    public void bossStart()
    {
        Time.timeScale = 0;
        BossText.SetActive(true);
        Text.text = "음? 자네는?";
    }

    public void clickText()
    {
        textCount++;
        switch (textCount)
        {
            case 1:
                {
                    Text.text = "음? 자네는?";
                    break;
                }
            case 2:
                {
                    Text.text = "여기 보스는 내가 처리했네";
                    break;
                }
            case 3:
                {
                    Text.text = "정말...";
                    break;
                }
            case 4:
                {
                    Text.text = "잔혹무도 했지...";
                    break;
                }
            case 5:
                {
                    Text.text = "어서 다른 곳으로 가서\n남은 보스를 처지하게";
                    break;
                }
            case 6:
                {
                    DataManager.Instance.setShootingSausageGameOpen();
                    DataManager.Instance.saveStageValue(DataManager.DataName.STAGE5_NAME, true); // stage5 open
                    GameClaerIMG.SetActive(true);
                    SoundManager.Instance.setSound(audioClip_clear);
                    break;
                }
        }
    }
}
