using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueBlock : MonoBehaviour
{
    private PresetSausageController sausageController;
    public float awarenessTime;


    Vector3 myPos; // 나의 위치
    float contactTime; // 플레이어가 발판을 밟고 있는 시간 
    
    bool isContact;
    bool useless;
    // Start is called before the first frame update
    void Start()
    {
        myPos = transform.position;
        useless = true;
        sausageController = GameObject.FindWithTag("player").GetComponent<PresetSausageController>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!sausageController.isJump && useless)
        {
            contactTime += Time.deltaTime;
            if (contactTime > awarenessTime)
            {
                sausageController.isJump = true;
                sausageController.isGlue = true;
                sausageController.SausageSpeed = 0f;
                useless = false;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(!useless)
        {
            useless = true;
        }
    }
}
