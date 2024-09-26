using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6Ammo5 : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(startAction());
    }
    
    IEnumerator startAction()
    {
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            transform.position += new Vector3(0, 5*Time.deltaTime);

            if(timer > 0.5f)
            {
                break;
            }
            yield return null;
        }
        GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(10f);
        Destroy(this.gameObject);
    }
}
