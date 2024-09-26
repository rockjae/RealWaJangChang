using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetBlockController : MonoBehaviour
{
    public bool isTutorial;

    public GameObject[] blockPrefab;

    //Vector3 targetPos; // 보스 위치
    protected Vector3 blockPositon; // 생성된 블럭 위치
    [SerializeField]
    protected float blockGap; // 블럭과 블럭 사이 간격
    [SerializeField]
    protected float blockMakeTerm; // 블럭 생성 시간
    [SerializeField]
    protected int blockCount; //최대 블럭 갯수
    [Header("Flag")]
    public GameObject Flag;

    public void blockMakeStart()
    {
        //targetPos = GameObject.Find("boss").transform.position; 
        //InvokeRepeating("MakeRandomBlock", 0f, blockMakeTerm); 
        Flag.transform.position = new Vector3(0, blockGap * blockCount - 2.5f, 0);
        blockPositon.y = -4f;
        StartCoroutine(MakeRandomBlock());
    }

    IEnumerator MakeRandomBlock()
    {
        for (int i = 0; i < blockCount; i++)
        {
            blockPositon.y += blockGap;
            float blokcPositionX = Random.Range(-2.0f, 2.0f);
            int blockIndex = Random.Range(0, blockPrefab.Length); //SausageGameManager -> inspector Block Prefab 
            GameObject randomBlock = new GameObject();
            if (i == blockCount - 1)
            {
                randomBlock = Instantiate(blockPrefab[0], new Vector3(blokcPositionX, blockPositon.y, 0f), Quaternion.identity);
            }
            else
            {
                randomBlock = Instantiate(blockPrefab[blockIndex], new Vector3(blokcPositionX, blockPositon.y, 0f), Quaternion.identity);
            }

            if (!isTutorial)
            {
                StartCoroutine(deleteBolck(randomBlock));
            }
            yield return new WaitForSeconds(blockMakeTerm);
        }
    }

    IEnumerator deleteBolck(GameObject deleteOBJ)
    {
        yield return new WaitForSeconds(15f);
        Destroy(deleteOBJ);
    }
}
