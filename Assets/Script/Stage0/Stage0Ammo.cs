using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0Ammo : MonoBehaviour
{
    public Stage0Sausage stage0Sausage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("player"))
        {
            GetComponent<AudioSource>().Play();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            stage0Sausage.shootAmmo();

            StartCoroutine(waitZen());
        }
    }

    IEnumerator waitZen()
    {
        yield return new WaitForSeconds(5f);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
