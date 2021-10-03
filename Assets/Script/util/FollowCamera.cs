using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform Sausage;
    public Vector3 setPos;

    private void Update()
    {
        //this.transform.position = Sausage.position + setPos;
        this.transform.position = new Vector3(0f, Sausage.position.y, -10);
    }
}
