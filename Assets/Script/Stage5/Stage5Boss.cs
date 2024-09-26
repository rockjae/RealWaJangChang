using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5Boss : MonoBehaviour
{
    public GameObject sausage;

    public GameObject[] boss;
    public GameObject boss1Bubble;
    public GameObject boss1Ammo;
    public GameObject boss3Ammo;
    public GameObject stage4Ammo;

    public PresetMain presetMain;
    public int BossHp;
    private int firstBossHP;
    private bool[] isPatten = new bool[4];

    private void Start()
    {
        firstBossHP = BossHp;
    }

    public void BossPatten()
    {
        sausage.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        sausage.GetComponent<Rigidbody2D>().Sleep();

        StartCoroutine(stage4AmmoStart());
        StartCoroutine(boss1shot());
        isPatten[0] = true;
    }

    IEnumerator stage4AmmoStart()
    {
        while (true)
        {
            yield return new WaitForSeconds(7f);
            float ran = Random.Range(-2.5f, 2.5f);
            GameObject tmp = Instantiate(stage4Ammo);
            tmp.transform.position = new Vector3(ran, boss[0].transform.position.y+5, boss[0].transform.position.z);
            tmp.SetActive(true);
        }
    }

    public void bossHit()
    {
        BossHp--;

        float inMyHearBeat = (float)BossHp / firstBossHP;

        if (BossHp == 0)
        {
            presetMain.GameClear();
            DataManager.Instance.saveStageValue(DataManager.DataName.STAGE6_NAME, true);//stage6 해금
        }
        else if (inMyHearBeat <= 0.85 && inMyHearBeat > 0.5)
        {
            if (!isPatten[1])
            {
                StartCoroutine(boss2shot());
            }
            isPatten[1] = true;
        }
        else if (inMyHearBeat <= 0.5 && inMyHearBeat > 0.25)
        {
            if (!isPatten[2])
            {
                StartCoroutine(boss3shot());
            }
            isPatten[2] = true;
        }
        else if (inMyHearBeat <= 0.25)
        {
            if (!isPatten[3])
            {
                StartCoroutine(boss4shot());
            }
            isPatten[3] = true;
        }
    }

    IEnumerator boss1shot()
    {
        yield return new WaitForSeconds(0.5f);
        /*
        GameObject[] ammo = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            boss[0].GetComponent<AudioSource>().Play();
            ammo[i] = Instantiate(bossAmmo[0], boss[0].transform);
            ammo[i].transform.localPosition = new Vector3(0, 0, 0);
            yield return new WaitForSeconds(0.5f);
        }
        */
        SpriteRenderer spr = boss1Bubble.GetComponent<SpriteRenderer>();
        Color color = spr.color;
        while (true)
        {
            color.a += 0.5f * Time.deltaTime;
            spr.color = color;

            if (color.a >= 1)
            {
                color.a = 0;
                spr.color = color;
                boss[0].GetComponent<AudioSource>().Play();

                GameObject tmp = Instantiate(boss1Ammo);
                Vector3 pos = boss[0].transform.position;
                pos.y -= 1;
                tmp.transform.position = pos;


                tmp.GetComponent<ChaseAmmo>().ammoSpeed = 0.5f;
                tmp.GetComponent<ChaseAmmo>().target = sausage.transform.position;
            }
            boss[0].transform.LookAt(sausage.transform);

            yield return null;
        }
        /*
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 3; i++)
        {
            boss[0].GetComponent<AudioSource>().Play();
            ammo[0].GetComponent<S_MWAmmo>().mwAmmoSpeed = 6f;
            StartCoroutine(deleteAmmo(ammo[i]));
            yield return new WaitForSeconds(0.1f);
        }

        

        BossPatten();
        */
    }

    IEnumerator boss2shot()
    {
        boss[1].SetActive(true);
        Vector3 firPos = boss[1].transform.position;
        yield return new WaitForSeconds(1f);

        float timer = 0;
        int ran = Random.Range(0, 2);

        Vector3 tmp = sausage.transform.position;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer < 0.5f)
            {
                boss[1].transform.position += new Vector3(2f * Time.deltaTime * tmp.x, 0, 0);
            }
            else if (timer > 0.5f && timer < 1.5f)
            {
                boss[1].transform.position += new Vector3(0, -6f * Time.deltaTime, 0);
            }
            else if (timer > 3f && timer < 6f)
            {
                if (ran == 0)
                {
                    boss[1].transform.position += new Vector3(2.5f * Time.deltaTime, 0, 0);
                }
                else
                {
                    boss[1].transform.position += new Vector3(-2.5f * Time.deltaTime, 0, 0);
                }
            }
            else if (timer > 6f)
            {
                boss[1].SetActive(false);
                boss[1].transform.position = firPos;
                break;
            }
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(boss2shot());
    }
    IEnumerator boss3shot()
    {
        boss[2].SetActive(true);
        while (true)
        {
            boss[2].transform.position = Vector3.Lerp(boss[2].transform.position, sausage.transform.position, Time.deltaTime * 0.5f);

            yield return null;
        }
    }
    IEnumerator boss4shot()
    {
        boss[3].SetActive(true);
        float timer = 0;
        int ran = Random.Range(0, 2);

        if(ran == 0)
        {
            float ShakeTime = 2f;
            StartCoroutine(rotateShake(ShakeTime, boss[3]));

            GameObject[] tmp = new GameObject[15];
            boss[3].GetComponent<AudioSource>().Play();
            for (int i = 0; i < tmp.Length; i++)
            {
                tmp[i] = Instantiate(boss3Ammo);
                tmp[i].GetComponent<MWAmmo>().mwAmmoSpeed = 4f;
                tmp[i].SetActive(true);
                tmp[i].transform.position = boss[3].transform.position;
                tmp[i].transform.localScale /= 3;
                tmp[i].transform.eulerAngles = boss[3].transform.eulerAngles;
                yield return new WaitForSeconds(ShakeTime / tmp.Length);
            }

            while (true)
            {
                timer += Time.deltaTime;

                if (timer > 1)
                {
                    boss[3].transform.eulerAngles = new Vector3(0, 0, 0);
                    break;
                }
                yield return null;
            }
        }
        else
        {
            while (true)
            {
                timer += Time.deltaTime;
                boss[3].transform.position = Vector3.Lerp(boss[3].transform.position, sausage.transform.position, Time.deltaTime * 0.5f);

                if (timer > 2f)
                {
                    break;
                }
                yield return null;
            }
        }

        StartCoroutine(boss4shot());
    }

    IEnumerator rotateShake(float moveTime,GameObject bossTartget)
    {
        float timer = 0;

        while (true)
        {
            timer += Time.deltaTime;
            bossTartget.transform.eulerAngles += new Vector3(0, 0, (360f / moveTime) * Time.deltaTime);

            if (timer > moveTime)
            {
                timer = 0;
                break;
            }
            yield return null;
        }
    }

    IEnumerator deleteAmmo(GameObject obj)
    {
        yield return new WaitForSeconds(5f);
        Destroy(obj);
    }
}
