using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3Block : PresetBlockController
{
    public float GetY()
    {
        return blockPositon.y;
    }

    public IEnumerator MakeBossBlock()
    {
        while (true)
        {
            float blokcPositionX = Random.Range(-2.0f, 2.0f);
            //int ran = Random.Range(0, blockPrefab.Length);
            GameObject bossBlock = Instantiate(blockPrefab[0], new Vector3(blokcPositionX, blockPositon.y, 0f), Quaternion.identity); //난이도 때문에 작은 블럭만 생성
            StartCoroutine(moveBossBolck(bossBlock));
            
            yield return new WaitForSeconds(1.5f);
            if (Flag.activeSelf)
            {
                Flag.SetActive(false);
            }
        }
    }

    public IEnumerator moveBossBolck(GameObject deleteOBJ)
    {
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            deleteOBJ.transform.position += new Vector3(0, 1.6f*Time.deltaTime,0);
            if (timer > 15f)
            {
                Destroy(deleteOBJ);
                break;
            }
            yield return null;
        }
    }
}
