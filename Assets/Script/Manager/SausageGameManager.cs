using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SausageGameManager : MonoBehaviour
{
    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip bossClip;

    [Header("Sausage")]
    public GameObject Sausage;

    [Header("startOBJ")]
    public GameObject StageGuide;
    public FollowCamera followCamera;
    public BlockController blockController;
    private bool isStart;

    [Header("Boss")]
    private bool isBoss;
    private float BossStartPos;
    public GameObject Boss;
    public MicroWave microWave;

    [Header("Flag")]
    private Collision2D flagCollision;

    public SausageController sausageController;

    private void Awake()
    {
        Time.timeScale = 0;
        blockController.Flag.GetComponent<FlagEvent>().FlagCollision += FlagEventOn; //c#에서 자주쓰는 이벤트로 구현해봤음
        followCamera.BossOn += BossStart;
    }

    private void Update()
    {
        if (!isStart && Input.anyKeyDown)
        {
            StartCoroutine(onStartTouch());
            isStart = true;
        }
    }

    private IEnumerator onStartTouch()
    {
        Time.timeScale = 1;
        StageGuide.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        blockController.blockMakeStart(); // 블럭 만들기 시작
        yield return new WaitForSeconds(0.5f);
        followCamera.isStart = true; //카메라 움직이기 시작
    }

    private void FlagEventOn(Collision2D collision)
    {
        if (!isBoss)
        {
            followCamera.isStart = false;
            audioSource.clip = bossClip;
            audioSource.Play();
            isBoss = true;

            BossStartPos = Sausage.transform.position.y;
            StartCoroutine(followCamera.startBoss(BossStartPos));
        }
    }

    private void BossStart() //보스 시작을 알림
    {
        Boss.SetActive(true);
        Boss.transform.position = new Vector3(0, BossStartPos + 10, 0);
        StartCoroutine(BossMove());
        sausageController.HpMakeStart();
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
                blockController.setBossBlock();//보스 블럭 최초 생성
                microWave.bossStart();
                break;
            }
            yield return null;
        }
    }

   
}