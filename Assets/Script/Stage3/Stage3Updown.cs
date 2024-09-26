using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage3Updown : MonoBehaviour
{
    [Header("Preset")]
    public Stage3Sausage stage3Sausage;
    public Stage3PresetMain stage3PresetMain;
    public Stage3Block stage3Block;
    [Header("Door")]
    private AudioClip mainClip;
    public AudioClip doorClip;
    public GameObject overworld;
    public GameObject doorBlock;
    [Header("Text")]
    public GameObject updownTextOBJ;
    public Text updownText;
    public InputField updownInputText;
    public MenuController menuController;
    [Header("Number")]
    int num = 0;
    int min = 0;
    int max = 0;
    int life = 10;
    int initMax = 0;

    private bool isStart;
    private bool isUpdown;

    private void Awake()
    {
        //stage3Sausage.enabled = false;
        stage3PresetMain.enabled = false;
        stage3Block.enabled = false;

        doorBlock.GetComponent<DoorBlock>().doorBlockCollision += doorCollision;
        updownTextOBJ.SetActive(false);
        overworld.SetActive(false);
    }

    private void doorCollision(Collision2D collision)
    {
        if (!isUpdown && collision.transform.tag.Equals("player"))
        {
            StartCoroutine(updownStart());
            isUpdown = true;
        }
    }

    IEnumerator updownStart()
    {
        Time.timeScale = 0;
        menuController.isStopTime = true;

        int ran = Random.Range(0, 4); //0 -> 10, 1 -> 100, 2 -> 1000, 3 -> 10000
        switch (ran)
        {
            case 0:
                {
                    num = Random.Range(1, 11);
                    min = 1;
                    max = 10;
                    break;
                }
            case 1:
                {
                    num = Random.Range(1, 101);
                    min = 1;
                    max = 100;
                    break;
                }
            case 2:
                {
                    num = Random.Range(1, 1001);
                    min = 1;
                    max = 1000;
                    break;
                }
            case 3:
                {
                    num = Random.Range(1, 10001);
                    min = 1;
                    max = 10000;
                    break;
                }
        }
        initMax = max;

        updownTextOBJ.gameObject.SetActive(true);
        updownText.text = "비번? (" + min + "~" + max + ")" + "\n기회:" + life;

        yield return null;
    }

    public void onClickUpDownBtn()
    {
        if (string.IsNullOrEmpty(updownInputText.text))
        {
            life--;
            updownText.text = "비번? (" + min + "~" + max + ")" + "\n기회:" + life;
            if(life == 0)
            {
                updownTextOBJ.SetActive(false);
                stage3PresetMain.FOGameOver();
            }
            return;
        }

        int target = int.Parse(updownInputText.text);
        Debug.Log(num);
        if (num == target)
        {
            if(initMax == 10000)
            {
                updownText.text = "어떻게 맞춤?\n이스터에그 발동!";
                StartCoroutine(stage3Sausage.playerInfinityAmmoShot());
                updownClear();
            }
            else
            {
                updownText.text = "정답!";
                updownClear();
            }
            return;
        }
        else if (num < target)
        {
            life--;
            max = target;
            updownText.text = "비번? (" + min + "~" + max + ")" + "\n기회:" + life;
        }
        else if(num > target)
        {
            life--;
            min = target;
            updownText.text = "비번? (" + min + "~" + max + ")" + "\n기회:" + life;
        }

        if (life == 0)
        {
            updownTextOBJ.SetActive(false);
            stage3PresetMain.FOGameOver();
        }
    }

    private void updownClear()
    {
        Time.timeScale = 1;
        menuController.isStopTime = false;
        StartCoroutine(slowOut());
        SoundManager.Instance.setSound(mainClip);
        overworld.SetActive(false);
        doorBlock.SetActive(false);
        stage3PresetMain.enabled = true;
        stage3Block.enabled = true;
        stage3PresetMain.onStart();
    }

    IEnumerator slowOut()
    {
        yield return new WaitForSeconds(1.5f);
        updownTextOBJ.SetActive(false);
    }

    private void Update()
    {
        if (!isStart && Input.anyKeyDown)
        {
            Time.timeScale = 1;
            stage3PresetMain.StageGuide.SetActive(false);

            mainClip = SoundManager.Instance.audioSource.clip;
            SoundManager.Instance.setSound(doorClip);

            overworld.SetActive(true);
            StartCoroutine(doorDown());
            isStart = true;
        }
    }

    IEnumerator doorDown()
    {
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            doorBlock.transform.position -= new Vector3(0, 1.2f*Time.deltaTime, 0);

            if (timer > 5f)
            {
                timer = 0;
                break;
            }
            yield return null;
        }

        yield return new WaitForSeconds(1.5f);

        while (true)
        {
            timer += Time.deltaTime;
            doorBlock.transform.position -= new Vector3(0, Time.deltaTime, 0);

            if (timer > 3f)
            {
                timer = 0;
                break;
            }
            yield return null;
        }
    }
}
