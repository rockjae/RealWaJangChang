using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6ExclamationMark : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(waitEnable());
    }

    IEnumerator waitEnable()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
