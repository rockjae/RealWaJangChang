using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OliveOBJ : MonoBehaviour
{
    public Stage2Sausage stage2Sausage;

    IEnumerator objTrigger()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(5f);
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("player"))
        {
            GetComponent<AudioSource>().Play();
            StartCoroutine(objTrigger());
            StartCoroutine(stage2Sausage.shotAmmo());
        }
    }
}
