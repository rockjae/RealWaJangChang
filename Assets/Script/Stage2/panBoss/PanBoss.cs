using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanBoss : MonoBehaviour
{
    private int InitBossHeart;
    public int bossHeart;
    public Stage2PresetMain stage2PresetMain;

    private void Start()
    {
        InitBossHeart = bossHeart;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("playerammo"))
        {
            Destroy(collision.gameObject);

            if (!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }

            bossHeart--;

            float inMyHearBeat = (float)bossHeart / InitBossHeart;

            if (bossHeart == 0)
            {
                stage2PresetMain.GameClear();
                DataManager.Instance.setFlappyGameOpen();
                DataManager.Instance.saveStageValue(DataManager.DataName.STAGE3_NAME, true);//stage3 해금
            }
            else if (inMyHearBeat > 0.9f)
            {
                stage2PresetMain.bossPatten = 0;
            }
            else if (inMyHearBeat <= 0.9 && inMyHearBeat > 0.8)
            {
                stage2PresetMain.bossPatten = 1;
            }
            else if (inMyHearBeat <= 0.8 && inMyHearBeat > 0.7)
            {
                stage2PresetMain.bossPatten = 2;
            }
            else if (inMyHearBeat <= 0.7)
            {
                stage2PresetMain.bossPatten = 3;
            }
        }
    }
}
