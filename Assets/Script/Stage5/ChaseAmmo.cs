using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAmmo : MonoBehaviour
{
    [HideInInspector]
    public float ammoSpeed=1f;
    public Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ammoAttck());
    }

    IEnumerator ammoAttck()
    {
        float timer = 0;
        target = target - transform.position;
        while (true)
        {
            transform.Translate(ammoSpeed* target * Time.deltaTime);

            if(timer > 5f)
            {
                Destroy(this.gameObject);
            }
            yield return null;
        }
    }
}
