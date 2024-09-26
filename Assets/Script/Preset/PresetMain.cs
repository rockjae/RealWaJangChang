using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PresetMain : MonoBehaviour
{
    [Header("PresetSausageController")]
    public PresetSausageController PresetSausageController;
    [Header("PresetBlockController")]
    public PresetBlockController PresetBlockController;

    [Header("Sound")]
    //public AudioSource audioSource;
    private AudioClip bossClip;

    [Header("Sausage")]
    public GameObject Sausage;

    [Header("startOBJ")]
    public GameObject StageGuide;
    public FollowCamera followCamera;
    protected bool isStart;

    [Header("Boss")]
    private bool isBoss;
    private float BossStartPos;
    public GameObject Boss;

    [Header("Flag")]
    private Collision2D flagCollision;
    [Header("FieldOutGameObject")]
    public FieldOverGameOverOBJ fieldOverGameOver;

    [Header("GameOver")]
    public AudioClip audioClip;
    public GameObject GameOverIMG;
    public GameObject pauseOBJ;
    [Header("GameClear")]
    public GameObject GameClaerIMG;
    public AudioClip audioClip_clear;

    private void Awake()
    {
        Time.timeScale = 0;
        PresetBlockController.Flag.GetComponent<FlagEvent>().FlagCollision += FlagEventOn; //c#에서 자주쓰는 이벤트로 구현해봤음
        fieldOverGameOver.GameOverCollision += FOGameOver; //c#에서 자주쓰는 이벤트로 구현해봤음
        followCamera.BossOn += BossStart;

        bossClip = Resources.Load<AudioClip>("SausageMan");
    }

    private void Update()
    {
        if (!isStart && Input.anyKeyDown)
        {
            onStart();
        }
    }

    private IEnumerator onStartTouch()
    {
        Time.timeScale = 1;
        StageGuide.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        PresetBlockController.blockMakeStart(); // 블럭 만들기 시작
        yield return new WaitForSeconds(0.5f);
        followCamera.isStart = true; //카메라 움직이기 시작
    }

    public void onStart()
    {
        StartCoroutine(onStartTouch());
        isStart = true;
    }

    private void FlagEventOn(Collision2D collision)
    {
        if (!isBoss)
        {
            followCamera.isStart = false;

            SoundManager.Instance.setSound(bossClip);
            isBoss = true;

            //BossStartPos = Sausage.transform.position.y;
            BossStartPos = PresetBlockController.Flag.transform.position.y;
            StartCoroutine(followCamera.startBoss(BossStartPos));
        }
    }

    private void BossStart() //보스 시작을 알림
    {
        Boss.SetActive(true);
        Boss.transform.position = new Vector3(0, BossStartPos + 10, 0);
        StartCoroutine(BossMove());
        PresetSausageController.HpMakeStart();
        //sausageController.SausageAttackStart();
    }

    IEnumerator BossMove() //보스 등장 움직임
    {
        float tmp = Boss.transform.position.y;

        SpriteRenderer spr = Boss.GetComponent<SpriteRenderer>();
        Color color = spr.color;
        color.a = 0f;
        spr.color = color;

        while (true)
        {
            Boss.transform.position -= new Vector3(0, 0.7f * Time.deltaTime, 0);
            color.a += 0.5f * Time.deltaTime;
            spr.color = color;


            if (Boss.transform.position.y < tmp - 3f)
            {
                BossStartEvent();
                break;
            }
            yield return null;
        }
    }

    public virtual void BossStartEvent()
    {
        Debug.Log("BossStartEvent");
    }

    public void FOGameOver()
    {
        GameOverIMG.SetActive(true);
        pauseOBJ.SetActive(false);
        SoundManager.Instance.setSound(audioClip);
        Time.timeScale = 0;
    }

    public void onClickTitleScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }

    public void GameClear()
    {
        pauseOBJ.SetActive(false);
        GameClaerIMG.SetActive(true);
        SoundManager.Instance.setSound(audioClip_clear);

        Time.timeScale = 0;
    }
}