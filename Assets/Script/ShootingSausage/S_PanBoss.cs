using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PanBoss : MonoBehaviour
{

    public int bossPatten;
    [Header("PanBoss")]
    public GameObject panBossAni;
    public CircleCollider2D panBossAniCollider;
    public GameObject panBossAmmo;
    public AudioSource panBossAudio;
    public SpriteRenderer myRenderer;
    public CircleCollider2D myCollider;
    

    [Header("PanSmashAttack1")]
    private Transform targetPlayer;
    public float speed;



    public GameObject Boss;

    // Start is called before the first frame update
    void Start()
    {
        panBossAniCollider.enabled = false;
        targetPlayer = GameObject.Find("player").transform;        
        bossStart();

    }

    public void bossStart()
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
        }
    }

    IEnumerator panAttack1()
    {

        float timer = 0;

        int ran = Random.Range(0, 2);

        myRenderer.enabled = false;
        myCollider.enabled = false;
        panBossAniCollider.enabled = true;


        panBossAni.transform.position = Boss.transform.position;
        panBossAni.SetActive(true);
        

        panBossAni.GetComponent<AudioSource>().Play();
        while (true)
        {
            timer += Time.deltaTime;
            panBossAni.transform.position = Vector3.Lerp(panBossAni.transform.position, targetPlayer.position, Time.deltaTime * speed);

            if (timer > 2f)
            {
                timer = 0;
                break;
            }
            yield return null;
        }
        myRenderer.enabled = true;
        myCollider.enabled = true;
        panBossAniCollider.enabled = false;

        panBossAni.SetActive(false);
        Boss.transform.position = panBossAni.transform.position;
        bossStart();
        yield return null;
    }
    IEnumerator panAttack2()
    {
        yield return new WaitForSeconds(0.5f);

        float timer = 0;
        Vector3 length;
        float angle = 0;

        while (true)
        {
            timer += Time.deltaTime;
            length = targetPlayer.position - Boss.transform.position;
            angle = Mathf.Atan2(length.y, length.x) * Mathf.Rad2Deg - 90;
            Boss.transform.rotation = Quaternion.Euler(0, 0, angle);

            if (timer > 1f)
            {
                timer = 0;
                break;
            }

            yield return null;
        }

        for (int i = 0; i < 3; i++)
        {
            panBossAudio.Play();
            GameObject tmpOBJ = Instantiate(panBossAmmo);
            
            /*tmpOBJ.transform.position = Boss.transform.position + new Vector3(0, -1.2f, 0);*/

            length = targetPlayer.position - Boss.transform.position;
            angle = Mathf.Atan2(length.y, length.x) * Mathf.Rad2Deg;
            tmpOBJ.transform.position = Boss.transform.position + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * 8.7f, Mathf.Sin(angle * Mathf.Deg2Rad) * 8.7f, 0);
            
            Boss.transform.rotation = Quaternion.Euler(0, 0, angle-90);

            yield return new WaitForSeconds(0.3f);
        }

        bossStart();
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("player"))
        {
            GameObject.Find("GameController").GetComponent<GameController>().gameOverShooting();
        }
    }
}
