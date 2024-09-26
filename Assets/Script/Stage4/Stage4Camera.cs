using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4Camera : MonoBehaviour
{
    public Transform sausage;
    // Update is called once per frame
    void Update()
    {
        Vector3 tmp = sausage.position;
        transform.position = new Vector3(tmp.x, tmp.y+1.5f, -10);
    }
}
