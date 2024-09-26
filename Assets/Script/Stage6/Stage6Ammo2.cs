using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6Ammo2 : MonoBehaviour
{
    [HideInInspector]
    public float mwAmmoSpeed;
    [HideInInspector]
    public GameObject sausage;
    [HideInInspector]
    public bool isShot;
    [HideInInspector]
    public bool isBack;
    private float timer;
    // Start is called before the first frame update

    public void ammoAttck()
    {
        isShot = true;
        timer = 0;
    }
    void Start()
    {
        sausage = GameObject.Find("Sausage");
        Vector3 tmp = sausage.transform.position;
        tmp.y += 1;
        transform.position = tmp;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isShot)
        {
            if (timer > 0.1f)
            {
                GetComponent<CircleCollider2D>().enabled = true;
            }
            timer += Time.deltaTime;
            Quaternion quaternion = transform.rotation;
            Vector3 tmp = quaternion * Vector3.up * mwAmmoSpeed * Time.deltaTime;
            transform.position += tmp;

            if (timer > 3f)
            {
                Destroy(gameObject);
            }
            return;
        }

        if (!isBack)
        {
            transform.RotateAround(sausage.transform.position, Vector3.forward, 270 * Time.deltaTime);
        }
        else
        {
            transform.RotateAround(sausage.transform.position, Vector3.back, 270 * Time.deltaTime);
        }
    }
}
