using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2PresetMain : PresetMain
{
    [Header("PanBoss")]
    public GameObject panBossAni;
    public GameObject panBossAmmo;
    public AudioSource panBossAudio;
    public int bossPatten;

    Vector3 firstPos = new Vector3();

    public override void BossStartEvent()
    {
        base.BossStartEvent();
        Debug.Log("child BossStartEvent");
        bossStart();
    }

    public void bossStart()
    {
        firstPos = Boss.transform.position;

        if(bossPatten == 0)
        {
            StartCoroutine(panAttack1());
        }
        else
        {
            int ran = Random.Range(0, bossPatten + 2); // 4가지 패턴
            switch (ran)
            {
                case 0:
                    {
                        StartCoroutine(panAttack1());
                        break;
                    }
                case 1:
                    {
                        StartCoroutine(panAttack2());
                        break;
                    }
                case 2:
                    {
                        StartCoroutine(panAttack3());
                        break;
                    }
                case 3:
                    {
                        StartCoroutine(panAttack3());
                        break;
                    }
                case 4:
                    {
                        StartCoroutine(panAttack4());
                        break;
                    }
            }
        }
    }

    IEnumerator panAttack1()
    {
        yield return new WaitForSeconds(0.5f);

        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            Boss.transform.position += new Vector3(0, 3*Time.deltaTime, 0);
            if (timer > 2f)
            {
                timer = 0;
                break;
            }
            yield return null;
        }

        int ran = Random.Range(0, 2);
        panBossAni.transform.position = (ran == 0) ? new Vector3(-4.5f, Boss.transform.position.y-12.5f, 0) : new Vector3(4.5f, Boss.transform.position.y - 12.5f, 0);
        panBossAni.SetActive(true);
        Boss.SetActive(false);
        panBossAni.GetComponent<AudioSource>().Play();
        while (true)
        {
            timer += Time.deltaTime;
            panBossAni.transform.position += (ran == 0) ? new Vector3(3 * Time.deltaTime, 0, 0) : new Vector3(-3 * Time.deltaTime, 0, 0);
            if(timer > 3f)
            {
                timer = 0;
                break;
            }
            yield return null;
        }
        Boss.SetActive(true);

        while (true)
        {
            timer += Time.deltaTime;
            Boss.transform.position -= new Vector3(0, 3 * Time.deltaTime, 0);
            if (timer > 2f)
            {
                timer = 0;
                Boss.transform.position = firstPos;
                break;
            }
            yield return null;
        }

        panBossAni.SetActive(false);
        bossStart();
        yield return null;
    }
    IEnumerator panAttack2()
    {
        yield return new WaitForSeconds(0.5f);

        float timer = 0;

        while (true)
        {
            timer += Time.deltaTime;
            Boss.transform.eulerAngles += new Vector3(0, 0, 360f* Time.deltaTime);
            if (timer > 0.5f)
            {
                Boss.transform.eulerAngles = new Vector3(0, 0, 180);
                timer = 0;
                break;
            }
            yield return null;
        }

        for(int i=0; i<3; i++)
        {
            panBossAudio.Play();
            GameObject tmpOBJ = Instantiate(panBossAmmo);
            tmpOBJ.transform.position = Boss.transform.position+new Vector3(0,-1.2f,0);
            tmpOBJ.GetComponent<PanBossAmmo>().mwAmmoSpeed = 4f;

            yield return new WaitForSeconds(0.3f);
        }

        while (true)
        {
            timer += Time.deltaTime;
            Boss.transform.eulerAngles += new Vector3(0, 0, 360f * Time.deltaTime);
            if (timer > 0.5f)
            {
                Boss.transform.eulerAngles = new Vector3(0, 0, 0);
                timer = 0;
                break;
            }
            yield return null;
        }

        bossStart();
        yield return null;
    }
    IEnumerator panAttack3()
    {
        yield return new WaitForSeconds(0.5f);

        float timer = 0;
        int ran = Random.Range(0, 2);

        if(ran == 0)
        {
            while (true)
            {
                timer += Time.deltaTime;
                Boss.transform.eulerAngles += new Vector3(0, 0, 240f * Time.deltaTime);
                if (timer > 0.5f)
                {
                    Boss.transform.eulerAngles = new Vector3(0, 0, 120);
                    timer = 0;
                    break;
                }
                yield return null;
            }

            panBossAudio.Play();
            GameObject tmpOBJ = Instantiate(panBossAmmo);
            tmpOBJ.transform.position = Boss.transform.position + new Vector3(-1.2f, -1.2f, 0);
            StartCoroutine(ammoMove(tmpOBJ.transform, -1));

            while (true)
            {
                timer += Time.deltaTime;
                Boss.transform.eulerAngles += new Vector3(0, 0, 240f * Time.deltaTime);
                if (timer > 0.5f)
                {
                    Boss.transform.eulerAngles = new Vector3(0, 0, 240);
                    timer = 0;
                    break;
                }
                yield return null;
            }

            panBossAudio.Play();
            GameObject tmpOBJ2 = Instantiate(panBossAmmo);
            tmpOBJ2.transform.position = Boss.transform.position + new Vector3(1.2f, -1.2f, 0);
            StartCoroutine(ammoMove(tmpOBJ2.transform, 1));

            while (true)
            {
                timer += Time.deltaTime;
                Boss.transform.eulerAngles += new Vector3(0, 0, 240f * Time.deltaTime);
                if (timer > 0.5f)
                {
                    Boss.transform.eulerAngles = new Vector3(0, 0, 360);
                    timer = 0;
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
                Boss.transform.eulerAngles -= new Vector3(0, 0, 240f * Time.deltaTime);
                if (timer > 0.5f)
                {
                    Boss.transform.eulerAngles = new Vector3(0, 0, 240);
                    timer = 0;
                    break;
                }
                yield return null;
            }

            panBossAudio.Play();
            GameObject tmpOBJ = Instantiate(panBossAmmo);
            tmpOBJ.transform.position = Boss.transform.position + new Vector3(1.2f, -1.2f, 0);
            StartCoroutine(ammoMove(tmpOBJ.transform, 1));

            while (true)
            {
                timer += Time.deltaTime;
                Boss.transform.eulerAngles -= new Vector3(0, 0, 240f * Time.deltaTime);
                if (timer > 0.5f)
                {
                    Boss.transform.eulerAngles = new Vector3(0, 0, 120);
                    timer = 0;
                    break;
                }
                yield return null;
            }

            panBossAudio.Play();
            GameObject tmpOBJ2 = Instantiate(panBossAmmo);
            tmpOBJ2.transform.position = Boss.transform.position + new Vector3(-1.2f, -1.2f, 0);
            StartCoroutine(ammoMove(tmpOBJ2.transform, -1));

            while (true)
            {
                timer += Time.deltaTime;
                Boss.transform.eulerAngles -= new Vector3(0, 0, 240f * Time.deltaTime);
                if (timer > 0.5f)
                {
                    Boss.transform.eulerAngles = new Vector3(0, 0, 0);
                    timer = 0;
                    break;
                }
                yield return null;
            }
        }

        

        bossStart();
        yield return null;
    }

    IEnumerator ammoMove(Transform obj,int leftright)
    {
        float timer=0;
        while (true)
        {
            timer += Time.deltaTime;
            if(obj.position.x < -2.8 || obj.position.x > 2.8)
            {
                leftright *= -1;
            }

            if(timer > 9)
            {
                break;
            }

            obj.position += new Vector3(leftright*6*Time.deltaTime, -3f*Time.deltaTime, 0);

            yield return null;
        }
        yield return null;
    }

    IEnumerator panAttack4()
    {
        yield return new WaitForSeconds(1f);

        float timer = 0;
        int ran = Random.Range(0, 2);
        bool isDown = false;

        while (true)
        {
            timer += Time.deltaTime;
            panBossAni.transform.position = Boss.transform.position;
            if (timer < 1f)
            {
                if (ran == 0)
                {
                    Boss.transform.position += new Vector3(2f * Time.deltaTime, 0, 0);
                }
                else
                {
                    Boss.transform.position += new Vector3(-2f * Time.deltaTime, 0, 0);
                }
            }
            else if (timer > 1f && timer < 1.5f)
            {
                if (!isDown)
                {
                    isDown = true;

                    Boss.SetActive(false);
                    panBossAni.SetActive(true);
                    panBossAni.GetComponent<AudioSource>().Play();
                }
                Boss.transform.position += new Vector3(0, -13f * Time.deltaTime, 0);
            }
            else if (timer > 1.5f && timer < 5f)
            {
                Boss.transform.position += (ran == 0) ? new Vector3(-3 * Time.deltaTime, 0, 0) : new Vector3(3 * Time.deltaTime, 0, 0);
            }
            else if (timer > 5f)
            {
                Boss.transform.position = firstPos;

                Boss.SetActive(true);
                panBossAni.SetActive(false);

                bossStart();
                break;
            }
            yield return null;
        }
    }
}
