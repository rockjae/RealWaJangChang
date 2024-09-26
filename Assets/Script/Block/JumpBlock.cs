using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBlock : MonoBehaviour
{
    private float power = 300;
    public bool isManyTime;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("player"))
        {
            collision.rigidbody.AddForce(new Vector2(0, power));
            GetComponent<AudioSource>().Play();
            GetComponent<BoxCollider2D>().enabled = false;
            if (isManyTime)
            {
                StartCoroutine(waitEnable());
            }
        }
    }
    IEnumerator waitEnable()
    {
        yield return new WaitForSeconds(1f);

        GetComponent<BoxCollider2D>().enabled = true;
    }
}
