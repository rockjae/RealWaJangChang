using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornBlock : MonoBehaviour
{
    public float power = 250;
    public bool isManyTime = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("player"))
        {
            collision.rigidbody.AddForce(new Vector2(collision.transform.eulerAngles.y == 0 ? -power : power, power));
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
