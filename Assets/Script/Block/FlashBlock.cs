using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBlock : MonoBehaviour
{
    public float visibleTime;
    public float inVisibleTime;

    bool isInvisible;
    float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if(!isInvisible && timer > visibleTime)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            isInvisible = true;
        }

        if(timer > visibleTime + inVisibleTime)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = true;
            timer = 0;
            isInvisible = false;
        }
    }
}
