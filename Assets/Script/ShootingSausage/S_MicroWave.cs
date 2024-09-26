using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MicroWave : MonoBehaviour
{
    /*public SausageGameManager sausageGameManager;*/

    public GameObject ammo;
    public int bossHeart;
    private int InitBossHeart;

    public AudioSource audioSource;
    public AudioClip[] audioClips;

    public AudioSource sausageAmmoHitSound;


    Vector3 firstPos = new Vector3();
    int ran;

    private void Start()
    {
        bossStart();
    }

    public void bossStart()
    {
        firstPos = transform.position;
        ran = Random.Range(0, 2);
        // 4가지 패턴
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
            tmp[i] = Instantiate(ammo);
            tmp[i].GetComponent<S_MWAmmo>().mwAmmoSpeed = 6f;
            tmp[i].transform.localScale *= 1.5f;
            tmp[i].SetActive(true);
        }
        tmp[0].transform.localPosition = new Vector3(transform.position.x-6, transform.position.y, 0);
        tmp[1].transform.localPosition = new Vector3(transform.position.x, transform.position.y, 0);
        tmp[2].transform.localPosition = new Vector3(transform.position.x+6, transform.position.y, 0);

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
            tmp[i].GetComponent<S_MWAmmo>().mwAmmoSpeed = 8f;
            tmp[i].SetActive(true);
            tmp[i].transform.position = transform.position;
            /*tmp[i].transform.localScale /= 3;*/
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
}
