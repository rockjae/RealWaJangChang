using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSceneManager : MonoBehaviour
{
    public const int MaxStage = 4; // 3스테이지까지 있다는 뜻
    public GameObject[] BtnOBJ;

    private void Start()
    {
        StageOpenCheck();
    }

    private void StageOpenCheck()
    {
        for (int i = 0; i < MaxStage; i++)
        {
            if (LoadStageOpen(i + 1))
            {
                BtnOBJ[i].SetActive(true);
            }
            else
            {
                BtnOBJ[i].SetActive(false);
            }
        }
    }

    public bool LoadStageOpen(int stageNum) //어떤 스테이지가 열렸는지 가져오는 함수
    {
        return PlayerPrefs.GetInt("Main"+stageNum) == 1 ? true:false;
    }

    public void SaveStageOpen(int stageNum)
    {
        if(stageNum > MaxStage)
        {
            Debug.Log("rockjae0 스테이지가 "+ stageNum+"스테이지 이상 구현 안됨");
            return;
        }
        PlayerPrefs.SetInt("Main" + stageNum, 1); // 0이면 미해금 1이면 해금 ㅇㅇ
    }

    public void SaveStageDisOpen(int stageNum) // 테스트 용
    {
        if (stageNum > MaxStage)
        {
            Debug.Log("rockjae0 스테이지가 " + stageNum + "스테이지 이상 구현 안됨");
            return;
        }
        PlayerPrefs.SetInt("Main" + stageNum, 0); // 0이면 미해금 1이면 해금 ㅇㅇ
    }
    public void SaveTestOff(int stage) // 테스트 용
    {
        SaveStageDisOpen(stage);
        StageOpenCheck();
    }

    public void SaveTestOn(int stage) // 테스트 용
    {
        SaveStageOpen(stage);
        StageOpenCheck();
    }
}
