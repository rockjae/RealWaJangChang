using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SausageFoot : MonoBehaviour
{    void Update()
    {
        RaycastHit2D hit_down = Physics2D.Raycast(transform.position, Vector2.down, 1f);
        if (hit_down.collider != null && hit_down.transform.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            hit_down.transform.GetComponent<SpriteRenderer>().color = Color.red;
            hit_down.transform.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}
