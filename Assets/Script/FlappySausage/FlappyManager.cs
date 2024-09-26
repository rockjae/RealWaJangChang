using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlappyManager : MonoBehaviour
{
    [Header("Sausage")]
    public GameObject Sausage;
    public GameObject Pipe;

    [Header("Audio")]
    public AudioSource SausageJumpAudio;

    public AudioSource scoreSound;
    public AudioClip audioClipEnd;

    [Header("GameOver")]
    public GameObject GameoverOBJ;
    public Text GameOverScoreText;
    private bool isGameOver;

    [Header("Score")]
    public Text ScoreText;
    private int score;

    private Rigidbody sausage_rigi;
    private int pipeCount;

    [Header("Ranking")]
    public GameObject pauseBtn;
    public GameObject setRank;
    public GameObject RankingBtn;
    public InputField inputFieldText;
    public GameObject rankNotiText;
    private bool isRankEnd;
    public GameObject Text8;

    [Header("JSON")]
    public FlappyTopRank flappyTopRank;
    public GameObject rankButton;
    public Text TopRankText;

    [System.Serializable]
    public class FlappyTopRank
    {
        public RANK[] RANK;
    }

    [System.Serializable]
    public class RANK
    {
        public string NO;
        public string NAME;
        public string SCORE;
    }

    // Start is called before the first frame update
    void Awake()
    {
        sausage_rigi = Sausage.GetComponent<Rigidbody>();
        InvokeRepeating("makePipe", 0, 2f);
    }
    private void Start()
    {
        setRank.SetActive(false);
        RankingBtn.SetActive(false);
        GameoverOBJ.SetActive(false);

        Time.timeScale = 0;
    }

    public void startFlappy()
    {
        Time.timeScale = 1;
    }

    public void gameOverFlappy()
    {
        if (isGameOver)
        {
            return;
        }
        pauseBtn.SetActive(false);
        GameoverOBJ.SetActive(true);
        SoundManager.Instance.setSound(audioClipEnd);

        ScoreText.gameObject.SetActive(false);
        GameOverScoreText.text = "님 점수 : " + score.ToString()+"점";
        isGameOver = true;

        int highScore = DataManager.Instance.getFlappyScore();
        if(score > highScore)
        {
            RankingBtn.SetActive(true);
        }

        StartCoroutine(GetRank(highScore.ToString()));
        StartCoroutine(viewRank());
    }

    public void gameOverImageClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Stage");
    }

    public void scoreUP()
    {
        if (isGameOver)
        {
            return;
        }
        scoreSound.Play();
        score++;
        ScoreText.text = "점수 : " + score;
    }

    // Update is called once per frame
    void Update()
    {
        Sausage.transform.position += new Vector3(0, 0, 5*Time.deltaTime);
        Sausage.transform.eulerAngles += new Vector3(0, 60 * Time.deltaTime, 0);

        if (!isGameOver && Input.GetMouseButtonDown(0))
        {
            sausage_rigi.AddForce(500*new Vector3(0,1,0));
            SausageJumpAudio.Play();
        }
    }

    void makePipe()
    {
        int ran = Random.Range(0, 6);
        int ran2 = Random.Range(0, 1);
        GameObject pipetmp = Instantiate(Pipe);

        if(ran2 == 0)
        {
            pipetmp.transform.position = new Vector3(0, ran * 1.5f, (pipeCount + 1) * 10);
        }
        else
        {
            pipetmp.transform.position = new Vector3(0, -ran * 1.5f, (pipeCount + 1) * 10);
        }

        StartCoroutine(destoryPipe(pipetmp));
        pipeCount++;
    }

    IEnumerator destoryPipe(GameObject target)
    {
        yield return new WaitForSeconds(5f);
        Destroy(target);
    }

    IEnumerator GetRank(string s)
    {
        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get("http://3fu1.iptime.org/MiSil/manggame_rank_srh.jsp?score="+ s))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                string[] tmp = request.downloadHandler.text.Split('>');
                string[] tmp2 = tmp[2].Split('<');
                Debug.Log(tmp2[0]);
                if (tmp2[0].Equals("null"))
                {
                    tmp2[0] = "꼴";
                }

                GameOverScoreText.text += "\n"+"님 최고점수 : "+ s +"점"+ "\n" + "님 랭킹 : " + tmp2[0] + "등";
            }
        }
    }

    public void onClickSetRankingBtn()
    {
        setRank.SetActive(true);
    }
    public void onClickSetRankEnd()
    {
        if (isRankEnd)
        {
            return;
        }

        Debug.Log(inputFieldText.text.Length);
        if(inputFieldText.text.Length > 8)
        {
            Text8.SetActive(true);
            return;
        }

        if(!string.IsNullOrEmpty(inputFieldText.text))
        {
            DataManager.Instance.setFlappyScore(score);
            StartCoroutine(SetRank(inputFieldText.text,score.ToString()));
            isRankEnd = true;
        }
    }

    public IEnumerator SetRank(string name,string score)
    {
        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get("http://3fu1.iptime.org/MiSil/manggame_rank_insert.jsp?name="+ name + "&score="+score))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
                /*
                string[] tmp = request.downloadHandler.text.Split('>');
                string[] tmp2 = tmp[2].Split('<');
                Debug.Log(tmp2[0]);
                */
                StartCoroutine(RankEnd());
            }
        }
    }

    IEnumerator RankEnd()
    {
        rankNotiText.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Stage");
    }

    

    IEnumerator viewRank()
    {
        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get("http://3fu1.iptime.org/MiSil/manggame_rank_select.jsp?num=10"))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                string[] tmp = request.downloadHandler.text.Split('>');
                string[] tmp2 = tmp[1].Split('<');
                Debug.Log(tmp2[0]);
                flappyTopRank = JsonUtility.FromJson<FlappyTopRank>(tmp2[0]);
                for(int i=0; i<flappyTopRank.RANK.Length; i++)
                {
                    TopRankText.text += flappyTopRank.RANK[i].NO + "등 " + flappyTopRank.RANK[i].NAME + " " + flappyTopRank.RANK[i].SCORE + "점\n";
                }
            }
        }
    }
}
