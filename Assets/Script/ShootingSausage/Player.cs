using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float scaleSpeed = 0.05f;
    [SerializeField] GameController gameController;
    [SerializeField] private JoyStick joyStick;
    
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;

    Vector3 mousePos, transPos, targetPos;


    // Update is called once per frame
    void Update()
    {
        float x = joyStick.Horizontal();
        float y = joyStick.Vertical();

        //Debug.Log("("+x+", "+y+")");

        
        if (Input.GetMouseButton(0))
        {
            speed = 20f;
        }
        else
        {
            speed = 10f;
        }

        if (x != 0 || y != 0)
        {
            transform.position += new Vector3(x, y, 0) * speed * Time.deltaTime;
            
        }

    }

    /*void CalTargetPos()
    {
        mousePos = Input.mousePosition;
        transPos = Camera.main.ScreenToWorldPoint(mousePos);
        targetPos = new Vector3(transPos.x, transPos.y, 0);
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
    }*/


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("ammo"))
        {
            gameController.gameOverShooting();
        }
    }

    
}
