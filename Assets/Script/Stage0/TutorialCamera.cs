using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCamera : MonoBehaviour
{
    public Transform sausage;
    public Transform background;
    // Update is called once per frame
    void Update()
    {
        Vector3 tmp = sausage.position;
        transform.position = new Vector3(0, tmp.y + 3.5f, -10);
        background.position = new Vector3(background.position.x, transform.position.y, background.position.z);
    }
}
