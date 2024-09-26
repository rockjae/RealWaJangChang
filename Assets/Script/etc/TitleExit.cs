using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleExit : MonoBehaviour
{
    public Button exitBtn;
    public GameObject exitClick;

    private void Start()
    {
        exitBtn.onClick.AddListener(() => StartCoroutine(onClickExitBtn()));
    }

    IEnumerator onClickExitBtn()
    {
        exitClick.SetActive(true);
        yield return new WaitForSeconds(3f);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Debug.Log("3");
        Application.Quit(); // 어플리케이션 종료
#endif
        yield return null;
    }
}
