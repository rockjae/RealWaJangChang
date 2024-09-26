using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5Boss1 : MonoBehaviour
{
    public Stage5Boss stage5Boss;
    public AudioSource bossHitSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "playerammo")
        {
            if (!bossHitSound.isPlaying)
            {
                bossHitSound.Play();
            }

            Destroy(collision.gameObject);
            stage5Boss.bossHit();
        }
    }
}
