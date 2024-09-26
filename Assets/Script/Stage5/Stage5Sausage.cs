using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5Sausage : PresetSausageController
{
    public bool stage5Start;
    public GameObject stage5Ammo;
    
    void Update()
    {
        if (!stage5Start)
        {
            SausageMobileMove();
            SausageKeyboardMove();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isHit)
        {
            return;
        }

        if (collision.gameObject.tag.Equals("ammo"))
        {
            Debug.Log("rockjae0 ammo : " + collision.gameObject.name);
            StartCoroutine(hitSausage());
        }
        else if (collision.gameObject.tag.Equals("stage5boss"))
        {
            Debug.Log("rockjae0 stage5boss : " + collision.gameObject.name);
            StartCoroutine(hitSausage());
        }
        else if (collision.gameObject.tag.Equals("stage4ammo"))
        {
            Debug.Log("stage4ammo");
            Destroy(collision.gameObject);
            this.GetComponent<AudioSource>().Play();
            StartCoroutine(shotAmmo());
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
            if (timer > 7)
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
            tmp = Instantiate(stage5Ammo, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(0.2f);
        }
    }
}
