using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappySausage : MonoBehaviour
{
    public FlappyManager flappyManager;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("flappyPipe"))
        {
            flappyManager.gameOverFlappy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        flappyManager.scoreUP();
    }
}
