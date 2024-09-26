using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private float CameraSpeed=1f;

    public Transform BackGround;
    public Vector3 setPos;

    [HideInInspector]
    public bool isStart;

    public Action BossOn;

    private void Update()
    {
        if (isStart)
        {
            this.transform.position = new Vector3(0f, BackGround.position.y, -10);
            BackGround.transform.position += new Vector3(0, CameraSpeed * Time.deltaTime, 0);
        }
    }

    public IEnumerator startBoss(float sausagePos)
    {
        while (true)
        {
            BackGround.localPosition += new Vector3(0, 0.7f*Time.deltaTime, 0);
            this.transform.localPosition += new Vector3(0, 0.7f * Time.deltaTime, 0);

            //Debug.Log("BackGround : " +BackGround.localPosition.y);
            //Debug.Log("camera : " + (transform.localPosition.y));
            //Debug.Log("sausagePos : " + (sausagePos + 4.5f));
            if (transform.localPosition.y > sausagePos + 4.5f)
            {
                break;
            }
            yield return null;
        }
        BossOn();
    }
}
