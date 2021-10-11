using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private float CameraSpeed=1f;

    public Transform BackGround;
    public Vector3 setPos;

    private void Update()
    {
        //this.transform.position = Sausage.position + setPos;
        this.transform.position = new Vector3(0f, BackGround.position.y, -10);
        BackGround.transform.position += new Vector3(0, CameraSpeed * Time.deltaTime, 0);
    }
}
