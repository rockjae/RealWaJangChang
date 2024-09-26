using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBossHit : MonoBehaviour
{
    public PresetMain presetMain;
    private int hp = 20;
    public AudioSource audioSource;
    public GameObject messageBox;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("playerammo"))
        {
            hp--;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            Destroy(collision.gameObject);

            if (hp == 0)
            {
                messageBox.SetActive(false);
                presetMain.GameClear();
            }
        }
    }
}
