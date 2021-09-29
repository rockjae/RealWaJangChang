using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform Sausage;
    public Vector3 setPos;

    private void Update()
    {
        this.transform.position = Sausage.position + setPos;
    }
}
