using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1Block : PresetBlockController
{
    [Header("BossBlock")]
    public GameObject[] BossBlock;

    public void setBossBlock()
    {
        for (int i = 0; i < BossBlock.Length; i++)
        {
            BossBlock[i].transform.position = new Vector3(0, blockGap * blockCount - 2.5f, 0);
        }
        StartCoroutine(bossBlockMake());
    }

    IEnumerator bossBlockMake()
    {
        for (int i = 0; i < BossBlock.Length; i++)
        {
            BossBlock[i].SetActive(false);
        }

        yield return new WaitForSeconds(5f);

        int ran = Random.Range(0, BossBlock.Length);
        BossBlock[ran].SetActive(true);

        yield return new WaitForSeconds(5f);

        setBossBlock();
        yield return null;
    }
}
