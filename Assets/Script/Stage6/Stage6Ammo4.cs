using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6Ammo4 : MonoBehaviour
{
    public GameObject obj1;
    public GameObject ammo;
    float timer;

    private GameObject sausage;

    private void Start()
    {
        sausage = GameObject.Find("Sausage");
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > 8)
        {
            GameObject tmp = Instantiate(ammo);
            tmp.GetComponent<Stage6ChaseSausage>().ammoSpeed = 0.4f;
            tmp.GetComponent<Stage6ChaseSausage>().HP = 1;
            tmp.transform.position = this.gameObject.transform.position;
            tmp.SetActive(true);

            timer = 0;
        }
        obj1.transform.eulerAngles += new Vector3(0, 0, 270 * Time.deltaTime);
    }
}
