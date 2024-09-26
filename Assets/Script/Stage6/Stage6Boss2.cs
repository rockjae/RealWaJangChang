using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6Boss2 : MonoBehaviour
{
    private GameObject target;
    private void Start()
    {
        target = GameObject.Find("Stage6Boss");
    }
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, target.transform.position.y, transform.position.z);
    }
}
