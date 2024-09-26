using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6Ammo3 : MonoBehaviour
{
    private GameObject sausage;
    // Start is called before the first frame update
    void Start()
    {
        sausage = GameObject.Find("Sausage");

        transform.SetParent(sausage.transform);
        Vector3 tmp = sausage.transform.position;
        tmp.y += 1;
        transform.position = tmp;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(sausage.transform.position, Vector3.forward, 360* 3f * Time.deltaTime);
    }
}
