using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
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
}
