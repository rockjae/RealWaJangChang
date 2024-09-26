using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4MovingBlock : MonoBehaviour
{
    Vector3 firstPos; //블럭 위치 저장

    public float moveRangeX = 2.0f;
    int turn=1;

    void Start()
    {
        firstPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > firstPos.x+moveRangeX)
        {
            turn = -1;
        }
        else if (transform.position.x < firstPos.x)
        {
            turn = 1;
        }
        transform.position += new Vector3(turn * Time.deltaTime, 0, 0);
    }
}
