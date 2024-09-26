using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGuideOff : MonoBehaviour
{
    public GameObject controlGuide;
    private void OnDisable()
    {
        controlGuide.SetActive(true);
    }
}
