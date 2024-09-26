using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void onClickTutorialScene()
    {
        DataManager.Instance.saveStageValue(DataManager.DataName.STAGE0_NAME, false);
        SceneManager.LoadScene("Tutorial");
    }
    public void onClickTitleScene()
    {
        SceneManager.LoadScene("Title");
    }

    public void onClickStageScene()
    {
        SceneManager.LoadScene("Stage");
    }
    public void onClickMainScene(int StgaeNum)
    {
        SceneManager.LoadScene("Main"+ StgaeNum);
    }

    public void onClickFlappySausage()
    {
        SceneManager.LoadScene("FlappySausage");
    }

    public void onClickShootingSausage()
    {
        SceneManager.LoadScene("ShootingSausage");
    }
    public void onClickHighNoonSausage()
    {
        SceneManager.LoadScene("HighNoonSausage");
    }
}
