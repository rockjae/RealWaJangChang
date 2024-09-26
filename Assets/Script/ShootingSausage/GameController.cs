using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Player")]
    public Player Sausage;
    public bool isBestPlayer;

    [Header("Boss")]
    public GameObject panBoss;
    public GameObject microWave;
    /*public GameObject medicalKitPrefab;*/

    [Header("Audio")]
    public AudioClip audioClipEnd;

    [Header("Ranking")]
    public GameObject setRank;
    public GameObject RankingBtn;
    public InputField inputFieldText;
    public GameObject rankNotiText;
    private bool isRankEnd;
    public GameObject Text8;

    [Header("GameOver")]
    public GameObject GameoverOBJ;
    public GameObject RestartBtn;
    public GameObject ExitBtn;
    public Text GameOverScoreText;
    private bool isGameOver;

    [Header("Score")]
    public Text ScoreText;
    private float score;

    [Header("JSON")]
    public ShootingTopRank shootingTopRank;
    public Text TopRankText;


    [System.Serializable]
    public class ShootingTopRank
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

    private bool isEnd = false;
    private bool isStart = false;


    // Start is called before the first frame update
    void Start()
    {
        setRank.SetActive(false);
        RankingBtn.SetActive(false);
        GameoverOBJ.SetActive(false);
        RestartBtn.SetActive(false);
        ExitBtn.SetActive(false);
        StartCoroutine(GetBestPlayer());
        
        
        Time.timeScale = 0;
        //InvokeRepeating("MakePanBoss", 0f, 10f);
    }

    private void Update()
    {
        if(isStart)
        {
            score += Time.deltaTime;
            ScoreText.text = score.ToString("N2");
        }
    }

    public void startShooting()
    {
        Time.timeScale = 1;
        isStart = true;
        StartCoroutine(makePanBoss());
        StartCoroutine(makeMicroWave());
    }

    IEnumerator makePanBoss()
    {
        Instantiate(panBoss, new Vector3(0f, 0f, 0f), Quaternion.identity);
        Instantiate(panBoss, new Vector3(40f, 0f, 0f), Quaternion.identity);
        yield return null;
    }

    IEnumerator makeMicroWave()
    {
        float term = -38f;
        for(int i=0;i<8;i++)
        {
            
            Instantiate(microWave, new Vector3(term + (i * 15), 36f, 0f), Quaternion.identity);
        }
        yield return null;
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
        if (inputFieldText.text.Length > 8)
        {
            Text8.SetActive(true);
            return;
        }

        if (!string.IsNullOrEmpty(inputFieldText.text))
        {
            //DataManager.Instance.setSausageScore(score);
            StartCoroutine(SetRank(inputFieldText.text, score.ToString("N2")));
            isRankEnd = true;
        }
    }

    public IEnumerator SetRank(string name, string score)
    {
        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get("http://3fu1.iptime.org/MiSil/manggame_rank_insert2.jsp?name=" + name + "&score=" + score))
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

    public void gameOverShooting()
    {
        Time.timeScale = 0;
        if (isGameOver)
        {
            return;
        }
        
        GameoverOBJ.SetActive(true);
        SoundManager.Instance.setSound(audioClipEnd);

        ScoreText.gameObject.SetActive(false);
        GameOverScoreText.text = "살아남은 시간 : " + score.ToString("N2") + "초";
        isGameOver = true;

        float highScore = DataManager.Instance.getShootingSausageScore();

        if (score > highScore)
        {
            RankingBtn.SetActive(true);
            RestartBtn.SetActive(true);
            ExitBtn.SetActive(true);
            DataManager.Instance.setShootingSausageScore(score);
        } 
        else
        {
            RankingBtn.SetActive(true);
            RestartBtn.SetActive(true);
            ExitBtn.SetActive(true);
        }

        StartCoroutine(GetRank(highScore.ToString("N2")));
        StartCoroutine(viewRank());
    }

    IEnumerator RankEnd()
    {
        rankNotiText.SetActive(true);
        StartCoroutine(viewRank());
        setRank.SetActive(false);
        RankingBtn.SetActive(false);
        yield return new WaitForSeconds(1f);
        
        //SceneManager.LoadScene("Stage");
    }

    IEnumerator viewRank()
    {
        UnityWebRequest request = new UnityWebRequest();
        TopRankText.text = "";
        using (request = UnityWebRequest.Get("http://3fu1.iptime.org/MiSil/manggame_rank_select2.jsp?num=10"))
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
                shootingTopRank = JsonUtility.FromJson<ShootingTopRank>(tmp2[0]);
                for (int i = 0; i < shootingTopRank.RANK.Length; i++)
                {
                    TopRankText.text += shootingTopRank.RANK[i].NO + "등 " + shootingTopRank.RANK[i].NAME + " " + shootingTopRank.RANK[i].SCORE + "초 생존\n";
                }
            }
        }
    }

    IEnumerator GetBestPlayer()
    {
        UnityWebRequest request = new UnityWebRequest();
        
        using (request = UnityWebRequest.Get("http://3fu1.iptime.org/MiSil/manggame_rank_select2.jsp?num=10"))
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
                shootingTopRank = JsonUtility.FromJson<ShootingTopRank>(tmp2[0]);
                if (DataManager.Instance.getShootingSausageScore() >= float.Parse(shootingTopRank.RANK[0].SCORE))
                {
                    Sausage.spriteRenderer.sprite = Sausage.newSprite;
                }
            }
        }
    }

    IEnumerator GetRank(string s)
    {
        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get("http://3fu1.iptime.org/MiSil/manggame_rank_srh2.jsp?score=" + s))
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
                if (tmp2[0].Equals("null"))
                {
                    tmp2[0] = "꼴";
                }

                GameOverScoreText.text += "\n" + "님 최고생존시간 : " + s + "초" + "\n" + "님 랭킹 : " + tmp2[0] + "등";
                
            }
        }
    }

    public void OnClickRestart()
    {
        SoundManager.Instance.setRestart();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickExit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }
}
