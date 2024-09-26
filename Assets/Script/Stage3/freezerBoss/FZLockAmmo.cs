using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FZLockAmmo : MonoBehaviour
{
    float timer;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("player"))
        {
            GameObject.Find("MainManager").GetComponent<Stage3PresetMain>().colliderLockAmmo();
            gameObject.SetActive(false);
        }
    }
}
