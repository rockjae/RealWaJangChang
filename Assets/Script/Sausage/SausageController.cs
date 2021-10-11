using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SausageController : MonoBehaviour
{
    private Transform Sausage;
    private Rigidbody2D SausageRig;

    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float jumpforce = 10f;
    private float touchTimer;

    private bool isJump;

    private Vector2 touchBeganPos;
    private Vector2 touchEndedPos;
    private Vector2 touchDif;
    private float swipeSensitivity;

    private void Awake()
    {
        Sausage = gameObject.transform;
        SausageRig = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        SausageMobileMove();
        SausageKeyboardMove();
    }
    private void SausageMobileMove()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Debug.Log("touch");

            if (touch.phase == TouchPhase.Began)
            {
                touchBeganPos = touch.position;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                touchEndedPos = touch.position;
                touchDif = (touchEndedPos - touchBeganPos);

                //스와이프. 터치의 x이동거리나 y이동거리가 민감도보다 크면
                if (Mathf.Abs(touchDif.y) > swipeSensitivity)
                {
                    if (touchDif.x > 0 && Mathf.Abs(touchDif.y) < Mathf.Abs(touchDif.x))
                    {
                        Debug.Log("right");
                        Sausage.eulerAngles = new Vector3(0, 0, 0);
                    }
                    else if (touchDif.x < 0 && Mathf.Abs(touchDif.y) < Mathf.Abs(touchDif.x))
                    {
                        Debug.Log("Left"); 
                        Sausage.eulerAngles = new Vector3(0, 180, 0);
                    }
                }
                //터치.
                else
                {
                    Debug.Log("touch"); 
                    if (!isJump)
                    {
                        isJump = true;
                        SausageRig.AddForce(Vector3.up * jumpforce, ForceMode2D.Impulse);
                    }
                }
            }
        }
        Sausage.position += Sausage.eulerAngles.y == 0 ? new Vector3(speed * Time.deltaTime, 0, 0) : new Vector3(-speed * Time.deltaTime, 0, 0);
    }
    private void SausageKeyboardMove()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Sausage.eulerAngles = new Vector3(0, 180, 0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Sausage.eulerAngles = new Vector3(0, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!isJump)
            {
                isJump = true;
                SausageRig.AddForce(Vector3.up * jumpforce, ForceMode2D.Impulse);
            }
        }

        Sausage.position += Sausage.eulerAngles.y == 0 ? new Vector3(speed * Time.deltaTime, 0, 0) : new Vector3(-speed * Time.deltaTime, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            isJump = false;
        }
    }

}
