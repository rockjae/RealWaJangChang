using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6Boss3 : MonoBehaviour
{
    [HideInInspector]
    public int HP;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("playerboxammo"))
        {
            Debug.Log("boss3 hit");
            HP--;
            GameObject.Find("SausageShotSound").GetComponent<AudioSource>().Play();
            Destroy(collision.gameObject);
            if (HP == 0)
            {
                GameObject.Find("Stage6Boss").GetComponent<Stage6Boss>().endBoss3();
                Destroy(this.gameObject);
            }
        }
    }
}
