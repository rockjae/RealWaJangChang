using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSCamera : MonoBehaviour
{
    public FlappyManager flappyManager;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = flappyManager.Sausage.transform.position + new Vector3(-1.5f, 2.5f, -5f);
    }
}
