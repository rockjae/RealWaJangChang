using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform Sausage;

    private void Update()
    {
        Vector3 tmp = Sausage.position;
        tmp.z = -10;
        this.transform.position = tmp;
    }
}
