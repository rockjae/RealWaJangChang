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

    private void Awake()
    {
        Sausage = gameObject.transform;
        SausageRig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        touchTimer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            if(touchTimer < 0.5f)
            {
                Sausage.eulerAngles = Sausage.eulerAngles.y == 0 ? new Vector3(0, 180, 0) : new Vector3(0, 0, 0);
                touchTimer = 0;
            }
            else
            {
                SausageRig.AddForce(Vector3.up * jumpforce, ForceMode2D.Impulse);
                touchTimer = 0;
            }
        }

        Sausage.position += Sausage.eulerAngles.y == 0 ? new Vector3(speed * Time.deltaTime, 0, 0) : new Vector3(-speed * Time.deltaTime, 0, 0);
    }
}
