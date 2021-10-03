using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    //Vector3 targetPos; // 소세지(플레이어) 위치
    Vector3 myPos; //블럭 위치 저장

    float rightMax = 2.0f; //좌로 이동가능한 (x)최대값
    float leftMax = -2.0f; //우로 이동가능한 (x)최대값
    float direction = 1.0f; //블럭 이동속도+방향

    // Start is called before the first frame update
    void Start()
    {
        myPos = transform.position;
        //targetPos = GameObject.Find("sausage").transform.position; 
        //Destroy(gameObject, 20f); // 메모리 관리 어떻게 할까?
    }

    // Update is called once per frame
    void Update()
    {

        myPos.x += Time.deltaTime * direction;

        if (myPos.x >= rightMax)

        {

            direction *= -1;

            myPos.x = rightMax;

        }

        else if (myPos.x <= leftMax)

        {

            direction *= -1;

            myPos.x = leftMax;

        }

        transform.position = new Vector3(myPos.x, myPos.y, 0f);
    }
}
