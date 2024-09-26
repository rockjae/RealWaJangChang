using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanBossAmmo : MonoBehaviour
{
    [HideInInspector]
    public float mwAmmoSpeed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ammoAttck());
    }

    IEnumerator ammoAttck()
    {
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            //transform.position += new Vector3(0, -mwAmmoSpeed * Time.deltaTime, 0);
            //transform.position += mwAmmoSpeed * Vector3.down * Time.deltaTime;
            Quaternion quaternion = transform.rotation;
            Vector3 tmp = quaternion * Vector3.down * mwAmmoSpeed * Time.deltaTime;
            transform.position += tmp;
            if (timer > 10f)
            {
                break;
            }
            yield return null;
        }
        Destroy(gameObject);
    }
}
