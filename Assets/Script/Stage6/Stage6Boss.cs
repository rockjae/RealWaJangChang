using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6Boss : MonoBehaviour
{
    public GameObject sausage;
    [HideInInspector]
    public bool isStart;
    [HideInInspector]
    public Vector3 rotateTarget;

    public AudioSource bossHitSound;
    public int HP = 100;

    public GameObject[] ammo;
    public GameObject[] Stage3Ammo;
    public GameObject[] boss;

    public GameObject Exclamation_mark_L;
    public GameObject Exclamation_mark_R;

    private IEnumerator _makeAmmo3;

    [Header("end")]
    public GameObject effect1;
    public GameObject effect2;
    public GameObject background;

    private void Update()
    {
        if (isStart)
        {
            transform.RotateAround(rotateTarget, Vector3.forward, 90 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("playerammo"))
        {
            HP--;

            if(HP == 95)//번개 생성
            {
                StartCoroutine(makeAmmo1());
            }
            else if(HP == 90) //boss1 생성
            {
                GameObject tmp = Instantiate(boss[0]);
                tmp.GetComponent<Stage6ChaseSausage>().tag = "boss1";
                tmp.GetComponent<Stage6ChaseSausage>().ammoSpeed = 0.05f;
                tmp.GetComponent<Stage6ChaseSausage>().HP = 5;
                int ran_x = Random.Range(0, 2) == 0 ? -5 : 5;
                float ran_y = Random.Range(sausage.transform.position.y - 5, sausage.transform.position.y + 5);
                tmp.transform.position = new Vector3(ran_x, ran_y, tmp.transform.position.z);
            }
            else if(HP == 83) //올리브 생성
            {
                StartCoroutine(makeAmmo2());
            }
            else if (HP == 75) //boss2 생성
            {
                GameObject tmp = Instantiate(boss[1]);
                tmp.GetComponent<Stage6ChaseSausage>().tag = "boss2";
                tmp.GetComponent<Stage6ChaseSausage>().ammoSpeed = 0.05f;
                tmp.GetComponent<Stage6ChaseSausage>().HP = 5;
                int ran_x = Random.Range(0, 2) == 0 ? -5 : 5;
                float ran_y = Random.Range(sausage.transform.position.y - 5, sausage.transform.position.y + 5);
                tmp.transform.position = new Vector3(ran_x, ran_y, tmp.transform.position.z);
            }
            else if(HP == 65) //boss3 생성
            {
                _makeAmmo3 = makeAmmo3();
                StartCoroutine(_makeAmmo3);

                Vector3 vec = sausage.transform.position;
                vec.y += 3;
                boss[2].GetComponent<Stage6Boss3>().HP = 2;
                boss[2].transform.position = vec;
                boss[2].SetActive(true);
            }
            else if(HP == 50)  //boss5-1
            {
                GameObject.Find("MainManager").GetComponent<Stage6PresetMain>().sausageMoveOn = true;

                GameObject tmp = Instantiate(boss[3]);
                tmp.GetComponent<Stage6ChaseSausage>().ammoSpeed = 0.05f;
                tmp.GetComponent<Stage6ChaseSausage>().HP = 3;
                int ran_x = Random.Range(0, 2) == 0 ? -5 : 5;
                float ran_y = Random.Range(sausage.transform.position.y - 5, sausage.transform.position.y + 5);
                tmp.transform.position = new Vector3(ran_x, ran_y, tmp.transform.position.z);
            }
            else if (HP == 45)//boss5-2
            {
                GameObject tmp = Instantiate(boss[4]);
                tmp.GetComponent<Stage6ChaseSausage>().ammoSpeed = 0.05f;
                tmp.GetComponent<Stage6ChaseSausage>().HP = 3;
                int ran_x = Random.Range(0, 2) == 0 ? -5 : 5;
                float ran_y = Random.Range(sausage.transform.position.y - 5, sausage.transform.position.y + 5);
                tmp.transform.position = new Vector3(ran_x, ran_y, tmp.transform.position.z);
            }
            else if (HP == 40)//boss5-3
            {
                GameObject tmp = Instantiate(boss[5]);
                tmp.GetComponent<Stage6ChaseSausage>().ammoSpeed = 0.05f;
                tmp.GetComponent<Stage6ChaseSausage>().HP = 3;
                int ran_x = Random.Range(0, 2) == 0 ? -5 : 5;
                float ran_y = Random.Range(sausage.transform.position.y - 5, sausage.transform.position.y + 5);
                tmp.transform.position = new Vector3(ran_x, ran_y, tmp.transform.position.z);
            }
            else if (HP == 35)//boss5-4
            {
                GameObject tmp = Instantiate(boss[6]);
                tmp.GetComponent<Stage6ChaseSausage>().tag = "boss5";
                tmp.GetComponent<Stage6ChaseSausage>().ammoSpeed = 0.02f;
                tmp.GetComponent<Stage6ChaseSausage>().HP = 3;

                Vector3 vec = sausage.transform.position;
                vec.x = 0;
                vec.y += 10;
                tmp.transform.position = vec;
            }

            if (!bossHitSound.isPlaying)
            {
                bossHitSound.Play();
            }
            Destroy(collision.gameObject);
        }
    }

    IEnumerator makeAmmo1()
    {
        while (true)
        {
            GameObject tmp = Instantiate(ammo[0]);
            tmp.GetComponent<Stage6ChaseSausage>().ammoSpeed = 0.1f;
            tmp.GetComponent<Stage6ChaseSausage>().HP = 1;
            int ran_x = Random.Range(0, 2) == 0 ? -3 : 3;
            float ran_y = Random.Range(sausage.transform.position.y - 5, sausage.transform.position.y + 5);
            tmp.transform.position = new Vector3(ran_x, ran_y, tmp.transform.position.z);

            yield return new WaitForSeconds(4f);
        }
    }
    IEnumerator makeAmmo2()
    {
        while (true)
        {
            GameObject tmp = Instantiate(ammo[1]);
            tmp.GetComponent<Stage6ChaseSausage>().ammoSpeed = 0.5f;
            tmp.GetComponent<Stage6ChaseSausage>().HP = 1;
            int ran_x = Random.Range(0, 2) == 0 ? -5 : 5;
            float ran_y = Random.Range(sausage.transform.position.y - 5, sausage.transform.position.y + 5);
            tmp.transform.position = new Vector3(ran_x, ran_y, tmp.transform.position.z);

            if(ran_x == -5)
            {
                Exclamation_mark_L.SetActive(true);
            }
            else
            {
                Exclamation_mark_R.SetActive(true);
            }

            yield return new WaitForSeconds(8f);
        }
    }
    IEnumerator makeAmmo3()
    {
        StartCoroutine(boss3Init());
        int count = 0;

        while (true)
        {
            count++;
            int ran = Random.Range(0, Stage3Ammo.Length-1);

            if (count > 3)
            {
                Debug.Log("Stage3Ammo count 4 : "+ (Stage3Ammo.Length - 1));

                GameObject tmp = Instantiate(Stage3Ammo[Stage3Ammo.Length - 1]);
                tmp.GetComponent<Stage6ChaseSausage>().ammoSpeed = 0.05f;
                tmp.GetComponent<Stage6ChaseSausage>().HP = 1;
                float ran_x_1 = Random.Range(sausage.transform.position.x - 5, sausage.transform.position.x - 2);
                float ran_x_2 = Random.Range(sausage.transform.position.x + 2, sausage.transform.position.x + 5);
                float ran_y = Random.Range(sausage.transform.position.y - 5, sausage.transform.position.y + 5);
                tmp.GetComponent<Stage6ChaseSausage>().tag = "playerboxammo";
                tmp.transform.position = new Vector3(sausage.transform.position.x + 2, sausage.transform.position.y + 3, tmp.transform.position.z);

                count = 0;
            }
            else
            {
                GameObject tmp = Instantiate(Stage3Ammo[ran]);
                tmp.GetComponent<Stage6ChaseSausage>().ammoSpeed = 0.05f;
                tmp.GetComponent<Stage6ChaseSausage>().HP = 1;
                float ran_x_1 = Random.Range(sausage.transform.position.x - 5, sausage.transform.position.x - 2);
                float ran_x_2 = Random.Range(sausage.transform.position.x + 2, sausage.transform.position.x + 5);
                float ran_y = Random.Range(sausage.transform.position.y - 5, sausage.transform.position.y + 5);
                tmp.transform.position = new Vector3(Random.Range(0, 2) == 0 ? ran_x_1 : ran_x_2, ran_y, tmp.transform.position.z);
            }


            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator boss3Init()
    {
        SpriteRenderer spr = boss[2].GetComponent<SpriteRenderer>();
        Color color = spr.color;
        color.a = 0;
        spr.color = color;
        while (true)
        {
            color.a += 0.5f * Time.deltaTime;
            spr.color = color;
            if (color.a >= 1)
            {
                break;
            }
            yield return null;
        }
    }

    public void endBoss3()
    {
        sausage.GetComponent<Stage6Sausage>().initAmmo4();

        StopCoroutine(_makeAmmo3);
        _makeAmmo3 = null;
    }
}
