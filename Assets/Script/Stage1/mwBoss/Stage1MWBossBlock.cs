using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1MWBossBlock : MonoBehaviour
{
    public GameObject sausage;
    public Stage1Sausage stage1Sausage;
    [SerializeField] public int BlockIndex;

    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == sausage)
        {
            GetComponent<AudioSource>().Play();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            stage1Sausage.SausageAttackStart(BlockIndex);
        }
    }
}
