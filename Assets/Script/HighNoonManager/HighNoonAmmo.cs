using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighNoonAmmo : MonoBehaviour
{
    public bool isPlayerGun;
    [HideInInspector]
    public bool isStart;
    public HighNoonManager highNoonManager;

    private void Update()
    {
        if (isStart)
        {
            if (isPlayerGun)
            {
                transform.position += new Vector3(0, 5 * Time.deltaTime);
            }
            else
            {
                transform.position -= new Vector3(0, 5 * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("playerammo"))
        {
            highNoonManager.endGame();
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
