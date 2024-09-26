using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6BossAmmo : MonoBehaviour
{
    private GameObject sausage;
    // Start is called before the first frame update
    void Start()
    {
        sausage = GameObject.Find("Sausage");
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(sausage.transform.position, Vector3.forward, 90 * Time.deltaTime);
    }
}
