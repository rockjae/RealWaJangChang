using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSceneManager : MonoBehaviour
{
    public GameObject[] BtnOBJ;
    public GameObject FlappySausageBtn;
    public GameObject ShootingSausageBtn;
    public GameObject HighNoonSausageBtn;
    //public GameObject ShootingSausageBtn;
    public bool TestMode;

    private void Start()
    {
        if (!TestMode)
        {
            StageOpenCheck();
        }
    }

    private void StageOpenCheck()
    {
        if (DataManager.Instance.getFlappyGameOpen() != 1)
        {
            FlappySausageBtn.GetComponent<Button>().enabled = false;

            Color tmp = FlappySausageBtn.GetComponent<Image>().color;
            tmp.a *= 0.3f;
            FlappySausageBtn.GetComponent<Image>().color = tmp;

            Color tmpc = FlappySausageBtn.transform.GetChild(0).GetComponent<Image>().color;
            tmpc.a *= 0.3f;
            FlappySausageBtn.transform.GetChild(0).GetComponent<Image>().color = tmpc;
        }
        else
        {
            DataManager.Instance.saveStageValue(DataManager.DataName.STAGE3_NAME, true);//stage3 해금
        }

        if (DataManager.Instance.getShootingSausageGameOpen() != 1)
        {
            ShootingSausageBtn.GetComponent<Button>().enabled = false;

            Color tmp = ShootingSausageBtn.GetComponent<Image>().color;
            tmp.a *= 0.3f;
            ShootingSausageBtn.GetComponent<Image>().color = tmp;

            Color tmpc = ShootingSausageBtn.transform.GetChild(0).GetComponent<Image>().color;
            tmpc.a *= 0.3f;
            ShootingSausageBtn.transform.GetChild(0).GetComponent<Image>().color = tmpc;
        }

        if (DataManager.Instance.getHighNoonSausageGameOpen() != 1)
        {
            HighNoonSausageBtn.GetComponent<Button>().enabled = false;

            Color tmp = HighNoonSausageBtn.GetComponent<Image>().color;
            tmp.a *= 0.3f;
            HighNoonSausageBtn.GetComponent<Image>().color = tmp;

            Color tmpc = HighNoonSausageBtn.transform.GetChild(0).GetComponent<Image>().color;
            tmpc.a *= 0.3f;
            HighNoonSausageBtn.transform.GetChild(0).GetComponent<Image>().color = tmpc;
        }

        for (int i = 0; i < BtnOBJ.Length; i++)
        {
            if (!LoadStageOpen(i + 1))
            {
                BtnOBJ[i].GetComponent<Button>().enabled = false;

                Color tmp = BtnOBJ[i].GetComponent<Image>().color;
                tmp.a *= 0.3f;
                BtnOBJ[i].GetComponent<Image>().color = tmp;

                Color tmpc = BtnOBJ[i].transform.GetChild(0).GetComponent<Image>().color;
                tmpc.a *= 0.3f;
                BtnOBJ[i].transform.GetChild(0).GetComponent<Image>().color = tmpc;
            }
        }
    }

    public bool LoadStageOpen(int stageNum) //어떤 스테이지가 열렸는지 가져오는 함수
    {
        return DataManager.Instance.loadStageValue("Main"+stageNum) == 1 ? true:false;
    }
}
