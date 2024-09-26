using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6PresetMain : PresetMain
{
    public GameObject sausageAmmo;
    public Stage6Boss stage6Boss;
    [HideInInspector]
    public bool sausageMoveOn;
    public override void BossStartEvent()
    {
        base.BossStartEvent();
        PresetSausageController.GetComponent<Stage6Sausage>().stage6Start = true;

        Sausage.GetComponent<Rigidbody2D>().gravityScale = 0;
        Sausage.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Sausage.GetComponent<Rigidbody2D>().Sleep();

        StartCoroutine(gotoMiddle());
    }

    IEnumerator gotoMiddle()
    {
        Vector3 vec = Boss.transform.position;

        Sausage.transform.eulerAngles = new Vector3(0, 0, 0);

        vec.y -= 3f;
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            Sausage.transform.position = Vector3.MoveTowards(Sausage.transform.position, vec, 2*Time.deltaTime);

            if (timer > 3)
            {
                sausageAmmo.transform.position = new Vector3(vec.x, vec.y + 1, vec.z);
                sausageAmmo.SetActive(true);
                stage6Boss.rotateTarget = Sausage.transform.position;
                stage6Boss.isStart = true;

                PresetBlockController.Flag.SetActive(false);
                PresetSausageController.GetComponent<Stage6Sausage>().stage6StartShot = true;

                StartCoroutine(sausageMiddle(vec));
                break;
            }
            yield return null;
        }
    }

    IEnumerator sausageMiddle(Vector3 middle)
    {
        bool isLeft = false;
        while (true)
        {
            Vector3 tmp = middle;
            if (sausageMoveOn)
            {
                if(middle.x - 1 == Sausage.transform.position.x || middle.x + 1 == Sausage.transform.position.x)
                {
                    isLeft = !isLeft;
                }

                tmp += isLeft ? new Vector3(1, 0) : new Vector3(-1, 0);
            }
            Sausage.transform.position = Vector3.MoveTowards(Sausage.transform.position, tmp, Time.deltaTime);
            yield return null;
        }
    }
}
