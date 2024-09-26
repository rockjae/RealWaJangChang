using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Stage3PresetMain : PresetMain
{
    [Header("FZAttack")]
    public SpriteRenderer BossSprite;
    public Sprite[] FZSprites;
    public GameObject Flag;
    public Stage3Block stage3Block;
    public GameObject[] BossAmmo;
    public AudioSource FZGroundSound;

    [Header("LockAmmo")]
    private IEnumerator LockAmmoCor;
    public GameObject LockAmmo;
    public InputField UpDownTextBossText;
    public GameObject UpDownTextBoss;
    public Stage3Sausage stage3Sausage;
    public AudioSource ammoGetSound;
    public MenuController menuController;

    [Header("Final")]
    public AudioClip finalSound;
    public GameObject AbsoluteZeroTextOBJ;
    public GameObject AbsoluteZeroTextOBJ2;
    public Text AbsoluteZeroText;
    public GameObject Flame;
    public GameObject Freeze;


    bool isBossBlock;
    bool isBossBlockEnd;

    Vector3 firstPos = new Vector3();
    public int bossPatten;

    public override void BossStartEvent()
    {
        base.BossStartEvent();
        LockAmmoCor = LockAmmoInit();
        StartCoroutine(LockAmmoCor);
        bossStart();
    }

    IEnumerator stage3BossBlockIntro()
    {
        float timer=0;
        FZGroundSound.GetComponent<AudioSource>().Play();
        while (true)
        {
            timer += Time.deltaTime;

            int ran = Random.Range(0, 4);
            BossSprite.sprite = FZSprites[ran];
            if (timer > 1f)
            {
                timer = 0;
                BossSprite.sprite = FZSprites[3];
                break;
            }
            yield return null;
        }

        Flag.GetComponent<AudioSource>().Play();
        while (true)
        {
            timer += Time.deltaTime;
            Flag.transform.position += new Vector3(0, Time.deltaTime, 0);
            Flag.transform.localScale -= new Vector3(3f * Time.deltaTime, 0.15f * Time.deltaTime, 0.1f * Time.deltaTime);
            if (timer > 3f)
            {
                break;
            }
            yield return null;
        }

        StartCoroutine(stage3Block.MakeBossBlock());
        isBossBlockEnd = true;
        bossStart();
        yield return null;
    }

    public void bossStart()
    {
        firstPos = Boss.transform.position;

        switch (bossPatten)
        {
            case 0:
                {
                    StartCoroutine(fzAttack1());
                    break;
                }
            case 1:
                {
                    StartCoroutine(fzAttack2());
                    break;
                }
            case 2:
                {
                    if (!isBossBlock)
                    {
                        StartCoroutine(stage3BossBlockIntro());
                        isBossBlock = true;
                    }
                    if (isBossBlockEnd)
                    {
                        StartCoroutine(fzAttack1());
                    }
                    break;
                }
            case 3:
                {
                    StartCoroutine(fzAttack3());
                    break;
                }
        }
    }

    public void colliderLockAmmo()
    {
        Time.timeScale = 0;
        menuController.isStopTime = true;
        stage3Sausage.enabled = false;
        UpDownTextBoss.SetActive(true);
    }

    public void onClickUpDownTextBoss()
    {
        int ran = Random.Range(0, 2);
        Debug.Log("ran : " +ran);
        if(int.Parse(UpDownTextBossText.text) == ran)
        {
            StartCoroutine(stage3Sausage.playerAmmoShot());
            ammoGetSound.Play();
        }
        else
        {
            int ran2 = Random.Range(0, 2);
            Debug.Log("ran2 : " + ran2);
            if (int.Parse(UpDownTextBossText.text) == ran2)
            {
                StartCoroutine(stage3Sausage.playerAmmoShot());
                ammoGetSound.Play();
            }
        }
        UpDownTextBossText.text = "";
        UpDownTextBoss.SetActive(false);
        stage3Sausage.enabled = true;
        Time.timeScale = 1;
        menuController.isStopTime = false;
    }

    IEnumerator LockAmmoInit()
    {
        yield return new WaitForSeconds(5f);

        while (true)
        {
            GameObject tmpLockOBJ = Instantiate(LockAmmo);
            tmpLockOBJ.transform.position = Boss.transform.position;

            yield return new WaitForSeconds(5f);
        }
    }

    IEnumerator fzAttack1()
    {
        yield return new WaitForSeconds(0.5f);
        BossSprite.sprite = FZSprites[1];

        int ran = Random.Range(0, BossAmmo.Length);
        GameObject tmpOBJ = Instantiate(BossAmmo[ran]);
        tmpOBJ.GetComponent<FZBossAmmo>().mode = ran;
        tmpOBJ.transform.position = Boss.transform.position;

        yield return new WaitForSeconds(0.5f);
        BossSprite.sprite = FZSprites[0];

        yield return new WaitForSeconds(6f);

        Destroy(tmpOBJ);
        bossStart();
        yield return null; 
    }
    IEnumerator fzAttack2()
    {
        yield return new WaitForSeconds(0.5f);
        BossSprite.sprite = FZSprites[1];

        int ran = Random.Range(0, BossAmmo.Length);
        GameObject tmpOBJ = Instantiate(BossAmmo[ran]);
        tmpOBJ.GetComponent<FZBossAmmo>().mode = ran;
        tmpOBJ.transform.position = Boss.transform.position;

        yield return new WaitForSeconds(0.5f);
        BossSprite.sprite = FZSprites[0];


        yield return new WaitForSeconds(0.5f);
        BossSprite.sprite = FZSprites[2];

        int ran2 = Random.Range(0, BossAmmo.Length);
        GameObject tmpOBJ2 = Instantiate(BossAmmo[ran2]);
        tmpOBJ2.GetComponent<FZBossAmmo>().mode = ran2;
        tmpOBJ2.GetComponent<FZBossAmmo>().reverse = true;
        tmpOBJ2.transform.position = Boss.transform.position;

        yield return new WaitForSeconds(0.5f);
        BossSprite.sprite = FZSprites[0];

        yield return new WaitForSeconds(6f);

        Destroy(tmpOBJ);
        Destroy(tmpOBJ2);
        bossStart();
        yield return null;
    }

    IEnumerator fzAttack3()
    {
        BossSprite.sprite = FZSprites[3];
        stage3Block.enabled = false;
        Sausage.transform.GetComponent<Stage3Sausage>().enabled = false;
        Sausage.transform.GetComponent<Rigidbody2D>().gravityScale = 0;
        AbsoluteZeroTextOBJ.SetActive(true);
        UpDownTextBoss.SetActive(false);
        Time.timeScale = 1;
        menuController.isStopTime = false;

        StartCoroutine(sausagePos());
        StartCoroutine(stage3Sausage.playerInfinityAmmoShot());


        StopCoroutine(LockAmmoCor);
        LockAmmoCor = null;
        SoundManager.Instance.setSound(finalSound);

         yield return new WaitForSeconds(0.5f);
        AbsoluteZeroText.text = "절";
        yield return new WaitForSeconds(0.5f);
        AbsoluteZeroText.text += "대";
        yield return new WaitForSeconds(0.5f);
        AbsoluteZeroText.text += "영";
        yield return new WaitForSeconds(0.5f);
        AbsoluteZeroText.text += "도";
        yield return new WaitForSeconds(0.5f);
        AbsoluteZeroText.text += "!";

        AbsoluteZeroTextOBJ2.SetActive(true);

        Freeze.SetActive(true);
        yield return null;
    }

    IEnumerator sausagePos()
    {
        int count = 0;
        int middle = 0;

        float bossY = stage3Block.GetY();
        bossY += 2;

        while (true)
        {
            Sausage.transform.position = new Vector3(0, bossY + (count * 0.01f) + (middle * 0.01f), 0);
            if (Input.anyKeyDown)
            {
                count++;
                if (Flame.activeSelf)
                {
                    middle++;
                }
            }

            if(Sausage.transform.position.y > bossY + 2)
            {
                Flame.SetActive(true);
                AbsoluteZeroTextOBJ.SetActive(false);
                AbsoluteZeroTextOBJ2.SetActive(false);

                Color c = Sausage.GetComponent<SpriteRenderer>().color;
                c.g -= 0.1f * Time.deltaTime;
                c.b -= 0.1f * Time.deltaTime;
                Sausage.GetComponent<SpriteRenderer>().color = c;
            }

            if (Sausage.transform.position.y > bossY + 4)
            {
                int ran = Random.Range(0, 4);
                BossSprite.sprite = FZSprites[ran];
            }

            if (Sausage.transform.position.y > bossY + 6)
            {
                Freeze.SetActive(false);
                Sausage.transform.localScale += new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
            }

            if(Sausage.transform.localScale.x > 2)
            {
                Flame.SetActive(false);
                DataManager.Instance.saveStageValue(DataManager.DataName.STAGE4_NAME, true); //stage4 해금
                GameClear();
                break;
            }
            yield return null;
        }
    }
}
