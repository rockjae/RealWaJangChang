using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SausageHead : MonoBehaviour
{
    void Update()
    {
        RaycastHit2D hit_top = Physics2D.Raycast(transform.position, Vector2.up, 1f);
        if(hit_top.collider != null && hit_top.transform.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            hit_top.transform.GetComponent<SpriteRenderer>().color = Color.green;
            hit_top.transform.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
}
