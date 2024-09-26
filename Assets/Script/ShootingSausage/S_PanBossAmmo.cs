using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PanBossAmmo : MonoBehaviour
{
    private const float moveSpeed = 1f;

    Vector3 targetPos;
    Vector3 myPos;
    Vector3 newPos;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = GameObject.Find("player").transform.position;
        myPos = transform.position;
        newPos = (targetPos - myPos);
        StartCoroutine(ammoAttck());
    }

    IEnumerator ammoAttck()
    {
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            Quaternion quaternion = transform.rotation;
            transform.position += quaternion * newPos * moveSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
