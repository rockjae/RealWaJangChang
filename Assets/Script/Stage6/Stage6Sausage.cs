using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6Sausage : PresetSausageController
{
    public Stage6Boss stage6Boss;
    [HideInInspector]
    public bool stage6Start;
    [HideInInspector]
    public bool stage6StartShot;
    public GameObject[] Ammo;
    public GameObject boxAmmo;
    IEnumerator ammoShot;
    IEnumerator ammoShot2;
    public AudioSource shotSound;

    [HideInInspector]
    public bool isAmmo2;
    [HideInInspector]
    public bool isAmmo2OnOff;
    [HideInInspector]
    public bool isEnd;

    void Update()
    {
        if (!stage6Start)
        {
            SausageMobileMove();
            SausageKeyboardMove();
        }
        else if(stage6StartShot)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(ammoShot == null)
                {
                    ammoShot = shotAmmo();
                    StartCoroutine(ammoShot);

                    isAmmo2OnOff = false;
                }

                if (isAmmo2 && ammoShot2 == null && isAmmo2OnOff)
                {
                    ammoShot2 = shotAmmo2();
                    StartCoroutine(ammoShot2);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (ammoShot != null)
                {
                    isAmmo2OnOff = true;
                }
            }
        }
    }

    IEnumerator shotAmmo()
    {
        shotSound.Play();
        Ammo[0].SetActive(false);
        GameObject tmp = Instantiate(Ammo[0]);

        tmp.transform.SetParent(this.transform);
        tmp.transform.rotation = Ammo[0].transform.rotation;
        tmp.transform.localScale = Ammo[0].transform.localScale;
        tmp.transform.position = Ammo[0].transform.position;

        tmp.SetActive(true);
        tmp.transform.tag = "playerammo";

        Stage6Ammo s6 = tmp.GetComponent<Stage6Ammo>();
        s6.mwAmmoSpeed = 8;
        s6.isShot = true;
        s6.ammoAttck();

        yield return new WaitForSeconds(0.5f);

        Ammo[0].SetActive(true);
        if (Ammo[0].GetComponent<Stage6Ammo>().isBack)
        {
            Ammo[0].GetComponent<Stage6Ammo>().isBack = false;
        }
        else
        {
            Ammo[0].GetComponent<Stage6Ammo>().isBack = true;
        }
        clearAmmo();
    }
    IEnumerator shotAmmo2()
    {
        shotSound.Play();
        Ammo[1].SetActive(false);
        GameObject tmp = Instantiate(Ammo[1]);

        tmp.transform.SetParent(this.transform);
        tmp.transform.rotation = Ammo[1].transform.rotation;
        tmp.transform.localScale = Ammo[1].transform.localScale;
        tmp.transform.position = Ammo[1].transform.position;

        tmp.SetActive(true);
        tmp.transform.tag = "playerammo";
        tmp.transform.localScale = new Vector3(tmp.transform.localScale.x * 5, tmp.transform.localScale.y * 10, tmp.transform.localScale.z);

        Stage6Ammo2 s6 = tmp.GetComponent<Stage6Ammo2>();
        s6.mwAmmoSpeed = 8f;
        s6.isShot = true;
        s6.ammoAttck();

        yield return new WaitForSeconds(5f);
        Ammo[1].SetActive(true);
        if (Ammo[1].GetComponent<Stage6Ammo2>().isBack)
        {
            Ammo[1].GetComponent<Stage6Ammo2>().isBack = false;
        }
        else
        {
            Ammo[1].GetComponent<Stage6Ammo2>().isBack = true;
        }
        clearAmmo2();
    }

    public void initAmmo2()
    {
        GameObject tmp = Instantiate(Ammo[1]);
        Ammo[1] = tmp;
        Ammo[1].transform.SetParent(transform);
        isAmmo2 = true;
    }
    public void initAmmo3()
    {
        StartCoroutine(initSausageAmmo3());
    }

    public void endGame()
    {
        Debug.Log("end");
        isEnd = true;
        presetMain.GetComponent<Stage6PresetMain>().sausageMoveOn = false;
        StartCoroutine(endBoss());
    }

    IEnumerator endBoss()
    {
        stage6Boss.effect1.SetActive(true);
        stage6Boss.background.SetActive(true);
        stage6Boss.isStart = false;
        GameObject tmp = stage6Boss.gameObject;
        tmp.transform.eulerAngles = new Vector3(0, 0, 0);
        Vector3 vec = stage6Boss.rotateTarget;
        vec.y += 3f;
        while (true)
        {
            tmp.transform.position = Vector3.MoveTowards(tmp.transform.position, vec, Time.deltaTime);

            if(tmp.transform.position.y == vec.y)
            {
                stage6Boss.effect2.SetActive(true);
                break;
            }
            yield return null;
        }

        yield return new WaitForSeconds(3f);

        DataManager.Instance.setHighNoonSausageGameOpen();
        presetMain.GameClear(); //스테이지 6클리어
    }

    public void initAmmo4()
    {
        Debug.Log("initAmmo4");
        StartCoroutine(initSausageAmmo4());
    }

    public void initBoxAmmo()
    {
        Debug.Log("initBoxAmmo");
        Instantiate(boxAmmo);
        boxAmmo.transform.position = transform.position;
    }

    IEnumerator initSausageAmmo3()
    {
        while (true)
        {
            GameObject tmp = Instantiate(Ammo[2]);

            tmp.transform.SetParent(transform);
            tmp.transform.rotation = transform.rotation;
            tmp.transform.localScale = Ammo[2].transform.localScale;
            tmp.transform.position = transform.position;

            yield return new WaitForSeconds(10f);
        }
    }

    IEnumerator initSausageAmmo4()
    {
        while (true)
        {
            GameObject tmp = Instantiate(Ammo[3]);
            tmp.transform.position = transform.position+new Vector3(0,1);

            yield return new WaitForSeconds(3f);
        }
    }

    void clearAmmo()
    {
        StopCoroutine(ammoShot);
        ammoShot = null;
    }
    void clearAmmo2()
    {
        StopCoroutine(ammoShot2);
        ammoShot2 = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isHit)
        {
            return;
        }

        if (isEnd)
        {
            Destroy(collision.gameObject);
            return;
        }

        if (collision.gameObject.tag.Equals("ammo"))
        {
            Debug.Log("rockjae0 ammo : " + collision.gameObject.name);
            Destroy(collision.gameObject);
            StartCoroutine(hitSausage());
        }
        /*
        else if (collision.gameObject.tag.Equals("stage6boss"))
        {
            Debug.Log("rockjae0 stage6boss : " + collision.gameObject.name);
            StartCoroutine(hitSausage());
        }
        */
    }
}
