using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PanBoss_Ani : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("player"))
        {
            GameObject.Find("GameController").GetComponent<GameController>().gameOverShooting();
        }
    }
}
