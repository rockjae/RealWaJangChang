using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2Sausage : PresetSausageController
{
    [Header("ammo")]
    public GameObject ammo;

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
        else if (collision.gameObject.tag.Equals("panboss"))
        {
            Debug.Log("panboss");
            StartCoroutine(hitSausage());
        }
    }

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
}
