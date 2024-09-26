using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroWave : MonoBehaviour
{
    public SausageGameManager sausageGameManager;

    public GameObject ammo;
    public GameObject thunder;
    public int bossHeart;
    private int InitBossHeart;

    public AudioSource audioSource;
    public AudioClip[] audioClips;

    public AudioSource sausageAmmoHitSound;

    private int bossPatten = 0;

    Vector3 firstPos = new Vector3();

    private void Start()
    {
        InitBossHeart = bossHeart;
    }

    public void bossStart()
    {
        firstPos = transform.position;
        int ran = Random.Range(0, bossPatten+1); // 4가지 패턴
        switch (ran)
        {
            case 0:
                {
                    StartCoroutine(mwAttack1());
                    break;
                }
            case 1:
                {
                    StartCoroutine(mwAttack2());
                    break;
                }
            case 2:
                {
                    StartCoroutine(mwAttack3());
                    break;
                }
            case 3:
                {
                    StartCoroutine(mwAttack4());
                    break;
                }
        }
    }

    IEnumerator mwAttack1()
    {
        yield return new WaitForSeconds(1f);

        float timer = 0;
        StartCoroutine(mwShake(0.4f));
        yield return new WaitForSeconds(0.4f);

        GameObject[] tmp = new GameObject[3];
        for (int i = 0; i < tmp.Length; i++)
        {
            tmp[i] = Instantiate(ammo, transform);
            tmp[i].GetComponent<MWAmmo>().mwAmmoSpeed = 4f;
            tmp[i].SetActive(true);
        }
        tmp[0].transform.localPosition = new Vector3(-6, 0, 0);
        tmp[1].transform.localPosition = new Vector3(0, 0, 0);
        tmp[2].transform.localPosition = new Vector3(6, 0, 0);

        GetComponent<AudioSource>().Play();

        while (true)
        {
            timer += Time.deltaTime;

            if (timer > 3)
            {
                bossStart();
                break;
            }
            yield return null;
        }
    }

    IEnumerator mwShake(float moveTime)
    {
        moveTime = moveTime / 3;
        float timer = 0;

        while (true)
        {
            timer += Time.deltaTime;

            if (timer < moveTime)
            {
                transform.position += new Vector3(Time.deltaTime, 0, 0);
            }
            else if (timer > moveTime && timer < moveTime * 3)
            {
                transform.position -= new Vector3(Time.deltaTime, 0, 0);
            }
            else if (timer > moveTime * 3 && timer < moveTime * 4)
            {
                transform.position += new Vector3(Time.deltaTime, 0, 0);
            }
            else if (timer > moveTime * 4)
            {
                timer = 0;
                break;
            }
            yield return null;
        }
    }

    IEnumerator mwAttack2()
    {
        yield return new WaitForSeconds(1f);

        float timer = 0;
        float ShakeTime = 1f;
        StartCoroutine(mwRotateShake(ShakeTime));

        GameObject[] tmp = new GameObject[20];
        for (int i = 0; i < tmp.Length; i++)
        {
            tmp[i] = Instantiate(ammo);
            tmp[i].GetComponent<MWAmmo>().mwAmmoSpeed = 8f;
            tmp[i].SetActive(true);
            tmp[i].transform.position = transform.position;
            tmp[i].transform.localScale /= 3;
            tmp[i].transform.eulerAngles = transform.eulerAngles;
            yield return new WaitForSeconds(ShakeTime / tmp.Length);
        }

        GetComponent<AudioSource>().Play();

        while (true)
        {
            timer += Time.deltaTime;

            if (timer > 3)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                bossStart();
                break;
            }
            yield return null;
        }
    }

    IEnumerator mwRotateShake(float moveTime)
    {
        float timer = 0;

        while (true)
        {
            timer += Time.deltaTime;
            transform.eulerAngles += new Vector3(0, 0, (360f / moveTime) * Time.deltaTime);

            if (timer > moveTime)
            {
                timer = 0;
                break;
            }
            yield return null;
        }
    }

    IEnumerator mwAttack3()
    {
        yield return new WaitForSeconds(1f);

        float timer = 0;
        float waitTimer = 2f;

        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        Color color = spr.color;
        spr.color = color;

        StartCoroutine(mwShake(waitTimer));

        while (true)
        {
            timer += Time.deltaTime;

            color.g -= waitTimer * Time.deltaTime;
            color.b -= waitTimer * Time.deltaTime;
            spr.color = color;

            if (timer > waitTimer)
            {
                color.g = 1;
                color.b = 1;
                spr.color = color;
                timer = 0;
                break;
            }
            yield return null;
        }

        GameObject TOBJ = Instantiate(thunder,transform);
        TOBJ.SetActive(true);
        TOBJ.transform.localPosition = new Vector3(0, -14f, 0);
        GetComponent<AudioSource>().Play();
        
        while (true)
        {
            timer += Time.deltaTime;

            if(timer > 2f)
            {
                if (TOBJ)
                {
                    Destroy(TOBJ);
                }
                bossStart();
                break;
            }
            yield return null;
        }
        yield return null;
    }

    IEnumerator mwAttack4()
    {
        yield return new WaitForSeconds(1f);

        float timer = 0;
        int ran = Random.Range(0, 2);
        bool isDown = false;

        bool isTrigger = false;

        while (true)
        {
            timer += Time.deltaTime;
            if (timer < 1f)
            {
                if(ran == 0)
                {
                    transform.position += new Vector3(2.5f * Time.deltaTime, 0, 0);
                }
                else
                {
                    transform.position += new Vector3(-2.5f * Time.deltaTime, 0, 0);
                }
            }
            else if(timer> 1f && timer <1.5f)
            {
                if (isDown)
                {
                    GetComponent<AudioSource>().Play();
                    isDown = true;
                }
                transform.position += new Vector3(0, -12f * Time.deltaTime, 0);
            }
            else if(timer > 1.5f && timer<3f)
            {
                if (!isTrigger)
                {
                    audioSource.clip = audioClips[1];
                    audioSource.Play();
                    transform.GetComponent<Animator>().enabled = true;
                    isTrigger = true;
                }
            }
            else if(timer > 3f)
            {
                transform.position = firstPos;
                audioSource.clip = audioClips[0];
                transform.GetComponent<Animator>().enabled = false;

                SpriteRenderer spr = GetComponent<SpriteRenderer>();
                Color color = spr.color;
                color.a = 1;
                spr.color = color;

                bossStart();
                break;
            }
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!sausageAmmoHitSound.isPlaying)
        {
            sausageAmmoHitSound.Play();
        }

        if (collision.transform.tag.Equals("playerammo"))
        {
            Destroy(collision.gameObject);
            bossHeart--;

            float inMyHearBeat = (float)bossHeart / InitBossHeart;

            if(bossHeart == 0)
            {
                sausageGameManager.sausageController.fieldOutGameOver.GameClear();
                DataManager.Instance.saveStageValue(DataManager.DataName.STAGE2_NAME, true);//stage2 해금
            }
            else if(inMyHearBeat > 0.9f)
            {
                bossPatten = 0;
            }
            else if(inMyHearBeat <= 0.9 && inMyHearBeat > 0.7)
            {
                bossPatten = 1;
            }
            else if (inMyHearBeat <= 0.7 && inMyHearBeat > 0.4)
            {
                bossPatten = 2;
            }
            else if (inMyHearBeat <= 0.4)
            {
                bossPatten = 3;
            }
        }
    }
}
