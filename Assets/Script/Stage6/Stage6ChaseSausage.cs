using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6ChaseSausage : MonoBehaviour
{
    [HideInInspector]
    public string tag;
    [HideInInspector]
    public float ammoSpeed = 1f;
    [HideInInspector]
    public int HP=1;
    private Vector3 target;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Sausage").transform.position;
        target = target - transform.position;
    }

    private void Update()
    {
        if(target == null)
        {
            return;
        }


        transform.Translate(ammoSpeed * target * Time.deltaTime);

        if (timer > 5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("playerammo"))
        {
            HP--;
            GameObject.Find("SausageShotSound").GetComponent<AudioSource>().Play();
            Destroy(collision.gameObject);
            if (HP == 0)
            {
                if(tag == "boss1")
                {
                    GameObject.Find("Sausage").GetComponent<Stage6Sausage>().initAmmo2();
                }
                else if (tag == "boss2")
                {
                    GameObject.Find("Sausage").GetComponent<Stage6Sausage>().initAmmo3();
                }
                else if(tag == "playerboxammo")
                {
                    GameObject.Find("Sausage").GetComponent<Stage6Sausage>().initBoxAmmo();
                }
                Destroy(this.gameObject);
            }
        }
    }
}
