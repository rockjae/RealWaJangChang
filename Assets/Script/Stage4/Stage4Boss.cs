using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4Boss : MonoBehaviour
{
    public Stage4Manager stage4Manager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("player"))
        {
            stage4Manager.bossStart();
        }
    }
}
