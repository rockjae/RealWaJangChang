using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5PresetMain : PresetMain
{
    public Stage5Boss stage5Boss;
    [Header("JoyStick")]
    [SerializeField] float joyStickSpeed = 1f;
    [SerializeField] float scaleSpeed = 0.05f;
    [SerializeField] private JoyStick joyStick;

    Vector3 mousePos, transPos, targetPos;

    public override void BossStartEvent()
    {
        base.BossStartEvent();
        PresetSausageController.GetComponent<Stage5Sausage>().stage5Start = true;
        Sausage.GetComponent<Rigidbody2D>().gravityScale = 0;
        joyStick.gameObject.SetActive(true);
        stage5Boss.BossPatten();
        StartCoroutine(joyStickCor());
    }

    IEnumerator joyStickCor()
    {
        while (true)
        {
            float x = joyStick.Horizontal();
            float y = joyStick.Vertical();

            //Debug.Log("("+x+", "+y+")");


            if (Input.GetMouseButton(0))
            {
                joyStickSpeed = 4f;
            }
            else
            {
                joyStickSpeed = 2f;
            }

            if (x != 0 || y != 0)
            {
                Sausage.transform.position += new Vector3(x, y, 0) * joyStickSpeed * Time.deltaTime;

            }
            yield return null;
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

    /*
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("ammo"))
        {
            gameController.gameOverShooting();
        }
    }
    */
}
