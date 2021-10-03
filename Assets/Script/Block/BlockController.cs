using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public GameObject[] blockPrefab;

    Vector3 targetPos; // 보스 위치
    Vector3 blockPositon;
    [SerializeField]
    float blockGap;
    






    void Start()
    {
        targetPos = GameObject.Find("boss").transform.position;
        InvokeRepeating("MakeRandomBlock", 0f, 1f);
        blockPositon.y = -4f;
    }

    void MakeLongBlock()
    {
        GameObject longBlock;
    }

    void MakeShortBlock()
    {
        GameObject shortBlock;
    }

    void MakeWeakBlock()
    {
        GameObject weakBlock;
    }

    void MakeMovingBlock()
    {
        GameObject movingBlock;

        blockPositon.y += blockGap; 
        if (blockPositon.y < targetPos.y)
        {
            movingBlock = Instantiate(blockPrefab[4], new Vector3(0f, blockPositon.y, 0f), Quaternion.identity);
        }

    }
    
    void MakeRandomBlock()
    {
        GameObject randomBlock;

        blockPositon.y += blockGap;
        float blokcPositionX = Random.Range(-2.0f, 2.0f);
        int blockIndex = Random.Range(0, blockPrefab.Length);
        if (blockPositon.y < targetPos.y)
        {
            randomBlock = Instantiate(blockPrefab[blockIndex], new Vector3(blokcPositionX, blockPositon.y, 0f), Quaternion.identity);
        }

    }

    void Update()
    {
        
    }

}
