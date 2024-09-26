using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Sausage : PresetSausageController
{
    [Header("ammo")]
    public GameObject[] ammoPrefab;
    [SerializeField]
    float speedOfAttack = 0f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isHit)
        {
            return;
        }

        if (collision.gameObject.tag.Equals("ammo"))
        {
            Debug.Log("ammo");
            StartCoroutine(hitSausage());
        }
        else if (collision.gameObject.tag.Equals("mwboss"))
        {
            Debug.Log("mwboss");
            StartCoroutine(hitSausage());
        }
    }

    /*
    public IEnumerator shotAmmo()
    {
        float timer = 0;

        IEnumerator attck_cor = ammoCor();
        StartCoroutine(attck_cor);

        while (true)
        {
            timer += Time.deltaTime;
            if (timer > 5)
            {
                StopCoroutine(attck_cor);
                break;
            }
            yield return null;
        }
        yield return null;
    }

    IEnumerator ammoCor()
    {
        while (true)
        {
            GameObject tmp;
            tmp = Instantiate(ammo, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(0.2f);
        }
    }
    */
    public void SausageAttackStart(int ammoIndex)
    {
        StartCoroutine(AttackStart(ammoIndex));
    }

    private IEnumerator AttackStart(int ammoIndex)
    {
        float timer = 0;
        IEnumerator attck_cor = Attack(ammoIndex);
        StartCoroutine(attck_cor);
        while (true)
        {
            timer += Time.deltaTime;

            if (timer > 3f)
            {
                StopCoroutine(attck_cor);
                break;
            }

            yield return null;
            //if( boss.isDie == true) { 보스 사망전까지 계속 발사

            //}
        }
    }

    private IEnumerator Attack(int ammoIndex)
    {
        while (true)
        {
            GameObject ammo;
            ammo = Instantiate(ammoPrefab[ammoIndex], presetMain.Sausage.transform.position, Quaternion.identity);

            yield return new WaitForSeconds(speedOfAttack);
            //if( boss.isDie == true) { 보스 사망전까지 계속 발사

            //}
        }
    }
}
