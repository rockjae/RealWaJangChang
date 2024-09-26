using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FZBoss : MonoBehaviour
{
    public Sprite[] sprites;
    public int bossHeart=10;
    private int InitBossHeart;

    public Stage3PresetMain stage3PresetMain;
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

            if (bossHeart <= 0)
            {
                stage3PresetMain.bossPatten = 3;
                //stage3PresetMain.GameClear();
                //DataManager.Instance.setFlappyGameOpen();
            }
            else if (inMyHearBeat > 0.7f)
            {
                stage3PresetMain.bossPatten = 0;
            }
            else if (inMyHearBeat <= 0.7 && inMyHearBeat > 0.5)
            {
                stage3PresetMain.bossPatten = 1;
            }
            else if (inMyHearBeat <= 0.5)
            {
                stage3PresetMain.bossPatten = 2;
            }
        }
    }
}
