using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HIghNoonMushroom : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(waitOff());
    }
    IEnumerator waitOff()
    {
        yield return new WaitForSeconds(5f);
        GetComponent<AudioSource>().enabled = false;
    }
}
