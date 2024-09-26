using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MWBossBlock : MonoBehaviour
{
    public SausageGameManager sausageGameManager;
    [SerializeField] public int BlockIndex;

    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == sausageGameManager.Sausage)
        {
            GetComponent<AudioSource>().Play();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            sausageGameManager.sausageController.SausageAttackStart(BlockIndex);
        }
    }
}
