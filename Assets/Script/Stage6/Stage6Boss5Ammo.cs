using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6Boss5Ammo : MonoBehaviour
{
    public GameObject spriteTarget;
    public GameObject ammo;
    float timer;
    SpriteRenderer spr;
    Color color;

    private void Start()
    {
        spr = spriteTarget.GetComponent<SpriteRenderer>();
        color = spr.color;
        color.a = 0;
        spr.color = color;
    }

    private void Update()
    {
        if(color.a < 1)
        {
            color.a += 0.5f * Time.deltaTime;
            spr.color = color;
        }

        timer += Time.deltaTime;

        if (timer > 4)
        {
            GetComponent<AudioSource>().Play();
            GameObject tmp = Instantiate(ammo);
            tmp.GetComponent<Stage6ChaseSausage>().ammoSpeed = 0.4f;
            tmp.GetComponent<Stage6ChaseSausage>().HP = 1;
            tmp.transform.position = this.gameObject.transform.position;
            tmp.SetActive(true);

            timer = 0;
        }
    }

    private void OnDisable()
    {
        GameObject.Find("Sausage").GetComponent<Stage6Sausage>().endGame();
    }
}
