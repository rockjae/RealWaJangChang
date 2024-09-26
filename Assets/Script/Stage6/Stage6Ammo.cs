using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6Ammo : MonoBehaviour
{
    [HideInInspector]
    public float mwAmmoSpeed;
    public GameObject sausage;
    [HideInInspector]
    public bool isShot;
    [HideInInspector]
    public bool isBack;
    private float timer;

    public void ammoAttck()
    {
        isShot = true;
        timer = 0;
    }

    private void Update()
    {
        if (isShot)
        {
            timer += Time.deltaTime;
            Quaternion quaternion = transform.rotation;
            Vector3 tmp = quaternion * Vector3.up * mwAmmoSpeed * Time.deltaTime;
            transform.position += tmp; 
            
            if (timer > 5f)
            {
                Destroy(gameObject);
            }
            return;
        }

        if (!isBack)
        {
            transform.RotateAround(sausage.transform.position, Vector3.forward, 360 * Time.deltaTime);
        }
        else
        {
            transform.RotateAround(sausage.transform.position, Vector3.back, 360 * Time.deltaTime);
        }
    }
}
