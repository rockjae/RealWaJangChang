using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0Sausage : PresetSausageController
{
    public GameObject ammo;
    public void shootAmmo()
    {
        StartCoroutine(makeAmmo());
    }

    IEnumerator makeAmmo()
    {
        for(int i =0;i<10; i++)
        {
            GameObject tmp = Instantiate(ammo);
            tmp.transform.position = transform.position;
            yield return new WaitForSeconds(0.3f);
        }
    }
}
