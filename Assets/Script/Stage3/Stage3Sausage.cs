using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Sausage : PresetSausageController
{
    [Header("PlayerAmmo")]
    public GameObject PlayerAmmo;

    public IEnumerator playerAmmoShot()
    {
        int count = 0;
        while (true)
        {
            count++;
            GameObject tmp = Instantiate(PlayerAmmo);
            tmp.transform.position = transform.position;
            if (count > 50)
            {
                break;
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    public IEnumerator playerInfinityAmmoShot()
    {
        while (true)
        {
            GameObject tmp = Instantiate(PlayerAmmo);
            tmp.transform.position = transform.position;
            yield return new WaitForSeconds(0.4f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("ground"))
        {
            if (GetComponent<Rigidbody2D>().velocity.y <= 0)
            {
                isJump = false;
            }
        }
        else if (collision.gameObject.tag.Equals("side"))
        {
            if (transform.eulerAngles.y == 0) //벽에 닿으면 방향꺽음
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        else if (collision.gameObject.tag.Equals("reverseGround"))
        {
            if (GetComponent<Rigidbody2D>().velocity.y <= 0)
            {
                Debug.Log("reverse start");
                isJump = false;
                reverse = true;
            }
        }
        else if (collision.transform.tag.Equals("ammo"))
        {
            if (isHit)
            {
                return;
            }
            StartCoroutine(hitSausage());
        }
        else if (collision.transform.tag.Equals("fzboss"))
        {
            if (isHit)
            {
                return;
            }
            StartCoroutine(hitSausage());
        }

    }
}
