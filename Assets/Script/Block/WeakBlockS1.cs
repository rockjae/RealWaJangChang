using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakBlockS1 : MonoBehaviour
{
    public SausageController sausageController;
    public float weakTime = 0.72f;

    Vector3 myPos; // 나의 위치
    float contactTime; // 플레이어가 발판을 밟고 있는 시간 

    bool isStartWeak;

    // Start is called before the first frame update
    void Start()
    {
        myPos = transform.position;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!sausageController.isJump)
        {
            isStartWeak = true;
        }
    }

    private void Update()
    {
        if (isStartWeak)
        {
            contactTime += Time.deltaTime;

            if (contactTime > weakTime)
            {
                sausageController.isJump = true;
                GetComponent<AudioSource>().Play();
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;
                isStartWeak = false;
            }
        }
    }
}
