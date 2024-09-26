using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1PresetMain : PresetMain
{
    [Header("Boss")]
    //private bool isBoss;
    //private float BossStartPos;
    //public GameObject Boss;
    public MWBoss microWave;
    public Stage1Block stage1Block;

    public override void BossStartEvent()
    {
        base.BossStartEvent();
        /*
        Boss.SetActive(true);
        Boss.transform.position = new Vector3(0, BossStartPos + 10, 0);
        StartCoroutine(BossMove());
        */
        stage1Block.setBossBlock();//보스 블럭 최초 생성
        microWave.bossStart();
    }
    /*
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
                stage1Block.setBossBlock();//보스 블럭 최초 생성
                microWave.bossStart();
                break;
            }
            yield return null;
        }
    }
    */
}
