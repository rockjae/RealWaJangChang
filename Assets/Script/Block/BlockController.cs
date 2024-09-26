using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public GameObject[] blockPrefab;

    //Vector3 targetPos; // 보스 위치
    Vector3 blockPositon; // 생성된 블럭 위치
    [SerializeField]
    float blockGap; // 블럭과 블럭 사이 간격
    [SerializeField]
    float blockMakeTerm; // 블럭 생성 시간
    [SerializeField]
    int blockCount; //최대 블럭 갯수
    [Header("Flag")]
    public GameObject Flag;
    [Header("BossBlock")]
    public GameObject[] BossBlock;

    public void blockMakeStart()
    {
        //targetPos = GameObject.Find("boss").transform.position; 
        //InvokeRepeating("MakeRandomBlock", 0f, blockMakeTerm); 
        Flag.transform.position = new Vector3(0, blockGap * blockCount-2.5f, 0);
        for(int i = 0; i < BossBlock.Length; i++)
        {
            BossBlock[i].transform.position = new Vector3(0, blockGap * blockCount - 2.5f, 0);
        }

        blockPositon.y = -4f;
        StartCoroutine(MakeRandomBlock());
    }    

    IEnumerator MakeRandomBlock()
    {
        for(int i = 0; i < blockCount; i++)
        {
            blockPositon.y += blockGap;
            float blokcPositionX = Random.Range(-2.0f, 2.0f);
            int blockIndex = Random.Range(0, blockPrefab.Length); //SausageGameManager -> inspector Block Prefab 
            GameObject randomBlock = Instantiate(blockPrefab[blockIndex], new Vector3(blokcPositionX, blockPositon.y, 0f), Quaternion.identity);
            StartCoroutine(deleteBolck(randomBlock));
            yield return new WaitForSeconds(blockMakeTerm);
        }      
    }

    IEnumerator deleteBolck(GameObject deleteOBJ)
    {
        yield return new WaitForSeconds(15f);
        Destroy(deleteOBJ);
    }

    public void setBossBlock()
    {
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
